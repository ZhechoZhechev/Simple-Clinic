namespace SimpleClinic.Infrastructure.CustomMiddleWares;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using SimpleClinic.Common;
using System;
using System.Threading.Tasks;

public class RoleCreationMiddleware
{
    private readonly RequestDelegate _next;

    public RoleCreationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await CreateRolesAsync(roleManager);

        await _next(context);
    }

    private async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync(RoleNames.AdminRoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(RoleNames.AdminRoleName));
        }
        if (!await roleManager.RoleExistsAsync(RoleNames.DoctorRoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(RoleNames.DoctorRoleName));
        }
        if (!await roleManager.RoleExistsAsync(RoleNames.PatientRoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(RoleNames.PatientRoleName));
        }
    }
}
