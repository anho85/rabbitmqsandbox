using RabbitMQ.Client;
using OSRabbitMQPublisher.Interface;
using System;
using System.Text;
using OSMessages;
using Newtonsoft.Json;

namespace OSRabbitMQPublisher.Implementation
{
    public class RabbitMQPublisher : IMQPublisher
    {
        internal Uri _rabbitMQUrl;
        internal ConnectionFactory _factory;
        internal IConnection _connection;
        public RabbitMQPublisher()
        {
            _rabbitMQUrl = null;
        }
        public RabbitMQPublisher(Uri rabbitMQUrl)
        {
            SetUrl(rabbitMQUrl);
        }

        public void SetUrl(Uri url)
        {
            _rabbitMQUrl = url;
            CloseConnection();
            _factory = new ConnectionFactory() { HostName = url.Host, Port = url.Port };
            _factory.AutomaticRecoveryEnabled = true;

        }

        public void Publish(Message message, string channelName)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: channelName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish(exchange: "",
                    routingKey: channelName,
                    basicProperties: null,
                    body: body);

            }
        }

        public void Connect()
        {
            if (_factory == null) throw new NullReferenceException("Factory is Null. Missing SetUrl()?");
            CloseConnection();
            _connection = _factory.CreateConnection();
        }

        public void CloseConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
            }
        }
    }
}
