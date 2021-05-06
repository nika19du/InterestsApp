using AutoMapper;
using InYourInterest.Data;
using InYourInterest.Data.Models;
using InYourInterest.InputModels.Events;
using InYourInterest.Services.Cloudinaries;
using InYourInterest.Services.Events;
using InYourInterest.Services.InYourInterest.Services.Models;
using InYourInterest.ViewModels.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using X.PagedList;

namespace InYourInterest.Controllers
{
    public class EventController : Controller
    {
        private IEventsService eventsService;
        private readonly Context context;
        private UserManager<User> userManager;
        private ICloudinaryService cloudinaryService;
        private readonly IMapper mapper;

        public EventController(IEventsService eventsService, Context context, UserManager<User> userManager, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            this.eventsService = eventsService;
            this.context = context;
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
            this.mapper = mapper;
        }

        public IActionResult Create()
        { 
            ViewData["Category"] = new SelectList(context.Categories, "Id", "Name");
            //new EventsCreateInputModel()
            if (TempData["Error"] == null)
                TempData["Error"] = "";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EventsCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                ViewData["Category"] = new SelectList(context.Categories, "Id", "Name", model.CategoriesId);
                return this.View(model);
            }
            if (model.IsItOnline==true && model.Location!=null) 
            { 
                // ModelState.AddModelError("Error", "* Can't be online & have location!");
                TempData["Error"] = "* Can't be online & have location!";
                ViewData["Category"] = new SelectList(context.Categories, "Id", "Name", model.CategoriesId);
                return View("Create");
            }
            var user = await userManager.GetUserAsync(User);
            string pictureUrl = await this.cloudinaryService.UploadPictureAsync(model.Image, model.Name);
            await this.eventsService.CreateAsync(model.Name, model.Description, model.Location, pictureUrl, user.Id, model.CategoriesId,model.Started,model.Ended,model.IsItOnline);
            return this.RedirectToAction(nameof(All));
        }
        
        public async Task<IActionResult> All( int? i,string? id= null)
        {
            //var queryable = new List<Event>(); 
            //foreach (var e in context.Events)
            //{
            //  //  if(e.CategoriesId==id)
            //         queryable.Add(e); 
            //}
            //if (queryable.Count > 0)
            //{
            //    var mappedItem = mapper.Map<List<EventsInfoViewModel>>(queryable); 
            //    var vm = new EventsAllViewModel { Search =null, Events = mappedItem };
            //    return this.View(vm);
            //}
            IEnumerable<EventsInfoViewModel> events = await this.eventsService.GetAllAsync<EventsInfoViewModel>(id); 
            var viewModel = new EventsAllViewModel
            { 
                Events = events
            };
            IPagedList<EventsInfoViewModel> grList = viewModel.Events.ToList().ToPagedList(i ?? 1, 8);
            viewModel.Events = grList;
            return View(viewModel);
        }
         
        public IActionResult Edit(string id)
        {
            var e = context.Events.FirstOrDefault(x => x.Id == id);
            EventsCreateInputModel ecm = new EventsCreateInputModel
            {
                CategoriesId = e.CategoriesId,
                Description = e.Description,
                Name = e.Name,
                IsItOnline=(bool)e.IsItOnline
            };
            ViewData["Category"] = new SelectList(context.Categories, "Id", "Name");
            return this.View(ecm);//new EventsCreateInputModel()
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EventsCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                ViewData["Category"] = new SelectList(context.Events, "Id", "Name", model.CategoriesId);
                return this.View(model);
            }
            var id = context.Events.FirstOrDefault(x => x.Name.ToLower() == model.Name.ToLower());

