namespace SimpleClinic.Core.Models.DoctorModels;

using System.ComponentModel.DataAnnotations;

using static SimpleClinic.Common.Constants.DataConstants.MedicamentConstants;

public class MedicamentViewModel
{
    public string Id { get; set; }

    /// <summary>
    /// Medicament name
    /// </summary>
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Quantity per day in milligrams
    /// </summary>
    [Required]
    public int QuantityPerDayMilligrams { get; set; }
}
