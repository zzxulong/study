using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using vuetest.AuthHelper;
using vuetest.Core;
using vuetest.Core.Common.LogHelper;
using vuetest.Core.IService;
using vuetest.Core.Service;

namespace vuetest
{
	public class Startup
	{
		/// <summary>
		/// log4net 仓储库
		/// </summary>
		public static ILoggerRepository Repository { get; set; }
		private IHostingEnvironment _hostingEnvironment;
		public Startup(IConfiguration configuration,IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
			Configuration = configuration;
			//log4net
			Repository = LogManager.CreateRepository(Configuration["Logging:Log4Net:Name"]);
			//指定配置文件，如果这里你遇到问题，应该是使用了InProcess模式，请查看Blog.Core.csproj,并删之
			XmlConfigurator.Configure(Repository,new FileInfo("log4net.config"));
		}
		

		public IConfiguration Configuration { get; }
		/// <summary>
		/// config
		/// </summary>
		/// <param name="services"></param>
		/// <param name="hostingEnvironment"></param>
		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.Replace(ServiceDescriptor.Transient<IControllerActivator,ServiceBasedControllerActivator>());

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			#region 初始化DB
			services.AddScoped<DBSeed>();
			services.AddScoped<MyContext>();
			#endregion

			services.AddAutoMapper(typeof(Startup));

			#region Swagger
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1",new Info {
					Version = "v0.1.0",
					Title = "API",
					Description = "框架说明文档",
					TermsOfService = "None",
					Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "wenk",Email = "Blog.Core@xxx.com",Url = "https://www.jianshu.com/u/94102b59cc2a" }
				});
				var basePath = _hostingEnvironment.ContentRootPath;
				var xmlPath = Path.Combine(basePath,"vuetest.xml");//这个就是刚刚配置的xml文件名
				c.IncludeXmlComments(xmlPath,true);

				#region Token绑定到ConfigureServices
				//添加header验证信息
				//c.OperationFilter<SwaggerHeader>();
				var security = new Dictionary<string,IEnumerable<string>> { { "API",new string[] { } },};
				c.AddSecurityRequirement(security);
				//方案名称“Blog.Core”可自定义，上下一致即可
				c.AddSecurityDefinition("API",new ApiKeyScheme {
					Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入{token}\"",
					Name = "Authorization",//jwt默认的参数名称
					In = "header",//jwt默认存放Authorization信息的位置(请求头中)
					Type = "apiKey"
				});
				#endregion

			});


			#endregion

			//#region Token服务注册
			services.AddSingleton<IMemoryCache>(factory => new MemoryCache(new MemoryCacheOptions()));
			
			//#endregion
			#region 认证
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(o =>
				{
					o.TokenValidationParameters = new TokenValidationParameters {
						ValidIssuer = "API",
						ValidAudience = "wr",
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtHelper.secretKey)),


						RequireSignedTokens = true,
							// 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。
							ValidateAudience = false,
						ValidateIssuer = true,
						ValidateIssuerSigningKey = true,
							// 是否要求Token的Claims中必须包含 Expires
							RequireExpirationTime = true,
							// 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
							ValidateLifetime = true
					};
				});

			//services.AddAuthorization(options =>
			//{
			//	options.AddPolicy("Admin",policy => policy.RequireClaim("AdminType").Build());//注册权限管理，可以自定义多个
			//});
			services.AddAuthorization(options =>
			{
				options.AddPolicy("Client",policy => policy.RequireRole("Client").Build());
				options.AddPolicy("Admin",policy => policy.RequireRole("Admin").Build());
				options.AddPolicy("AdminOrClient",policy => policy.RequireRole("Admin,Client").Build());
			});
			#endregion

			#region AutoFac
			//实例化 AutoFac  容器   
			var builder = new ContainerBuilder();

			var assemblys = AppDomain.CurrentDomain.GetAssemblies().Where(e => e.FullName.Contains("vuetest")).ToList();
			builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("Services")).AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
			builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
			builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("Controller")).PropertiesAutowired();

			//var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;//获取项目路径
			//var servicesDllFile = Path.Combine(basePath,"Blog.Core.Services.dll");//获取注入项目绝对路径
			//var assemblysServices = Assembly.LoadFile(servicesDllFile);//直接采用加载文件的方法

			// log日志注入
			builder.RegisterType<LogHelper>().As<ILoggerHelper>().PropertiesAutowired().InstancePerDependency();

			//注册要通过反射创建的组件
			builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>().PropertiesAutowired().InstancePerDependency();


			//将services填充到Autofac容器生成器中
			builder.Populate(services);

			//使用已进行的组件登记创建新容器
			var ApplicationContainer = builder.Build();

			#endregion

			#region CORS
			services.AddCors(c =>
			{
				//↓↓↓↓↓↓↓注意正式环境不要使用这种全开放的处理↓↓↓↓↓↓↓↓↓↓
				c.AddPolicy("AllRequests",policy =>
				{
					policy
					.AllowAnyOrigin()//允许任何源
					.AllowAnyMethod()//允许任何方式
					.AllowAnyHeader()//允许任何头
					.AllowCredentials();//允许cookie
				});
				//↑↑↑↑↑↑↑注意正式环境不要使用这种全开放的处理↑↑↑↑↑↑↑↑↑↑


				//一般采用这种方法
				c.AddPolicy("LimitRequests",policy =>
				{
					policy
					.WithOrigins("http://localhost:8020","http://blog.core.xxx.com","")//支持多个域名端口
					.WithMethods("GET","POST","PUT","DELETE")//请求方法添加到策略
					.WithHeaders("authorization");//标头添加到策略
				});

			});
			#endregion

			return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app,IHostingEnvironment env)
		{
			if(env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			#region Swagger
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json","ApiHelp V1");
			});
			#endregion

			app.UseMiddleware<JwtTokenAuth>();

			#region CORS
			//跨域第二种方法，使用策略，详细策略信息在ConfigureService中
			app.UseCors("LimitRequests");//将 CORS 中间件添加到 web 应用程序管线中, 以允许跨域请求。


			#region 跨域第一种版本
			//跨域第一种版本，请要ConfigureService中配置服务 services.AddCors();
			    app.UseCors(options => options.WithOrigins("http://localhost:8021").AllowAnyHeader().AllowAnyMethod());
			#endregion
			#endregion

			app.UseMvc();
		}
	}
}
