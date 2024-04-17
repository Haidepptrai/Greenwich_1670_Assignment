using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FPT_Pharmacy_Assignment.Areas.Identity.Data;

// Add profile data for application users by adding properties to the CustomUser class
public class CustomUser : IdentityUser
{
    [PersonalData]
    [Required(ErrorMessage = "Full name is required.")]
    public required string FullName { get; set; }

    [PersonalData]
    [StringLength(255)]
    public string? Address { get; set; }

    [PersonalData]
    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public int? Phone { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateTime? DateOfBirth { get; set; }
}

