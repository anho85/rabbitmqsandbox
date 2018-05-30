using OSRabbitMQConsumer.Args;
using System;

namespace OSRabbitMQConsumer.Interface
{
    public interface IMQConsumer
    {
        void Connect();
        void CloseConnection();
        void Consume(string channelName);
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}
