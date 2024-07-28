namespace Domain.Delivery;

public partial class StreetBlank
{
    public Guid? Id { get; set; }

    public Guid? TownId { get; set; }

    public String? Name { get; set; }

    public StreetType? Type { get; set; }
}

public partial class StreetBlank
{
    public class Validated
    {
        public Guid Id { get;}

        public Guid TownId { get; }

        public String Name { get; }

        public StreetType Type { get; }

        public Validated(Guid id, Guid townId, String name, StreetType streetType)
        {
            Id = id;
            TownId = townId;
            Name = name;
            Type = streetType;
        }
    }
}