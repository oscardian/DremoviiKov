namespace Service.Category.Repository.Models;

public class CategoryDB
{
    public CategoryDB() { }

    public Guid Id { get; set; }

    public String Name { get; set; }

    public Domain.Categories.Category ToCategory()
    {
        return new Domain.Categories.Category(Id, Name);
    }
}

