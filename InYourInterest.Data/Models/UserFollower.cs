using InYourInterest.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace InYourInterest.Data.Models
{
    public class UserFollower : IAuditInfo, IDeletableEntity
    {
        public string UserId { get; set; } 
        public User User { get; set; } 
        public string FollowerId { get; set; } 
        public User Follower { get; set; } 
        public DateTime CreatedOn { get; set; } 
        public DateTime? ModifiedOn { get; set; } 
        public bool IsDeleted { get; set; } 
        public DateTime? DeletedOn { get; set; }
    }
}
