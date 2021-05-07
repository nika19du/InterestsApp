using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InYourInterest.InputModels.Replies
{
    public class RepliesCreateInputModel
    {
        public string? ParentId { get; set; }

        [Required]
        public string PostId { get; set; }

        [Required] 
        public string Description { get; set; }
    }
}
