using System.ComponentModel.DataAnnotations;

namespace InYourInterest.InputModels.Categories
{
    public class CategoriesEditInputModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
