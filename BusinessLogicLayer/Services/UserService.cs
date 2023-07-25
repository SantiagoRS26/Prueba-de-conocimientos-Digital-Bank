using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interface;
using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        public UserService() { }

        public UserService(IGenericRepository<User> userService)
        {
            _userRepository = userService;
        }
        public async Task<bool> AddUser(User user)
        {
            return await _userRepository.Add(user);
        }

        public async Task<bool> DeleteUser(string userId)
        {
            return await _userRepository.Delete(userId);
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            return users;
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _userRepository.GetById(userId);
        }

        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.Update(user);
        }
    }
}
