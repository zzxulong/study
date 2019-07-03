using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using vuetest.AuthHelper;

namespace vuetest
{
	public class Startup
	{
		public Startup(IConfiguration configuration,IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
			Configuration = configuration;
		}
		private IHostingEnvironment _hostingEnvironment;

		public IConfiguration Configuration { get; }
		/// <summary>
		/// config
		/// </summary>
		/// <param name="services"></param>
		/// <param name="hostingEnvironment"></param>
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
			#endregion

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

			app.UseMvc();
		}
	}
}
