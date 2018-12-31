using Nagarro.Gitter.Shared.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nagarro.Gitter.Shared.Models
{
    public class UserModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]+", ErrorMessage = "Name only contain alphabetic and space")]
        public string UserName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password should be between 8 and 20 characters")]
        public string Password { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password should be between 8 and 20 characters")]
        [ConfirmPassword]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter a valid Mobile Number")]
        public string ContactNumber { get; set; }

        public string Image { get; set; }

        public string Country { get; set; }
    }
}