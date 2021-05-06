using Ganss.XSS;
using InYourInterest.Data.Models;
using System.Net;
using System.Text.RegularExpressions;

namespace InYourInterest.ViewModels.Events
{
    public class EventsDetailsViewModel
    { 
        public Event Event { get; set; }

        //public string Content
        //{
        //    get
        //    {
        //        var content = WebUtility.HtmlDecode(Regex.Replace(this.Event.Description, @"<[^>]+>", string.Empty));
        //        return new HtmlSanitizer().Sanitize(this.Content);
        //    }
        //}
        //public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        //public IEnumerable<E MyProperty { get; set; }
    }
}
