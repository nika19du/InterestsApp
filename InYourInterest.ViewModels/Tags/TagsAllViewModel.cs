using System;
using System.Collections.Generic;
using System.Text;

namespace InYourInterest.ViewModels.Tags
{
    public class TagsAllViewModel
    {
        public string Search { get; set; }

        public IEnumerable<TagsInfoViewModel> Tags { get; set; }
    }
}
