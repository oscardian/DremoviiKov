namespace Domain.Delivery;

public partial class TownBlank
{
    public Guid? Id { get; set; }

    public String? Name { get; set; }

    public TownType? Type { get; set; }
}

public partial class TownBlank
{
    public class Validated
    {
        public Guid Id { get; }

        public String Name { get; }

        public TownType Type { get; }

        public Validated(Guid id, String name, TownType townType)
        {
            Id = id;
            Name = name;
            Type = townType;
        }
    }
}