using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dapper.Web.Controllers;
using Dapper.Web.Core.Extentions;
using Dapper.Web.Data.SqlServer;
using Dapper.Web.DataAbstractions;
using Dapper.Web.DataAbstractions.Entities;
using Dapper.Web.DataAbstractions.Options;
using Dapper.Web.Repository;
using Dapper.Web.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Dapper.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
            //替换控制器所有者（属性注入使用）
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            var builder = new ContainerBuilder();
            //repositories


            var conn= services.ConfigureStartupConfig<DbOptions>(Configuration.GetSection("DbOptions"));
            services.AddSingleton(conn);

            foreach (var item in conn.Connections)
            {
                foreach(var domain in AppDomain.CurrentDomain.GetAssemblies().Where(e=>e.FullName.Contains("Dapper")).ToList())
                {
                    item.EntityTypes = domain.GetTypes().Where(t => t.IsClass && typeof(IEntity).IsImplementType(t)).ToList();
                    if (item.EntityTypes != null && item.EntityTypes.Count > 0)
                    {
                        var contextOptions = (IDbContextOptions)Activator.CreateInstance(typeof(SqlServerDbContextOptions), conn, item);
                        var dbContext = (IDbContext)Activator.CreateInstance(typeof(DapperContext), contextOptions);
                        services.AddSingleton(typeof(IDbContext), sp => dbContext);
                        //services.AddSingleton(typeof(IBaseRepository<>), Activator.CreateInstance(typeof(BaseRepositoryImpl<>), dbContext));
                        //services.AddSingleton(typeof(IDapperRepository<>), Activator.CreateInstance(typeof(DapperRepositoryImpl<>), dbContext));
                    }
                }
            }
            
            builder.RegisterGeneric(typeof(DapperRepositoryImpl<>)).As(typeof(IDapperRepository<>)).InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<TestServiceImpl>().As<ITestService>().InstancePerLifetimeScope().PropertiesAutowired();

            builder.RegisterType<HomeController>().PropertiesAutowired().InstancePerLifetimeScope();
            builder.RegisterType<TestController>().PropertiesAutowired().InstancePerLifetimeScope();
            services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});


			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

          
            builder.Populate(services);
            return new AutofacServiceProvider(builder.Build());
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
