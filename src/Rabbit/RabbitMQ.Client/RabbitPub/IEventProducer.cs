namespace RabbitPub
{
    public interface IEventProducer
    {
        void Publish(byte[] message);
    }
}