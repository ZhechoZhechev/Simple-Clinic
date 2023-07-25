namespace SimpleClinic.Infrastructure.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using SimpleClinic.Infrastructure.Entities;

/// <summary>
/// Patient entity configuration
/// </summary>
public class PatientEntityConfiguration : IEntityTypeConfiguration<Patient>
{

    /// <summary>
    /// Configure method from IEntityTypeConfiguration interface
    /// </summary>
    /// <param name="builder">builder class</param>
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder
            .HasOne(u => u.MedicalHistory)
            .WithOne(m => m.Patient)
            .HasForeignKey<MedicalHistory>(m => m.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(x => x.FormsCompleted)
            .HasDefaultValue(false);
    }
}
