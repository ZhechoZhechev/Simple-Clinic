namespace SimpleClinic.Core.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

using SimpleClinic.Common;

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
    [DisplayName("Repeat Password")]
    public string PasswordRepeat { get; set; } = null!;

    /// <summary>
    /// User first name
    /// </summary>
    [Required]
    [DisplayName("Firist Name")]
    [StringLength(FirstAndLastNameMaxLength, MinimumLength = FirstAndLastNameMinLength)]
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// User last name
    /// </summary>
    [Required]
    [DisplayName("Last Name")]
    [StringLength(FirstAndLastNameMaxLength, MinimumLength = FirstAndLastNameMinLength)]
    public string LastName { get; set; } = null!;

    /// <summary>
    /// User home address
    /// </summary>
    [Required]
    [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
    public string Address { get; set; } = null!;

    [Required]
    public string SelectedRole { get; set; } = null!;

    [Required]
    public SelectList AvailableRoles => GetAvailableRoles();

    private SelectList GetAvailableRoles()
    {
        var roles = typeof(RoleNames).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(string))
            .Select(f => f.GetValue(null) as string)
            .Where(r => r != RoleNames.AdminRoleName)
            .ToList();

        return new SelectList(roles, SelectedRole);
    }
}
