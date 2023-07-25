namespace SimpleClinic.Infrastructure.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Common.Constants.DataConstants.NextOfKinConstants;

/// <summary>
/// Patients next of kin
/// </summary>
public class NextOfKin
{
    public NextOfKin()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public string Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    [Required]
    [StringLength(NameMaxLength)]
    public string Name { get; set; }

    /// <summary>
    /// Phone number
    /// </summary>
    [Required]
    [StringLength(PhoneNumberMaxLength)]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    [Required]
    [StringLength(AddressMaxLength)]
    public string Address { get; set; }

    /// <summary>
    /// Patients identifier
    /// </summary>
    [Required]
    [ForeignKey(nameof(Patient))]
    public string PatientId { get; set; }
    public Patient Patient { get; set; }
}