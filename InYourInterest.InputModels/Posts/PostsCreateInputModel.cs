using InYourInterest.Data.Models.Enums;
using InYourInterest.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InYourInterest.InputModels.Posts
{
    public class PostsCreateInputModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Can't be more than 20 symbols", MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [EnumDataType(typeof(PostType))]
        [Display(Name = "Post type")]
        public PostType PostType { get; set; }

        [Required]
        [MaxLength(600)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required] 
        public string CategoryId { get; set; }
        public string GroupId { get; set; }

        [Display(Name = "Tags")]
        public IEnumerable<string> TagIds { get; set; }

        public IEnumerable<PostsCategoryDetailsViewModel> Categories { get; set; }

        public IEnumerable<PostsTagsDetailsViewModel> Tags { get; set; }
    }
}
