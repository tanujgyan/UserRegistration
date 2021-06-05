using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistration.Models
{
    public class UserAddress
    {
        [Key]
        public int AddressId {get;set;}
        public string Suite { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string StreetNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        //[RegularExpression(@"[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]",
        //ErrorMessage = "Enter a valid postal code")] 
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }
        public UserDetails UserDetails { get; set; }


    }
}
