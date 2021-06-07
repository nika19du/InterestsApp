using InYourInterest.Data.Models;
using InYourInterest.InputModels.Replies;
using InYourInterest.Services.Replies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InYourInterest.Controllers
{
    public class RepliesController : Controller
    {
        private readonly IRepliesService repliesService;
        private readonly UserManager<User> userManager;

        public RepliesController(IRepliesService repliesService, UserManager<User> userManager)
        {
            this.repliesService = repliesService;
            this.userManager = userManager;
        }
         
        public async Task<IActionResult> Create(string par,string content,string parentId,string postId)
        { 
            var userId = userManager.GetUserId(this.User);
            if (par != null && parentId == null)
                parentId = par;
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Details", "Posts", new { id =postId});
            } 
            await this.repliesService.CreateAsync(content, parentId, postId, userId);
            return this.RedirectToAction("Details", "Posts", new { id =postId }); 
        }
    }
}
