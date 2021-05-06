using System.Collections.Generic;

namespace InYourInterest.ViewModels.Categories
{
    public class CategoriesAllViewModel
    {
        public string Search
        {
            get;
            set;
        }
        public IEnumerable<CategoriesInfoViewModel> Categories { get; set; }
    }
}
