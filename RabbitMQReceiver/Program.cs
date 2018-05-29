using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQPublisher
{
    class Program
    {
        public static void Main(string[] args)
        {
            //var factory = new ConnectionFactory() { HostName = "10.162.65.102", UserName = "admin", Password = "8cd508e592ea965551f2ed8302929a6d" };
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            try
            {
                Console.WriteLine(factory.Endpoint);
                Console.WriteLine(factory.HostName);
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "TestChannel",
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body);
                            Console.WriteLine(" [x] Received {0}", message);
                        };
                        channel.BasicConsume(queue: "TestChannel",
                                             autoAck: true,
                                             consumer: consumer);

                        Console.WriteLine(" Press [enter] to exit.");
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
