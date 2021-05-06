using System.Collections.Generic;
using System.Threading.Tasks;

namespace InYourInterest.Services.Users
{
    public interface IUsersService
    {
        Task<bool> IsUsernameUsedAsync(string username);
        Task DeleteAsync(string id);
        Task<TModel> GetByIdAsync<TModel>(string id);
        Task<bool> IsDeletedAsync(string username);
        Task<IEnumerable<TModel>> GetAllAsync<TModel>();
    }
}
