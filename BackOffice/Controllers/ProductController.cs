using Domain.Products;
using Domain.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Tools;

namespace BackOffice.Controllers;

public class ProductController : ControllerBase
{
    private readonly IProductsService _productService;

    public ProductController(IProductsService productService)
    {
        _productService = productService;
    }

    [HttpPost("product/save")]
    public Result SaveProduct([FromBody] ProductBlank productBlank)
    {
        return _productService.SaveProduct(productBlank);
    }

    [HttpGet("products/get")]
    public Product ShowProduct([FromQuery] Guid productId)
    {
        return _productService.GetProduct(productId);
    }

    [HttpGet("product/search")]
    public Page<Product> SearchProduct(String? searchText, Int32 pageNumber, Int32 countInPage)
    {
        return _productService.GetProducts(searchText ?? String.Empty, pageNumber, countInPage);
    }

    [HttpGet("product/remove")]
    public Result RemoveProduct([FromQuery] Guid productId)
    {
        return _productService.RemoveProduct(productId);
    }
}
