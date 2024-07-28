using Domain.Products;

namespace Service.Products.Repository.Models;

public class ProductDB
{
    public Guid Id { get; set; }

    public String Name { get; set; }

    public String? Description { get; set; }

    public String Imageurl { get; set; }

    public Guid Category { get; set; }

    public DateTime Salesstartdate { get; set; }

    public Decimal Price { get; set; }

    public DateTime Createddatetime { get; set; }

    public DateTime Createddatetimeutc { get; set; }

    public DateTime? Modfieddatetime { get; set; }

    public DateTime? Modifieddatetimeutc { get; set; }

    public Product ToProduct()
    {
        return new Product(Name, Description, Id, Imageurl, Category, Salesstartdate, Price);
    }

    public ProductDB() {} 
}
