using System;
using NeuralNetowrkCore.Models;

namespace NeuralNetowrkCore
{
	public interface IUserRepositroy
	{
        Task<bool> Authenticate(string username, string password);

        Task<UserDTO?> GetUser(string username);

        Task<bool> AddUser(string username, string password);

    }
}

