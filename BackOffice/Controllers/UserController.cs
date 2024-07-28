using Domain.Services.Users;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Tools;

namespace BackOffice.Controllers;

public class UserController : ControllerBase
{
    private readonly IUsersService _userService;

    public UserController(IUsersService userService)
    {
        _userService = userService;
    }

    [HttpPost("account/create")]
    public Result CreateAccount([FromBody] UserBlank userBlank)
    {
        return _userService.CreateAccount(userBlank);
    }

    public record UserLoginRequest(String Login, String Password);

    [HttpPost("account/login")]
    public DataResult<User?> UserLogin([FromBody] UserLoginRequest userRequest)
    {
        return _userService.UserLogin(userRequest.Login, userRequest.Password);
    }

    [HttpPost("account/password")]
    public Result UserEditing([FromBody] UserBlank userBlank)
    {
        return _userService.UserEditing(userBlank);
    }

    [HttpPost("restore")]
    public Result AccountRestore([FromBody] String email)
    {
        return _userService.RestoreAccount(email);
    }
}
