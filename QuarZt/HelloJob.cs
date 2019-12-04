using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuarZt
{
    public class HelloJob : IJob
    {
        public Task Execute (IJobExecutionContext context)
        {
            return Task.Run(()=> {
                Console.WriteLine("Greetings from HelloJob!");
            });
        }
    }
}
