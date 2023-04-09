using System;
using NeuralNetworkDatabase.Entities;

namespace NeuralNetworkDatabase
{
    public class UserService : IUserService
    {
        protected readonly NeuralNetworkDbContext _dbContext;

        public UserService(NeuralNetworkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(User user) => await _dbContext.Users.AddAsync(user);
        
        public Task DeactivateUser(string userName)
        {
            var user = _dbContext.Users.Single(x => x.UserName.ToLower() == userName.ToLower());
            user.IsActive = false;
            _dbContext.Update(user);

            return Task.CompletedTask;
        }

        public Task<User> GetUser(string userName)
        {
            var user = _dbContext.Users.Single(x => x.UserName.ToLower() == userName.ToLower() && x.IsActive);
            return Task.FromResult(user);
        }

        public Task<bool> UserExist(string userName) {

            var exist = _dbContext.Users.Any(x => x.UserName.ToLower() == userName.ToLower());

            return Task.FromResult(exist);
        }
    }
}

