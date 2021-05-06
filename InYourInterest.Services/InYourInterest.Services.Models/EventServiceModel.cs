using System;

namespace InYourInterest.Services.InYourInterest.Services.Models
{ 
    public class EventServiceModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public string CategoriesId { get; set; }
    }
}
