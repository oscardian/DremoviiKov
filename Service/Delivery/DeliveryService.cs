using Domain.Delivery;
using Domain.Services.Delivery;
using Service.Delivery.Repository;
using Tools;

namespace Service.Delivery;

public class DeliveryService : IDeliveryService
{
    private DeliveryRepository _deliveryRepository;

    public Page<Town> GetTowns(String searchText, Int32 pageNumber, Int32 countInPage)
    {
        return _deliveryRepository.GetTowns(searchText, pageNumber, countInPage);
    }

    public Result SaveTown(TownBlank Blank)
    {
        Result validateResult = ValidateTownBlank(Blank,out TownBlank.Validated validatedTown);
        if (!validateResult.IsSuccess) return Result.Fail(validateResult.Errors);

        _deliveryRepository.SaveTown(validatedTown);

        return Result.Success();
    }

    private Result ValidateTownBlank(TownBlank blank, out TownBlank.Validated validatedTown)
    {
        validatedTown = null;

        blank.Id ??= Guid.NewGuid();

        if (blank.Id is not { } id) throw new Exception("Id города был Null");

        if (blank.Name.IsNullOrWhiteSpace()) return Result.Fail("Имя города указанно не верно");

        if (blank.Type is not { } type) return Result.Fail("Был не выбран тип города");

        validatedTown = new TownBlank.Validated(id, blank.Name, type);

        return Result.Success();
    }

    public Result SaveStreet(StreetBlank streetBlank)
    {
        Result validateResult = ValidateStreetBlank(streetBlank, out StreetBlank.Validated validatedStreet);
        if (!validateResult.IsSuccess) return Result.Fail(validateResult.Errors);

        _deliveryRepository.SaveStreet(validatedStreet);

        return Result.Success();
    }

    private Result ValidateStreetBlank(StreetBlank blank, out StreetBlank.Validated validatedStreet)
    {
        validatedStreet = null;

        blank.Id ??= Guid.NewGuid();

        if (blank.TownId is not { } townId) return Result.Fail("Был не выбран город");

        if (blank.Name.IsNullOrWhiteSpace()) return Result.Fail("Название улицы указанно не верно");

        if (blank.Type is not { } type) return Result.Fail("Не выбран тип города");

        validatedStreet = new StreetBlank.Validated(blank.Id.Value, townId, blank.Name, type);

        return Result.Success();
    }

    public Result RemoveTown(Guid? townId)
    {
        if (townId is not { } id) throw new Exception("id города был Null");

        _deliveryRepository.RemoveTown(id);

        return Result.Success();
    }
}
