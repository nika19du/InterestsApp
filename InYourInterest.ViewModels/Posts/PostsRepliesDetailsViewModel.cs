using Ganss.XSS;
using InYourInterest.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace InYourInterest.ViewModels.Posts
{
   public class PostsRepliesDetailsViewModel
    {
        private readonly IHtmlSanitizer sanitizer;

        public PostsRepliesDetailsViewModel()
        {
            this.sanitizer = new HtmlSanitizer();
            this.sanitizer.AllowedTags.Add(GlobalConstants.IFrameTag);
        }

        public string Id { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription
            => this.sanitizer.Sanitize(this.Description);

        public bool IsBestAnswer { get; set; }

        public int Likes { get; set; }

        public int Loves { get; set; }

        public int HahaCount { get; set; }

        public int WowCount { get; set; }

        public int SadCount { get; set; }

        public int AngryCount { get; set; }

        public string CreatedOn { get; set; }

        public string? ParentId { get; set; }

        public PostsAuthorDetailsViewModel Author { get; set; }
    }
}
