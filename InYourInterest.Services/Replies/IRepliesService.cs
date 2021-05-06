using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InYourInterest.Services.Replies
{
    public interface IRepliesService
    {
        Task CreateAsync(string description, string? parentId, string postId, string authorId);

        Task EditAsync(string id, string description);

        Task DeleteAsync(string id);

        Task MakeBestAnswerAsync(string id);

        Task<string> GetAuthorIdByIdAsync(string id);

        Task<TModel> GetByIdAsync<TModel>(string id);

        Task<IEnumerable<TModel>> GetAllByUserIdAsync<TModel>(string userId);

        Task<IEnumerable<TModel>> GetAllByPostIdAsync<TModel>(string postId, string sort = null);
    }
}
