using Atom.Domain.Enum;
using Atom.Events.Interface;
using System;

namespace Atom.Events.Model
{
    public class EventBriteEvent : IEvent, IEventSearchOptions
    {
        public string Name { get; set ; }
        public string Description { get; set; }
        public DateTime StartDate { get ; set ; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public double? Cost { get; set; }
        public string HostedBy { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsPaid { get; set; }
        public bool IsForKids { get; set; }
        public string ImageURL { get; set; }
        public EventType EventType { get; set; }
        public string EventID { get; set; }
    }
}
