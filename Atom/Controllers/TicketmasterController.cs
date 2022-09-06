using Atom.Base;
using Atom.Events.Interface;
using Atom.Events.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
/*
using Atom.Base;
using Atom.Events.Interface;
using Atom.Events.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
*/
namespace Atom.Controllers
{
    [Route("api/Ticketmaster/[Action]")]
    [ApiController]
    public class TicketmasterController : APIControllerBase, IEventController
    {
        public TicketmasterController(IServiceProvider service) : base(service)
        {

        }

        public async Task<IEnumerable<IEvent>> Tester()
        {
            IEventSearchOptions searchOptions = new Filter()
            {
                IsPaid = true,
            };


            return await GetEvents(searchOptions);
        }

        internal class Filter : IEventSearchOptions
        {
            public bool? IsOutdoor { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public bool IsPaid { get; set; }
            public int? Capacity { get; set; }
            public bool IsForKids { get; set; }
            public bool? IsPetFriendly { get; set; }
            public bool? IsHandicapAccessible { get; set; }
            public string Name { get; set; }
        }

        public async Task<IEvent> GetEvent(string eventID)
        {
            using (HttpResponseMessage response = await HttpClient.GetAsync($"https://app.ticketmaster.com/discovery/v2/events?apikey=KQ42oqCBqdXtZhRe6QnuME1yTkgf5Oyu&id={eventID}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var x = await response.Content.ReadAsStringAsync();

                    JSON tm = JsonConvert.DeserializeObject<JSON>(x);


                    foreach (var e in tm._embedded.events)
                    {
                        TicketmasterEvent ticketmasterEvent = new TicketmasterEvent()
                        {
                            Name = e.name,
                            Description = e.url,
                            Type = e.type,
                            StartDate = e.dates.start.localDate.Date.Add(e.dates.start.localTime.TimeOfDay),
                            EndDate = e.dates.start.localDate.AddDays(1).AddSeconds(-1),
                            Cost = e.priceRanges == null ? null : e.priceRanges[0]?.max,
                            Location = e._embedded.venues[0].address.line1,
                            HostedBy = e.promoter?.name,
                            AgeRestriction = e.ageRestrictions?.legalAgeEnforced,
                            ImageURL = e.images[0].url,
                            EventType = Domain.Enum.EventType.TicketMaster,
                            EventID = e.id
                        };

                        return ticketmasterEvent;
                    }

                }

                return null;
            }
        }


        public async Task<IEnumerable<IEvent>> GetAllEvents()
        {
            List<string> eventIds;

            List<string> allEvents = new List<string>();

            List<TicketmasterEvent> events = new List<TicketmasterEvent>();

            using (HttpResponseMessage response = await HttpClient.GetAsync("https://app.ticketmaster.com/discovery/v2/events?apikey=KQ42oqCBqdXtZhRe6QnuME1yTkgf5Oyu&radius=20&unit=miles&locale=*&page=1&city=Savannah&countryCode=US&stateCode=GA"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var x = await response.Content.ReadAsStringAsync();

                    JSON tm = JsonConvert.DeserializeObject<JSON>(x);


                    foreach (var e in tm._embedded.events)
                    {
                        TicketmasterEvent ticketmasterEvent = new TicketmasterEvent()
                        {
                            Name = e.name,
                            Description = e.url,
                            Type = e.type,
                            StartDate = e.dates.start.localDate.Date.Add(e.dates.start.localTime.TimeOfDay),
                            EndDate = e.dates.start.localDate.AddDays(1).AddSeconds(-1),
                            Cost = e.priceRanges == null ? null : e.priceRanges[0]?.max,
                            Location = e._embedded.venues[0].address.line1,
                            HostedBy = e.promoter?.name,
                            AgeRestriction = e.ageRestrictions?.legalAgeEnforced,
                            ImageURL = e.images[0].url,
                            EventType = Domain.Enum.EventType.TicketMaster,
                            EventID = e.id
                        };

                        //if (e.ageRestrictions.legalAgeEnforced)
                        //{
                        //    ticketmasterEvent.AgeRestriction = 18;
                        //}
                        events.Add(ticketmasterEvent);
                    }

                }


                return events;

            }


        }

        public async Task<IEnumerable<IEvent>> GetEvents(IEventSearchOptions searchOptions)
        {
            IEnumerable<IEvent> allEvent = await GetAllEvents();
            List<TicketmasterEvent> events = new List<TicketmasterEvent>();

            foreach (TicketmasterEvent ev in allEvent)
            {
                bool isValid = true;

                if (searchOptions.StartTime != null)
                {


                    if (searchOptions.StartTime < ev.StartDate)
                    {
                        isValid = false;
                    }
                    if (searchOptions.StartTime >= ev.StartDate)
                    {
                        //isValid = true;
                    }



                }


                if (searchOptions.EndTime != null)
                {
                    if (ev.EndDate != null)
                    {

                        if (searchOptions.EndTime < ev.EndDate)
                        {
                            isValid = false;
                        }
                        if (searchOptions.EndTime >= ev.EndDate)
                        {
                            //isValid = true;
                        }

                    }

                }

                if (searchOptions.IsPaid)
                {
                    if ((bool)searchOptions.IsPaid)
                    {
                        if (ev.Cost == null)
                        {

                            isValid = false;

                        }
                    }
                    else
                    {
                        if (ev.Cost != null)
                            isValid = false;
                    }

                }

                if (searchOptions.IsForKids)
                {
                    if (ev.AgeRestriction != searchOptions.IsForKids)
                    {
                        isValid = false;
                    }

                }

                if (searchOptions.Name != null)
                {
                    if (!ev.Name.Contains(searchOptions.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        isValid = false;
                    }

                }

                if (isValid)
                {
                    events.Add(ev);
                }

            }
            return events;
        }
        //testing


        internal class JSON
        {
            public _embedded _embedded;
        }
        internal class _embedded
        {
            public events[] events { get; set; }

            //public venues venues { get; set; }
        }

        internal class events
        {
            public string name { get; set; }
            public string type { get; set; }
            public string url { get; set; }

            public string id { get; set; }
            public dates dates { get; set; }
            //public string _embedded { get; set; }
            public priceRanges[] priceRanges { get; set; }
            public ageRestrictions ageRestrictions { get; set; }
            public promoter? promoter { get; set; }

            public detail _embedded { get; set; }

            public images[] images { get; set; }
            //public _embedded _embedded { get; set; }            

        }

        internal class images
        {
            public string url { get; set; }
        }

        internal class detail
        {
            public venues[] venues { get; set; }
        }
        internal class dates
        {
            public start start { get; set; }
        }

        internal class start
        {
            public DateTime localDate { get; set; }
            public DateTime localTime { get; set; }
        }

        //internal class _embedded
        //{
        //    public string venues { get; set; }
        //}


        internal class venues
        {
            public city city { get; set; }
            public state state { get; set; }
            public address address { get; set; }
        }

        internal class city
        {
            public string name { get; set; }
        }

        internal class state
        {
            public string name { get; set; }
        }

        internal class address
        {
            public string line1 { get; set; }
        }

        internal class priceRanges
        {
            public double min { get; set; }
            public double max { get; set; }
        }

        internal class ageRestrictions
        {
            public bool? legalAgeEnforced { get; set; }
        }

        internal class promoter
        {
            public string? name { get; set; }
        }

    }
}
