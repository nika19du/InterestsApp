using InYourInterest.Data.Models;
using System.Collections.Generic;

namespace InYourInterest.ViewModels.User
{
    public class UsersDetailsViewModel
    {
        public string Id { get; set; }

        public string UserName
        {
            get;
            set;
        }
        public string ProfilePicture
        { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
