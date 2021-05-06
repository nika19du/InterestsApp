using InYourInterest.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InYourInterest.Services.Categories
{
    public interface ICategoriesService
    {
        Task CreateAsync(string name);

        Task EditAsync(string id, string name);

        Task DeleteAsync(string id);

        Task<bool> IsExistingAsync(string name);

        Task<TModel> GetByIdAsync<TModel>(string id);

        Task<IEnumerable<TModel>> GetAllAsync<TModel>(string search = null);
         
    }
}
