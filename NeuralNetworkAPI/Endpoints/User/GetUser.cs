using System;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using NeuralNetworkCore;
using NeuralNetworkCore.Models;

namespace NeuralNetworkAPI.Endpoints.User
{
    [HttpGet("/user/{userName}")]
    public class UserEndpoint : EndpointWithoutRequest<UserDTO>
    {
        private readonly IUserRepositroy _userRepositry;
        public UserEndpoint(IUserRepositroy userRepositroy)
        {
            _userRepositry = userRepositroy;
        }

        public override async Task<UserDTO> ExecuteAsync(CancellationToken ct)
        {
            var userName = Route<string>("userName");
            var user = await _userRepositry.GetUser(userName!);
            return user;
        }
    }
}

