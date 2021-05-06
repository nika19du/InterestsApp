using AutoMapper;
using AutoMapper.QueryableExtensions;
using InYourInterest.Data;
using InYourInterest.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InYourInterest.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly Context db;
        private readonly IMapper mapper;

        public UsersService(Context db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task DeleteAsync(string id)
        {
            var user = await this.GetByIdAsync(id);

            user.Email = null;
            user.NormalizedEmail = null;
            user.FirstName = null;
            user.LastName = null;
            user.IsDeleted = true;
            user.UserName = null;
            user.DeletedOn = DateTime.Now;

            await this.db.SaveChangesAsync();
        }
        public async Task<TModel> GetByIdAsync<TModel>(string id)
        {
            return await this.db.Users.AsNoTracking()
                .Where(x => x.Id == id && !x.IsDeleted)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
        private async Task<User> GetByIdAsync(string id)
        {
            return await this.db.Users.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
        public async Task<IEnumerable<TModel>> GetAllAsync<TModel>()
        {
            return await this.db.Users.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> IsDeletedAsync(string username)
        {
            return await this.db.Users.AnyAsync(x => x.UserName == username && x.IsDeleted);
        }

        public async Task<bool> IsUsernameUsedAsync(string username)
        {
            return await this.db.Users.AnyAsync(x => x.UserName == username && !x.IsDeleted);
        }

        //public Task SavedEvents(string userId)
        //{

        //}
    }
}
