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
        this.Id = new Guid().ToString();
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
    public string Name { get; set; } //- a string with min length 3 and max length 20 (required)

    /// <summary>
    /// Equipment picture
    /// </summary>
    public string EquipmentPicture { get; set; } //- a string with min length 5 and max length 500

    /// <summary>
    /// How much it costs
    /// </summary>
    public decimal Price { get; set; } //- number from 1 to 99 999(required)

    /// <summary>
    /// Schedule for the service
    /// </summary>
    public ICollection<Schedule> Schedules { get; set; }
    
    /// <summary>
    /// Patient apointments for the service
    /// </summary>
    public ICollection<ServiceAppointment> Appointments { get; set; }
}
