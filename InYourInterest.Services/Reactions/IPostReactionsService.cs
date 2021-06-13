using InYourInterest.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InYourInterest.Services.Reactions
{
    public interface IPostReactionsService
    {
        Task<ReactionsCountServiceModel> ReactAsync(ReactionType reactionType, string 
postId,string authorId);

        Task<int> GetTotalCountAsync();
    }
}
