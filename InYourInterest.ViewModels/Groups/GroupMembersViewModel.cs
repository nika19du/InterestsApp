using InYourInterest.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InYourInterest.ViewModels.Groups
{
    public class GroupMembersViewModel
    {
        public string GroupId { get; set; } 
        public DateTime DateJoined { get; set; } 
        public DateTime DateLeft { get; set; } 
        public string GroupName { get; set; }
        public string GroupCategory { get; set; }

        public string UserName { get; set; }

        public string SmallImage { get; set; }
    }
}
