namespace SimpleClinic.Infrastructure.Entities;

using System.ComponentModel.DataAnnotations;



/// <summary>
/// User with role patient
/// </summary>
public class Patient : ApplicationUser
{
    /// <summary>
    /// Patient constructor
    /// </summary>
    public Patient()
    {
        this.Prescriptions = new HashSet<Prescription>();
        this.DoctorAppointments = new HashSet<DoctorAppointment>();
        this.ServiceAppointments = new HashSet<ServiceAppointment>();
    }

    /// <summary>
    /// If patient has health insurance
    /// </summary>
    [Required]
    public bool HasInsurance { get; set; }

    /// <summary>
    /// Date of birth
    /// </summary>
    [Required]
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Patients medical history
    /// </summary>
    [Required]
    public int MedicalHistoryId { get; set; }
    public virtual MedicalHistory MedicalHistory { get; set; } = null!;

    /// <summary>
    /// Patients next of kind details
    /// </summary>
    [Required]
    public int NextOfKinId { get; set; }
    public virtual NextOfKin NextOfKin { get; set; } = null!;

    /// <summary>
    /// Patients prescriptions
    /// </summary>
    public virtual ICollection<Prescription> Prescriptions { get; set; }

    /// <summary>
    /// Patients doctor appointments
    /// </summary>
    public virtual ICollection<DoctorAppointment> DoctorAppointments { get; set; }

    /// <summary>
    /// Patients service appointments
    /// </summary>
    public virtual ICollection<ServiceAppointment> ServiceAppointments { get; set; }

}
