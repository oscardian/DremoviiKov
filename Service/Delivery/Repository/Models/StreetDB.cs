using Domain.Delivery;

namespace Service.Delivery.Repository.Models;

public class StreetDB
{
    public Guid Id { get; set; }

    public Guid TownId { get; set; }

    public String Name { get; set; }

    public StreetType Type { get; set; }

    public StreetDB() { }

    public Street ToStreet()
    {
        return new Street(Id, TownId, Name, Type);
    }
}
