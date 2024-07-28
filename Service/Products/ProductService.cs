using Domain.Products;
using Domain.Services.Products;
using Service.Products.Repository;
using System.Numerics;
using Tools;

namespace Service.Products;

public class ProductService: IProductsService
{
    private readonly ProductsRepository _productsRepository;

    public ProductService()
    {
        _productsRepository = new ProductsRepository();
    }

    public Result SaveProduct(ProductBlank productBlank)
    {
        productBlank.Id ??= Guid.NewGuid(); 

        Result validateResult = ValidateProductBlank(productBlank, out ProductBlank.Validated validatedProduct);
        if (!validateResult.IsSuccess) return Result.Fail(validateResult.Errors);

        _productsRepository.SaveProduct(validatedProduct);

        return Result.Success();
    }

    private Result ValidateProductBlank(ProductBlank blank, out ProductBlank.Validated validatedProduct)
    {
        validatedProduct = null!;

        if (blank.Id is not { } id) throw new Exception("ID товара при регистрации null;");

        if (blank.Name.IsNullOrWhiteSpace()) return Result.Fail("Укажите название товара");
        if (blank.Name!.Length > 100) return Result.Fail("Слишком длинное имя");

        if (blank.Description.IsNullOrWhiteSpace()) return Result.Fail("Укажите описание товара");
        if (blank.Description!.Length > 400) return Result.Fail("Слишком длинное описание");

        if (blank.Imageurl.IsNullOrWhiteSpace()) return Result.Fail("Укажите изображение товара");
        if (blank.Imageurl!.Length > 400) return Result.Fail("Слишком длинная ссылка");

        if (blank.Category is not { } category) return Result.Fail("Не выбранна категория");

        if (blank.SalesStartDate is not { } salesstarDate) return Result.Fail("Укажите дату продаж");
        if (blank.SalesStartDate < DateTime.Now) return Result.Fail("Дата начала продаж выставленна некорректно");

        if (blank.Price is not { } price) return Result.Fail("Укажите цену");
        if (blank.Price <= -1) return Result.Fail("Цена указанна неккоректно");

        validatedProduct = new ProductBlank.Validated(id, blank.Name, blank.Description, blank.Imageurl, category, salesstarDate, price);

        return Result.Success();
    }

    public Product? GetProduct(Guid id)
    {
        return _productsRepository.GetProduct(id);
    }

    public Page<Product> GetProducts(String searchText, Int32 pageNumber, Int32 countInPage)
    {
        return _productsRepository.GetProducts(searchText, pageNumber, countInPage);
    }

    public Result RemoveProduct(Guid productId)
    {
        _productsRepository.RemoveProduct(productId);

        return Result.Success();
    }
}
