using System;

namespace RabbitPub
{
    public class EventModelProvider : IEventModelProvider
    {
        private static readonly Random Random;

        static EventModelProvider()
        {
            Random = new();
        }

        public EventModel GetEventModel() =>
            new()
            {
                OrderNumber = Random.Next(int.MaxValue),
                Status = Random.Next(30)
            };
    }
}