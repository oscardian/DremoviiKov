namespace Domain.Orders;

public class OrderBlank
{
    public String? Adress { get; set; }

    public Guid? TownID { get; }

    public Guid? StreetID { get; }

    public OrderType? Type { get; }

    public Int32? OrderNumber { get; set; }

    public String? ClientName { get; set; }

    public String? ClientPhoneNumber { get; set; }

    public Decimal Price => ItemBlanks.Sum((item) => item.Price);

    public OrderItemBlank[] ItemBlanks { get; set; } = Array.Empty<OrderItemBlank>();
}
