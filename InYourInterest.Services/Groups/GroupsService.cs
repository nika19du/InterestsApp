using AutoMapper;
using AutoMapper.QueryableExtensions;
using InYourInterest.Data;
using InYourInterest.Data.Models;
using InYourInterest.Services.Cloudinaries;
using InYourInterest.Services.InYourInterest.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace InYourInterest.Services.Groups
{
    public class GroupsService : IGroupsService
    {
        private readonly Context context;
        private readonly IMapper mapper;
        public GroupsService(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string CreateAsync(string name, string description, string smallImage, string userId, string categoriesId)
        {
            var categ = this.context.Categories.FirstOrDefault(x => x.Id == categoriesId);
            var g = new Group()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Description = description,
                SmallImage = smallImage,
                UserId = userId,//organizer
                CategoriesId = categoriesId,
                Category = categ
            };
            var u = context.Users.FirstOrDefault(x => x.Id == userId);
            g.UserId = u.Id;
            g.Host = u;
            context.Groups.Add(g);
            context.SaveChanges();
            return g.Id;
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel>(string? search)
        {
            var queryable = this.context.Groups.AsNoTracking();
            if (!String.IsNullOrWhiteSpace(search))
            {
                queryable = queryable.Where(x => x.Name.Contains(search));
            }
            var groups = await queryable.ProjectTo<TModel>(this.mapper.ConfigurationProvider).ToListAsync();

            return groups;
        }
        public async Task<TModel> GetByIdAsync<TModel>(string id)
        {
            return await context.Groups.AsNoTracking()
                .Where(x => x.Id == id)
                 .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
        public List<GroupMembers> GetUserGroups(User user)
        { 
            var userGroups = new List<GroupMembers>();
            var grMembers = context.GroupMembers.ToList();

            foreach (var item in grMembers)
            {
                if (item.UserId == user.Id)
                { 
                    userGroups.Add(item);
                }
            }  

            return userGroups;
        }
        public string EditAsync(GroupServiceModel evnt, string id)
        {
            var g = context.Groups.FirstOrDefault(x => x.Id == id);

            g.Name = evnt.Name;
            if (evnt.Image != null && evnt.LargeImage == null)
            {
                g.SmallImage = evnt.Image;
            }
            g.LargeImage = evnt.LargeImage;
            g.Location = evnt.Location;
            g.Description = evnt.Description;
            g.CategoriesId = evnt.CategoriesId;
            var categ = this.context.Categories.FirstOrDefault(x => x.Id == g.CategoriesId);
            g.Category = categ;
            context.Groups.Update(g);
            context.SaveChanges();

            return g.Id;
        }

        public bool Delete(string id)
        {
            var groups = context.Groups.Where(x => x.Id == id).FirstOrDefault();
            if (groups != null)
            {
                context.Groups.Remove(groups);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}
