namespace Domain.Products;

public class Product
{
    public String Name { get; }

    public String? Description { get; }

    public String Imageurl { get; }

    public Guid Category { get; }

    public DateTime SalesStartDate { get; }

    public Decimal Price { get; }

    public Product(String name, String description, Guid id, String imagerul, Guid category, DateTime saleStartDate, Decimal price)
    {
        Name = name;
        Description = description;
        Imageurl = imagerul;
        Category = category;
        SalesStartDate = saleStartDate;
        Price = price;
    }
}
