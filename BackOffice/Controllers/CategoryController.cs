using Domain.Products;
using Domain.Services.Categories;
using Domain.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Service.Categories.Repository;
using Service.Products;
using Tools;

namespace BackOffice.Controllers;

public class CategoryController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoryController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    [HttpPost("Category/save")]
    public Result SaveCategory([FromBody] Domain.Categories.CategoryBlank categoryBlank)
    {
        return _categoriesService.SaveCategory(categoryBlank);
    }

    [HttpPost("GetCategories")]
    public Domain.Categories.Category[] GetCategories()
    {
        return _categoriesService.GetCategories();
    }
}
