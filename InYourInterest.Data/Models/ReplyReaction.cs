using InYourInterest.Data.Common;
using InYourInterest.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InYourInterest.Data.Models
{
    public class ReplyReaction : IAuditInfo
    {
        public string Id { get; set; } 
        public ReactionType ReactionType { get; set; } 
        public string ReplyId { get; set; } 
        public Reply Reply { get; set; } 
        public string AuthorId { get; set; } 
        public User Author { get; set; } 
        public DateTime CreatedOn { get; set; } 
        public DateTime? ModifiedOn { get; set; }
    }
}
