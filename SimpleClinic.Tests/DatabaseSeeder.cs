namespace SimpleClinic.Tests;

using Microsoft.AspNetCore.Identity;

using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;


public class DatabaseSeeder
{
    public static Patient patient;

    public static void SeedDatabase(SimpleClinicDbContext context)
    {
        patient = new Patient()
        {
            UserName = "Pesho",
            NormalizedUserName = "PESHO",
            Email = "pesho@MAIL.com",
            NormalizedEmail = "PESHO@MAIL.COM",
            EmailConfirmed = true,
            PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
            ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
            SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
            TwoFactorEnabled = false,
            FirstName = "Pesho",
            LastName = "Petrov",
            Address = "Test address",
            HasInsurance = true,
            DateOfBirth = new DateTime(1982, 01, 29),
            FormsCompleted = false,
            MedicalHistoryId = "c84702b4-c3fd-4db4-8699-0288211f677d\r\n",
            NextOfKinId = "33f29002-010f-41c9-bd19-fce963be36a0"

        };
        var userRole = new IdentityUserRole<string>
        {
            UserId = patient.Id,
            RoleId = "TestRoleId"
        };

        context.UserRoles.Add(userRole);
        context.Patients.Add(patient);
        context.SaveChanges();
    }
}
