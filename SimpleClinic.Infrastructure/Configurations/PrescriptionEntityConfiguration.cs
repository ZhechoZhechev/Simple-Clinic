namespace SimpleClinic.Infrastructure.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SimpleClinic.Infrastructure.Entities;

/// <summary>
/// Prescription entity configuration
/// </summary>
public class PrescriptionEntityConfiguration : IEntityTypeConfiguration<Prescription>
{

    /// <summary>
    /// Configure method from IEntityTypeConfiguration interface
    /// </summary>
    /// <param name="builder">builder class</param>
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder
            .HasOne(p => p.Doctor)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(p => p.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(p => p.Patient)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
