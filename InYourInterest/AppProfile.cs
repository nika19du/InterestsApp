using AutoMapper;
using InYourInterest.Data.Models;
using InYourInterest.InputModels.Categories;
using InYourInterest.InputModels.Events;
using InYourInterest.InputModels.Groups;
using InYourInterest.ViewModels.Categories;
using InYourInterest.ViewModels.Events;
using InYourInterest.ViewModels.Groups;
using InYourInterest.ViewModels.Posts;
using InYourInterest.ViewModels.User;
using InYourInterest.ViewModels.Tags; 
using System.Linq;

namespace InYourInterest
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            #region
            this.CreateMap<User, UsersLoginStatusViewModel>();

            this.CreateMap<User, HomeAdminViewModel>();
            this.CreateMap<User, UsersDetailsViewModel>();
            #endregion

            #region Categories
            this.CreateMap<Category, CategoriesInfoViewModel>();
            this.CreateMap<Category, CategoriesEditInputModel>();
            this.CreateMap<Category, UsersThreadsCategoryViewModel>();
            #endregion

            #region
            this.CreateMap<Event, EventsInfoViewModel>();
            this.CreateMap<EventsInfoViewModel, EventsAllViewModel>().ReverseMap();
            this.CreateMap<Event, EventsSavedViewModel>(); 
            this.CreateMap<EventsEditInputModel, Event>().ForMember(x => x.Category, option => option.Ignore()).ReverseMap();
            this.CreateMap<Event, EventsDetailsViewModel>();
            this.CreateMap<Event, Event>();
            #endregion

            #region Groups 
            this.CreateMap<Group, Group>();
            this.CreateMap<Group, GroupInfoViewModel>(); 
            this.CreateMap<Group, Event>();
            this.CreateMap<GroupsUpdateInputModel, Group>().ForMember(x => x.Category, option => option.Ignore()).ReverseMap();
            this.CreateMap<GroupMembersViewModel, GroupMembers>().ReverseMap();
            this.CreateMap<Group, GroupMembers>().ReverseMap();
            #endregion

            #region Tags
            this.CreateMap<Tag, TagsInfoViewModel>()
                .ForMember(
                    dest => dest.PostsCount,
                    dest => dest.MapFrom(src => src.Posts.Count(p => !p.Post.IsDeleted)));
            this.CreateMap<Tag, PostsTagsDetailsViewModel>();
            //this.CreateMap<Tag, UsersThreadsTagsViewModel>();
            this.CreateMap<PostTag, TagsInfoViewModel>();
            this.CreateMap<PostTag, PostsTagsDetailsViewModel>();
            // this.CreateMap<PostTag, UsersThreadsTagsViewModel>();
            CreateMap<Category, PostsCategoryDetailsViewModel>();;
          
            #endregion 
            CreateMap<Post,PostsDetailsViewModel>();
            CreateMap<Post, PostsAuthorDetailsViewModel>();
            this.CreateMap<User, PostsAuthorDetailsViewModel>();
            CreateMap<Reply, PostsRepliesDetailsViewModel>();
        }
    }
}
