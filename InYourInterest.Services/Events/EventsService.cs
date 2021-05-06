using AutoMapper;
using AutoMapper.QueryableExtensions;
using InYourInterest.Data;
using InYourInterest.Data.Models;
using InYourInterest.Services.Categories;
using InYourInterest.Services.InYourInterest.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InYourInterest.Services.Events
{
    public class EventsService : IEventsService
    {
        private readonly Context context;
        private readonly IMapper mapper;
        public EventsService(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task CreateAsync(string name, string description, string location, string image, string userId, string categoriesId,DateTime started,DateTime ended, bool isItOnline)
        {
            var categ = await this.context.Categories.FirstOrDefaultAsync(x => x.Id == categoriesId);
            var e = new Event()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Description = description,
                Location = location,
                Image = image,
                UserId = userId,//organizer
                CategoriesId = categoriesId,
                Category = categ,
                Started=started,
                Ended=ended,
                IsItOnline= isItOnline
            };
            context.Events.Add(e);
            var u = context.Users.FirstOrDefault(x => x.Id == userId);
            u.Events.Add(e);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var e = await this.GetByIdAsync(id);
            this.context.Events.Remove(e);
            await this.context.SaveChangesAsync();
        }

        private async Task<Event> GetByIdAsync(string id)
        {
            return await this.context.Events.FirstOrDefaultAsync(c => c.Id == id);
        }
        public string EditAsync(EventServiceModel evnt, string id)
        {
            var e = context.Events.FirstOrDefault(x => x.Id == id);

            e.Name = evnt.Name;
            if (evnt.Image != null)
                e.Image = evnt.Image;
            e.Location = evnt.Location;
            e.Description = evnt.Description;
            e.CategoriesId = evnt.CategoriesId;
            var categ = this.context.Categories.FirstOrDefault(x => x.Id == e.CategoriesId);
            e.Category = categ;
            context.Events.Update(e);
            context.SaveChanges();

            return e.Id;
        }
        
        public async Task<IEnumerable<TModel>> GetAllAsync<TModel>(string search = null)
        {
            var queryable = this.context.Events.AsNoTracking(); 
            if (!String.IsNullOrWhiteSpace(search))
            {
                queryable = queryable.Where(x => x.Name.Contains(search));
            } 
            var events = await queryable.ProjectTo<TModel>(this.mapper.ConfigurationProvider).ToListAsync(); 
            return events;
        } 
        public async Task<TModel> GetByIdAsync<TModel>(string id)
        {
            return await context.Events.AsNoTracking()
                .Where(x => x.Id == id)
                 .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task RegistrationParticipants(Event evnt, EventUser eventUser)
        {
            var e = await context.Events.FirstOrDefaultAsync(x => x.Id == evnt.Id);
            e.EventUser.Add(eventUser);
            await context.SaveChangesAsync();
        }
    }
}
