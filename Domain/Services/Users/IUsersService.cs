using Domain.Users;
using Tools;

namespace Domain.Services.Users;

public interface IUsersService
{
    Result CreateAccount(UserBlank userBlank);

    Result UserEditing(UserBlank userBlank);

    DataResult<User?> UserLogin(String login, String password);

    Result RestoreAccount(String email);
}
