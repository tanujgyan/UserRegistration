using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistration.Services.Contracts
{
    public interface IPasswordEncryption
    {
        public string HashPassword(string password);
    }
}
