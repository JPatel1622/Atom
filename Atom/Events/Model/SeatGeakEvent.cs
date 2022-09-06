
using Atom.Events.Interface;
using System;

namespace Atom.Events.Model
{
    public class SeatGeakEvent
    {
        public Object Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public double? Cost { get; set; }
        public string HostedBy { get; set; }
        public string Type { get; set; }
    }
}