            if (model.Image != null && id != null)
            {
                string pictureUrl = await this.cloudinaryService.UploadPictureAsync(model.Image, model.Name);

                EventServiceModel ev = new EventServiceModel
                {
                    Name = model.Name,
                    Description = model.Description,
                    Image = pictureUrl,
                    Location = model.Location,
                    CategoriesId = model.CategoriesId
                };

                eventsService.EditAsync(ev, id.Id);
                return this.RedirectToAction("All", "Event");
            }
            else if (model.Image == null && id != null)
            {
                EventServiceModel ev = new EventServiceModel
                {
                    Name = model.Name,
                    Description = model.Description,
                    Location = model.Location,
                    CategoriesId = model.CategoriesId
                };

                eventsService.EditAsync(ev, id.Id);
                return this.RedirectToAction("All", "Event", new { id = id.Id, area = "" });
            }
            else return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            var e = await this.eventsService.GetByIdAsync<Event>(id);
            if (e == null)
            {
                return this.NotFound();
            }

            var viewModel = new EventsDetailsViewModel
            {
                Event = e
            };

            return View(viewModel);
        }
        [Authorize]
        public async Task<IActionResult> Attend(string id)
        {
            var e = await eventsService.GetByIdAsync<Event>(id);
            var user = await userManager.GetUserAsync(User);
            var check = CheckIfAlreadyAttend(e.Id, user.Id);
            if(!check)
            { 
                EventUser eventUser = new EventUser()
                {
                    EventId = id,
                    Event = e,
                    UserId = user.Id,
                    User = user
                };
                context.EventUsers.Attach(eventUser);
                this.context.EventUsers.Add(eventUser);

                user.EventUser.Add(eventUser);
                e.EventUser.Add(eventUser);
                await this.context.SaveChangesAsync();
            }
            return this.RedirectToAction("SavedEvents", "Event");
        }

        private bool CheckIfAlreadyAttend(string evnId, string userId)
        {
            foreach (var item in context.EventUsers)
            {
                if (item.EventId==evnId && item.UserId==userId)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IActionResult> Delete(string id)
        {
            var evn = context.Events.Where(x => x.Id == id).FirstOrDefault();
            var user = await userManager.GetUserAsync(User);

            if (evn != null)
            {
                var usrEvnt = user.Events.Contains(evn);
                if (usrEvnt) user.Events.Remove(evn); 
                foreach (var evnUsr in context.EventUsers)
                {
                    if (evnUsr.EventId == evn.Id)
                    {
                        context.EventUsers.Remove(evnUsr);
                    }
                }
                 
                context.Events.Remove(evn);
                await context.SaveChangesAsync();
                return this.RedirectToAction(nameof(All));
            }
            return NotFound();
        }

        public async Task<IActionResult> SavedEvents()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEvId = context.EventUsers.Where(x => x.UserId == userId);
            if (userEvId.Count() == 0)
            {
                return PartialView("~/Views/Event/_NoEventsSaved.cshtml");
            }
            List<EventsSavedViewModel> viewModels = new List<EventsSavedViewModel>();
            foreach (var eventUser in context.EventUsers)
            {
                if (eventUser.UserId == userId)
                {
                    var evn = await eventsService.GetByIdAsync<Event>(eventUser.EventId);
                    EventsSavedViewModel model = new EventsSavedViewModel
                    {
                        Id = evn.Id,
                        Name = evn.Name,
                        Location = evn.Location,
                        Description = evn.Description,
                        Image = evn.Image,
                        CategoriesId = evn.Category.Name
                    };
                    viewModels.Add(model);
                }
            }
            return View(viewModels);
        }

        public async Task<IActionResult> YourEvents()
        {
            var user = await userManager.GetUserAsync(User);
            //  var ev = context.Events.(user.Id); 
            List<Event> viewModels = new List<Event>();

            foreach (var evn in context.Events)
            {
                if (evn.UserId == user.Id)
                {
                    var content = WebUtility.HtmlDecode(Regex.Replace(evn.Description, @"<[^>]+>", string.Empty));
                    evn.Description = content;
                    viewModels.Add(evn);
                }
            }
            if (viewModels.Count == 0)
                return PartialView("~/Views/Event/_NoEventsCreate.cshtml");

            return View(viewModels);
        }
    }
}
