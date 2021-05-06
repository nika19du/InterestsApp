using System;
using System.ComponentModel.DataAnnotations;

namespace InYourInterest.Data.Models
{
    public class GroupMembers
    {
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public Group Group { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public User User { get; set; }
        [Required]
        public DateTime DateJoined { get; set; }
        [Required]
        public DateTime DateLeft { get; set; }
    }
}
