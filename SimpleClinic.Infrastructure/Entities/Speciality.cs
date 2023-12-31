﻿namespace SimpleClinic.Infrastructure.Entities;

using System.ComponentModel.DataAnnotations;

using static Common.Constants.DataConstants.SpecialityConstants;

/// <summary>
/// Each doctor has a speciality, maybe several
/// </summary>
public class Speciality
{
    /// <summary>
    /// Constructor
    /// </summary>
    public Speciality()
    {
        this.Doctors = new HashSet<Doctor>();
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Speciality name
    /// </summary>
    [Required]
    [StringLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Collection of doctors having this speciality
    /// </summary>
    public ICollection<Doctor> Doctors { get; set; }
}
