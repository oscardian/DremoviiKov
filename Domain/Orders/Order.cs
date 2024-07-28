namespace Domain.Orders;

public class Order
{
    public Guid Id { get; }

    public DateTime SaleDate { get; }

    public String Adress { get; }

    public Guid? TownID { get; }

    public Guid? StreetID { get; }

    public OrderType Type { get; }

    public String OrderNumber { get; }

    public String ClientName { get; }

    public String ClientPhoneNumber { get; }

    public Decimal Price { get; }

    public OrderItem[] OrderItems { get; }

    public Order(Guid id, DateTime saleDate, String adress, OrderType type, String ordernumber, String сlientname, String сlientphoneumber, Decimal price, OrderItem[] orderItems)
    {
        Id = id;
        SaleDate = saleDate;
        Adress = adress;
        Type = type;
        OrderItems = orderItems;
        OrderNumber = ordernumber;
        ClientName = сlientname;
        ClientPhoneNumber = сlientphoneumber;
        Price = price;
    }

    public Order(Guid id, DateTime saleDate, String adress, Guid townId, Guid streetId, OrderType type, String ordernumber, String сlientname, String сlientphoneumber, Decimal price, OrderItem[] orderItems)
    {
        Id = id;
        SaleDate = saleDate;
        Adress = adress;
        TownID = townId;
        StreetID = streetId;
        Type = type;
        OrderItems = orderItems;
        OrderNumber = ordernumber;
        ClientName = сlientname;
        ClientPhoneNumber = сlientphoneumber;
        Price = price;
    }
}
