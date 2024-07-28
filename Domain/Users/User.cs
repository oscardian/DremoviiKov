namespace Domain.Users;

public class User
{
    public String Login { get;}

    public String Email { get;}

    public String Password {get;}

    public DateTime CreateDateTime { get;}

    public User(String login, String email, String password, DateTime createDateTime)
    {
        Login = login;
        Email = email;
        Password = password;
        CreateDateTime = createDateTime;
    }

    public User() { }
}
