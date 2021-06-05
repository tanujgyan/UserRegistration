using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistration.Models
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
       
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<UserDetails> UserAddress { get; set; }
    }
}
