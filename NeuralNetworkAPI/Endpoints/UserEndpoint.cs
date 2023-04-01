using System;
using Microsoft.AspNetCore.Authorization;

namespace NeuralNetworkAPI.Endpoints
{
	[HttpPost("/user")]
	[AllowAnonymous]
	public class UserEndpoint : Endpoint<User>
	{
        public override async Task HandleAsync(User req, CancellationToken ct)
        {
            var name = req.Username;

        }
    }

	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}

