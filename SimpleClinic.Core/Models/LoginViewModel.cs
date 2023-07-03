namespace SimpleClinic.Core.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Log in model
/// </summary>
public class LoginViewModel
{
    /// <summary>
    /// Provide email to log in
    /// </summary>
    [Required]
    [EmailAddress]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Provide password to log in
    /// </summary>
    [Required]
    [DisplayName("Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    /// <summary>
    /// The return link when you authenticate yourself, if any
    /// </summary>
    [UIHint("hidden")]
    public string? ReturnUrl { get; set; }
}
