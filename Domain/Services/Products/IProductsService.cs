using Domain.Products;
using Tools;

namespace Domain.Services.Products
{
    public interface IProductsService
    {
        Result SaveProduct(ProductBlank productBlank);

        Product? GetProduct(Guid id);

        Page<Product> GetProducts(String searchText, Int32 pageNumber, Int32 countInPage);

        Result RemoveProduct(Guid productId);
    }
}
