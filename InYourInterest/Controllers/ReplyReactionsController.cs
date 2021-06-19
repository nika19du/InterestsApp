using InYourInterest.Data;
using InYourInterest.Data.Models;
using InYourInterest.Data.Models.Enums;
using InYourInterest.Infrastructure.Extensions;
using InYourInterest.Services.Reactions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace InYourInterest.Controllers
{
   //[Route("api/ReplyReactions")]  
    public class ReplyReactionsController:Controller
    {
        private readonly IReplyReactionsService replyReactionsService;
        private UserManager<User> userManager;
        private readonly Context context;
        public ReplyReactionsController(IReplyReactionsService replyReactionsService, UserManager<User> userManager,Context context)
        {
            this.replyReactionsService = replyReactionsService;
            this.userManager = userManager;
            this.context = context;
        }

       // [HttpPost("like/{replyId}")]  <ReactionsCountServiceModel>
       [HttpGet]
        public async Task<ActionResult<ReactionsCountServiceModel>> Like(string id)
        {
            var user = await userManager.GetUserAsync(User);
            return await this.replyReactionsService.ReactAsync(ReactionType.Like, id, user.Id); 
        }
        [HttpGet]
        public async Task<ActionResult<ReactionsCountServiceModel>> Love(string id)
        {
            var user = await userManager.GetUserAsync(User); 
            return await this.replyReactionsService.ReactAsync(ReactionType.Love, id,user.Id);
        }
        [HttpGet]
        public async Task<ActionResult<ReactionsCountServiceModel>> Haha(string id)
        {
            var user = await userManager.GetUserAsync(User); 
            return await this.replyReactionsService.ReactAsync(ReactionType.Haha, id,user.Id);
        }
        [HttpGet]
        public async Task<ActionResult<ReactionsCountServiceModel>> Wow(string id)
        {
            var user = await userManager.GetUserAsync(User); 
            return await this.replyReactionsService.ReactAsync(ReactionType.Wow, id, user.Id);
        }
        [HttpGet]
        public async Task<ActionResult<ReactionsCountServiceModel>> Sad(string id)
        {
            var user = await userManager.GetUserAsync(User); 
            return await replyReactionsService.ReactAsync(ReactionType.Sad, id,user.Id);
        }
        [HttpGet]
        public async Task<ActionResult<ReactionsCountServiceModel>> Angry(string id)
        {
            var user = await userManager.GetUserAsync(User);
            return await this.replyReactionsService.ReactAsync(ReactionType.Angry, id,user.Id);
        }
    }
}
