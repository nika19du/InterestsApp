using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace InYourInterest.ViewModels.Events
{
    public class EventsSavedViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));
                return content;
            }
        } 
        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);
        public string Image { get; set; }
        public string Location { get; set; }
        public string CategoriesId { get; set; }

    }
}
