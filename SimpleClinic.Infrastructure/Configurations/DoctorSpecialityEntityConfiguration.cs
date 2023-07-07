namespace SimpleClinic.Infrastructure.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SimpleClinic.Infrastructure.Entities;

/// <summary>
/// DoctorSpeciality entity configuration
/// </summary>
public class DoctorSpecialityEntityConfiguration : IEntityTypeConfiguration<DoctorSpeciality>
{
    /// <summary>
    /// Configure method from IEntityTypeConfiguration interface
    /// </summary>
    /// <param name="builder">builder class</param>
    public void Configure(EntityTypeBuilder<DoctorSpeciality> builder)
    {
        builder
            .HasKey(pk => new {pk.DoctorId, pk.SpecialityId});
    }
}
