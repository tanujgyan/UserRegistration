using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistration.Models
{
    public class UserDetails
    {
        [Key]
        public long UserId { get; set; }
        [Required]
       
        public string UserName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{10}$",
         ErrorMessage = "Password must be a combination of uppercase, lowercase and digits and should have exactly 10 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; }
        [ForeignKey("AddressId")]
        public virtual UserAddress UserAddress { get; set; }

    }
}
