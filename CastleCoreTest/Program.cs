using Castle.DynamicProxy;
using CastleCoreTest.Core;
using System;

namespace CastleCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var proxyGenerate = new ProxyGenerator();//实例化【代理类生成器】  
            //TestIntercept t = new TestIntercept();//实例化【拦截器】 
            var t = new Interceptor();
            //使用【代理类生成器】创建Person对象，而不是使用new关键字来实例化  
            var pg = proxyGenerate.CreateClassProxy<MyClass>(t);
            pg.MyMethod();

            Console.WriteLine("Hello World!");
        }
    }
}
