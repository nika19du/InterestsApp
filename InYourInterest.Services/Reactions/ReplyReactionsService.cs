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
    public class ReplyReactionsService : IReplyReactionsService
    {
        private readonly Context context;

        public ReplyReactionsService(Context context)
        {
            this.context = context;
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await this.context.ReplyReactions.Where(x => !x.Reply.IsDeleted).CountAsync();
        }

        public async Task<ReactionsCountServiceModel> ReactAsync(ReactionType reactionType, string replyId, string authorId)
        {
            var replyReaction = await context.ReplyReactions.FirstOrDefaultAsync(x => x.ReplyId == replyId && x.AuthorId == authorId);

            if (replyReaction==null)
            {
                replyReaction = new Data.Models.ReplyReaction
                {
                    ReactionType=reactionType,
                    ReplyId = replyId,
                    AuthorId=authorId,
                    CreatedOn=DateTime.UtcNow
                };
                await this.context.ReplyReactions.AddAsync(replyReaction);
            }
            else
            {
                replyReaction.ModifiedOn = DateTime.UtcNow;
                replyReaction.ReactionType = replyReaction.ReactionType == reactionType ? ReactionType.Neutral : reactionType;
            }
            await this.context.SaveChangesAsync();
            return await this.GetCountByReplyIdAsync(replyId);
        }

        private async Task<ReactionsCountServiceModel> GetCountByReplyIdAsync(string replyId)
        {
            return new ReactionsCountServiceModel
            {
                Likes = await this.GetCountByTypeAndReplyIdAsync(ReactionType.Like, replyId),
                Loves = await this.GetCountByTypeAndReplyIdAsync(ReactionType.Like, replyId),
                HahaCount = await this.GetCountByTypeAndReplyIdAsync(ReactionType.Like, replyId),
                WowCount = await this.GetCountByTypeAndReplyIdAsync(ReactionType.Like, replyId),
                SadCount = await this.GetCountByTypeAndReplyIdAsync(ReactionType.Like, replyId),
                AngryCount = await this.GetCountByTypeAndReplyIdAsync(ReactionType.Like, replyId)
            };
        }

        private async Task<int> GetCountByTypeAndReplyIdAsync(ReactionType reactionType, string replyId)
        {
            return await this.context.ReplyReactions.Where(x => !x.Reply.IsDeleted && x.ReplyId == replyId).CountAsync(x => x.ReactionType == reactionType);
        }
    }
}
