using InYourInterest.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace InYourInterest.ViewModels.Tags
{
    public class TagsDetailsViewModel
    {
        public string Search { get; set; }

        public TagsInfoViewModel Tag { get; set; }

        public IEnumerable<PostsListingViewModel> Posts { get; set; }
    }
}
