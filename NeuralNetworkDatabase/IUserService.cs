using System;
using NeuralNetworkDatabase.Entities;

namespace NeuralNetworkDatabase
{
	public interface IUserService
	{
		Task AddUser(User user);

		Task<User> GetUser(string userName);

		Task DeactivateUser(string userName);

		Task<bool> UserExist(string userName);
	}
}

