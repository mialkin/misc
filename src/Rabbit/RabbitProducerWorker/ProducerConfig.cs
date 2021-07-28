namespace RabbitProducerWorker
{
    public class ProducerConfig
    {
        public string Hostname { get; set; }
        public string Queue { get; set; }
        public string RoutingKey { get; set; }
    }
}