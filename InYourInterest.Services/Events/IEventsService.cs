using InYourInterest.Data.Models;
using InYourInterest.Services.InYourInterest.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InYourInterest.Services.Events
{
    public interface IEventsService
    {
        Task CreateAsync(string name, string description, string location, string image, string userId, string categoriesId, DateTime started, DateTime ended, bool isItOnline);

        /*Task<Event>*/
        string EditAsync(EventServiceModel evnt, string id);//string id, string name, string description, string location, string image, string categoriesId
        Task<TModel> GetByIdAsync<TModel>(string id);
        Task DeleteAsync(string id);
        Task<IEnumerable<TModel>> GetAllAsync<TModel>(string search = null);

        Task RegistrationParticipants(Event evnt, EventUser eventUser);
    }
}
