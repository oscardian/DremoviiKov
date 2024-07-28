using Microsoft.AspNetCore.Mvc;
using Tools;

namespace BackOffice.Controllers;

public class OrderController
{
    private readonly IOrdersService ordersService;

    public OrderController()
    {
        ordersService = new OrderService();
    }

    [HttpGet("order/get")]
    public Order GetOrder([FromQuery] Guid id)
    {
        return ordersService.GetOrder(id);
    }

    [HttpGet("order/getAll")]
    public Order[] GetOrders()
    {
        return ordersService.GetOrders();
    }

    [HttpGet("order/getOrders")]
    public Page<Order> GetOrders([FromQuery] String? searchText, [FromQuery] Int32 pageNumber, [FromQuery] Int32 countInPage)
    {
        return ordersService.GetOrders(searchText ?? String.Empty, pageNumber, countInPage);
    }

    [HttpGet("order/remove")]
    public Result RemoveOrder([FromQuery] Guid id)
    {
        return ordersService.RemoveOrder(id);
    }

    [HttpPost("order/save")]
    public Result SaveOrder([FromBody] OrderBlank orderBlank)
    {
        return ordersService.SaveOrder(orderBlank);
    }
}
