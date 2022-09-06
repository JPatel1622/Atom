using Atom.Base;
using Atom.Controllers;
using Atom.Data.Model.Security;
using Atom.Domain.Enum;
using Atom.Events.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Atom.Pages
{
    public class IndexModel : PageModelBase
    {
        #region Constructor
        public IndexModel(IServiceProvider provider) : base(provider)
        {
            var controllers = provider.GetServices<IEventController>();

            foreach (var controller in controllers)
            {
                if (controller.GetType() == typeof(EventBriteController))
                {
                    EventBriteController = (EventBriteController)controller;
                }
                else if (controller.GetType() == typeof(TicketmasterController))
                {
                    TicketmasterController = (TicketmasterController)controller;
                }
            }
        }
        #endregion
        public ConcurrentStack<IEvent> UserEvents { get; set; } = new ConcurrentStack<IEvent>();
        public ConcurrentStack<Task> Tasks { get; set; } = new ConcurrentStack<Task>();
        public IEnumerable<FavoriteEventModel> FavoriteEvents { get; set; }
        private readonly EventBriteController EventBriteController;
        private readonly TicketmasterController TicketmasterController;

        public async Task OnGetAsync()
        {
            if (IsLoggedIn)
            {
                FavoriteEvents = SecurityManager.GetUsersFavoriteEvents(LoggedInUser);

                Parallel.ForEach(FavoriteEvents, favorite =>
                {
                    switch (favorite.EventTypeId)
                    {
                        case (EventType.EventBrite):
                            Tasks.Push(GetEvent(EventBriteController, favorite.EventId));
                            break;
                        case (EventType.TicketMaster):
                            Tasks.Push(GetEvent(TicketmasterController, favorite.EventId));
                            break;
                    }
                });

                Task.WaitAll(Tasks.ToArray());
            }
        }

        private async Task GetEvent(IEventController controller, string eventid)
        {
            DateTime now = DateTime.Now;

            Debug.WriteLine("Now " + now);

            if (controller.GetType() == typeof(EventBriteController))
            {
                IEvent ev = await EventBriteController.GetEvent(eventid);
                UserEvents.Push(ev);
            }
            else if (controller.GetType() == typeof(TicketmasterController))
            {
                IEvent ev = await TicketmasterController.GetEvent(eventid);
                UserEvents.Push(ev);
            }

            DateTime finished = DateTime.Now;

            Debug.WriteLine("Finished " + finished);
        }
    }
}
