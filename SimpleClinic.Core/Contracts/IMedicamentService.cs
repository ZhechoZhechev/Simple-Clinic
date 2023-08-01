using SimpleClinic.Core.Models.DoctorModels;

namespace SimpleClinic.Core.Contracts;

public interface IMedicamentService
{
    Task<List<MedicamentViewModel>> GetAllMedicaments(string searchTerm);
}
