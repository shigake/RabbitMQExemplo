using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MQ1
{
    class Program
    {
        static void Main(string[] args)
        {
            int p = 0;
            while (true) {

                _ = send("Hello", p);
                p++;
            }
        }

        private static async Task<string> send(string message, int p)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                
                channel.QueueDeclare(queue: "Queue",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null
                                    );

                message = p.ToString();

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "Queue",
                                     basicProperties: null,
                                     body: body
                                     );

                Console.WriteLine("[x] sent {0}", message);
                Console.WriteLine(" Press [enter] to exit.");
                return message;
            }
        }

       

    }
}
