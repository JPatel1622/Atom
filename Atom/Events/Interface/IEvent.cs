using Atom.Domain.Enum;
using System;

namespace Atom.Events.Interface
{
    public interface IEvent
    {
        string Name { get; set; }
        string? Description { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string Location { get; set; }
        double? Cost { get; set; }
        string HostedBy { get; set; }
        string ImageURL { get; set; }
        EventType EventType { get; set; }
        string EventID { get; set; }
    }
}
