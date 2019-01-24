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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebTest2.Models.Services;

namespace WebTest2
{
    public class Startup
    {
        public IContainer AutofacContainer;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region autofacע��
            var builder = new ContainerBuilder();

            //ע������д��
            ////��ÿһ��������ÿһ�ε��ô���һ���µ�Ψһ��ʵ������Ҳ��Ĭ�ϵĴ���ʵ���ķ�ʽ��
            //builder.RegisterType<GuidTransientAppService>().As<IGuidTransientAppService>().InstancePerDependency();
            ////��һ�������������У�ÿһ����������ô���һ����һ�Ĺ����ʵ������ÿһ����ͬ������������ʵ����Ψһ�ģ�������ġ�
            //builder.RegisterType<GuidScopedAppService>().As<IGuidScopedAppService>().InstancePerLifetimeScope();
            ////ÿһ��������������Resolve()��������õ�һ����ͬ�Ĺ����ʵ������ʵ���ǵ���ģʽ��
            //builder.RegisterType<GuidSingletonAppService>().As<IGuidSingletonAppService>().SingleInstance();
            //��ģ�����ע��
            builder.RegisterModule<DefaultModuleRegister>();
            //��services�еķ�����䵽Autofac��.
            builder.Populate(services);
            this.AutofacContainer = builder.Build();
            return new AutofacServiceProvider(AutofacContainer);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
            //����ֹͣ���ú���
            appLifetime.ApplicationStopped.Register(() => { AutofacContainer.Dispose(); });
        }
    }
}
