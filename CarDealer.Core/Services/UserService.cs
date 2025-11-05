using System;
using Model;
using CarDealer.Data;

namespace CarDealer.Core.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        
        public User? AuthenticateUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            User? user = _userRepository.AuthenticateUser(email, password);

            
            if (user != null && user.IsActive)
            {
                _userRepository.UpdateLastLogin(user.UserId);
                return user;
            }

            return null;
        }

        public bool RegisterUser(string email, string password, string firstName, string? lastName = null)
        {
           
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Email and password are required");

            if (password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters");

            
            if (_userRepository.GetUserByEmail(email) != null)
                throw new InvalidOperationException("User with this email already exists");

            User newUser = new User
            {
                Email = email,
                Password = password, // TODO: Hash this!
                Username = email,
                FirstName = firstName,
                LastName = lastName,
                Role = "User",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            return _userRepository.AddUser(newUser) > 0;
        }

        
        public User? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        
        public bool UpdateUser(User user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Email))
                return false;

            return _userRepository.UpdateUser(user);
        }

        
        public bool IsAdmin(User user)
        {
            return user?.Role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) ?? false;
        }
    }
}
