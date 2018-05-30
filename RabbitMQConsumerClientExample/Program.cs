using OSRabbitMQConsumer.Args;
using OSRabbitMQConsumer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConsumerClientExample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var uri = new Uri("http://localhost:5672");
                var consumer = new RabbitMQConsumer(uri);
                Console.WriteLine(consumer.Factory.HostName + ":" + consumer.Factory.Port);
                consumer.Connect();
                consumer.MessageReceived += Consumer_MessageReceived;
                consumer.Consume("TestChannel");

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Consumer_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine(" [x] Received {0}", e.Message.Text);
        }
    }
}
