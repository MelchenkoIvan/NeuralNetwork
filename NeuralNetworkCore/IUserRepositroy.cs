using System;
using NeuralNetworkCore.Models;

namespace NeuralNetworkCore
{
	public interface IUserRepositroy
	{
		UserDTO CurrentUser { get; }
		Task<UserDTO?> Authenticate(string username, string password);

        Task<UserDTO?> GetUser(string username);

        Task<bool> AddUser(string username, string password);

    }
}

