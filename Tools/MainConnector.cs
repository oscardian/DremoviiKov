using Dapper;
using Npgsql;
using System.Data;

namespace Tools;

public static class MainConnector
{
    private const string ConnecttionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=MusicShop";

    public static void Execute(String sqlCommand, DynamicParameters parameters)
    {
        using (IDbConnection connection = new NpgsqlConnection(ConnecttionString)) 
        {
            connection.Execute(sqlCommand, parameters);
        }
    }

    public static T Get<T>(String sqlCommand, DynamicParameters parameters)
    {
        using (IDbConnection connection = new NpgsqlConnection(ConnecttionString))
        {
            return connection.Query<T>(sqlCommand, parameters).FirstOrDefault();
        }
    }

    public static List<T> GetList<T>(String sqlCommand, DynamicParameters parameters)
    {
        using (IDbConnection connection = new NpgsqlConnection(ConnecttionString))
        {
            return connection.Query<T>(sqlCommand, parameters).ToList();
        }
    }

    public static List<T> GetList<T>(String sqlCommand)
    {
        using (IDbConnection connection = new NpgsqlConnection(ConnecttionString))
        {
            return connection.Query<T>(sqlCommand).ToList();
        }
    }
}
