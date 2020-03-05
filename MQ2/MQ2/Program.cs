using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace MQ2
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);

                    Process();

                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "Queue",
                                     autoAck: true,
                                     consumer: consumer);


                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        private static void Process()
        
        {
            wait(10000000);
        }

        public static void wait(int milliseconds)
        {
            Thread thread = new Thread(delegate ()
            {
                System.Threading.Thread.Sleep(milliseconds);
            });
            thread.Start();
            //while (thread.IsAlive)
                //Application.DoEvents();
        }


    }
}
