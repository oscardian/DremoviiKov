using Domain.Orders;
using Npgsql;

namespace Service.Orders.Repository.Models;

public class OrderDB
{
    Guid Id { get; set; }

    DateTime SaleDate { get; set; }

    String Address { get; set; }

    Guid? TownID { get; set; }

    Guid? StreetID { get; set; }

    OrderType Type { get; set; }

    String Ordernumber { get; set; }

    String Clientname { get; set; }

    String Clientphoneumber { get; set; }

    Decimal Price { get; set; }

    DateTime CreatedDataTime { get; set; }

    DateTime CreatedDataTimeUtc { get; set; }

    public Order ToOrder(OrderItem[] orderItems)
    {
        return new Order(Id, SaleDate, Address, Ordernumber, Clientname, Clientphoneumber, Price, orderItems);
    }
}
