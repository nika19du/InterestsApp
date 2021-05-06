using InYourInterest.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System; 
namespace InYourInterest.InputModels.Events
{
    public class EventsCreateInputModel 
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Location { get; set; }
        ///[Required]
        public IFormFile Image { get; set; }
        public string CategoriesId { get; set; }
        public Category Category { get; set; } 
        [Required]
        [DataType(DataType.Date)]
        public DateTime Started { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Ended { get; set; }
        public bool IsItOnline { get; set; }
    }
}
