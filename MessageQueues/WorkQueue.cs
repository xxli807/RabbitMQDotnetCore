using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueues
{
    public class WorkQueue : IQueue
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
                    channel.QueueDeclare(queue: "Messages", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.ExchangeDeclare("MessagesExchange", ExchangeType.Direct);
                    channel.QueueBind("Messages", "MessagesExchange", routingKey: "message", arguments: null);
                    var properties = channel.CreateBasicProperties();
                    //properties.Persistent = true;
                    properties.DeliveryMode = 2;

                    //declare the message
                    var message = "this is my working queue message........"; 
                    var messageBody = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "MessagesExchange", routingKey: "message", basicProperties: properties, body: messageBody);
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
