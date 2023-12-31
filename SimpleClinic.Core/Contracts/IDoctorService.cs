﻿namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models;
using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Infrastructure.Entities;

/// <summary>
/// DoctorService interface
/// </summary>
public interface IDoctorService
{
    Task<IEnumerable<FirstThreeDoctorsViewModel>> GetFirstThreeDoctors();

    Task<DoctorDetailsViewModel> DoctorDetails(string id);

    Task<bool> DoctorExistsById(string id);

    Task<DoctorQueryServiceModel> All(string speciality = null, string searchTerm = null, int currentPage = 1, int doctorsPerPage = 1);

    Task<DoctorServiceModel> DetailsForPatient(string id);

}
