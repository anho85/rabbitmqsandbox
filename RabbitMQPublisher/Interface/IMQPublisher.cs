using OSMessages;

namespace OSRabbitMQPublisher.Interface
{
    public interface IMQPublisher
    {
        void Connect();
        void CloseConnection();
        void Publish(Message message, string channelName);
    }
}
