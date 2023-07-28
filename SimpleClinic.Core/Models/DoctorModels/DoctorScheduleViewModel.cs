using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SimpleClinic.Core.Models.DoctorModels;

public class DoctorScheduleViewModel
{
    public string? DoctorId { get; set; }

    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    public List<DayScheduleViewModel> Days { get; set; }
}
