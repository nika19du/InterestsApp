using System.Collections.Generic;

namespace InYourInterest.ViewModels.Events
{
    public class EventsAllViewModel
    {
        public string Search
        {
            get;
            set;
        }
        public IEnumerable<EventsInfoViewModel> Events { get; set; }
    }
}
