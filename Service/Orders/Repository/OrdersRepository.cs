using Dapper;
using Domain.Orders;
using Tools;

namespace Service.Orders.Repository;

public class OrdersRepository
{
    private const String connectionString = "Host=localhost;Username=postgres;Password=Liza;Database=postgres";

    public Result SaveOrder(OrderBlank orderBlank)
    {
        String sqlOrder = @"INSERT INTO Orders(id, adress , createddate, createddateutc, modifieddatetime, modifieddatetimeutc, isremove, ordernumber, clientname,clientphonenumber,price)
           values(@p_id, @p_adress, @p_modifieddatetime, @p_modifieddatetimeutc, null, null, false,@p_ordernumber, @p_clientname,@p_clientphonenumber, @p_orderPrice)
           ON CONFLICT(id) DO UPDATE SET modifieddatetime = @p_modifieddatetime, modifieddatetimeutc = @p_modifieddatetimeutc, 
                               adress = @p_adress, ordernumber = @p_ordernumber,clientname = @p_clientname, clientphonenumber = @p_clientphonenumber, price = @p_orderPrice";

        String sqlOrderItems = @"Insert into Orderitems (id,orderid,productid,price,isremove, createddate , createddateutc)
                      values(@p_orderItemId,@p_id,@p_productId,@p_price,false, @p_createdtime , @p_createdtimeutc ) 
                   ON CONFLICT(id) DO UPDATE SET id = @p_orderItemId, orderid = @p_id, productid = @p_productid, price = @p_price, 
                                                                   modifieddatetime = @p_modifieddatetime, modifieddatetimeutc = @p_modifieddatetimeutc";

        DynamicParameters orderParameters = new DynamicParameters(new
        {
            p_ordernumber = orderBlank.OrderNumber,
            p_clientname = orderBlank.ClientName,
            p_clientphonenumber = orderBlank.ClientPhoneNumber,
            p_adress = orderBlank.Adress,
            p_createdtime = DateTime.Now,
            p_modifieddatetime = DateTime.Now,
            p_modifieddatetimeutc = DateTime.UtcNow,
            p_createdtimeutc = DateTime.UtcNow,
            p_id = Guid.NewGuid(),
            p_orderPrice = orderBlank.Price
        });

        MainConnector.Execute(sqlOrder, orderParameters);

        foreach (var item in orderBlank.ItemBlanks)
        {
            DynamicParameters orderItemParameters = new DynamicParameters(new
            {
                p_orderItemId = Guid.NewGuid(),
                p_productid = item.ProductId,
                p_price = item.Price
            });

            MainConnector.Execute(sqlOrderItems,orderItemParameters);
        }

        return Result.Success();
    }

    public void RemoveOrder(Guid id)
    {
        String sql = "UPDATE Orders SET isremove = true, modifieddatetime = @p_modfieddatettime , modifieddatetimeutc = @p_modifieddatetimeutc WHERE id = @p_id";

        String sqlOrderItems = "UPDATE orderitems SET isremove = true, modifieddatetime = @p_modfieddatettime , modifieddatetimeutc = @p_modifieddatetimeutc WHERE orderid = @p_id";

        DynamicParameters parameters = new DynamicParameters(new
        {
            p_id = id,
            p_modifieddatetime = DateTime.Now,
            p_modifieddatetimeutc = DateTime.UtcNow
        });

        MainConnector.Execute(sql, parameters);

        MainConnector.Execute(sqlOrderItems, parameters);
    }

