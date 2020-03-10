using OrderHandler.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderHandler
{
    class Program
    {
        static RMQ_Receiver message_receiver;

        static void Main(string[] args)
        {
            Console.WriteLine("Order Handler Starting...");

            Thread.Sleep(2000);

            message_receiver = RMQ_Receiver.GetInstance();

            Thread.Sleep(2000);

            message_receiver.ReceiveMessage();
        }
    }
}
