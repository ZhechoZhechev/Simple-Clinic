using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleClinic.Infrastructure.Entities;

/// <summary>
/// Many to many table doctors and their specialities
/// </summary>
public class DoctorSpeciality
{
    /// <summary>
    /// DoctorId
    /// </summary>
    [Required]
    [ForeignKey(nameof(Doctor))]
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    /// <summary>
    /// SpecialityId
    /// </summary>
    [Required]
    [ForeignKey(nameof(Speciality))]
    public int SpecialityId { get; set; }
    public Speciality Speciality { get; set; }
}