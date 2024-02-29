namespace SimpleClinic.Infrastructure.Entities;

using System.ComponentModel.DataAnnotations;

using static Common.Constants.DataConstants.MedicamentConstants;

/// <summary>
/// Medicaments
/// </summary>
public class Medicament
{
    /// <summary>
    /// Constructor
    /// </summary>
    public Medicament()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public string Id { get; set; }

    /// <summary>
    /// Medicament name
    /// </summary>
    [Required]
    [StringLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Quantity per day in milligrams
    /// </summary>
    [Required]
    public int? QuantityPerDayMilligrams { get; set; }
}