using Atom.Events.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace Atom.Events.Model
{
    public class EventSearchOptions : IEventSearchOptions
    {
        [Display(Name = "Start")]
        public DateTime? StartTime { get; set; }
        [Display(Name = "End")]
        public DateTime? EndTime { get; set; }
        [Display(Name = "Payment Required")]
        public bool IsPaid { get; set; }
        [Display(Name = "For Kids")]
        public bool IsForKids { get; set; }
        [Display(Name = "Search")]
        public string Name { get; set; }
    }
}
