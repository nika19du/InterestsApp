using InYourInterest.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InYourInterest.InputModels.Groups
{
    public class GroupsCreateInputModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(60)]
        public string Description { get; set; }  
        public IFormFile Image { get; set; }
        public string CategoriesId { get; set; }
        public Category Category { get; set; }
    }
}
