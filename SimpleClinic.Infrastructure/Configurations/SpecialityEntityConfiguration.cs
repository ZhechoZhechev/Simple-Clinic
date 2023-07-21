namespace SimpleClinic.Infrastructure.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SimpleClinic.Infrastructure.Entities;

/// <summary>
/// Configuration of speciality entity
/// </summary>
public class SpecialityEntityConfiguration : IEntityTypeConfiguration<Speciality>
{
    /// <summary>
    /// Configure method from IEntityTypeConfiguration interface
    /// </summary>
    /// <param name="builder">builder class</param>
    public void Configure(EntityTypeBuilder<Speciality> builder)
    {
        builder.HasData(CreateSpecialities());


    }

    private Speciality[] CreateSpecialities()
    {
        var specialities = new Speciality[]
        {
            new Speciality
            {
                Id = 1,
                Name = "Surgery"
            },

            new Speciality
            {
                Id = 2,
                Name = "Pediatrics"
            },

            new Speciality
            {
                Id = 3,
                Name = "Family medicine"
            },

            new Speciality
            {
                Id = 4,
                Name = "Cardiology"
            },

            new Speciality
            {
                Id = 5,
                Name = "Gynaecology"
            },

            new Speciality
            {
                Id = 6,
                Name = "Dermatology"
            },

            new Speciality 
            {
                Id = 7,
                Name = "Phycology"
            },

            new Speciality
            {
                Id = 8,
                Name = "Neurology"
            },

            new Speciality 
            {
                Id = 9,
                Name = "Ophthalmology"
            },

            new Speciality 
            { 
                Id = 10,
                Name = "Pathology"
            }
        };

        return specialities;
    }
}
