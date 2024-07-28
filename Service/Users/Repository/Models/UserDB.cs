using Domain.Users;
using Npgsql;
using System.Data;

namespace Service.Users.Repository.Models;

public class UserDB
{
    public Guid Id { get; set;}

    public String Login { get; set;}

    public String Email { get; set;}

    public String Password { get; set;}

    public DateTime CreateDateTime { get; set;}

    public User ToUser()
    {
        return new User(Login, Email, Password, CreateDateTime);
    }
}

