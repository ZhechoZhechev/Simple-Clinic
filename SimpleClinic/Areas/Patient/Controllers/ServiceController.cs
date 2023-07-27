﻿namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Common;

[Authorize(Roles = RoleNames.PatientRoleName)]
[Area("Patient")]
public class ServiceController : Controller
{
    private readonly IServiceService serviceService;

    public ServiceController(IServiceService serviceService)
    {
        this.serviceService = serviceService;
    }

    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllServicesPaginationModel queryModel)
    {
        var queryResult = await serviceService.All(queryModel.CurrentPage, queryModel.ServicesPerPage);

        queryModel.TotalServicesCount = queryResult.TotalServicesCount;
        queryModel.Services = queryResult.Services;

        return View(queryModel);
    }

    }
