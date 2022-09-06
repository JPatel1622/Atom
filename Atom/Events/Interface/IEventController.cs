using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Atom.Events.Interface
{
    public interface IEventController
    {
        Task<IEnumerable<IEvent>> GetEvents(IEventSearchOptions searchOptions);
        Task<IEnumerable<IEvent>> GetAllEvents();
    }
}
