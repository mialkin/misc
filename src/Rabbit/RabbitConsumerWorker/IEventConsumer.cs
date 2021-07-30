using System.Threading.Tasks;

namespace RabbitConsumerWorker
{
    public interface IEventConsumer
    {
        Task StartConsuming();
    }
}