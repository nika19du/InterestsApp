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
    [Route("api/post-reactions")]
    public class PostReactionsController : ApiController
    {
        private readonly IPostReactionsService postReactionsService;

        public PostReactionsController(IPostReactionsService postReactionsService)
        {
            this.postReactionsService = postReactionsService;
        }

        [HttpPost("like/{postId}")]
        public async Task<ActionResult<ReactionsCountServiceModel>> Like(string postId)
        {
            return await postReactionsService.ReactAsync(ReactionType.Like, postId, User.GetId());
        }
        [HttpPost("love/{postId}")]
        public async Task<ActionResult<ReactionsCountServiceModel>> Love(string postId)
        {
            return await this.postReactionsService.ReactAsync(ReactionType.Love, postId, this.User.GetId());
        }
        [HttpPost("haha/{postId}")]
        public async Task<ActionResult<ReactionsCountServiceModel>> Haha(string postId)
        {
            return await this.postReactionsService.ReactAsync(ReactionType.Haha, postId, this.User.GetId());
        }
        [HttpPost("wow/{postId}")]
        public async Task<ActionResult<ReactionsCountServiceModel>> Wow(string postId)
        {
            return await this.postReactionsService.ReactAsync(ReactionType.Wow, postId, this.User.GetId());
        }
        [HttpPost("sad/{postId}")]
        public async Task<ActionResult<ReactionsCountServiceModel>> Sad(string postId)
        {
            return await this.postReactionsService.ReactAsync(ReactionType.Sad, postId, this.User.GetId());
        }
        [HttpPost("angry/{postId}")]
        public async Task<ActionResult<ReactionsCountServiceModel>> Angry(string postId)
        {
            return await this.postReactionsService.ReactAsync(ReactionType.Angry, postId, this.User.GetId());
        }
    }
}
