using Tools;

namespace Domain.Services.Categories;

public interface ICategoriesService
{
    public Result SaveCategory(Domain.Categories.CategoryBlank categoryBlank);

    public Domain.Categories.Category[] GetCategories();
}
