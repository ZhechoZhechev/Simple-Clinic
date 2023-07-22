namespace SimpleClinic.Core.CustomValidationAttributes;

using System.ComponentModel.DataAnnotations;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models;

/// <summary>
/// Attribute that check if speciality already exists
/// </summary>
public class UniqueCustomSpeciality : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var viewModel = validationContext.ObjectInstance as DoctorRegistrationViewModel;
        var customSpeciality = value as string;

        var serviceProvider = validationContext.GetService(typeof(IServiceProvider)) as IServiceProvider;
        var specialityService = serviceProvider.GetService(typeof(ISpecialityService)) as ISpecialityService;

        viewModel.Specialities = specialityService.GetAllSpecialities().Result;

        var isDuplicate = viewModel.Specialities.Any(s => s.Name == customSpeciality);

        if (isDuplicate)
        {
            return new ValidationResult("Custom speciality already exists in the list.");
        }

        return ValidationResult.Success;
    }
}
