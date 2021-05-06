using InYourInterest.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InYourInterest.Data.Models
{
    public class Message : IAuditInfo,IDeletableEntity
    {
        [Key]
        public string Id { get; set; } 
        public string Content { get; set; } 
        public string AuthorId { get; set; } 
        public virtual User Author { get; set; } 
        public string ReceiverId { get; set; } 
        public virtual User Receiver { get; set; } 
        public DateTime CreatedOn { get; set; } 
        public DateTime? ModifiedOn { get; set; } 
        public bool IsDeleted { get; set; } 
        public DateTime? DeletedOn { get; set; }
    }
}
