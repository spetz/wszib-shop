using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Core.Repositories;
using System;

namespace Shop.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Login(string email, string password)
        {
            var user = _userRepository.Get(email);
            if (user == null)
            {
                throw new Exception($"User '{email}' was not found.");
            }
            if (user.Password != password)
            {
                throw new Exception("Invalid password.");
            }
        }

        public void Register(string email, string password, RoleDto role)
        {
            var user = _userRepository.Get(email);
            if (user != null)
            {
                throw new Exception($"User '{email}' already exists.");
            }
            var userRole = (Role)Enum.Parse(typeof(Role), role.ToString(), true);
            user = new User(email, password, userRole);
            _userRepository.Add(user);
        }
    }
}
