using InYourInterest.Data.Common;
using InYourInterest.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InYourInterest.Data.Models
{
    public class Post : IAuditInfo,IDeletableEntity
    { 
        public string Id { get; set; } 
        public string Title { get; set; } 
        public PostType Type { get; set; } 
        public string Description { get; set; } 
        public int Views { get; set; } 
        public string AuthorId { get; set; } 
        public User Author { get; set; } 
        public string CategoryId { get; set; } 
        public Category Category { get; set; } 
        public string GroupId { get; set; }
        public Group Group { get; set; }
        public DateTime CreatedOn { get; set; } 
        public DateTime? ModifiedOn { get; set; } 
        public bool IsDeleted { get; set; } 
        public DateTime? DeletedOn { get; set; }
        public ICollection<Reply> Replies { get; set; } = new HashSet<Reply>();
        public ICollection<PostTag> Tags { get; set; } = new HashSet<PostTag>(); 
        public ICollection<PostReaction> Reactions { get; set; } = new HashSet<PostReaction>(); 
    }
}
