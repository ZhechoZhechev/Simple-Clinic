namespace SimpleClinic.Infrastructure.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleClinic.Infrastructure.Entities;

/// <summary>
/// Configuration of doctor entity
/// </summary>
public class DoctorEntityConfiguration : IEntityTypeConfiguration<Doctor>
{
    /// <summary>
    /// Configure method from IEntityTypeConfiguration interface
    /// </summary>
    /// <param name="builder">builder class</param>
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder
            .HasMany(p => p.Prescriptions)
            .WithOne(d => d.Doctor)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
