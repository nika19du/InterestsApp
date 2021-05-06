using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InYourInterest.Data.Models
{
    public class Event
    { 
        public string Id { get; set; }

        [Required]
        public string Name { get; set; } 
        [Required]
        public string Description { get; set; } 
        public string? Location { get; set; } 
        public string Image { get; set; }
        public ICollection<EventUser> EventUser { get; set; } = new List<EventUser>();
        public string UserId { get; set; }
        public User Organizer { get; set; }//Created by 
        public string CategoriesId { get; set; }
        public Category Category { get; set; }//ChangeToCategoryType
        public DateTime Started { get; set; }
        public DateTime Ended { get; set; }
        public bool? IsItOnline { get; set; }
    }
}
