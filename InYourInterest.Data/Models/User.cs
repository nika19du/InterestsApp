using InYourInterest.Data.Common;
using InYourInterest.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InYourInterest.Data.Models
{
    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public GenderType Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProfilePicture { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();//list ot napravenite ot nego eventy
        public List<EventUser> EventUser { get; set; }=new List<EventUser>();
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Biography { get; set; }
        public string Location { get; set; }
        public ICollection<Message> SentMessages { get; set; } = new HashSet<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new HashSet<Message>();
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        public ICollection<Reply> Replies { get; set; } = new HashSet<Reply>();
        public ICollection<PostReaction> PostReactions { get; set; } = new HashSet<PostReaction>();
        public ICollection<ReplyReaction> ReplyReactions { get; set; } = new HashSet<ReplyReaction>();
        public ICollection<UserFollower> Followers { get; set; } = new HashSet<UserFollower>();
    }
}
