using InYourInterest.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InYourInterest.Services.Reactions
{
    public interface IReplyReactionsService
    {
        Task<ReactionsCountServiceModel> ReactAsync(ReactionType reactionType, string replyId,string authorId);

        Task<int> GetTotalCountAsync();
    }
}
