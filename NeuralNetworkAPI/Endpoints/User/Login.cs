using System;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using NeuralNetowrkCore;
using NeuralNetowrkCore.Models;
using NeuralNetworkAPI.Models;

namespace NeuralNetworkAPI.Endpoints.User
{
    [HttpGet("/user/login")]
	[AllowAnonymous]
    public class Login : Endpoint<LoginViewModel>
    {
        private readonly IUserRepositroy _userRepositry;
        
        public Login(IUserRepositroy userRepositroy)
		{
            _userRepositry = userRepositroy;
        }

        public override async Task HandleAsync(LoginViewModel req, CancellationToken ct)
        {

            var isAuthentificated = await _userRepositry.Authenticate(req.UserName, req.Password);

            if (isAuthentificated)
            {
                await CookieAuth.SignInAsync(u =>
                {
                    u.Claims.Add(new("usr", req.UserName));
                });
            }
        }

    }
}

