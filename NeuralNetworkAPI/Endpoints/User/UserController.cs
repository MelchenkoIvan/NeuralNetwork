using Microsoft.AspNetCore.Mvc;
using NeuralNetworkAPI.Models;
using NeuralNetworkCore;
using NeuralNetworkCore.Models;

namespace NeuralNetworkAPI.Endpoints.User;

[Route("[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserRepositroy _userRepositry;
    
    public UserController(IUserRepositroy userRepositroy)
    {
        _userRepositry = userRepositroy;
    }
    
    [HttpGet("{userName}")]
    public async Task<UserDTO> GetUser([FromRoute]string userName)
    {
        var user = await _userRepositry.GetUser(userName!);
        return user;
    }
    
    [HttpPost("login")]
    public async Task<string> Login(LoginViewModel req)
    {
        var isAuthenticated = await _userRepositry.Authenticate(req.UserName, req.Password);

        if (isAuthenticated)
        {
            // await CookieAuth.SignInAsync(u =>
            // {
            //     u.Claims.Add(new("usr", req.UserName));
            // });
            return req.UserName;
        }

        return string.Empty;
    }
    [HttpPost("registration")]
    public async Task Registration(LoginViewModel req)
    {
        var added = await _userRepositry.AddUser(req.UserName, req.Password);
    }
}