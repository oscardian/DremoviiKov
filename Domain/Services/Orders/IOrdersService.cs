using Domain.Orders;
using Tools;

namespace Domain.Services.Orders;
public interface IOrdersService
{
    public Result SaveOrder(OrderBlank orderBlank);

    public Result RemoveOrder(Guid id);

    public Orders.Order GetOrder(Guid id);

    public Orders.Order[] GetOrders();

    public Page<Orders.Order> GetOrders(String searchText, Int32 pageNumber, Int32 countInPage);
}
