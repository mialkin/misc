namespace RabbitProducerWorker
{
    public interface IEventProducer
    {
        void Publish(byte[] message);
    }
}