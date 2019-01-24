using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using WebTest.Controllers;

namespace WebTest.Models.autofacTest
{
    public class DefaultModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            

            //注册当前程序集中以“Ser”结尾的类,暴漏类实现的所有接口，生命周期为PerLifetimeScope
            //builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Ser")).AsImplementedInterfaces().InstancePerLifetimeScope();
            //builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            //注册所有"MyApp.Repository"程序集中的类
            //builder.RegisterAssemblyTypes(GetAssembly("MyApp.Repository")).AsImplementedInterfaces();
            //对每一个依赖或每一次调用创建一个新的唯一的实例。这也是默认的创建实例的方式。
            builder.RegisterType<GuidTransientAppService>().As<IGuidTransientAppService>().PropertiesAutowired().InstancePerDependency();
            //在一个生命周期域中，每一个依赖或调用创建一个单一的共享的实例，且每一个不同的生命周期域，实例是唯一的，不共享的。
            builder.RegisterType<GuidScopedAppService>().As<IGuidScopedAppService>().PropertiesAutowired().InstancePerLifetimeScope();
            //每一次依赖组件或调用Resolve()方法都会得到一个相同的共享的实例。其实就是单例模式。
            builder.RegisterType<GuidSingletonAppService>().As<IGuidSingletonAppService>().PropertiesAutowired().SingleInstance();

            //属性注入控制器
            builder.RegisterType<HomeController>().PropertiesAutowired();
        }
        public static Assembly GetAssembly(string assemblyName)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + $"{assemblyName}.dll");
            return assembly;
        }
    }
}
