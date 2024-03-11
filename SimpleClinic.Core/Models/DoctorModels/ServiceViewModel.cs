namespace SimpleClinic.Core.Models.DoctorModels;

using System.ComponentModel.DataAnnotations;
using static SimpleClinic.Common.Constants.DataConstants.ServiceConstants;

public class ServiceViewModel
{
    /// <summary>
    /// Name of the service
    /// </summary>
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Equipment picture
    /// </summary>
    [Required]
    [StringLength(PictureMaxLength, MinimumLength = PictureMinLength)]
    public string EquipmentPicture { get; set; } = null!;

    /// <summary>
    /// How much it costs
    /// </summary>
    [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
    public decimal Price { get; set; }
}
