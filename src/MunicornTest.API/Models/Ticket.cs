using System;
using System.Collections.Generic;

namespace MunicornTest.BusinnessLogic.Models
{
    public class Ticket
    {
        private static Random _random = new Random(123456789);
        public int Id { get; set; } = _random.Next();
        public string Title { get; set; }
        public State CurrentState { get; set; }
        public string AssignedToUser { get; set; }
        public int Count { get; set; }
        public List<string> Comments { get; set; }

        public override string ToString()
        {
            return $"{Id} {Title} {CurrentState} {AssignedToUser} {Count} {String.Join(", ", Comments.ToArray())}";
        }

        public enum State
        {
            Open,
            InProgress,
            Closed
        }
    }
}
