using Dapper;
using Domain.Users;
using Npgsql;
using Service.Users.Repository.Models;
using System.Data;
using System.Data.SqlClient;
using Tools;

namespace Service.Users.Repository;

public class UsersRepository
{
    public void SaveAccount(UserBlank.Validated userBlank)
    {
        String sqlCommand = "INSERT INTO Users (id, login, email, password, createdatetime, modifieddatetime, createdatetimeutc, modifieddatetimeutc, isremoved)" +
            "values (@p_id, @p_login, @p_email, @p_password, @p_createdatetime, @p_modifieddatetime, @p_createdatetimeutc, @p_modifieddatetimeutc, false)" +
            "ON CONFLICT(id) DO UPDATE SET id = @p_id, login = @p_login, email = @p_email, password = @p_password, modifieddatetime = @p_modifieddatetime, createdatetimeutc = @p_createdatetimeutc, isremoved = false";

        DynamicParameters parameters = new DynamicParameters(new
        {
            p_id = userBlank.Id,
            p_login = userBlank.Login,
            p_email = userBlank.Email,
            p_password = userBlank.Password.Hashing(),
            p_createdatetime = DateTime.Now,
            p_modifieddatetime = DateTime.Now,
            p_createdatetimeutc = DateTime.UtcNow,
            p_modifieddatetimeutc = DateTime.UtcNow
        });

        MainConnector.Execute(sqlCommand, parameters);
    }
     
    public DataResult<User?> UserLogin(String login, String password)
    {
        String sqlCommand = "SELECT * From Users Where login = @p_login and password = @p_password and isremoved = false";

        DynamicParameters parameters = new DynamicParameters(new
        {
            p_login = login,
            p_password = password
        });

        UserDB userDB = MainConnector.Get<UserDB>(sqlCommand, parameters);

        return DataResult<User>.Success(userDB.ToUser());
    }

    public Boolean IsLoginBusy(String login)
    {
        String sqlCommand = "SELECT * FROM Users WHERE login = @p_login";

        DynamicParameters parameters = new DynamicParameters(new {
            p_login = login
        });

        UserDB userDB = MainConnector.Get<UserDB>(sqlCommand, parameters);
 
        return userDB.ToUser() != null ? true : false;
    }


    public Boolean IsMailOnAccount(String email)
    {
        String SqlCommand = "Select * From Users WHERE email = @p_email";

        DynamicParameters parameters = new DynamicParameters(new
        { p_email = email }
        );

        UserDB userDB = MainConnector.Get<UserDB>(SqlCommand, parameters);

        return userDB.ToUser() != null ? true : false;
    }
}
