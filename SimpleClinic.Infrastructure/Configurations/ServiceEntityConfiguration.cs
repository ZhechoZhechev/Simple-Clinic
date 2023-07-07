namespace SimpleClinic.Infrastructure.Configurations;

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
                Id = "f64ebfd3-ab81-4d55-90ce-8cf90ac40890",
                Name = "Rehabilitation",
                EquipmentPicture = "https://www.neuro-forma.com/wp-content/uploads/2020/10/img-6.jpg",
                Price = 2400
            },

            new Service
            {
                Id = "c9382034-7d75-4be5-ba96-a8df9c91cc46",
                Name = "Magnetic Resonance Imaging.",
                EquipmentPicture = "https://upload.wikimedia.org/wikipedia/commons/e/ee/MRI-Philips.JPG",
                Price = 5400
            },

            new Service 
            { 
                Id = "38847c4c-40c5-47cf-a4c7-1be84b58891d",
                Name = "Ultrasound Imaging",
                EquipmentPicture = "https://my.clevelandclinic.org/-/scassets/images/org/health/articles/4995-ultrasound-imaging",
                Price = 150
            }
        };

        return service;
    }
}
