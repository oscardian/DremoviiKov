using Dapper;
using Domain.Products;
using Service.Products.Repository.Models;
using Tools;

namespace Service.Products.Repository;

public class ProductsRepository
{
    public void SaveProduct(ProductBlank.Validated productBlank)
    {
        String sqlCommand = @"INSERT INTO products 
          (id, name, description, ImageUrl, Category, Price, salesstartdate, createddatetime, createddatetimeutc, modfieddatetime, modifieddatetimeutc, isremoved)
          values(@p_id, @p_name, @p_description, @p_imageurl, @p_category, @p_price, @p_salestart, @p_createdtime, @p_createdtimeutc, null, null, false) 
          ON CONFLICT(id) DO UPDATE SET name = @p_name, description = @p_description, imageurl = @p_imageurl, modfieddatetime = @p_modifieddatetime,
          modifieddatetimeutc = @p_modifieddatetimeutc, category = @p_category, price = @p_price, salesstartdate = @p_salestart, isremoved = false";

        DynamicParameters parameters = new DynamicParameters(new
        {
            p_id = productBlank.Id,
            p_name = productBlank.Name,
            p_description = productBlank.Description,
            p_imageUrl = productBlank.Imageurl,
            p_category = productBlank.Category,
            p_price = productBlank.Price,
            p_salestart = productBlank.SalesStartDate,
            p_createdtime = DateTime.Now,
            p_createdtimeutc = DateTime.UtcNow,
            p_modifieddatetime = DateTime.Now,
            p_modifieddatetimeutc =  DateTime.UtcNow
        });

        MainConnector.Execute(sqlCommand, parameters);
    }

    public Product? GetProduct(Guid id)
    {
        String sqlCommand = "Select * From Products WHERE id = @p_id and isremoved=false";

        DynamicParameters parameters = new DynamicParameters(new
        {
           p_id = id,
        });

        ProductDB productDB = MainConnector.Get<ProductDB>(sqlCommand, parameters);

        return productDB.ToProduct();  
    }

    public Page<Product> GetProducts(String searchText, Int32 pageNumber, Int32 countInPage)
    {
        String sqlCommand = "SELECT *, count(*) OVER() AS full_count FROM Products WHERE name ILIKE '%' || @p_searchText || '%' AND isremoved = false LIMIT @p_limit OFFSET @p_offset";

        DynamicParameters parameters = new DynamicParameters(new
        {
            p_searchText = searchText,
            p_limit = countInPage,
            p_offset = (pageNumber - 1) * countInPage
        });

        List<ProductDB> productDBs = MainConnector.GetList<ProductDB>(sqlCommand, parameters);

        Int32 totalRows = 0;

        totalRows = productDBs.Count;

        Product[] product = productDBs.Select(productDB => productDB.ToProduct()).ToArray();

        return new Page<Product>(totalRows, product);
    }

    public void RemoveProduct(Guid id)
    {
        String sqlCommand = "UPDATE Products SET isremoved = true, modfieddatetime = @p_modifieddatetime , modifieddatetimeutc = @p_modifieddatetimeutc WHERE id = @p_id";

        DynamicParameters parameters = new DynamicParameters(new
        {
            p_id = id,
            p_modifieddatetime = DateTime.Now,
            p_modifieddatetimeutc = DateTime.UtcNow
        });

        MainConnector.Execute(sqlCommand, parameters);
    }
}
