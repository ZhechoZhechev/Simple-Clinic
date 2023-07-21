using System.ComponentModel.DataAnnotations;

namespace SimpleClinic.Core.Models
{
    /// <summary>
    /// Specialities view model
    /// </summary>
    public class SpecialityViewModel
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>

        public string Name { get; set; } = null!;
    }
}
