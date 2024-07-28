namespace Domain.Delivery;

public class Street
{
    public Guid Id { get; }

    public Guid TownId { get; }

    public String Name { get; }
    
    public StreetType Type { get; }

    public Street(Guid id, Guid townId, String name, StreetType type)
    {
        Id = id;
        TownId = townId;
        Name = name;
        Type = type;
    }
}
