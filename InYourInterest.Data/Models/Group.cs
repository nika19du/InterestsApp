using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InYourInterest.Data.Models
{
    public class Group
    { 
        public string Id { get; set; } 
        [Required]
        public string Name { get; set; } 
        [Required]
        [MinLength(50, ErrorMessage = "Please write at least 50 characters")]
        public string Description { get; set; } 
        public string SmallImage { get; set; } 
        public string LargeImage { get; set; }
        public string Location { get; set; }
        public string CategoriesId { get; set; }
        public Category Category { get; set; } 
        public string UserId { get; set; }
        public User Host { get; set; } //Organized by:... 

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();//edna gr moje da ima mn postove
    }
}
