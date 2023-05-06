using AutoMapper;
using Microsoft.AspNetCore.Http;
using NeuralNetworkCore.Models;
using NeuralNetworkDatabase;
using NeuralNetworkDatabase.Entities;

namespace NeuralNetworkCore
{
	public class UserRepository : IUserRepositroy
	{
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
    
        public UserDTO CurrentUser
        {
            get
            {
                var user = (UserDTO)_httpContextAccessor.HttpContext.Items["User"];
                if (user == null)
                    throw new Exception("Can not find current user!");
                return user;
            }
        }

        public UserRepository(IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
            _userService = userService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<UserDTO?> Authenticate(string username, string password)
        {
            if (!(await _userService.UserExist(username)))
                return null;

            var user = await _userService.GetUser(username);
            if (user.Password == password)
                return _mapper.Map<UserDTO>(user);

            return null;
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

