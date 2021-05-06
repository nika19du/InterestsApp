using AutoMapper;
using AutoMapper.QueryableExtensions;
using InYourInterest.Data;
using InYourInterest.Data.Models;
using InYourInterest.Data.Models.Enums;
using InYourInterest.Services.Groups;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InYourInterest.Services.Posts
{
    public class PostsService: IPostsService
    {
        private readonly Context context; 
        private readonly IGroupsService groupsService;
        private readonly IMapper mapper;
        public PostsService(Context context, IGroupsService groupsService, IMapper mapper)
        {
            this.context = context; 
            this.groupsService = groupsService;
            this.mapper = mapper;
        }

        public async Task<string> CreateAsync(string title, PostType type, string description, string authorId, string categoryId, IEnumerable<string> tagIds,string groupId)
        {
            Post post = new Post
            {
                Id=Guid.NewGuid().ToString(),
                Title=title,
                Type=type,
                Description=description,
                AuthorId=authorId,
                CreatedOn=DateTime.UtcNow,
                CategoryId=categoryId,
                GroupId=groupId
            }; 
            await this.context.Posts.AddAsync(post);
            var gr = await groupsService.GetByIdAsync<Group>(groupId);
            gr.Posts.Add(post);
            await this.context.SaveChangesAsync();
            await this.AddTagsAsync(post.Id, tagIds);

            return post.Id;
        }
        private async Task AddTagsAsync(string id, IEnumerable<string> tagIds)
        {
            var post = await this.GetByIdAsync(id);

            foreach (var tagId in tagIds)
            {
                post.Tags.Add(new PostTag
                {
                    PostId = id,
                    TagId = tagId
                });
            } 
            await this.context.SaveChangesAsync();
        }
        public async Task<Post> GetByIdAsync(string id)
           => await this.context.Posts
               .AsNoTracking()
               .Where(p => p.Id == id && !p.IsDeleted) 
               .FirstOrDefaultAsync();
        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(string id, string title, string description, string categoryId, IEnumerable<string> tagIds)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> GetAllAsync<TModel>(string search = null, int skip = 0, int? take = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> GetAllByCategoryIdAsync<TModel>(string categoryId, string search = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> GetAllByTagIdAsync<TModel>(string tagId, string search = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> GetAllByUserIdAsync<TModel>(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAuthorIdByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TModel> GetByIdAsync<TModel>(string id)
        {
            return await context.Posts.AsNoTracking()
                .Where(x => x.Id == id)
                 .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public Task<string> GetLatestActivityByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistingAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PinAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task ViewAsync(string id)
        {
            var post = await this.GetByIdAsync(id);  
            post.Views++; 
            await this.context.SaveChangesAsync();
        }
         
    }
}
