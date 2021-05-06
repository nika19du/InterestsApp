using InYourInterest.Data.Models;
using InYourInterest.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InYourInterest.Services.Posts
{
    public interface IPostsService
    {
        Task<string> CreateAsync(string title, PostType type, string description, string authorId, string categoryId, IEnumerable<string> tagIds,string groupId);
        Task<TModel> GetByIdAsync<TModel>(string id);
        Task EditAsync(string id, string title, string description, string categoryId, IEnumerable<string> tagIds);

        Task DeleteAsync(string id);

        Task ViewAsync(string id);

        Task<bool> PinAsync(string id);

        Task<bool> IsExistingAsync(string id);

        Task<string> GetAuthorIdByIdAsync(string id);

        Task<string> GetLatestActivityByIdAsync(string id); 
        Task<Post> GetByIdAsync(string id);
        Task<IEnumerable<TModel>> GetAllAsync<TModel>(string search = null, int skip = 0, int? take = null);

        Task<IEnumerable<TModel>> GetAllByTagIdAsync<TModel>(string tagId, string search = null);

        Task<IEnumerable<TModel>> GetAllByCategoryIdAsync<TModel>(string categoryId, string search = null);

        Task<IEnumerable<TModel>> GetAllByUserIdAsync<TModel>(string userId);

    }
}
