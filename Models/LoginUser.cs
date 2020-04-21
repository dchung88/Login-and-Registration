using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegistration.Models
{
    public class LoginUser
    {
        [Key]
        public int UserId {get;set;}

        [EmailAddress(ErrorMessage ="Email is invalid")]
        [Required(ErrorMessage = "Your email address is required to login")]
        public string Email {get;set;}
        
        [Required(ErrorMessage ="Password is needed to login")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
    }
}