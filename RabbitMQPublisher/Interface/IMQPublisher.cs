using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSRabbitMQPublisher.Interface
{
    public interface IMQPublisher
    {
        void Connect();
        void CloseConnection();
        void Publish(string message, string channelName);
    }
}
