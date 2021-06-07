using AutoMapper;
using InYourInterest.Data;
using InYourInterest.Data.Models;
using InYourInterest.InputModels.Posts;
using InYourInterest.Services.Categories;
using InYourInterest.Services.Posts;
using InYourInterest.Services.Replies;
using InYourInterest.Services.Tags;
using InYourInterest.ViewModels.Posts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InYourInterest.Controllers
{
    public class PostsController : Controller
    {
        private IPostsService postsService;
        private readonly Context context;
        private UserManager<User> userManager; 
        private readonly IMapper mapper;
        private ICategoriesService categoriesService;
        private ITagsService tagsService;
        private IRepliesService repliesService;

        public PostsController(IPostsService postsService, Context context, UserManager<User> userManager,IMapper mapper, ICategoriesService categoriesService, ITagsService tagsService, IRepliesService repliesService)
        {
            this.postsService = postsService;
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
            this.categoriesService = categoriesService;
            this.tagsService = tagsService;
            this.repliesService = repliesService;
        }
        [HttpGet]
        public async Task<IActionResult> Create(string Id)
        {
            var viewModel = new PostsCreateInputModel
            {
                Tags = await this.tagsService.GetAllAsync<PostsTagsDetailsViewModel>(),
                Categories = await this.categoriesService.GetAllAsync<PostsCategoryDetailsViewModel>(),
                GroupId=Id
            };
            //  viewModel.GroupId = Id;
            //  return View("~/Views/Posts/Create.cshtml",id,viewModel); 
            return View("~/Views/Posts/Create.cshtml",viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Id,PostsCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = await this.categoriesService.GetAllAsync<PostsCategoryDetailsViewModel>();
                input.Tags = await this.tagsService.GetAllAsync<PostsTagsDetailsViewModel>();

                return this.View(input);
            }
            var user = await userManager.GetUserAsync(User); 
            var postId = await this.postsService.CreateAsync(
                input.Title,
                input.PostType,
                input.Description,
                user.Id,
                input.CategoryId,
                input.TagIds,
                Id);

            return this.RedirectToAction(nameof(Details), new { id = postId });
        }

        public async Task<IActionResult> Details(string id, string sort = null)
        { 
            var post = await this.postsService.GetByIdAsync<PostsDetailsViewModel>(id);
            if (post == null)
            {
                return this.NotFound();
            }

            await this.postsService.ViewAsync(id);

            post.Tags = await this.tagsService.GetAllByPostIdAsync<PostsTagsDetailsViewModel>(id);
            post.Replies = await this.repliesService.GetAllByPostIdAsync<PostsRepliesDetailsViewModel>(id, sort);

            return this.View(post);
        }
    }
}
