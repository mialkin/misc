namespace RabbitConsumerWorker
{
    public class ConsumerConfig
    {
        public string Hostname { get; set; }
        public string Queue { get; set; }
        public string RoutingKey { get; set; }
    }
}