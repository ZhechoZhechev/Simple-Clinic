namespace SimpleClinic.Core.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

using static Common.Constants.DataConstants.DoctorConstants;

public class DoctorRegistrationViewModel : RegisterViewModel
{
  
    /// <summary>
    /// Doctor license number
    /// </summary>
    [Required]
    [DisplayName("License Number")]
    [StringLength(LicenseNumberMaxLength, MinimumLength = LicenseNumberMinLength)]
    public string LicenseNumber { get; set; } = null!;

    /// <summary>
    /// Doctors biography
    /// </summary>
    [Required]
    [StringLength(BiographyMaxLength, MinimumLength = BiographyMinLength)]
    public string Biography { get; set; } = null!;

    /// <summary>
    /// Doctors office telephone number
    /// </summary>
    [Required]
    [Phone]
    [DisplayName("Office Number")]
    [StringLength(OfficePhoneNumberMaxLength, MinimumLength = OfficePhoneNumberMinLength)]
    public string OfficePhoneNumber { get; set; } = null!;

    /// <summary>
    /// Doctors price per appointment
    /// </summary>
    [Required]
    [Range(typeof(decimal), PricePerAppointmentMinValue, PricePerAppointmentMaxValue)]
    public decimal PricePerAppointment { get; set; }
}
