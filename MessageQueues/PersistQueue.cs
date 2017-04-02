using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueues
{
    public class PersistQueue : IQueue
    {
        public void Send()
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    //declare a queue
                    channel.QueueDeclare(queue: "persist_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.ExchangeDeclare("persist_queue_exchange", ExchangeType.Direct);
                    channel.QueueBind("persist_queue", "persist_queue_exchange", routingKey: "persist", arguments: null);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    //declare the message
                    var message = "this is my persistent message";
                    var messageBody = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "persist_queue_exchange", routingKey: "persist", basicProperties: null, body: messageBody);
                    Console.WriteLine(" [x] Sent {0}", message);
                }

                Console.WriteLine(" Press [enter] to exit.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
