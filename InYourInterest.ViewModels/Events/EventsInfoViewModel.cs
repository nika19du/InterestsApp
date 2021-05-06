using Ganss.XSS;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;

namespace InYourInterest.ViewModels.Events
{
    public class EventsInfoViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
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

        [Required]
        public string Location { get; set; }
        [Required]
        public string Image { get; set; }
        public string UserId { get; set; }

        public string CategoriesId { get; set; }
    }
}
