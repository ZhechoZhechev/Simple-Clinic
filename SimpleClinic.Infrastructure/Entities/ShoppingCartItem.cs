namespace SimpleClinic.Infrastructure.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// Items you can add to shopping cart
/// </summary>
public class ShoppingCartItem
{
    public ShoppingCartItem()
    {
        this.Id = new Guid().ToString();
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public string Id { get; set; }

    /// <summary>
    /// Service identifier
    /// </summary>
    [ForeignKey(nameof(ServiceAppointment))]
    public int ServiceAppointmentId { get; set; }
    public ServiceAppointment ServiceAppointment { get; set; }

    /// <summary>
    /// Doctor identifier
    /// </summary>
    [ForeignKey(nameof(DoctorAppointment))]
    public int DoctorAppointmentId { get; set; }
    public DoctorAppointment DoctorAppointment { get; set; }

    /// <summary>
    /// Shopping cart identifier
    /// </summary>
    [Required]
    [ForeignKey(nameof(ShoppingCart))]
    public string ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
}

