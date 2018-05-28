using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQPublisher
{
    class Program
    {
        public static void Main(string[] args)
        {
            //var factory = new ConnectionFactory() { HostName = "10.162.65.102", Port = 8082, UserName="admin",Password= "8cd508e592ea965551f2ed8302929a6d" };
            var factory = new ConnectionFactory(){ HostName = "localhost" };
            try
            {
                Console.WriteLine(factory.Endpoint);
                Console.WriteLine(factory.HostName);
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "hello",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
                        string message = "Hello World";
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                            routingKey: "hello",
                            basicProperties: null,
                            body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
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
