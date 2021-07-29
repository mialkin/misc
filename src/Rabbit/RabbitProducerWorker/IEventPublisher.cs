namespace RabbitProducerWorker
{
    public interface IEventPublisher
    {
        void Publish(byte[] message);
    }
}