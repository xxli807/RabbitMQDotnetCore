using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageQueueConsumers
{
    public class WorkQueueConsumer
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

                    while (true)
                    {
                        //channel.BasicConsume(queue: "Messages", noAck: true, consumer: consumer);
                        //turn on the ack make sure message deliverd
                        channel.BasicConsume(queue: "Messages", noAck: false, consumer: consumer);
                        var eventArgs = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        string message = Encoding.UTF8.GetString(eventArgs.Body);
                        Console.WriteLine(message);

                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);

                        Console.WriteLine("Received {0}", message);
                        Console.WriteLine("Done");

                        // send the ack back
                        channel.BasicAck(eventArgs.DeliveryTag, false);
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
