using Domain.Delivery;
using Tools;

namespace Domain.Services.Delivery;

public interface IDeliveryService
{
    public Page<Town> GetTowns(String searchText, Int32 pageNumber, Int32 countInPage);

    public Result SaveTown(TownBlank townBlank);

    public Result SaveStreet(StreetBlank streetBlank);

    public Result RemoveTown(Guid? id);
}
