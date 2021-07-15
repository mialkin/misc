#nullable enable
using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitProducer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost"};
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: "hello",
                    basicProperties: null,
                    body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            
            //await Task.Run(Function);
        }

        private static async Task Function()
        {
            while (true)
            {
                Console.WriteLine("Hello World!");
                await Task.Delay(1000);
            }
        }
    }
}