using InYourInterest.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InYourInterest.Services.InYourInterest.Services.Models
{
    public class GroupServiceModel
    {
        public string Id { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; } 
        public string Image { get; set; }
        public string LargeImage { get; set; }
        public string Location { get; set; }
        public string CategoriesId { get; set; }
        public Category Category { get; set; }
    }
}
