using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InYourInterest.Services.Tags
{
    public interface ITagsService
    {
        Task CreateAsync(string name);

        Task DeleteAsync(string id);

        Task<bool> IsExistingByIdAsync(string id);

        Task<bool> IsExistingByNameAsync(string name);

        Task<bool> AreExistingAsync(IEnumerable<string> ids);

        Task<int> GetCountAsync(string searchFilter = null);

        Task<TModel> GetByIdAsync<TModel>(string id);

        Task<IEnumerable<TModel>> GetAllAsync<TModel>(string search = null, int skip = 0, int? take = null);

        Task<IEnumerable<TModel>> GetAllByPostIdAsync<TModel>(string postId);
    }
}
