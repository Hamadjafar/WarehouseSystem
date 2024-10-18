﻿
namespace DomainLayer.Entities
{
    public class Logs
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Exception { get; set; }

        public string RenderedMessage { get; set; }
        public string Properties { get; set; }
    }
}
