﻿using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace QuarztCore
{
    class Program
    {
        static void Main (string[] args)
        {
            RunProgram().GetAwaiter().GetResult();
        }

        private static async Task RunProgram ()
        {
            int i = 0;
            DateTime myStartTime = Convert.ToDateTime("2016-12-08");
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();


                // 启动任务调度器
                await scheduler.Start();


                // 定义一个 Job
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();
                ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                    .WithIdentity("trigger1") // 给任务一个名字
                   .StartNow()// .StartAt(myStartTime) // 设置任务开始时间
                    .ForJob("job1", "group1") //给任务指定一个分组
                    .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)  //循环的时间
                    .RepeatForever())
                    .Build();


                // 等待执行任务
                await scheduler.ScheduleJob(job, trigger);


                // some sleep to show what's happening
               // await Task.Delay(TimeSpan.FromMilliseconds(2000));


                // and last shut down the scheduler when you are ready to close your program
                await scheduler.Shutdown();
                i++;
                Console.WriteLine(DateTime.Now.ToString() + "这是第" + i + "次执行任务");
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }
    }
}
