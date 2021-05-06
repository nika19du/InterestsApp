using InYourInterest.Data.Models;
using InYourInterest.Services.InYourInterest.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InYourInterest.Services.Groups
{
    public interface IGroupsService
    {
        string CreateAsync(string name, string description, string smallImage, string userId, string categoriesId); 
        Task<IEnumerable<TModel>> GetAllAsync<TModel>(string? search);
        Task<TModel> GetByIdAsync<TModel>(string id);

        string EditAsync(GroupServiceModel evnt, string id);

        bool Delete(string id);

        List<GroupMembers> GetUserGroups(User user);
    }
}
