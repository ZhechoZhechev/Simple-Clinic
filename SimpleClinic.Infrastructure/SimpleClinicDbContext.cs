namespace SimpleClinic.Infrastructure;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using SimpleClinic.Infrastructure.Entities;
using System.Reflection;

/// <summary>
/// Aplication database context
/// </summary>
public class SimpleClinicDbContext : IdentityDbContext<ApplicationUser>
{
    /// <summary>
    /// Context constructor
    /// </summary>
    /// <param name="options">options for the context</param>
    public SimpleClinicDbContext(DbContextOptions<SimpleClinicDbContext> options)
        : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<DoctorAppointment> DoctorAppointments { get; set; } = null!;
    public DbSet<DoctorSpeciality> DoctorsSpecialities { get; set; } = null!;
    public DbSet<MedicalHistory> MedicalHistories { get; set; } = null!;
    public DbSet<Medicament> Medicaments { get; set; } = null!;
    public DbSet<NextOfKin> NextOfKins { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Prescription> Prescriptions { get; set; } = null!;
    public DbSet<Schedule> Schedules { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<ServiceAppointment> ServiceAppointments { get; set; } = null!;
    public DbSet<Speciality> Specialities { get; set; } = null!;
    public DbSet<TimeSlot> TimeSlots { get; set; } = null!;

    /// <summary>
    /// Method for modelating entities
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        Assembly configAssembly = Assembly.GetAssembly(typeof(SimpleClinicDbContext)) ??
            Assembly.GetExecutingAssembly();

        builder.ApplyConfigurationsFromAssembly(configAssembly);

        base.OnModelCreating(builder);
    }
}