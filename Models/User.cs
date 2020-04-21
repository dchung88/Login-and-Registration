using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegistration.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required]
        [MinLength(2)]
        public string FirstName {get;set;}

        [Required(ErrorMessage = "This field is required")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string LastName {get;set;}

        [EmailAddress]
        [Required(ErrorMessage = "Valid email is required")]
        public string Email {get;set;}

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required")]
        [MinLength(8, ErrorMessage = "Name must be at least 8 characters long")]
        public string Password {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}

    }
}