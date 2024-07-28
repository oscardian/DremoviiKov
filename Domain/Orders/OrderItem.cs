namespace Domain.Orders;

public class OrderItem
{
    public Guid Id { get; }

    public Guid ProductId { get; }

    public Decimal Price { get; }

    public DateTime CreatedDateTime { get; }

    public DateTime CreatedDateTimeUtc { get; }

    public OrderItem(Guid id, Guid productId, Decimal price)
    {
        Id = id;
        ProductId = productId;
        Price = price;
    }
}
