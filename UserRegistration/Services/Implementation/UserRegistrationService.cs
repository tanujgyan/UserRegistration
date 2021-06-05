using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserRegistration.Models;
using UserRegistration.Repository.Contracts;
using UserRegistration.Services.Contracts;

namespace UserRegistration.Services.Implementation
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        

        public UserRegistrationService(IUserRegistrationRepository userRegistrationRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
        }

        public string AddNewUser(UserDetails user)
        {
           
            var u = _userRegistrationRepository.GetCurrentUser(user.UserName);
            Regex regex = new Regex(@"[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]");
            Match match = regex.Match(user.UserAddress.PostalCode);
            if(!match.Success)
            {
                return "Invalid Postal Code";
            }
            if (u == null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _userRegistrationRepository.AddNewUser(user);
                return "User Created";
            }
            else
            {
                return "Username already exists";
            }
            
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
