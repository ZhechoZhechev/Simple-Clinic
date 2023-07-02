namespace SimpleClinic.Core.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;

    [Required]
    [DisplayName("Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RegisterSuccess { get; set; }
}
