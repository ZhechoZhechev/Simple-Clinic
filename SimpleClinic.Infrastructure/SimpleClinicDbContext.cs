namespace SimpleClinic.Infrastructure;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Aplication database context
/// </summary>
public class SimpleClinicDbContext : IdentityDbContext
{
    /// <summary>
    /// Context constructor
    /// </summary>
    /// <param name="options">options for the context</param>
    public SimpleClinicDbContext(DbContextOptions<SimpleClinicDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Method for modelating entities
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}