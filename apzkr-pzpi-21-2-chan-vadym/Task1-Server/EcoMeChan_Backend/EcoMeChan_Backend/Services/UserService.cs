// UserService.cs


using AutoMapper;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using EcoMeChan.Services.Interfaces;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using EcoMeChan.ViewModels.Extended;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace EcoMeChan.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserViewModel> CreateAsync(UserCreateViewModel userCreateViewModel)
        {
            // Validate login
            if (string.IsNullOrWhiteSpace(userCreateViewModel.Login))
            {
                throw new ArgumentException("Login is required.");
            }

            var existingUser = await _userRepository.GetByLoginAsync(userCreateViewModel.Login);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with the same login already exists.");
            }

            // Validate email
            if (string.IsNullOrWhiteSpace(userCreateViewModel.Email))
            {
                throw new ArgumentException("Email is required.");
            }

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(userCreateViewModel.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            // Validate password
            if (string.IsNullOrWhiteSpace(userCreateViewModel.Password))
            {
                throw new ArgumentException("Password is required.");
            }

            if (userCreateViewModel.Password.Length < 8)
            {
                throw new ArgumentException("Password must be at least 8 characters long.");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(userCreateViewModel.FirstName))
            {
                throw new ArgumentException("First name is required.");
            }

            if (string.IsNullOrWhiteSpace(userCreateViewModel.LastName))
            {
                throw new ArgumentException("Last name is required.");
            }

            var user = _mapper.Map<User>(userCreateViewModel);
            user.Password = HashPassword(user.Password);
            var createdUser = await _userRepository.CreateAsync(user);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("role", createdUser.Role.ToString());
            return _mapper.Map<UserViewModel>(createdUser);
        }

        public async Task<UserViewModel> AuthenticateAsync(string login, string password)
        {
            var user = await _userRepository.GetByLoginAsync(login);
            if (user == null || !VerifyPassword(password, user.Password))
            {
                throw new InvalidOperationException("Invalid credentials.");
            }
            _httpContextAccessor.HttpContext.Response.Cookies.Append("role", user.Role.ToString());
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserExtendedViewModel> GetAsync(int userId)
        {
            var user = await _userRepository.GetAsync(userId);
            Validate(user);
            return _mapper.Map<UserExtendedViewModel>(user);
        }

        public async Task<UserExtendedViewModel> GetByLoginAsync(string login)
        {
            var user = await _userRepository.GetByLoginAsync(login);
            Validate(user);
            return _mapper.Map<UserExtendedViewModel>(user);
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        public async Task<UserViewModel> UpdateAsync(int userId, UserEditViewModel userEditViewModel)
        {
            var user = await _userRepository.GetAsync(userId);
            Validate(user);

            if (userEditViewModel.Id != userId)
            {
                throw new ArgumentException("The provided user ID does not match the user being updated.");
            }

            _mapper.Map(userEditViewModel, user);
            await _userRepository.UpdateAsync(user);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await _userRepository.GetAsync(userId);
            Validate(user);
            await _userRepository.DeleteAsync(userId);
        }

        public void Logout()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("role");
        }

        private void Validate(User user)
        {
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
        }

        public string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();
            string hashed = HashUsingPbkdf2(password, salt);
            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private string HashUsingPbkdf2(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            if (!TryExtractSaltAndHash(hashedPassword, out byte[]? salt, out string? storedHash))
            {
                return false;
            }

            string hashToVerify = HashUsingPbkdf2(password, salt);
            return hashToVerify == storedHash;
        }

        private bool TryExtractSaltAndHash(string hashedPassword, out byte[]? salt, out string? hash)
        {
            var parts = hashedPassword.Split('.');
            if (parts.Length != 2)
            {
                salt = null;
                hash = null;
                return false;
            }

            salt = Convert.FromBase64String(parts[0]);
            hash = parts[1];
            return true;
        }
    }
}