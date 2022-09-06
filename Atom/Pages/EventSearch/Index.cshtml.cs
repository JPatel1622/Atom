using Atom.Base;
using Atom.Data.Model.Security;
using Atom.Events.Interface;
using Atom.Events.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Atom.Pages.EventSearch
{
    public class IndexModel : PageModelBase
    {
        [BindProperty]
        public EventSearchOptions SearchOptions { get; set; } = new EventSearchOptions();

        [BindProperty]
        [Display(Name = "Paid For")]
        public bool IsPaid { get; set; }

        public IEnumerable<FavoriteEventModel> UsersFavorites { get; set; }

        [BindProperty]
        [Display(Name = "For Kids")]
        public bool IsForKids { get; set; }

        private IEnumerable<IEventController> Controllers;

        ConcurrentStack<IEnumerable<IEvent>> Events { get; set; } = new();

        [BindProperty]
        public IEnumerable<IEvent> FlattenedEvents { get; set; }

        public IndexModel(IServiceProvider provider) : base(provider) 
        { 
            Controllers = provider.GetServices<IEventController>();
        }

        public void OnGet()
        {
            if(LoggedInUser != null)
            {
                UsersFavorites = SecurityManager.GetUsersFavoriteEvents(LoggedInUser);
            }

            ConcurrentStack<Task> tasks = new();

            foreach (var controller in Controllers)
            {
                tasks.Push(Task.Run(() => SearchAllEventsAsync(controller)));
            }

            Task.WaitAll(tasks.ToArray());

            FlattenedEvents = (from list in Events
                               from item in list
                               select item).ToList();
        }

        private async Task<Task> SearchAllEventsAsync(IEventController controller)
        {
            Stopwatch timer = Stopwatch.StartNew();
            Events.Push(await controller.GetAllEvents());
            timer.Stop();

            Debug.WriteLine(controller.ToString() + "finished after " + timer.ElapsedMilliseconds);
            return Task.CompletedTask;
        }  
        
        private async Task<Task> SearchAllEventsWithOptionsAsync(IEventController controller)
        {
            Stopwatch timer = Stopwatch.StartNew();
            Events.Push(await controller.GetEvents(SearchOptions));
            timer.Stop();

            Debug.WriteLine(controller.ToString() + "finished after " + timer.ElapsedMilliseconds);
            return Task.CompletedTask;
        }

        public IActionResult OnPost()
        {
            if (LoggedInUser != null)
            {
                UsersFavorites = SecurityManager.GetUsersFavoriteEvents(LoggedInUser);
            }

            ConcurrentStack<Task> tasks = new();

            foreach (var controller in Controllers)
            {
                tasks.Push(Task.Run(() => SearchAllEventsWithOptionsAsync(controller)));
            }

            Task.WaitAll(tasks.ToArray());

            FlattenedEvents = (from list in Events
                               from item in list
                               select item).ToList();
            return Page();
        }
    }
}
