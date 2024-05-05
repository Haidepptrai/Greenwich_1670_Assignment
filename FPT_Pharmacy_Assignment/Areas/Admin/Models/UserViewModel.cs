using System;
using System.ComponentModel.DataAnnotations;

public class UsersViewModel
{
    public string Id { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Role is required")]
    public string Role { get; set; }

    [Required(ErrorMessage = "Full Name is required")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Date of Birth is required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Phone Number is required")]
    [RegularExpression(@"^(\+[0-9]{1,3})?[0-9]{10}$", ErrorMessage = "Invalid Phone Number")]
    public string PhoneNumber { get; set; }
}
