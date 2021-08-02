using System;

namespace RabbitPub
{
    public class EventModel
    {
        public int OrderNumber { get; set; }
        public int Status { get; set; }

        public DateTime StatusChangeDate { get; set; } = DateTime.Now;
    }
}