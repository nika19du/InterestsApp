using AutoMapper;
using InYourInterest.Data;
using InYourInterest.Data.Models;
using InYourInterest.InputModels.Groups;
using InYourInterest.Services.Cloudinaries;
using InYourInterest.Services.Groups;
using InYourInterest.Services.InYourInterest.Services.Models;
using InYourInterest.ViewModels.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;

namespace InYourInterest.Controllers
{
    public class GroupController : Controller
    {
        private IGroupsService groupsService;
        private readonly Context context;
        private UserManager<User> userManager;
        private ICloudinaryService cloudinaryService;
        private readonly IMapper mapper;
        public GroupController(IGroupsService eventsService, Context context, UserManager<User> userManager, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            this.groupsService = eventsService;
            this.context = context;
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(context.Categories, "Id", "Name");
            return View(new GroupsCreateInputModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupsCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                ViewData["Category"] = new SelectList(context.Categories, "Id", "Name", model.CategoriesId);
                return this.View(model);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId 
            string pictureUrl = await this.cloudinaryService.UploadPictureAsync(model.Image, model.Name);
            var id = this.groupsService.CreateAsync(model.Name, model.Description, pictureUrl, userId, model.CategoriesId);
            return this.RedirectToAction("MyGroup", "Group", new { id = id, area = "" });

        }
        //smth like details method 

        public async Task<IActionResult> Index(string? search, int? i)
        {
            var groupInfoViewModels = await this.groupsService.GetAllAsync<GroupInfoViewModel>(search);
            GroupAllViewModel vmodel = new GroupAllViewModel() { Groups = groupInfoViewModels, Search = search };
            IPagedList<GroupInfoViewModel> grList = vmodel.Groups.ToList().ToPagedList(i ?? 1, 3);
            vmodel.Groups = grList;
            return View(vmodel);
        }

        public async Task<IActionResult> MyGroup(string id)
        {
            var group = await this.groupsService.GetByIdAsync<Group>(id);
            var user= await userManager.GetUserAsync(User); 
            var check = CheckIfAlreadyAttend(user.Id,group.Id);
            var countOfMembers = CountOfMembers(user.Id,group.Id);
            var mappedItem = mapper.Map<GroupInfoViewModel>(group);
            ViewData["AlreadyMember"] = check;
            ViewData["Count"] = countOfMembers;
            return View(mappedItem);
        }
        //Like All groups
        public async Task<ActionResult> MyCommunities()
        {
            var user = await userManager.GetUserAsync(User);
            //  var ev = context.Events.(user.Id); 
            List<Group> viewModels = new List<Group>();

            foreach (var evn in context.Groups)
            {
                if (evn.UserId == user.Id)
                {
                    viewModels.Add(evn);
                }
            }
            if (viewModels.Count == 0)
                return PartialView("~/Views/Group/_NoGroupsCreate.cshtml");
            return View(viewModels);
        }

        public IActionResult Update(string id)
        { //edit group info
            var g = context.Groups.FirstOrDefault(x => x.Id == id);
            GroupsUpdateInputModel ecm = new GroupsUpdateInputModel
            {
                Id=g.Id,
                CategoriesId = g.CategoriesId,
                Description = g.Description,
                Name = g.Name,
                Location = g.Location
            };
            ViewData["Category"] = new SelectList(context.Categories, "Id", "Name");
            return this.View(ecm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(GroupsUpdateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                ViewData["Category"] = new SelectList(context.Groups, "Id", "Name", model.CategoriesId);
                return this.View(model);
            }
            var id = context.Groups.FirstOrDefault(x => x.Name.ToLower() == model.Name.ToLower());

            string picture = await this.cloudinaryService.UploadPictureAsync(model.Logo, model.Name);
            string pictureUrl = await this.cloudinaryService.UploadPictureAsync(model.Image, model.Name);
            GroupServiceModel ev = new GroupServiceModel
            {
                Name = model.Name,
                Description = model.Description,
                LargeImage = pictureUrl,
                Image = picture,
                Location = model.Location,
                CategoriesId = model.CategoriesId
            };
            groupsService.EditAsync(ev, id.Id);
            return this.RedirectToAction("MyGroup", "Group", new { id = id.Id, area = "" });

        }

        [Authorize]
        public async Task<IActionResult> Attend(string id)
        {
            var group = await this.groupsService.GetByIdAsync<Group>(id);
            var user = await userManager.GetUserAsync(User);

            var check = CheckIfAlreadyAttend(user.Id, group.Id);
            if (check)
                return RedirectToAction("YourGroups","Group");
            GroupMembers groupMembers = new GroupMembers
            {
                Id=Guid.NewGuid().ToString(),
                GroupId=group.Id,
                GroupName=group.Name,
                UserId=user.Id,
                UserName=user.UserName,
                DateJoined=DateTime.Now
            };   
            groupMembers.Group=group;
            groupMembers.User = user;
            this.context.GroupMembers.Attach(groupMembers); 
            this.context.GroupMembers.Add(groupMembers);
            await context.SaveChangesAsync();

            return this.RedirectToAction("MyGroup", "Group", new { id = id, area = "" }); 
        }

        private bool CheckIfAlreadyAttend(string userId, string groupId)
        {
            var groupMemb = this.context.GroupMembers.ToList(); 
            foreach (var item in groupMemb)
            {
                if (item.GroupId==groupId && item.UserId==userId)
                {
                    return true;
                }
            }
            return false;
        }
        private int CountOfMembers(string userId,string groupId)
        {
            var groupMemb = this.context.GroupMembers.ToList();
            int counter = 0;
            foreach (var item in groupMemb)
            {
                if (item.GroupId == groupId)
                {
                    counter++; 
                }
            }
            return counter;
        }
        public async Task<IActionResult> YourGroups()
        {
            var user = await userManager.GetUserAsync(User);
            var model = this.groupsService.GetUserGroups(user);
            if (model.Count>0)
            {  //TODO: 
                foreach (var item in model)
                {
                    var gr =await this.groupsService.GetByIdAsync<Group>(item.GroupId);
                    item.Group = gr;
                }
               
                return this.View(model);
            }
  
            return NotFound();
        }

        public IActionResult Delete(string id)
        {
            var isDeleted = groupsService.Delete(id); 
            if (isDeleted)
            { 
                return this.RedirectToAction(nameof(MyCommunities));
            }
            return NotFound();
        }
    }
}
