using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleClinic.Infrastructure.Entities
{
    /// <summary>
    /// Appointment for a service
    /// </summary>
    public class ServiceAppointment
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceAppointment()
        {
            this.Id = new Guid().ToString();
        }

        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Identifier of the service
        /// </summary>
        [Required]
        [ForeignKey(nameof(Service))]
        public string ServiceId { get; set; }
        public Service Service { get; set; }

        /// <summary>
        /// Timeslot for the appointment
        /// </summary>
        [Required]
        [ForeignKey(nameof(TimeSlot))]
        public string TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }

        /// <summary>
        /// When it is made
        /// </summary>
        [Required]
        public DateTime BookingDateTime { get; set; }
    }
}