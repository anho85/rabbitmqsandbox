using RabbitMQ.Client;
using RabbitMQPublisher.Interface;
using System;
using System.Text;

namespace RabbitMQPublisher.Implementation
{
    public class RabbitMQPublisher : IMQPublisher
    {
        public Uri _rabbitMQUrl;
        public ConnectionFactory _factory;
        public RabbitMQPublisher()
        {
            _rabbitMQUrl = null;
        }
        public RabbitMQPublisher(Uri rabbitMQUrl)
        {
            _rabbitMQUrl = rabbitMQUrl;
        }

        public void SetUrl(Uri url)
        {
            _rabbitMQUrl = url;
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }
        public void Publish(string message)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: "hello",
                        basicProperties: null,
                        body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }

    }
}
