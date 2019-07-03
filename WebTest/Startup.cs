using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore.Entities;
using WebTest.DAL;
using WebTest.Models.autofacTest;

namespace WebTest
{
    public class Startup
    {
        public static IContainer AutofacContainer;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            //替换控制器所有者（属性注入使用）
            //var descriptor = ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>();
            //var registeredServiceDescriptor = services.FirstOrDefault(s => s.ServiceType == descriptor.ServiceType);
            //if (registeredServiceDescriptor != null)
            //    services.Remove(registeredServiceDescriptor);
            //services.Add(descriptor);
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<NopMallContext>();
            services.AddDirectoryBrowser();

            #region .netcore自带注入
            ////Transient 服务在每次请求时被创建，它最好被用于轻量级无状态服务
            //services.AddTransient<IGuidTransientAppService, GuidTransientAppService>();
            ////Scoped 服务在每次请求时被创建，生命周期横贯整次请求
            //services.AddScoped<IGuidScopedAppService, GuidScopedAppService>();
            ////Singleton（单例） 服务在第一次请求时被创建（或者当我们在ConfigureServices中指定创建某一实例并运行方法），其后的每次请求将沿用已创建服务。如果开发者的应用需要单例服务情景，请设计成允许服务容器来对服务生命周期进行操作，而不是手动实现单例设计模式然后由开发者在自定义类中进行操作
            //services.AddSingleton<IGuidSingletonAppService, GuidSingletonAppService>();
            #endregion

            #region autofac注入
            var builder = new ContainerBuilder();

            //注意以下写法
            ////对每一个依赖或每一次调用创建一个新的唯一的实例。这也是默认的创建实例的方式。
            //builder.RegisterType<GuidTransientAppService>().As<IGuidTransientAppService>().InstancePerDependency();
            ////在一个生命周期域中，每一个依赖或调用创建一个单一的共享的实例，且每一个不同的生命周期域，实例是唯一的，不共享的。
            //builder.RegisterType<GuidScopedAppService>().As<IGuidScopedAppService>().InstancePerLifetimeScope();
            ////每一次依赖组件或调用Resolve()方法都会得到一个相同的共享的实例。其实就是单例模式。
            //builder.RegisterType<GuidSingletonAppService>().As<IGuidSingletonAppService>().SingleInstance();
            //新模块组件注册
            builder.RegisterModule<DefaultModuleRegister>();
            //将services中的服务填充到Autofac中.
            builder.Populate(services);
            AutofacContainer = builder.Build();
            return new AutofacServiceProvider(AutofacContainer);
            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net(new Log4NetProviderOptions() {
                Log4NetConfigFileName = "log4net.config",
                Watch = false,
                OverrideCriticalLevelWith=string.Empty,
                Name= "errorHandle",
                PropertyOverrides = new List<NodeInfo>(),
                LoggerRepository="default"
            });
            if (env.IsDevelopment())//读取环境变量是否为Development，在launchSettings.json中定义
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //程序停止调用函数
            appLifetime.ApplicationStopped.Register(() => { AutofacContainer.Dispose(); });
        }
    }
}
