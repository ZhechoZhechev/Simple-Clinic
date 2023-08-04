namespace SimpleClinic.Infrastructure.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SimpleClinic.Infrastructure.Entities;


public class DoctorAppointmentEntityConfiguration : IEntityTypeConfiguration<DoctorAppointment>
{
    public void Configure(EntityTypeBuilder<DoctorAppointment> builder)
    {
        builder.Property(p => p.IsActive)
             .HasDefaultValue(false)
             .IsRequired(true);
    }
}
