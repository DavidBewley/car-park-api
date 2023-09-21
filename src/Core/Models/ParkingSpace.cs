using System;

namespace Core.Models
{
    public class ParkingSpace
    {
        public Guid ParkingSpaceId { get; set; }
        public decimal Width { get; set; }
        public string BayIdentifier { get; set; }
    }
}
