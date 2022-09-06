using Atom.Base;
using Atom.Events.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Atom.Controllers
{
    public class MeetupController : APIControllerBase, IEventController
    {
        public MeetupController(IServiceProvider service) : base(service)
        {
        }

        public async Task<IEnumerable<IEvent>> GetAllEvents()
        {
            var x = GetEvents().Result;




            //return x;
            return null;
        }

        private async Task<IEnumerable<IEvent>> GetEvents()
        {
            using (HttpResponseMessage response = await HttpClient.GetAsync("https://api.seatgeek.com/2/events?client_id=MjYzODI1MDV8MTY0ODkwMzkwMC43MTYyNjg"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var x = await response.Content.ReadAsStringAsync();

                    return null;
                }
                else
                {
                    return null;
                }

            }
        }

        public Task<IEnumerable<IEvent>> GetEvents(IEventSearchOptions searchOptions)
        {
            throw new NotImplementedException();
        }
    }
}
