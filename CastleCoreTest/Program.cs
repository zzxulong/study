using Castle.DynamicProxy;
using CastleCoreTest.Core;
using System;
using System.Threading.Tasks;

namespace CastleCoreTest
{
    class Program
    {
		private static int _result;
		static void Main(string[] args)
        {
			#region 拦截器
			//Interceptor();
			#endregion

			#region interlocked
			//Interlocked();
			#endregion

			Console.WriteLine("Hello World!");

		}

		#region 拦截器
		public static void Interceptor()
		{
			var proxyGenerate = new ProxyGenerator();//实例化【代理类生成器】  
													 //TestIntercept t = new TestIntercept();//实例化【拦截器】 
			var t = new Interceptor();
			//使用【代理类生成器】创建Person对象，而不是使用new关键字来实例化  
			var pg = proxyGenerate.CreateClassProxy<MyClass>(t);
			pg.MyMethod();
		}
		#endregion


		#region Interlocked
		private static void Interlocked()
		{
			//运行后按住Enter键数秒，对比使用Interlocked.Increment(ref _result);与 _result++;的不同
			while(true)
			{
				Task[] _tasks = new Task[100];
				int i = 0;

				for(i = 0;i < _tasks.Length;i++)
				{
					_tasks[i] = Task.Factory.StartNew((num) =>
					{
						var taskid = (int)num;
						Work(taskid);
					},i);
				}

				Task.WaitAll(_tasks);
				Console.WriteLine(_result);

				Console.ReadKey();
			}

		}

		//线程调用方法
		private static void Work(int TaskID)
		{
			for(int i = 0;i < 10;i++)
			{
				_result++;

				//Interlocked.Increment(ref _result);
			}
		}
		#endregion

	}
}
