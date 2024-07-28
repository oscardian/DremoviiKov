namespace Domain.Delivery;
public class Town
{
    public Guid Id { get; }

    public String Name { get; }

    public TownType Type { get; }

    public Street[] Streets { get; }

    public Town(Guid id, String name, TownType type, Street[] streets)
    {
        Id = id;
        Name = name;
        Type = type;
        Streets = streets;
    }
}
