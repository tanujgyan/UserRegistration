using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistration.Models;
using UserRegistration.Repository.Contracts;

namespace UserRegistration.Repository.Implementation
{
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public UserRegistrationRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddNewUser(UserDetails user)
        {
            _dbContext.UserDetails.Add(user);
            _dbContext.SaveChanges();
        }

        public UserDetails GetCurrentUser(string userName)
        {
            return _dbContext.UserDetails
                     .Include(a => a.UserAddress)
                     .FirstOrDefault(x => x.UserName == userName);
        }

        public IEnumerable<UserDetails> GetUsers()
        {
            return _dbContext.UserDetails.Include(a => a.UserAddress).ToList();
        }
    }
}
