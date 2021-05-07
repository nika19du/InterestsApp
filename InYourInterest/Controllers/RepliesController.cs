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

        [HttpPost]
        public async Task<IActionResult> Create(RepliesCreateInputModel input)
        {
             
            var userId = userManager.GetUserId(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Details", "Posts", new { id = input.PostId });
            }

            await this.repliesService.CreateAsync(input.Description, input.ParentId, input.PostId, userId);

            return this.RedirectToAction("Details", "Posts", new { id = input.PostId });

        }
    }
}
