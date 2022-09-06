using Atom.Base;
using Atom.Domain.Enum;
using Atom.Events.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Atom.Controllers
{
    [Route("/api/serpi/[action]")]
    [ApiController]
    public class SerpiController : APIControllerBase, IEventController
    {
        private const string LOCATION_PARAM = "&location=Savannah, GA, United States";
        private const string DEFAULT_SEARCH_STRING = "&q=events in Savannah, GA, United States";
        private const string URL = "https://serpapi.com/search?engine=google_events";
        private const string API_KEY = "&api_key=b66124ae489eaac7d98a5728613dc844fc18beea4e56ad9375ef57877b7394eb";

        public SerpiController(IServiceProvider service) : base(service)
        {

        }

        [HttpGet]
        public async Task<IEnumerable<IEvent>> GetAllEvents()
        {
            var events = Task.Run(async () => await GetAllEventsAsync()).Result;

            return events;
        }

        private async Task<IEnumerable<IEvent>> GetAllEventsAsync()
        {
            using (HttpResponseMessage response = await HttpClient.GetAsync($"{URL}{LOCATION_PARAM}{DEFAULT_SEARCH_STRING}{API_KEY}"))
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

        public class SerpiEvent : IEvent
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Location { get; set; }
            public double? Cost { get; set; }
            public string HostedBy { get; set; }
            public string Type { get; set; }
            public string ImageURL { get; set; }
            public EventType EventType { get; set; }
            public string EventID { get; set; }
        }
    }
}
