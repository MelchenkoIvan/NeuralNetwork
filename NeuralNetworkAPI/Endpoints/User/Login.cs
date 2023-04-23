using FastEndpoints.Security;
using Microsoft.AspNetCore.Authorization;
using NeuralNetworkCore;
using NeuralNetworkAPI.Models;

namespace NeuralNetworkAPI.Endpoints.User
{
    [HttpPost("/user/login")]
	[AllowAnonymous]
    public class Login : Endpoint<LoginViewModel, string>
    {
        private readonly IUserRepositroy _userRepositry;
        
        public Login(IUserRepositroy userRepositroy)
		{
            _userRepositry = userRepositroy;
        }

        public override async Task<string> ExecuteAsync(LoginViewModel req, CancellationToken ct)
        {

            var isAuthenticated = await _userRepositry.Authenticate(req.UserName, req.Password);

            if (isAuthenticated)
            {
                await CookieAuth.SignInAsync(u =>
                {
                    u.Claims.Add(new("usr", req.UserName));
                });
                return req.UserName;
            }

            return string.Empty;
        }

    }
}