    public Order GetOrder(Guid id)
    {
        String orderSql = "Select * FROM Orders WHERE id = @p_id and isremove = false";

        String orderItemsSql = "Select * FROM Orderitems WHERE orderid = @p_id and isremove = false";

        return Database.UseSqlCommand(connectionString, (command) =>
        {
            command.CommandText = orderSql;

            command.AddParameter("@p_id", id);

            OrderDB orderDB = new OrderDB();
            using (NpgsqlDataReader orderReader = command.ExecuteReader())
            {
                while (orderReader.Read())
                {
                    orderDB = OrderDB.FromDateReader(orderReader);
                }
            }
            command.CommandText = orderItemsSql;

            NpgsqlDataReader orderItemsReader = command.ExecuteReader();

            List<OrderItemDB> orderItemDBs = new List<OrderItemDB>();

            while (orderItemsReader.Read())
            {
                OrderItemDB orderItemDB = OrderItemDB.FromDateReader(orderItemsReader);
                orderItemDBs.Add(orderItemDB);
            }
            


             Order order = orderDB.ToOrder(orderItemDBs.Select(orderItems => orderItems.ToOrderItem()).ToArray());

            return order;

        });
    }

    public Order[] GetOrders()
    {
        String orderSql = "Select * FROM Orders WHERE isremove = false";

        String orderItemsSql = "Select * FROM Orderitems WHERE isremove = false";

        return Database.UseSqlCommand(connectionString, (command) =>
        {
            command.CommandText = orderSql;
            List<OrderDB> orderDBs = new List<OrderDB>();
            using (NpgsqlDataReader ordersReader = command.ExecuteReader())
            {
                while (ordersReader.Read())
                {
                    OrderDB orderDB = OrderDB.FromDateReader(ordersReader);
                    orderDBs.Add(orderDB);
                }

            }

            command.CommandText = orderItemsSql;

            NpgsqlDataReader orderItemsReader = command.ExecuteReader();

            List<OrderItemDB> orderItemDBs = new List<OrderItemDB>();

            while (orderItemsReader.Read())
            {
                OrderItemDB orderItemDB = OrderItemDB.FromDateReader(orderItemsReader);
                orderItemDBs.Add(orderItemDB);
            }

            List<Order> orders = new List<Order>();
            foreach (OrderDB orderDB in orderDBs)
            {
                orders.Add(orderDB.ToOrder(orderItemDBs.Select(orderItems => orderItems.ToOrderItem()).ToArray()));
            }

            return orders.ToArray();
        });
    }

    public Page<Order> GetOrders(String searchText, Int32 pageNumber, Int32 countInPage)
    {
        String orderSql = "SELECT *, count(*) OVER() AS full_count FROM Orders WHERE ordernumber ILIKE @p_searchText OR clientphoneumber ILIKE @p_searchText and isremove = false LIMIT @p_limit OFFSET @p_offset";

        String orderItemsSql = "SELECT *, count(*) OVER() AS full_count FROM Orderitems WHERE isremove = false LIMIT @p_limit OFFSET @p_offset";

        return Database.UseSqlCommand(connectionString, (command) =>
        {
            command.CommandText = orderSql;

            command.AddParameter("p_searchText", '%' + searchText + '%');
            command.AddParameter("p_limit", countInPage);
            command.AddParameter("p_offset", (pageNumber - 1) * countInPage);

            List<OrderDB> orderDBs = new List<OrderDB>();
            Int32 totalRows = 0;
            using (NpgsqlDataReader ordersReader = command.ExecuteReader())
            {
                while (ordersReader.Read())
                {
                    OrderDB orderDB = OrderDB.FromDateReader(ordersReader);
                    orderDBs.Add(orderDB);
                    totalRows = Int32.Parse(ordersReader["full_count"].ToString());
                }
            }


            command.CommandText = orderItemsSql;

            NpgsqlDataReader orderItemsReader = command.ExecuteReader();

            List<OrderItemDB> orderItemDBs = new List<OrderItemDB>();

            while (orderItemsReader.Read())
            {
                OrderItemDB orderItemDB = OrderItemDB.FromDateReader(orderItemsReader);
                orderItemDBs.Add(orderItemDB);
            }

            List<Order> orders = new List<Order>();
            foreach (OrderDB orderDB in orderDBs)
            {
                orders.Add(orderDB.ToOrder(orderItemDBs.Select(orderItems => orderItems.ToOrderItem()).ToArray()));
            }


            return new Page<Order>(totalRows, orders.ToArray());

        });
    }
}
