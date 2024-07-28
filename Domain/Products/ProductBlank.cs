namespace Domain.Products;

public partial class ProductBlank
{
    public Guid? Id { get; set; }

    public String? Name { get; set; }

    public String? Description { get; set; }

    public String? Imageurl { get; set; }

    public Guid? Category { get; set; }

    public DateTime? SalesStartDate { get; set; }

    public Decimal? Price { get; set; }
}

public partial class ProductBlank
{
    public class Validated
    {
        public Guid Id { get; }

        public String Name { get; }

        public String Description { get; }

        public String Imageurl { get; }

        public Guid Category { get; }

        public DateTime SalesStartDate { get; }

        public Decimal Price { get; }

        public Validated(Guid id, String name, String description, String imageurl, Guid category, DateTime salesStartDate, Decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Imageurl = imageurl;
            Category = category;
            SalesStartDate = salesStartDate;
            Price = price;
        }
    }
}