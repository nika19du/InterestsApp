using AutoMapper;
using AutoMapper.QueryableExtensions;
using InYourInterest.Data;
using InYourInterest.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InYourInterest.Services.Categories
{
    public class CategoriesService : ICategoriesService
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public CategoriesService(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task CreateAsync(string name)
        {
            var category = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = name
            };
            await this.context.Categories.AddAsync(category);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var category = await this.GetByIdAsync(id);

            this.context.Categories.Remove(category);
            await this.context.SaveChangesAsync();
        }

        public async Task EditAsync(string id, string name)
        {
            var category = await this.GetByIdAsync(id);

            category.Name = name;

            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel>(string search = null)
        {
            var queryable = this.context.Categories
                .AsNoTracking();

            if (!String.IsNullOrWhiteSpace(search))
            {
                queryable = queryable.Where(x => x.Name.Contains(search));
            }

            var categories = await queryable.ProjectTo<TModel>(this.mapper.ConfigurationProvider).ToListAsync();

            return categories;
        }

        public async Task<TModel> GetByIdAsync<TModel>(string id)
        {
            return await this.context.Categories
                .AsNoTracking()
                .Where(x => x.Id== id)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
        private async Task<Category> GetByIdAsync(string id)
        {
            return await this.context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
         
        public async Task<bool> IsExistingAsync(string name)
        => await this.context.Categories.AnyAsync(x => x.Name == name);
    }
}
