using Ganss.XSS;
using InYourInterest.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
namespace InYourInterest.ViewModels.Groups
{
    public class GroupInfoViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Location { get; set; }
        public string SmallImage { get; set; }
        public string LargeImage { get; set; }
        public string UserId { get; set; }
        public string CategoriesId { get; set; }
        public string CategoryName { get; set; }
        public string HostUserName { get; set; }
        public string HostProfilePicture { get; set; }
        public string HostCreatedOn { get; set; }
        public ICollection<Post> Posts { get; set; }

        //public List<string> SanitizedContent=> new HtmlSanitizer().Sanitize(this.Posts.Where(x=>x.Description));
           
    }
}
