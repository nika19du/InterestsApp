using InYourInterest.Data;
using InYourInterest.Data.Models;
using InYourInterest.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InYourInterest.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly Context context;
        private UserManager<User> userManager;


        public UsersController(IUsersService usersService, Context context, UserManager<User> userManager)
        {
            this.usersService = usersService;
            this.context = context;
            this.userManager = userManager;
        }

        //public async Task SavedEvents()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId EventUsers eventUsers = new EventUsers();
        //    var eu = this.context.EventUsers.SelectMany(x=>x.UserId).Where(x => x.Equals (userId));

        //    // var evnt=context.EventUsers.SelectMany(x=>x.e)

        //    var user = await userManager.GetUserAsync(User);

        //    foreach (var eventUser in user.EventUser)
        //    {
        //        eventUser.
        //    }
        //}
    }
}
