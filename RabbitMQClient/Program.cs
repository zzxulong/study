using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQClient
{
    class Program
    {
        static void Main (string[] args)
        {
            Console.WriteLine("Start");
            IConnectionFactory conFactory = new ConnectionFactory//创建连接工厂对象
            {
                HostName = "127.0.0.1",//IP地址
                Port = 5672,//端口号
                UserName = "mq",//用户账号
                Password = "123456"//用户密码
            };

            #region worker
            //using (IConnection con = conFactory.CreateConnection())//创建连接对象
            //{
            //    using (IModel channel = con.CreateModel())//创建连接会话对象
            //    {
            //        String queueName = String.Empty;
            //        if (args.Length > 0)
            //            queueName = args[0];
            //        else
            //            queueName = "queue1";
            //        //声明一个队列
            //        channel.QueueDeclare(
            //          queue: queueName,//消息队列名称
            //          durable: false,//是否缓存
            //          exclusive: false,
            //          autoDelete: false,
            //          arguments: null
            //           );
            //        while (true)
            //        {
            //            Console.WriteLine("消息内容:");
            //            String message = Console.ReadLine();
            //            //消息内容
            //            byte[] body = Encoding.UTF8.GetBytes(message);
            //            //发送消息
            //            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            //            Console.WriteLine("成功发送消息:" + message);
            //        }
            //    }
            //}
            #endregion
            #region 发布订阅模式(fanout)
            //using (IConnection conn = conFactory.CreateConnection())
            //{
            //    using (IModel channel = conn.CreateModel())
            //    {
            //        //交换机名称
            //        String exchangeName = String.Empty;
            //        if (args.Length > 0)
            //            exchangeName = args[0];
            //        else
            //            exchangeName = "exchange1";
            //        //声明交换机
            //        channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");
            //        while (true)
            //        {
            //            Console.WriteLine("消息内容:");
            //            String message = Console.ReadLine();
            //            //消息内容
            //            byte[] body = Encoding.UTF8.GetBytes(message);
            //            //发送消息
            //            channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: body);
            //            Console.WriteLine("成功发送消息:" + message);
            //        }
            //    }
            //}

            #endregion

            #region 路由模式(direct)
            //using (IConnection conn = conFactory.CreateConnection())
            //{
            //    using (IModel channel = conn.CreateModel())
            //    {
            //        //交换机名称
            //        String exchangeName = "exchange2";
            //        //路由名称
            //        String routeKey = "route1";
            //        //声明交换机   路由交换机类型direct
            //        channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
            //        while (true)
            //        {
            //            Console.WriteLine("消息内容:");
            //            String message = Console.ReadLine();
            //            //消息内容
            //            byte[] body = Encoding.UTF8.GetBytes(message);
            //            //发送消息  发送到路由匹配的消息队列中
            //            channel.BasicPublish(exchange: exchangeName, routingKey: routeKey, basicProperties: null, body: body);
            //            Console.WriteLine("成功发送消息:" + message);
            //        }
            //    }
            //}
            #endregion

            #region 通配符模式(topic)
            using (IConnection conn = conFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //交换机名称
                    String exchangeName = "exchange3";
                    //路由名称
                    String routeKey = "route1";
                    //声明交换机   通配符类型为topic
                    channel.ExchangeDeclare(exchange: exchangeName, type: "topic");
                    while (true)
                    {
                        Console.WriteLine("消息内容:");
                        String message = Console.ReadLine();
                        //消息内容
                        byte[] body = Encoding.UTF8.GetBytes(message);
                        //发送消息  发送到路由匹配的消息队列中
                        channel.BasicPublish(exchange: exchangeName, routingKey: routeKey, basicProperties: null, body: body);
                        Console.WriteLine("成功发送消息:" + message);
                    }
                }
            }
            #endregion
        }
    }
}
