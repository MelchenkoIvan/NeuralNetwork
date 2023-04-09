using System;
using AutoMapper;
using NeuralNetowrkCore.Models;
using NeuralNetworkDatabase;
using NeuralNetworkDatabase.Entities;

namespace NeuralNetowrkCore
{
	public class UserRepository : IUserRepositroy
	{
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

		public UserRepository(IUserService userService, IMapper mapper)
		{
            _userService = userService;
            _mapper = mapper;
		}

        public async Task<bool> AddUser(string username, string password)
        {
            var isExist = await _userService.UserExist(username);
            if (isExist)
                return false;

            var user = new User
            {
                UserName = username,
                Password = password,
                IsActive = true,
                CreationDate = DateTime.Now
            };
            await _userService.AddUser(user);
            return true;
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            if (!(await _userService.UserExist(username)))
                return false;

            var user = await _userService.GetUser(username);
            if (user.Password == password)
                return true;

            return false;
        }

        public async Task<UserDTO?> GetUser(string username)
        {
            if (!(await _userService.UserExist(username)))
                return null;

            var user = await _userService.GetUser(username);

            return _mapper.Map<UserDTO>(user);
        }
    }
}

