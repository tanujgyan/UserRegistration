using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistration.Models;
using UserRegistration.Repository.Contracts;
using UserRegistration.Services.Contracts;

namespace UserRegistration.Services.Implementation
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IPasswordEncryption _passwordEncryption;

        public UserRegistrationService(IUserRegistrationRepository userRegistrationRepository, IPasswordEncryption passwordEncryption)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _passwordEncryption = passwordEncryption;
        }

        public void AddNewUser(UserDetails user)
        {
            user.Password = _passwordEncryption.HashPassword(user.Password);
            _userRegistrationRepository.AddNewUser(user);
        }

        public IEnumerable<UserDetails> GetUsers()
        {
            return _userRegistrationRepository.GetUsers();
        }

        public UserDetails GetCurrentUser(string userName)
        {
            return _userRegistrationRepository.GetCurrentUser(userName);
        }
    }
}
