using Domain.Delivery;

namespace Service.Delivery.Repository.Models;

public class TownDB
{
    public Guid Id { get; set; }

    public String Name { get; set; }

    public TownType Type { get; set; }

    public Street[] Streets { get; set; }

    public TownDB() { }
    
    public Town ToTown(Street[] streets)
    {
        return new Town(Id, Name, Type, streets);
    }
}
