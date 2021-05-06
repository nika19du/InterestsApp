using InYourInterest.Data;
using System;
using System.Collections.Generic;
using System.Text;
using InYourInterest.Data.Models;

namespace InYourInterest.ViewModels.Groups
{
    public class GroupAllViewModel
    { 
        public string Search
        {
            get;
            set;
        }
        public IEnumerable<GroupInfoViewModel> Groups;//= new List<Group>();
    }
}
