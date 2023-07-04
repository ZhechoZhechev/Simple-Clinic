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
        this.Id = new Guid().ToString();
        this.ShoppingCartItems = new HashSet<ShoppingCartItem>();
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public string Id { get; set; }

    /// <summary>
    /// Patients identifier
    /// </summary>
    [Required]
    [ForeignKey(nameof(Patient))]
    public string PatientId { get; set; }
    public Patient Patient { get; set; }

    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
}
