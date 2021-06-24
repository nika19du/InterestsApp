using InYourInterest.Data;
using InYourInterest.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InYourInterest.Services.Reactions
{
    public class PostReactionsService : IPostReactionsService
    {
        private readonly Context db;
        public PostReactionsService(Context context)
        {
            this.db = context;
        }
        public async Task<int> GetTotalCountAsync()
            => await this.db.PostReactions.Where(x => !x.Post.IsDeleted).CountAsync();

        public async Task<ReactionsCountServiceModel> ReactAsync(ReactionType reactionType, string postId, string authorId)
        {
            var postReaction = await this.db.PostReactions.FirstOrDefaultAsync(x => x.PostId == postId && x.AuthorId == authorId);

            if (postReaction==null)
            {
                postReaction = new Data.Models.PostReaction
                {
                    Id = Guid.NewGuid().ToString(), 
                    ReactionType = reactionType,
                    PostId = postId,
                    AuthorId = authorId,
                    CreatedOn = DateTime.UtcNow
                };
                await this.db.PostReactions.AddAsync(postReaction);
            }
            else
            {
                postReaction.ModifiedOn = DateTime.UtcNow;
                postReaction.ReactionType = postReaction.ReactionType == reactionType ? ReactionType.Neutral : reactionType;
            }

            await this.db.SaveChangesAsync();
            return await this.GetCountByPostIdAsync(postId);
        }

        private async Task<ReactionsCountServiceModel> GetCountByPostIdAsync(string postId)
        {
            return new ReactionsCountServiceModel
            {
                Likes = await this.GetCountByTypeAndPostIdAsync(ReactionType.Like, postId),
                Loves=await this.GetCountByTypeAndPostIdAsync(ReactionType.Love,postId),
                HahaCount = await this.GetCountByTypeAndPostIdAsync(ReactionType.Haha, postId),
                WowCount = await this.GetCountByTypeAndPostIdAsync(ReactionType.Wow, postId),
                SadCount = await this.GetCountByTypeAndPostIdAsync(ReactionType.Sad, postId),
                AngryCount = await this.GetCountByTypeAndPostIdAsync(ReactionType.Angry, postId)
            };
        }

        private async Task<int> GetCountByTypeAndPostIdAsync(ReactionType reactionType, string postId)
        {
            return await this.db.PostReactions.Where(x => !x.Post.IsDeleted && x.PostId == postId)
                .CountAsync(x => x.ReactionType == reactionType);
        }
    }
}
