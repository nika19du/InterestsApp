using InYourInterest.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace InYourInterest.InputModels.Events
{
    public class EventsEditInputModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public string UserId { get; set; }
        public string CategoriesId { get; set; }
        public Category Category { get; set; }
    }
}
