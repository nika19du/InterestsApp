using InYourInterest.Data;
using InYourInterest.Models;
using InYourInterest.Services.Events;
using InYourInterest.ViewModels.Events;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace InYourInterest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly Context context;
       // private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(IEventsService eventsService, Context context)// , RoleManager<IdentityRole> roleManager)
        {
            this.eventsService = eventsService;
            this.context = context;
          //  this.roleManager = roleManager;
        }

        //private readonly Context context;
        //private readonly RoleManager<IdentityRole> roleManager;
        //public HomeController(Context context,
        //    RoleManager<IdentityRole> roleManager, IEventsService eventsService)
        //{
        //    this.context = context;
        //    this.roleManager = roleManager;
        //    this.eventsService = eventsService;
        //}
        //public async Task<IActionResult> Index(string search = null)
        //{
        //    IdentityRole userRole = new IdentityRole()
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Name = "User"
        //    };
        //    IdentityRole adminRole = new IdentityRole()
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Name = "Admin"
        //    };
        //    await this.roleManager.CreateAsync(userRole);
        //    await this.roleManager.CreateAsync(adminRole);

        //    IEnumerable<EventsInfoViewModel> events = await this.eventsService.GetAllAsync<EventsInfoViewModel>(search);
        //    var viewModel = new EventsAllViewModel
        //    {
        //        Search = search,
        //        Events = events
        //    };
        //    return View(viewModel);
        //}

        public async Task<IActionResult> Index(string search = null)
        {
            ViewBag.Category = this.context.Categories.ToList();
            IEnumerable<EventsInfoViewModel> events = await this.eventsService.GetAllAsync<EventsInfoViewModel>(search);
            var viewModel = new EventsAllViewModel
            {
                Search = search,
                Events = events
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
