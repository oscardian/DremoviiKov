using Domain.Services.Categories;
using Tools;

namespace Service.Category;

public class CategoryService : ICategoriesService
{
    private Service.Categories.Repository.CategoryRepository _categoryRepository;

    public Domain.Categories.Category[] GetCategories()
    {
        return _categoryRepository.GetCategories();
    }

    public Result SaveCategory(Domain.Categories.CategoryBlank categoryBlank)
    {
        Result validateResult = ValidateCategoryBlank(categoryBlank, out Domain.Categories.CategoryBlank.Validated validatedCategory);
        if(!validateResult.IsSuccess) return Result.Fail(validateResult.Errors);

        _categoryRepository.SaveCategory(validatedCategory);

        return Result.Success();
    }

    private Result ValidateCategoryBlank(Domain.Categories.CategoryBlank categoryBlank, out Domain.Categories.CategoryBlank.Validated validatedCategory)
    {
        validatedCategory = null;

        categoryBlank.Id ??= Guid.NewGuid();

        if (categoryBlank.Name.IsNullOrWhiteSpace()) return Result.Fail("Не указанно имя категории");

        validatedCategory = new Domain.Categories.CategoryBlank.Validated(categoryBlank.Id.Value, categoryBlank.Name);

        return Result.Success();
    }
}
