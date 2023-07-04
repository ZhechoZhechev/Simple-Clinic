namespace SimpleClinic.Infrastructure.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SimpleClinic.Infrastructure.Entities;


/// <summary>
/// Service entiry configuration
/// </summary>
public class ServiceEntityConfiguration : IEntityTypeConfiguration<Service>
{
    /// <summary>
    /// Configure method from IEntityTypeConfiguration interface
    /// </summary>
    /// <param name="builder">builder class</param>
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder
            .HasData(CreateService());
    }

    private Service[] CreateService() 
    {
        var service = new Service[]
        {
            new Service
            {
                Id = new Guid().ToString(),
                Name = "Rehabilitation",
                EquipmentPicture = "https://www.neuro-forma.com/wp-content/uploads/2020/10/img-6.jpg",
                Price = 2400
            },

            new Service
            {
                Id = new Guid().ToString(),
                Name = "Magnetic Resonance Imaging.",
                EquipmentPicture = "https://upload.wikimedia.org/wikipedia/commons/e/ee/MRI-Philips.JPG",
                Price = 5400
            },

            new Service 
            { 
                Id = new Guid().ToString(),
                Name = "Ultrasound Imaging",
                EquipmentPicture = "https://my.clevelandclinic.org/-/scassets/images/org/health/articles/4995-ultrasound-imaging",
                Price = 150
            }
        };

        return service;
    }
}
