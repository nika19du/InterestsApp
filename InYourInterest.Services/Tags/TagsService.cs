using AutoMapper;
using AutoMapper.QueryableExtensions;
using InYourInterest.Data;
using InYourInterest.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InYourInterest.Services.Tags
{
    public class TagsService : ITagsService
    {
        private readonly Context context;
        private readonly IMapper mapper;
        public TagsService(Context context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> AreExistingAsync(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                var isExisting = await this.context.Tags.AnyAsync(t => t.Id == id && !t.IsDeleted);
                if (!isExisting)
                {
                    return false;
                }
            } 
            return true;
        }

        public async Task CreateAsync(string name)
        {
            var tag = new Tag
            {
                Name = name,
                CreatedOn = DateTime.UtcNow
            };

            await this.context.Tags.AddAsync(tag);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var tag = await this.context.Tags.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            tag.IsDeleted = true;
            tag.DeletedOn = DateTime.UtcNow;
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel>(string search = null, int skip = 0, int? take = null)
        {
            var queryable = this.context.Tags.AsNoTracking().OrderByDescending(x => x.Posts.Count(x => !x.Post.IsDeleted))
                .Where(x => !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                queryable = queryable.Where(x => x.Name.Contains(search));
            }
            if (take.HasValue)
                queryable = queryable.Skip(skip).Take(take.Value);

            var tags = await queryable.ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();
            return tags;
        }

        public async Task<IEnumerable<TModel>> GetAllByPostIdAsync<TModel>(string postId)
        {
            return await context.PostsTags.AsNoTracking().Where(x => x.PostId == postId).Select(x => x.Tag).Where(x => !x.IsDeleted).ProjectTo<TModel>(this.mapper.ConfigurationProvider).ToListAsync();
        } 
        public async Task<TModel> GetByIdAsync<TModel>(string id)
        {
            var tag =await context.Tags.AsNoTracking().Where(x => x.Id == id).ProjectTo<TModel>(this.mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return tag;
        }

        public async Task<int> GetCountAsync(string searchFilter = null)
        {
            var queryable = this.context.Tags
                 .Where(p => !p.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchFilter))
            {
                queryable = queryable
                    .Where(t => t.Name.Contains(searchFilter));
            } 
            var count = await queryable.CountAsync(); 
            return count;
        } 
        public async Task<bool> IsExistingByIdAsync(string id)
              => await this.context.Tags.AnyAsync(t => t.Id == id && !t.IsDeleted);

        public async Task<bool> IsExistingByNameAsync(string name)
              => await this.context.Tags.AnyAsync(t => t.Name == name && !t.IsDeleted); 
    }
}
