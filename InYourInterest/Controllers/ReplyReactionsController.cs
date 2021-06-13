using InYourInterest.Data.Models.Enums;
using InYourInterest.Infrastructure.Extensions;
using InYourInterest.Services.Reactions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace InYourInterest.Controllers
{
    [Route("api/reply-reactions")]
    public class ReplyReactionsController:ApiController
    {
        private readonly IReplyReactionsService replyReactionsService;

        public ReplyReactionsController(IReplyReactionsService replyReactionsService)
        {
            this.replyReactionsService = replyReactionsService;
        }

        [HttpPost("like/{replyId}")]
        public async Task<ActionResult<ReactionsCountServiceModel>> Like(string replyId)
        {
            return await this.replyReactionsService.ReactAsync(ReactionType.Like, replyId, this.User.GetId());
        }
        [HttpPost("love/{replyId}")]
        public async Task<ActionResult<ReactionsCountServiceModel>> Love(string replyId)
        {
            return await this.replyReactionsService.ReactAsync(ReactionType.Love, replyId,this.User.GetId());
        }
        [HttpPost]
        public async Task<ActionResult<ReactionsCountServiceModel>> Haha(string replyId)
        {
            return await this.replyReactionsService.ReactAsync(ReactionType.Haha, replyId, this.User.GetId());
        }
        [HttpPost("wow/{replyId}")]
        public async Task<ActionResult<ReactionsCountServiceModel>> Wow(string replyId)
        {
            return await this.replyReactionsService.ReactAsync(ReactionType.Wow, replyId, this.User.GetId());
        }
        [HttpPost("sad/{replyId}")]
        public async Task<ActionResult<ReactionsCountServiceModel>> Sad(string replyId)
        {
            return await replyReactionsService.ReactAsync(ReactionType.Sad, replyId,this.User.GetId());
        }
        [HttpPost("angry/{replyId}")]
        public async Task<ActionResult<ReactionsCountServiceModel>> Angry(string replyId)
        {
            return await this.replyReactionsService.ReactAsync(ReactionType.Angry, replyId, this.User.GetId());
        }
    }
}
