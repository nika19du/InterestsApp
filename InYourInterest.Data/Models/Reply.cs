using InYourInterest.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace InYourInterest.Data.Models
{
    public class Reply : IAuditInfo, IDeletableEntity
    {
        public string Id { get; set; } 
        public string Description { get; set; } 
        public bool IsBestAnswer { get; set; } 
        public string? ParentId { get; set; } 
        public Reply Parent { get; set; } 
        public string PostId { get; set; } 
        public Post Post { get; set; } 
        public string AuthorId { get; set; } 
        public User Author { get; set; } 
        public DateTime CreatedOn { get; set; } 
        public DateTime? ModifiedOn { get; set; } 
        public bool IsDeleted { get; set; } 
        public DateTime? DeletedOn { get; set; } 
        public ICollection<ReplyReaction> Reactions { get; set; } = new HashSet<ReplyReaction>();
    }
}
