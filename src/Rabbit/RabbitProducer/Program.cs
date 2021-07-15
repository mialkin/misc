#nullable enable
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitProducer
{
    class Program
    {
        private static IModel? _channel;

        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            await Task.Run(Function);
        }

        private static async Task Function()
        {
            while (true)
            {
                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);

                await Task.Delay(100);
            }
        }
    }
}