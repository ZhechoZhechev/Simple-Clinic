namespace SimpleClinic.Infrastructure.Entities;

using System.ComponentModel.DataAnnotations;

using static Common.Constants.DataConstants.DoctorConstants;

/// <summary>
/// User with role doctor
/// </summary>
public class Doctor : ApplicationUser
{
    /// <summary>
    /// Doctor constructor
    /// </summary>
    public Doctor()
    {
        this.Prescriptions = new HashSet<Prescription>();
        this.Schedules = new HashSet<Schedule>();
        this.Appointments = new HashSet<DoctorAppointment>();
        this.DoctorSpecialties = new HashSet<DoctorSpecialty>();
    }

    /// <summary>
    /// Doctors license number
    /// </summary>
    [Required]
    [StringLength(LicenseNumberMaxLength)]
    public string LicenseNumber { get; set; } = null!;

    /// <summary>
    /// Doctors biography
    /// </summary>
    [Required]
    [StringLength(BiographyMaxLength)]
    public string Biography { get; set; } = null!;

    /// <summary>
    /// Doctors office phone number
    /// </summary>
    [Required]
    [StringLength(OfficePhoneNumberMaxLength)]
    public string OfficePhoneNumber { get; set; } = null!;

    /// <summary>
    /// Doctors price per appointment
    /// </summary>
    [Required]
    public decimal PricePerAppointment { get; set; }

    /// <summary>
    /// Prescription issued by the doctor
    /// </summary>
    public ICollection<Prescription> Prescriptions { get; set; }

    /// <summary>
    /// Doctors schedules
    /// </summary>
    public ICollection<Schedule> Schedules { get; set; }

    /// <summary>
    /// Doctors appointments with patients
    /// </summary>
    public ICollection<DoctorAppointment> Appointments { get; set; }

    /// <summary>
    /// Doctors one or many specialities
    /// </summary>
    public ICollection<DoctorSpecialty> DoctorSpecialties { get; set; }
}

