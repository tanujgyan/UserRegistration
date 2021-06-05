using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistration.Models;

namespace UserRegistration.Services.Contracts
{
    public interface IUserRegistrationService
    {
        public IEnumerable<UserDetails> GetUsers();
        public void AddNewUser(UserDetails user);
        public UserDetails GetCurrentUser(string userName);
    }
}
