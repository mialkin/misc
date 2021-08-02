using System.Threading.Tasks;

namespace RabbitSub
{
    public interface IEventConsumer
    {
        Task StartConsuming();
    }
}