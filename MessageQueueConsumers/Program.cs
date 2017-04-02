using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueueConsumers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BasicQueueConsumer bq = new BasicQueueConsumer();
            //bq.Receive();

            WorkQueueConsumer wq1 = new WorkQueueConsumer();
            wq1.Receive();


            WorkQueueConsumer wq2 = new WorkQueueConsumer();
            wq2.Receive(); 
        }
    }
}
