using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQServer
{
    class Program
    {
        static void Main (string[] args)
        {
            Console.WriteLine("Start");
            IConnectionFactory connFactory = new ConnectionFactory//创建连接工厂对象
            {
                HostName = "127.0.0.1",//IP地址
                Port = 5672,//端口号
                UserName = "mq",//用户账号
                Password = "123456"//用户密码
            };

            #region worker
            //using (IConnection conn = connFactory.CreateConnection())
            //{
            //    using (IModel channel = conn.CreateModel())
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
            //        //告诉Rabbit每次只能向消费者发送一条信息,再消费者未确认之前,不再向他发送信息
            //        channel.BasicQos(0, 1, false);
            //        //创建消费者对象
            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            Thread.Sleep((new Random().Next(1, 6)) * 1000);//随机等待,实现能者多劳,   
            //            byte[] message = ea.Body;//接收到的消息
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message));
            //            //返回消息确认
            //            channel.BasicAck(ea.DeliveryTag, true);
            //        };
            //        //消费者开启监听
            //        //channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            //        //将autoAck设置false 关闭自动确认
            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            //        Console.ReadKey();
            //    }
            //}
            #endregion

            //创建一个随机数,以创建不同的消息队列
            int random = new Random().Next(1, 1000);
            Console.WriteLine("Start" + random.ToString());
            #region 发布订阅模式(fanout)
            //using (IConnection conn = connFactory.CreateConnection())
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
            //        //消息队列名称
            //        String queueName = exchangeName + "_" + random.ToString();
            //        //声明队列
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        //将队列与交换机进行绑定
            //        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");
            //        //声明为手动确认
            //        channel.BasicQos(0, 1, false);
            //        //定义消费者
            //        var consumer = new EventingBasicConsumer(channel);
            //        //接收事件
            //        consumer.Received += (model, ea) =>
            //        {
            //            byte[] message = ea.Body;//接收到的消息
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message));
            //            //返回消息确认
            //            channel.BasicAck(ea.DeliveryTag, true);
            //        };
            //        //开启监听
            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            //        Console.ReadKey();
            //    }
            //}
            #endregion
            #region 路由模式
            //using (IConnection conn = connFactory.CreateConnection())
            //{
            //    using (IModel channel = conn.CreateModel())
            //    {
            //        //交换机名称
            //        String exchangeName = "exchange2";
            //        //声明交换机
            //        channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
            //        //消息队列名称
            //        String queueName = exchangeName + "_" + random.ToString();
            //        //声明队列
            //        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //        //将队列与交换机进行绑定
            //        //foreach (var routeKey in args)
            //        //{//匹配多个路由
            //        //    channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routeKey);
            //        //}
            //        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "route1");
            //        //声明为手动确认
            //        channel.BasicQos(0, 1, false);
            //        //定义消费者
            //        var consumer = new EventingBasicConsumer(channel);
            //        //接收事件
            //        consumer.Received += (model, ea) =>
            //        {
            //            byte[] message = ea.Body;//接收到的消息
            //            Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message));
            //            //返回消息确认
            //            channel.BasicAck(ea.DeliveryTag, true);
            //        };
            //        //开启监听
            //        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            //        Console.ReadKey();
            //    }
            //}
            #endregion
            #region 通配符模式(topic)
            using (IConnection conn = connFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //交换机名称
                    String exchangeName = "exchange3";
                    //声明交换机    通配符类型为topic
                    channel.ExchangeDeclare(exchange: exchangeName, type: "topic");
                    //消息队列名称
                    String queueName = exchangeName + "_" + random.ToString();
                    //声明队列
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    //将队列与交换机进行绑定
                    //foreach (var routeKey in args)
                    //{//匹配多个路由
                    //    channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routeKey);
                    //}
                    channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "route1");
                    //声明为手动确认
                    channel.BasicQos(0, 1, false);
                    //定义消费者
                    var consumer = new EventingBasicConsumer(channel);
                    //接收事件
                    consumer.Received += (model, ea) =>
                    {
                        byte[] message = ea.Body;//接收到的消息
                        Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message));
                        //返回消息确认
                        channel.BasicAck(ea.DeliveryTag, true);
                    };
                    //开启监听
                    channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
                    Console.ReadKey();
                }
            }
            #endregion
        }
    }
}
