using Domain.Services.Users;
using Domain.Users;
using Service.Users.Repository;
using Tools;

namespace Service.Users;

public class UsersService : IUsersService
{
    private readonly UsersRepository _usersRepository;
    private readonly EmailService _emailService;

    public UsersService()
    {
        _usersRepository = new UsersRepository();
    }

    public Result CreateAccount(UserBlank userBlank)
    {
        Result validateResult = ValidateUserBlank(userBlank, out UserBlank.Validated validatedBlank);
        if (!validateResult.IsSuccess) return Result.Fail(validateResult.Errors);

        _usersRepository.SaveAccount(validatedBlank);

        return Result.Success();
    }

    public Result UserEditing(UserBlank userBlank)
    {
        Result validateResult = ValidateUserBlank(userBlank, out UserBlank.Validated validatedBlank);
        if (!validateResult.IsSuccess) return Result.Fail(validateResult.Errors);

        _usersRepository.SaveAccount(validatedBlank);

        return Result.Success();
    }

    private Result ValidateUserBlank(UserBlank userBlank, out UserBlank.Validated validatedUser)
    {
        validatedUser = null!;

        if (userBlank.Id is not { } id) throw new Exception("ID пользователя при регистрации null;");

        if (userBlank.Login.IsNullOrWhiteSpace()) return Result.Fail("Укажите логин");
        if (IsLoginBusy(userBlank.Login!)) return Result.Fail("Логин занят");

        if (userBlank.Password.IsNullOrWhiteSpace()) return Result.Fail("Укажите пароль");

        if (userBlank.Email.IsNullOrWhiteSpace()) return Result.Fail("Укажите почту");
        if (userBlank.Email!.IsEmail()) return Result.Fail("Почта указанна не корректно");

        validatedUser = new UserBlank.Validated(id, userBlank.Login!, userBlank.Email!, userBlank.Password!);

        return Result.Success();
    }

    private Boolean IsLoginBusy(String login)
    {
        return _usersRepository.IsLoginBusy(login);
    }

    public DataResult<User?> UserLogin(String? login, String? password)
    {
        if (login.IsNullOrWhiteSpace()) return new DataResult<User?>(Result.Fail("Укажите логин"), null);
        if (password.IsNullOrWhiteSpace()) return new DataResult<User?>(Result.Fail("Укажите пароль"), null);

        return _usersRepository.UserLogin(login, password);
    }

    public Result RestoreAccount(String email)
    {
        if (email.IsNullOrWhiteSpace()) return Result.Fail("Почта введена не корректно");
        if (email.IsEmail()) return Result.Fail("Почта введена не корректно");

        if (_usersRepository.IsMailOnAccount(email))
        {
            _emailService.SendPasswordResetEmail(email);
        }

        return Result.Success();
    }
}
