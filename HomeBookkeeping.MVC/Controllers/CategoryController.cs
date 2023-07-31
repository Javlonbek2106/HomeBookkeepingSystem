using HomeBookkeeping.Api.Filters;
using HomeBookkeeping.Application.UseCases.Categories.Commands.CreateCategory;
using HomeBookkeeping.Application.UseCases.Categories.Commands.DeleteCategory;
using HomeBookkeeping.Application.UseCases.Categories.Commands.UpdateCategory;
using HomeBookkeeping.Application.UseCases.Categories.Queries.GetAllCategories;
using HomeBookkeeping.Application.UseCases.Categories.Queries.GetCategoryById;
using HomeBookkeeping.Application.UseCases.Categorys.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolMonitoringSystem.Api.Controllers;
using X.PagedList;

namespace HomeBookkeeping.Controllers
{
    public class CategoryController : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            //ViewData["subjects"] = subjects;
            //ViewData["students"] = students;
            return View();
        }
        [HttpPost]
        [ActionModelValidationAttribute]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand CategoryCommand)
        {
            var result = await Mediator.Send(CategoryCommand);
            return RedirectToAction(nameof(GetAllCategories));
        }
        [HttpGet]
        public async Task<IActionResult> GetByCategoryId(GetCategoryByIdQuery CategoriesQuery)
        {
            var Categories = await Mediator.Send(new GetAllCategoriesQuery());
            ViewData["categories"] = Categories;
            return View(await Mediator.Send(CategoriesQuery));
        }
        [HttpGet]
        [EnableRateLimiting("Token")]
        public async Task<IActionResult> GetAllCategories(int page = 1)
        {
            //ViewData["students"] = await Mediator.Send(new GetAllStudentQuery());
            IPagedList<CategoryResponse> query = (await Mediator
                .Send(new GetAllCategoriesQuery()))
                .ToPagedList(page, 5);
            return View(query);
        }
        [HttpGet]
        public IActionResult UpdateCategory(CategoryResponse CategoryDto)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryCommand CategoryCommand)
        {
            await Mediator.Send(CategoryCommand);
            return RedirectToAction(nameof(GetAllCategories));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(DeleteCategoryCommand CategoryCommand)
        {
            await Mediator.Send(CategoryCommand);
            return RedirectToAction(nameof(GetAllCategories));
        }

    }
}
