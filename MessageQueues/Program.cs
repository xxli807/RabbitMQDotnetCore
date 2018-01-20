using MessageQueues;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueuesSender 
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //IQueue bq = new BasicQueue();
            //bq.Send();

            IQueue wq = new WorkQueue();
            wq.Send();
        }


         
    }
}
