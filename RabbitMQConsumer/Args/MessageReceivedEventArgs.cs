using OSMessages;
using System;

namespace OSRabbitMQConsumer.Args
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public Message Message { get; set; }

        public MessageReceivedEventArgs(Message message)
        {
            Message = message;
        }
    }
}
