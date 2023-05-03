using Microsoft.AspNetCore.Mvc;
using NeuralNetworkAPI.Endpoints.Authorization;
using NeuralNetworkAPI.Models;
using NeuralNetworkCore;
using NeuralNetworkCore.Models;

namespace NeuralNetworkAPI.Endpoints.User;

[Route("[controller]")]
[ApiController]
[Authorize]
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
    
    [HttpPost("registration")]
    public async Task Registration(LoginViewModel req)
    {
        var added = await _userRepositry.AddUser(req.UserName, req.Password);
    }
}