using System;

namespace RabbitProducerWorker
{
    public class EventModel
    {
        public int OrderNumber { get; set; }
        public int Status { get; set; }

        public DateTime StatusChangeDate { get; set; } = DateTime.Now;
    }
}