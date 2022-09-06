using Atom.Base;
using Atom.Events.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Atom.Controllers
{
    [Route("/api/SeatGeak/[Action]")]
    [ApiController]
    public class SeatGeakController : APIControllerBase, IEventController
    {
        public SeatGeakController(IServiceProvider service) : base(service)
        {

        }

        public Task<IEnumerable<IEvent>> GetAllEvents()
        {
            return null;
        }

        public Task<IEnumerable<IEvent>> GetEvents(IEventSearchOptions searchOptions)
        {
            throw new NotImplementedException();
        }

        //public Task<IEnumerable<IEvent>> GetAllEvents()
        //{
        //    throw new NotImplementedException();
        //}

        //    string endPoint = "https://api.seatgeek.com/2/events?client_id=MjYzODI1MDV8MTY0ODkwMzkwMC43MTYyNjg&per_page=1000";
        //    using (HttpClient client = new HttpClient())
        //    {
        //        using (HttpResponseMessage response = await client.GetAsync($"{endPoint}"))
        //        {
        //            var resultStr = response.Content.ReadAsStringAsync().Result.ToString();

        //            if (response.IsSuccessStatusCode)
        //            {

        //                var result =  JsonConvert.DeserializeObject<Root>(resultStr);

        //                foreach (var item in result.events)
        //                {
        //                    list.Add(new SeatGeakEvent()
        //                    {
        //                        StartDate = item.announce_date,
        //                        EndDate = item.enddatetime_utc,
        //                        Type = item.type,
        //                        Name = item.title,
        //                        Description = item.description,
        //                        Cost = item.stats.average_price,
        //                        Location = item.venue.display_location,
        //                        HostedBy = item.venue.name
        //                    }) ;
        //                }

        //    }
        //}

        //[HttpGet]
        //public async Task<JsonResult> GetEvents()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<IEvent>> GetEvents(IEventSearchOptions searchOptions)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
