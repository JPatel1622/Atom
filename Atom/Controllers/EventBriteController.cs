using Atom.Base;
using Atom.Events.Interface;
using Atom.Events.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Atom.Controllers
{
    [Route("api/EventBrite/[Action]")]
    [ApiController]
    public class EventBriteController : APIControllerBase, IEventController
    {

        public EventBriteController(IServiceProvider service) : base(service)
        {

        }

        public async Task<IEvent> GetEvent(string eventID)
        {
            string authToken = "RFD5LVOUCWKMPBRDVQCN";

            using (HttpResponseMessage response = await HttpClient.GetAsync("https://www.eventbriteapi.com/v3/events/" + eventID + "/?token=" + authToken))
            {
                if (response.IsSuccessStatusCode)
                {
                    var x = response.Content.ReadAsStringAsync();
                    Console.WriteLine(x);

                    var formattedResponse = JsonConvert.DeserializeObject(x.Result).ToString();

                    EBEvent z = JsonConvert.DeserializeObject<EBEvent>(formattedResponse);

                    string venue = z.Venue_id;

                    Venue eventVenue;

                    using (HttpResponseMessage r = await HttpClient.GetAsync("https://www.eventbriteapi.com/v3/venues/" + venue + "/?token=" + authToken))
                    {
                        var y = r.Content.ReadAsStringAsync();

                        var fR = JsonConvert.DeserializeObject(y.Result).ToString();

                        eventVenue = JsonConvert.DeserializeObject<Venue>(fR);

                    }

                    var startDateTime = z.Start.Local.ToString("yyyy-MM-dd");

                    EventBriteEvent eventBriteEvent = new EventBriteEvent
                    {
                        Name = z.Name.Text,
                        Description = z.Description.Text,
                        StartDate = z.Start.Local,
                        EndDate = z.End.Local,
                        Location = eventVenue.Address.Localized_address_display,
                        Cost = z.Cost,
                        StartTime = z.Start.Local,
                        EndTime = z.End.Local,
                        IsPaid = !z.Is_Free,
                        IsForKids = eventVenue.Age_Restriction == "18+" ? false : true,
                        ImageURL = z.logo.original.url,
                        EventType = Domain.Enum.EventType.EventBrite,
                        EventID = z.id
                    };

                    return eventBriteEvent;


                }
                return null;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<IEvent>> GetAllEvents()
        {

            List<string> eventIds;

            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent: Other");
                using (var stream = client.OpenRead("https://www.eventbrite.com/d/ga--savannah/all-events/"))
                using (var textReader = new StreamReader(stream, Encoding.UTF8, true))
                {
                    var x = textReader.ReadToEnd();

                    //Regex pattern = new Regex ("-[0-9]{11}");

                    string patternStr = "eid=[0-9]{12}";

                    List<string> allEventIds = new List<string>();

                    MatchCollection matchs = Regex.Matches(x, patternStr);

                    foreach (Match match in matchs)
                    {
                        allEventIds.Add(match + "");
                    }

                    List<string> eventIdsHypen = new HashSet<string>(allEventIds).ToList();
                    eventIds = eventIdsHypen.Select(s => s.Replace("eid=", "")).ToList();



                }
            }


            string authToken = "RFD5LVOUCWKMPBRDVQCN";

            List<string> allEvents = new List<string>();

            ConcurrentStack<EventBriteEvent> events = new ConcurrentStack<EventBriteEvent>();

            ConcurrentStack<Task> tasks = new ConcurrentStack<Task>();

            foreach (var eventId in eventIds)
            {
                tasks.Push(Task.Run(() => GetEventsAsync(authToken, events, eventId)));
            }

            Task.WaitAll(tasks.ToArray());

            // make events a global variable so dont have to make constant api calls for search options
            return events;

        }

        private async Task GetEventsAsync(string authToken, ConcurrentStack<EventBriteEvent> events, string eventId)
        {
            using (HttpResponseMessage response = await HttpClient.GetAsync("https://www.eventbriteapi.com/v3/events/" + eventId + "/?token=" + authToken))
            {
                if (response.IsSuccessStatusCode)
                {
                    var x = response.Content.ReadAsStringAsync();
                    Console.WriteLine(x);

                    var formattedResponse = JsonConvert.DeserializeObject(x.Result).ToString();

                    EBEvent z = JsonConvert.DeserializeObject<EBEvent>(formattedResponse);

                    string venue = z.Venue_id;

                    Venue eventVenue;

                    using (HttpResponseMessage r = await HttpClient.GetAsync("https://www.eventbriteapi.com/v3/venues/" + venue + "/?token=" + authToken))
                    {
                        var y = r.Content.ReadAsStringAsync();

                        var fR = JsonConvert.DeserializeObject(y.Result).ToString();

                        eventVenue = JsonConvert.DeserializeObject<Venue>(fR);

                    }

                    var startDateTime = z.Start.Local.ToString("yyyy-MM-dd");

                    EventBriteEvent eventBriteEvent = new EventBriteEvent
                    {
                        Name = z.Name.Text,
                        Description = z.Description.Text,
                        StartDate = z.Start.Local,
                        EndDate = z.End.Local,
                        Location = eventVenue.Address.Localized_address_display,
                        Cost = z.Cost,
                        StartTime = z.Start.Local,
                        EndTime = z.End.Local,
                        IsPaid = !z.Is_Free,
                        IsForKids = eventVenue.Age_Restriction == "18+" ? false : true,
                        ImageURL = z.logo.original.url,
                        EventType = Domain.Enum.EventType.EventBrite,
                        EventID = z.id
                    };

                    events.Push(eventBriteEvent);


                }

            }
        }

        public async Task<IEnumerable<IEvent>> GetEventsTest()
        {
            EventBriteEvent searchOptions = new EventBriteEvent();
            searchOptions.Name = "Taco";
            searchOptions.IsForKids = false;



            List<EventBriteEvent> currEvent = (List<EventBriteEvent>)await GetEvents(searchOptions);


            return currEvent;
        }

        public async Task<IEnumerable<IEvent>> GetEvents(IEventSearchOptions searchOptions)
        {
            IEnumerable<IEvent> events = await GetAllEvents();
            List<EventBriteEvent> currEvent = new List<EventBriteEvent>();

            foreach (EventBriteEvent e in events)
            {
                bool isValid = false;
                if (searchOptions.Name != null)
                {
                    if (e.Name.Contains(searchOptions.Name))
                    {
                        isValid = true;
                    }

                    if (e.Description.Contains(searchOptions.Name))
                    {
                        isValid = true;
                    }
                }

                if (searchOptions.IsForKids)
                {
                    isValid = e.IsForKids == searchOptions.IsForKids;
                }

                if (searchOptions.IsPaid)
                {
                    isValid = e.IsPaid == searchOptions.IsPaid;
                }

                if (searchOptions.StartTime != null)
                {
                    if (e.StartDate == searchOptions.StartTime || e.StartDate.Date == searchOptions.StartTime.Value.Date || e.StartDate.TimeOfDay == searchOptions.StartTime.Value.TimeOfDay)
                    {
                        isValid = true;
                    }
                }

                if (searchOptions.EndTime != null)
                {
                    if (e.EndDate == searchOptions.EndTime || e.EndDate.Date == searchOptions.EndTime.Value.Date || e.EndDate.TimeOfDay == searchOptions.EndTime.Value.TimeOfDay)
                    {
                        isValid = true;
                    }
                }

                if (isValid)
                {
                    currEvent.Add(e);
                }
            }


            return currEvent;


        }
    }


    internal class EBEvent
    {
        public Name Name { get; set; }
        public Description? Description { get; set; }
        public Start Start { get; set; }
        public End? End { get; set; }
        public string Venue_id { get; set; }
        public double? Cost { get; set; }
        public bool Is_Free { get; set; }
        public bool IsForKids { get; set; }
        public logo logo { get; set; }
        public string id { get; set; }
    }

    internal class logo
    {
        public original original { get; set; }
    }

    internal class original
    {
        public string url { get; set; }
    }

    internal class Name
    {
        public string Text { get; set; }
    }

    internal class Description
    {
        public string Text { get; set; }
    }

    internal class Start
    {
        public DateTime Local { get; set; }
    }

    internal class End
    {
        public DateTime Local { get; set; }
    }

    internal class Venue
    {
        public Address Address { get; set; }

        public string? Age_Restriction { get; set; }
    }

    internal class Cost
    {
        public double Currency { get; set; }
    }

    internal class HostedBy
    {
        public string Organizer_id { get; set; }
    }

    internal class Address
    {
        public string Localized_address_display { get; set; }
    }

}
