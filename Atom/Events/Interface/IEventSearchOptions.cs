using System;

namespace Atom.Events.Interface
{
    public interface IEventSearchOptions
    {
        DateTime? StartTime { get; set; }
        DateTime? EndTime { get; set; }
        bool IsPaid { get; set; }
        bool IsForKids { get; set; }
        string? Name { get; set; }

    }
}
