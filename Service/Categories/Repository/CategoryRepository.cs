using Dapper;
using Tools;

namespace Service.Categories.Repository;

public class CategoryRepository
{
    public Domain.Categories.Category[] GetCategories()
    {
        String sqlCommand = "Select * Producttype where isremoved = false";

        List<Service.Category.Repository.Models.CategoryDB> categoryDBs = MainConnector.GetList<Service.Category.Repository.Models.CategoryDB>(sqlCommand);

        List<Domain.Categories.Category> categories = categoryDBs.Select(categoryDB => categoryDB.ToCategory()).ToList();

        return categories.OrderBy(category => category.Name).ToArray();
    }

    public void SaveCategory(Domain.Categories.CategoryBlank.Validated validatedCategory)
    {
        String sqlCommand = @"INSERT INTO Category 
                              (id, name, isremoved) values(@p_id, @p_name, false) 
                                 ON CONFLICT(id) DO UPDATE SET name = @p_name, isremoved = false";

        DynamicParameters dynamicParameters = new DynamicParameters(new 
        {
            p_id = validatedCategory.Id,
            p_name = validatedCategory.Name
        });

        MainConnector.Execute(sqlCommand, dynamicParameters);
    }

}
