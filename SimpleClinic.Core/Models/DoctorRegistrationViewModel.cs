namespace SimpleClinic.Core.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using static Common.Constants.DataConstants.DoctorConstants;

/// <summary>
/// Doctor registration view model
/// </summary>
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
    [DisplayName("Short Bio")]
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
    [DisplayName("Price Per Appointment")]
    [Range(typeof(decimal), PricePerAppointmentMinValue, PricePerAppointmentMaxValue)]
    public decimal PricePerAppointment { get; set; }

    /// <summary>
    /// Doctors picture
    /// </summary>
    [FromForm]
    public IFormFile ProfilePicture { get; set; }

}
