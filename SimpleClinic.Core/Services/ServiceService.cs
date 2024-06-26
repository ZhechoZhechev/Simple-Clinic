﻿namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;

using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Infrastructure.Entities;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Core.Models.PatientModels;

public class ServiceService : IServiceService
{
    private readonly SimpleClinicDbContext context;

    public ServiceService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    public async Task<ServiceViewModel> GetServiceForEditing(string id)
    {
        return await context.Services
            .Where(s => s.Id == id)
            .Select(s => new ServiceViewModel
            {
                Name = s.Name,
                EquipmentPicture = s.EquipmentPicture,
                Price = s.Price,
            })
            .FirstAsync();
    }


    public async Task SaveEditedService(ServiceViewModel serviceModel, string id)
    {
        var service = await context.Services
            .FirstAsync(x => x.Id == id);

        service.Name = serviceModel.Name;
        service.EquipmentPicture = serviceModel.EquipmentPicture;
        service.Price = serviceModel.Price;

        await context.SaveChangesAsync();
    }

    public async Task AddServiceAsync(ServiceViewModel serviceModel)
    {
        var service = new Service()
        {
            Name = serviceModel.Name,
            EquipmentPicture = serviceModel.EquipmentPicture,
            Price = serviceModel.Price,
        };

        await context.Services.AddAsync(service);
        await context.SaveChangesAsync();
    }

    public async Task<ServiceQueryServiceModel> All(int currentPage = 1, int servicesPerPage = 1)
    {
        var serviceQuery = context.Services.AsQueryable();

        var services = await serviceQuery
            .Skip((currentPage - 1) * servicesPerPage)
            .Take(servicesPerPage)
            .Select(d => new ServiceServiceModel()
            {
                Id = d.Id,
                Name = d.Name,
                EquipmentPicture = d.EquipmentPicture,
                Price = d.Price.ToString()
            })
            .ToListAsync();

        var totalServices = serviceQuery.Count();

        return new ServiceQueryServiceModel()
        {
            TotalServicesCount = totalServices,
            Services = services
        };
    }

    public async Task<List<AllServicesForScheduleViewModel>> GetAllServicesForSchedule()
    {
        var model = await context.Services
            .Select(s => new AllServicesForScheduleViewModel() 
            {
                Id = s.Id,
                ServiceName = s.Name
            })
            .ToListAsync();

        return model;
    }

    public async Task<List<FirstThreeServicesViewModel>> GetFirstThreeServices()
    {
        var model = await context.Services.Take(3).
            Select(s => new FirstThreeServicesViewModel()
            {
                Name = s.Name,
                Price = s.Price,
                EquipmentPicture = s.EquipmentPicture
            })
            .ToListAsync();

        return model;
    }
}
