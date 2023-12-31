﻿namespace SimpleClinic.Infrastructure.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



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
    /// If next of kin and medical history forms are completed
    /// </summary>
    [Required]
    public bool FormsCompleted { get; set; }

    /// <summary>
    /// Patients medical history
    /// </summary>
    [ForeignKey(nameof(MedicalHistory))]
    public string MedicalHistoryId { get; set; }
    public virtual MedicalHistory MedicalHistory { get; set; } = null!;

    /// <summary>
    /// Patients next of kind details
    /// </summary>
    [ForeignKey(nameof(NextOfKin))]
    public string NextOfKinId { get; set; }
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
