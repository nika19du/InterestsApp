using InYourInterest.InputModels.Categories;
using InYourInterest.Services.Categories;
using InYourInterest.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InYourInterest.Areas.Administration.Controllers
{
    public class CategoriesController : AdminController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categories)
        {
            this.categoriesService = categories;
        }

        public async Task<IActionResult> All(string search = null)
        {
            var categories = await this.categoriesService.GetAllAsync<CategoriesInfoViewModel>(search);
            var viewModel = new CategoriesAllViewModel
            {
                Search = search,
                Categories = categories
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputModels.Categories.CategoriesCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            await this.categoriesService.CreateAsync(model.Name);
            return this.RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var category = await this.categoriesService.GetByIdAsync<CategoriesEditInputModel>(id);

            if (category == null)
            {
                return this.NotFound();
            }
            return this.View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoriesEditInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.categoriesService.EditAsync(model.Id, model.Name);
            return this.RedirectToAction("All", "Categories", new { id = model.Id, area = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var isExisting = await this.categoriesService.IsExistingAsync(id);
            if (isExisting == false)
            {
                return this.NotFound();
            }
            await this.categoriesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(All));
        }
    }
}
