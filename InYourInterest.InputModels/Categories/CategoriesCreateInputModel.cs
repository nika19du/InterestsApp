using System.ComponentModel.DataAnnotations;

namespace InYourInterest.ViewModels.Categories
{
    public class CategoriesCreateInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
