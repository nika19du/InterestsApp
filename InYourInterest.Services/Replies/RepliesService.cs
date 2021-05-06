using AutoMapper;
using AutoMapper.QueryableExtensions;
using InYourInterest.Data;
using InYourInterest.Data.Models;
using InYourInterest.Services.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InYourInterest.Services.Replies
{
    public class RepliesService : IRepliesService
    {
        private readonly Context context;
        private readonly IMapper mapper;
        //private readonly IUsersService usersService;
        public RepliesService(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task CreateAsync(string description, string parentId, string postId, string authorId)
        {
            Reply reply = new Reply
            {
                Id = Guid.NewGuid().ToString(),
                Description = description,
                CreatedOn = DateTime.UtcNow,
                ParentId = parentId,
                PostId = postId,
                AuthorId = authorId
            };
            this.context.Replies.Add(reply);
            await this.context.SaveChangesAsync();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(string id, string description)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TModel>> GetAllByPostIdAsync<TModel>(string postId, string sort = null)
        {
            var queryable = this.context.Replies.AsNoTracking()
          .Where(x => x.PostId == postId && !x.IsDeleted).OrderByDescending(x => x.IsBestAnswer);

            queryable = sort switch
            {
                "recent" => queryable.ThenByDescending(x => x.CreatedOn),
                "most reacted" => queryable.ThenByDescending(x => x.Reactions.Count),
                "longest" => queryable.ThenByDescending(x => x.Description.Length),
                "shortest" => queryable.ThenBy(x => x.Description.Length),
                _ => queryable.ThenByDescending(x => x.CreatedOn)
            };
            var replies = await queryable.ProjectTo<TModel>(this.mapper.ConfigurationProvider).ToListAsync();

            return replies;
        }

        public Task<IEnumerable<TModel>> GetAllByUserIdAsync<TModel>(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAuthorIdByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> GetByIdAsync<TModel>(string id)
        {
            throw new NotImplementedException();
        }

        public Task MakeBestAnswerAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
