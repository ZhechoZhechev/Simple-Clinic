namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure;


public class DoctorService : IDoctorService
{
    private readonly SimpleClinicDbContext context;


    public DoctorService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Checks if doctors exists
    /// </summary>
    /// <param name="id">identifier to check</param>
    /// <returns></returns>
    public async Task<bool> DoctorExistsById(string id) 
    {
        var result = await context.Doctors.FindAsync(id);

        if (result != null)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns the details of a doctor by id
    /// </summary>
    /// <param name="id">identifier</param>
    /// <returns></returns>
    public async Task<DoctorDetailsViewModel> DoctorDetails(string id)
    {
        var doctor = await context.Doctors
            .Include(s => s.Speciality)
            .FirstOrDefaultAsync(d => d.Id == id);

            var model = new DoctorDetailsViewModel() 
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                ProfilePictureFilename = doctor.ProfilePictureFilename,
                Speciality = doctor.Speciality.Name,
                PricePerHour = doctor.PricePerAppointment.ToString(),
                ShortBio = doctor.Biography
            };

        return model;
    }

    /// <summary>
    /// Get first 3 saved doctors
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<FirstThreeDoctorsViewModel>> GetFirstThreeDoctors()
    {
        var model = await context.Doctors.Take(3)
            .Select(d => new FirstThreeDoctorsViewModel()
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                ProfilePictureFilename = d.ProfilePictureFilename,
                Speciality = d.Speciality.Name
            })
            .ToListAsync();

        return model;
    }
}

