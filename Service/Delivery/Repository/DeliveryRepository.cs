using Dapper;
using Domain.Delivery;
using Service.Delivery.Repository.Models;
using Service.Products.Repository.Models;
using System.Collections.Generic;
using Tools;

namespace Service.Delivery.Repository;

public class DeliveryRepository
{
   public Page<Town> GetTowns(String searchText, Int32 pageNumber, Int32 countInPage)
   {
        String townSqlCommand = @"Select * From towns Where isremoved = false LIMIT @p_limit OFFSET @p_offset";

        DynamicParameters parameters = new DynamicParameters(new
        {
            p_searchText = searchText,
            p_limit = countInPage,
            p_offset = (pageNumber - 1) * countInPage
        });

        List<TownDB> townDBs = MainConnector.GetList<TownDB>(townSqlCommand,parameters);

        Int32 totalRows = 0;

        totalRows = townDBs.Count;

        String streetSqlCommand = @"Select * From streets Where id = @p_id, isremoved = false";
        
        List<Town> towns = new List<Town>();

        foreach (TownDB townDB in townDBs)
        {
            DynamicParameters streetParameters = new DynamicParameters(new
            {
                p_id = townDB.Id
            });

            List<StreetDB> streetDBs = MainConnector.GetList<StreetDB>(streetSqlCommand, streetParameters);

            towns.AddRange(townDBs.Select(townDB => townDB.ToTown(
                streetDBs.Select(streetDB => streetDB.ToStreet())
                .ToArray())));
        }

        return new Page<Town>(totalRows, towns.OrderBy(town => town.Name).ToArray());
   }

   public void SaveTown(TownBlank.Validated townBlank)
   {
        String townSqlCommand = @"INSERT INTO towns (id, name, isremoved, type , createddatetime, createddatetimeutc, modfieddatetime, modifieddatetimeutc)
                                              values(@p_id, @p_name, false, @p_type, @p_createddatetime, @p_createddatetimeutc, null, null) 
                                                  ON CONFLICT(id) DO UPDATE SET name = @p_name, type = @p_type, isremoved = false, 
                                                                                  modfieddatetime = @p_modifieddatetime, modifieddatetimeutc = @p_modifieddatetimeutc";

        DynamicParameters parameters = new DynamicParameters(new
        {
            p_id = townBlank.Id,
            p_name = townBlank.Name,
            P_type = townBlank.Type,
            p_createddatetime = DateTime.Now,
            p_createddatetimeutc = DateTime.UtcNow,
            p_modfieddatetime = DateTime.Now,
            p_modifieddatetimeutc = DateTime.UtcNow
        });

        MainConnector.Execute(townSqlCommand, parameters);
   }

   public void SaveStreet(StreetBlank.Validated streetBlank)
   {
        String streetSqlCommand = @"INSERT INTO streets (id, townid, name, isremoved, type, createddatetime, createddatetimeutc, modfieddatetime, modifieddatetimeutc)
                                                          values(@p_id, @p_townid, @p_name, false, @p_type, @p_createddatetime, @p_createddatetimeutc, null , null )
                                                                ON CONFLICT(id) DO UPDATE SET townid = @p_townid, name = @p_name, type = @p_type, 
                                                                                modfieddatetime = @p_modfieddatetime, modifieddatetimeutc = @p_modifieddatetimeutc";

        DynamicParameters parameters = new DynamicParameters(new
        {
            p_id = streetBlank.Id,
            p_townid = streetBlank.TownId,
            p_name = streetBlank.Name,
            p_type = streetBlank.Type,
            p_createddatetime = DateTime.Now,
            p_createddatetimeutc = DateTime.UtcNow,
            p_modfieddatetime = DateTime.Now,
            p_modifieddatetimeutc = DateTime.UtcNow
        });

        MainConnector.Execute(streetSqlCommand, parameters);
   }

    public void RemoveTown(Guid id)
    {
        String townSqlCommand = @"UPDATE towns SET isremoved = true WHERE id = @p_id";

        String steetSqlCommand = @"UPDATE street SET isremove = true WHERE townId = @p_id";

        DynamicParameters parameters = new DynamicParameters(new
        {
            p_id = id
        });

        MainConnector.Execute(townSqlCommand, parameters);

        MainConnector.Execute(steetSqlCommand, parameters);
    }
}
