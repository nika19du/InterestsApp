using System.ComponentModel.DataAnnotations;

namespace InYourInterest.InputModels.Categories
{
    public class CategoriesCreateInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
