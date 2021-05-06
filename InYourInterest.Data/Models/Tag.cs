using InYourInterest.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace InYourInterest.Data.Models
{
    public class Tag : IAuditInfo,IDeletableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; } 
        public DateTime CreatedOn { get; set; } 
        public DateTime? ModifiedOn { get; set; } 
        public bool IsDeleted { get; set; } 
        public DateTime? DeletedOn { get; set; } 
        public ICollection<PostTag> Posts { get; set; } = new HashSet<PostTag>();
    }
}
