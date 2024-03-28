namespace SimpleClinic.Infrastructure.Entities;

using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

using static Common.Constants.DataConstants.ApplicationUserConstants;

/// <summary>
/// Extendint the default identity user class
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Application user first name
    /// </summary>
    [Required]
    [StringLength(FirstAndLastNameMaxLength)]
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Application user first name
    /// </summary>
    [Required]
    [StringLength(FirstAndLastNameMaxLength)]
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Patients address
    /// </summary>
    [Required]
    [StringLength(AddressMaxLength)]
    [Display(Name ="Address/Working Place")]
    public string Address { get; set; } = null!;
}
