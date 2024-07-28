namespace Domain.Users;

public partial class UserBlank
{
    public Guid? Id { get; set; }

    public String? Login { get; set; }

    public String? Password { get; set; }

    public String? Email { get; set;}
}

public partial class UserBlank
{
    public class Validated
    {
        public Guid Id { get; }

        public String Login { get; }

        public String Password { get; }

        public String Email { get; }

        public Validated(Guid id, String login, String email, String password)
        {
            Id = id;
            Login = login;
            Email = email;
            Password = password;      
        }
    }
}
