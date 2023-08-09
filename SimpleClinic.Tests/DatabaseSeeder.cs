namespace SimpleClinic.Tests;

using Microsoft.AspNetCore.Identity;

using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;


public class DatabaseSeeder
{
    public static Patient patient;
    public static Service service;
    public static List<Doctor> doctors;
    public static List<Service> services;

    public static void SeedDatabase(SimpleClinicDbContext context)
    {

        services = new List<Service>()
        {
            new Service()
            {
            Id = "TestServiceId1",
            Name = "TestServiceName1",
            EquipmentPicture = "some url1",
            Price = 9999
            },
            new Service()
            {
            Id = "TestServiceId2",
            Name = "TestServiceName2",
            EquipmentPicture = "some url2",
            Price = 9999
            }
        };
        context.Services.AddRange(services);

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

        doctors = new List<Doctor>()
        {
            new Doctor()
            {
                UserName = "Joro",
                NormalizedUserName = "JORO",
                Email = "Joro@MAIL.com",
                NormalizedEmail = "JORO@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
                SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
                TwoFactorEnabled = false,
                FirstName = "Joro",
                LastName = "Jorov",
                Address = "Test address",
                LicenseNumber = "23152131",
                Biography = "realy short bio",
                OfficePhoneNumber = "2311334121",
                PricePerAppointment = 400,
                ProfilePictureFilename = "joro.jpg",
                SpecialityId = 3
            },
            new Doctor()
            {
                UserName = "pesho",
                NormalizedUserName = "PESHO",
                Email = "pesho@MAIL.com",
                NormalizedEmail = "PESHO@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
                SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
                TwoFactorEnabled = false,
                FirstName = "pesho",
                LastName = "peshev",
                Address = "Test address",
                LicenseNumber = "23152131",
                Biography = "realy short bio",
                OfficePhoneNumber = "2311334121",
                PricePerAppointment = 400,
                ProfilePictureFilename = "pesho.jpg",
                SpecialityId = 2
            },
            new Doctor()
            {
                UserName = "gosho",
                NormalizedUserName = "GOSHO",
                Email = "gosho@MAIL.com",
                NormalizedEmail = "GOSHO@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
                SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
                TwoFactorEnabled = false,
                FirstName = "gosho",
                LastName = "goshev",
                Address = "Test address",
                LicenseNumber = "23152131",
                Biography = "realy short bio",
                OfficePhoneNumber = "2311334121",
                PricePerAppointment = 400,
                ProfilePictureFilename = "gosho.jpg",
                SpecialityId = 1
            }
        };
        ;
        context.Doctors.AddRange(doctors);

        var userRole = new IdentityUserRole<string>
        {
            UserId = patient.Id,
            RoleId = "TestRoleId"
        };

        var timeSlot = new TimeSlot()
        {
            Id = "TestSlotId",
            StartTime = new DateTime(2023, 8, 9, 8, 0, 0),
            EndTime = new DateTime(2023, 8, 9, 9, 0, 0),
            IsAvailable = false
        };
        context.TimeSlots.Add(timeSlot);

        var docAppointment = new DoctorAppointment()
        {
            Id = "TestAppointmentId",
            DoctorId = "TestDoctor",
            TimeSlotId = timeSlot.Id,
            PatientId = patient.Id,
            BookingDateTime = new DateTime(2023, 8, 9),
            IsActive = true
        };
        var docAppointment1 = new DoctorAppointment()
        {
            Id = "TestAppointmentId1",
            DoctorId = doctors.First().Id,
            TimeSlotId = "TestSlotId",
            PatientId = patient.Id,
            BookingDateTime = new DateTime(2023, 9, 9),
            IsActive = true
        };
        context.DoctorAppointments.Add(docAppointment);
        context.DoctorAppointments.Add(docAppointment1);

        service = new Service()
        {
            Id = "TestServiceId",
            Name = "TestServiceName",
            EquipmentPicture = "some url",
            Price = 10000
        };
        context.Services.Add(service);

        var serviceAppointment = new ServiceAppointment()
        {
            Id = "TestServiceAppointmentId",
            ServiceId = "TestServiceId",
            TimeSlotId = timeSlot.Id,
            PatientId = patient.Id,
            BookingDateTime = new DateTime(2023, 8, 9),
            IsActive = true
        };
        context.ServiceAppointments.Add(serviceAppointment);


        context.UserRoles.Add(userRole);
        context.Patients.Add(patient);
        context.SaveChanges();
    }
}
