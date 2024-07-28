using Domain.Orders;
using Npgsql;

namespace Service.Orders.Repository.Models;

public class OrderItemDB
{
    Guid Id { get; set; }

    Guid OrderId { get; set; }

    Guid ProductId { get; set; }

    Int32 Price { get; set; }

    DateTime Createddate { get; set; }

    DateTime Createdatetimeutc { get; set; }

    public OrderItem ToOrderItem()
    {
        return new OrderItem(Id, ProductId, Price);
    }
}
