using System;

namespace RabbitProducerWorker
{
    public class EventModel
    {
        public int OrderNumber { get; set; }
        public int Status { get; set; }

        public DateTime StatusChangeDate { get; set; } = DateTime.Now;
    }

    public class EventModelGetter
    {
        private static readonly Random Random;

        static EventModelGetter()
        {
            Random = new();
        }

        public static EventModel GetEventModel() =>
            new()
            {
                OrderNumber = Random.Next(int.MaxValue),
                Status = Random.Next(30)
            };
    }
}