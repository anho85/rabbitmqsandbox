using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQPublisher.Interface
{
    public interface IMQPublisher
    {
        void Publish(string message);
    }
}
