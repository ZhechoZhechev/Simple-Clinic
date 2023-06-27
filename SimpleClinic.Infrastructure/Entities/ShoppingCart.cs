namespace SimpleClinic.Infrastructure.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Shpping card
/// </summary>
public class ShoppingCart
{
    /// <summary>
    /// Constructor
    /// </summary>
    public ShoppingCart()
    {
        this.ShoppingCartItems = new HashSet<ShoppingCartItem>();
    }

    /// <summary>
    /// Patients identifier
    /// </summary>
    [Required]
    [ForeignKey(nameof(Patient)]
    public string PatientId { get; set; }
    public Patient Patient { get; set; }

    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
}
