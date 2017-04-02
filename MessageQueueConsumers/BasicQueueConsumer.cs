using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MessageQueueConsumers
{
    public class BasicQueueConsumer
    {
        public void Receive()
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Messages", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.ExchangeDeclare("MessagesExchange", ExchangeType.Direct);
                    channel.QueueBind("Messages", "MessagesExchange", routingKey: "message", arguments: null);

                    var consumer = new QueueingBasicConsumer(channel);
                    //consumer.Received += (model, ea) =>
                    //{
                    //    var body = ea.Body;
                    //    var message = Encoding.UTF8.GetString(body);
                    //    Console.WriteLine(" Received {0}", message);
                    //};
                    while (true)
                    {
                        channel.BasicConsume(queue: "Messages", noAck: true, consumer: consumer);
                        var eventArgs = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        string message = Encoding.UTF8.GetString(eventArgs.Body);
                        Console.WriteLine(message);

                    }
                }
            }

            catch (Exception e)
            {
                throw e;
            }
        } 
    }
}
