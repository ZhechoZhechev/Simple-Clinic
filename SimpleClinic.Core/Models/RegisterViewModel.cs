using System.ComponentModel.DataAnnotations;

namespace SimpleClinic.Core.Models;

using static Common.Constants.DataConstants.ApplicationUserConstants;

/// <summary>
/// Model used for initial registration
/// </summary>
public class RegisterViewModel
{
    /// <summary>
    /// User email
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    /// <summary>
    /// User password
    /// </summary>
    [Required]
    [Compare(nameof(PasswordRepeat))]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    /// <summary>
    /// User password repeat
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string PasswordRepeat { get; set; } = null!;

    /// <summary>
    /// User first name
    /// </summary>
    [Required]
    [StringLength(FirstAndLastNameMaxLength, MinimumLength = FirstAndLastNameMinLength)]
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// User last name
    /// </summary>
    [Required]
    [StringLength(FirstAndLastNameMaxLength, MinimumLength = FirstAndLastNameMinLength)]
    public string LastName { get; set; } = null!;

    /// <summary>
    /// User home address
    /// </summary>
    [Required]
    [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
    public string Address { get; set; } = null!;
}
