namespace SimpleClinic.Infrastructure.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SimpleClinic.Infrastructure.Entities;


public class ServiceAppointmentEntityConfiguration : IEntityTypeConfiguration<ServiceAppointment>
{
    public void Configure(EntityTypeBuilder<ServiceAppointment> builder)
    {
        builder.Property(p => p.IsActive)
             .HasDefaultValue(false)
             .IsRequired(true);
    }
}
