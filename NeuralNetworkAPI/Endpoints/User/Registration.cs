using System;
using NeuralNetworkCore;
using NeuralNetworkAPI.Models;

namespace NeuralNetworkAPI.Endpoints.User
{
    [HttpGet("/user/registration")]
    public class Registration : Endpoint<LoginViewModel>
    {
        private readonly IUserRepositroy _userRepositry;
        public Registration(IUserRepositroy userRepositroy)
        {
            _userRepositry = userRepositroy;
        }

        public override async Task HandleAsync(LoginViewModel req, CancellationToken ct)
        {
            var added = await _userRepositry.AddUser(req.UserName, req.Password);
        }
    }
}

