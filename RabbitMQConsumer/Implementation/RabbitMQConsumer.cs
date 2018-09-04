using Newtonsoft.Json;
using OSMessages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using OSRabbitMQConsumer.Interface;
using System;
using System.Text;
using OSRabbitMQConsumer.Args;
using System.Diagnostics;

namespace OSRabbitMQConsumer.Implementation
{
    public class RabbitMQConsumer : IMQConsumer
    {
        internal Uri _rabbitMQUrl;
        internal ConnectionFactory _factory;
        internal IConnection _connection;
        internal IModel _channel;
        internal EventingBasicConsumer _consumer;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public ConnectionFactory Factory { get { return _factory; } }
        public RabbitMQConsumer(Uri rabbitMQUrl)
        {
            SetUrl(rabbitMQUrl);
        }

        public void SetUrl(Uri url)
        {
            _rabbitMQUrl = url;
            CloseConnection();
            _factory = new ConnectionFactory
            {
                //_factory.VirtualHost = "/%2frabbitmqapi%2f";
                Uri = url,
                //UserName = "admin",
                //Password = "8cd508e592ea965551f2ed8302929a6d",
                AutomaticRecoveryEnabled = true
            };

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

        public void Consume(string channelName)
        {
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: channelName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var jsonMessage = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<Message>(jsonMessage);
                MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
            };
            _channel.BasicConsume(queue: channelName,
                                     autoAck: true,
                                     consumer: _consumer);
        }
    }
}
