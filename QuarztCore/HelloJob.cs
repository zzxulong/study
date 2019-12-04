using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuarztCore
{
    public class HelloJob : IJob
    {
        public Task Execute (IJobExecutionContext context)
        {
          
            Console.WriteLine("Greetings from HelloJob core!");
            return Task.CompletedTask;
        }
    }
}
