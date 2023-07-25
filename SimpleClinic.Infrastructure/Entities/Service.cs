namespace SimpleClinic.Infrastructure.Entities;

/// <summary>
/// Clinic service
/// </summary>
public class Service
{
    /// <summary>
    /// Service constructor
    /// </summary>
    public Service()
    {
        this.Id = Guid.NewGuid().ToString();
        this.Schedules = new HashSet<Schedule>();
        this.Appointments = new HashSet<ServiceAppointment>();
    }

    /// <summary>
    /// Identifier
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Name of the service
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Equipment picture
    /// </summary>
    public string EquipmentPicture { get; set; }

    /// <summary>
    /// How much it costs
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Schedule for the service
    /// </summary>
    public ICollection<Schedule> Schedules { get; set; }
    
    /// <summary>
    /// Patient apointments for the service
    /// </summary>
    public ICollection<ServiceAppointment> Appointments { get; set; }
}
