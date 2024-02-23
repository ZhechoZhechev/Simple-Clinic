namespace SimpleClinic;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SimpleClinic.Common.Helpers;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.CustomMiddleWares;
using SimpleClinic.Infrastructure.Entities;
using SimpleClinic.Infrastructure.Extensions;
using SimpleClinic.Infrastructure.ModelBinders;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<SimpleClinicDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        //Compression Algorithms,Automatic Compression
        builder.Services.AddResponseCompression(options => 
        {
            options.EnableForHttps = true;
        });

        builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount =
                builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
            options.Password.RequireLowercase =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
            options.Password.RequireUppercase =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
            options.Password.RequireNonAlphanumeric =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
            options.Password.RequiredLength =
                builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<SimpleClinicDbContext>();

        builder.Services.AddApplicationServices(typeof(IAccountService));
        builder.Services.AddScoped(typeof(EmailService));

        builder.Services.AddMemoryCache();

        builder.Services.AddControllersWithViews()
            .AddMvcOptions(options => 
            {
                options.ModelBinderProviders.Insert(0, new DecimaModelBinderProvider());
                //options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider());
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("DoctorAdmin", policy =>
            {
                policy.RequireRole("Doctor", "Administrator");
            });
        });

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error/500");
            app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

            app.UseHsts();
        }

         
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseResponseCompression();

        app.UseRouting();

        app.UseMiddleware<RoleCreationMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "Area",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapDefaultControllerRoute();
        app.MapRazorPages();


        app.Run();
    }
}