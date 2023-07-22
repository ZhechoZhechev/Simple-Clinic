namespace SimpleClinic.Core.Services;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure;


public class DoctorService : IDoctorService
{
    private readonly SimpleClinicDbContext context;
    private readonly string directoryPath;
    private readonly IWebHostEnvironment webHostEnvironment;


    public DoctorService(SimpleClinicDbContext context,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
    {
        this.context = context;
        this.directoryPath = configuration["UpploadSettings:ImageDir"];
        this.webHostEnvironment = webHostEnvironment;
    }
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

