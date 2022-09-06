using Atom.Base;
using Atom.Events.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Atom.Pages.Test
{
    public class TestApiModel : PageModelBase
    {
        IEnumerable<IEventController> EventControllers { get; init; }


        public TestApiModel(IServiceProvider service) : base(service)
        {
            EventControllers = service.GetServices<IEventController>();
        }


        public void OnGet()
        {
            foreach(IEventController searchEvent in EventControllers)
            {
                var x = searchEvent.GetAllEvents();
            }
        }
    }
}
