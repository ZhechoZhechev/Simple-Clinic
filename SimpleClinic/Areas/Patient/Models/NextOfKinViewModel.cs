namespace SimpleClinic.Areas.Patient.Models;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static SimpleClinic.Common.Constants.DataConstants.NextOfKinConstants;

/// <summary>
/// Next of kin view model
/// </summary>
public class NextOfKinViewModel
{

    /// <summary>
    /// Name
    /// </summary>
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; }

    /// <summary>
    /// Phone number
    /// </summary>
    [Required]
    [Phone]
    [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    [Required]
    [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
    public string Address { get; set; }

    /// <summary>
    /// Patients identifier
    /// </summary>
    [Required]
    public string PatientId { get; set; }
}
