using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleClinic.Infrastructure.Entities;

namespace SimpleClinic.Infrastructure.Configuration;

public class DoctorAppointmentConfiguration : IEntityTypeConfiguration<DoctorAppointment>
{
    public void Configure(EntityTypeBuilder<DoctorAppointment> builder)
    {
    }
}
