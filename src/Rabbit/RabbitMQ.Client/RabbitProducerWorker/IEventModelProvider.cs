namespace RabbitProducerWorker
{
    public interface IEventModelProvider
    {
        EventModel GetEventModel();
    }
}