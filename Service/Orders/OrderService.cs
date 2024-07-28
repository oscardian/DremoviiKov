using Domain.Orders;
using Domain.Services.Orders;
using Service.Orders.Repository;
using Service.Products.Repository;
using Tools;

namespace Service.Orders;

public class OrderService : IOrdersService
{
    private readonly OrderRepository repository;
    private readonly ProductsRepository productsRepository;

    public OrderService()
    {
        repository = new OrderRepository();
    }

    public Order GetOrder(Guid id)
    {
        return repository.GetOrder(id);
    }

    public Order[] GetOrders()
    {
        return repository.GetOrders();
    }

    public Page<Order> GetOrders(String searchText, Int32 pageNumber, Int32 countInPage)
    {
        return repository.GetOrders(searchText, pageNumber, countInPage);
    }

    public Result RemoveOrder(Guid id)
    {
        Order? order = GetOrder(id);

        if (order is null)
            return Result.Fail("Продукта не существует");

        repository.RemoveOrder(id);

        return Result.Success();
    }

    public Result SaveOrder(OrderBlank orderBlank)
    {
        if (orderBlank.SaleDate is null)
            return Result.Fail("Нет даты заказа");

        if (String.IsNullOrEmpty(orderBlank.Adress))
            return Result.Fail("Не указан адресс");

        return repository.SaveOrder(orderBlank);
    }
}
