﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleClinic.Infrastructure;

#nullable disable

namespace SimpleClinic.Infrastructure.Migrations
{
    [DbContext(typeof(SimpleClinicDbContext))]
    partial class SimpleClinicDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApplicationUser");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.DoctorAppointment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BookingDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimeSlotId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("TimeSlotId");

                    b.ToTable("DoctorAppointments", (string)null);
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.MedicalHistory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MedicalConditions")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Surgery")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId")
                        .IsUnique();

                    b.ToTable("MedicalHistories", (string)null);
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Medicament", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MedicalHistoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("QuantityPerDayMilligrams")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicalHistoryId");

                    b.ToTable("Medicaments", (string)null);
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.NextOfKin", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(99)
                        .HasColumnType("nvarchar(99)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("NextOfKins", (string)null);
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Prescription", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Instructions")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("MedicamentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("PrescriptionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("MedicamentId");

                    b.HasIndex("PatientId");

                    b.ToTable("Prescriptions", (string)null);
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Schedule", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Day")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ServiceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Schedules", (string)null);
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Service", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EquipmentPicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Services", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "f64ebfd3-ab81-4d55-90ce-8cf90ac40890",
                            EquipmentPicture = "https://www.neuro-forma.com/wp-content/uploads/2020/10/img-6.jpg",
                            Name = "Rehabilitation",
                            Price = 2400m
                        },
                        new
                        {
                            Id = "c9382034-7d75-4be5-ba96-a8df9c91cc46",
                            EquipmentPicture = "https://upload.wikimedia.org/wikipedia/commons/e/ee/MRI-Philips.JPG",
                            Name = "Magnetic Resonance Imaging.",
                            Price = 5400m
                        },
                        new
                        {
                            Id = "38847c4c-40c5-47cf-a4c7-1be84b58891d",
                            EquipmentPicture = "https://my.clevelandclinic.org/-/scassets/images/org/health/articles/4995-ultrasound-imaging",
                            Name = "Ultrasound Imaging",
                            Price = 150m
                        });
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.ServiceAppointment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BookingDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ServiceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimeSlotId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TimeSlotId");

                    b.ToTable("ServiceAppointments", (string)null);
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Specialities", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Surgery"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Pediatrics"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Family medicine"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Cardiology"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Gynaecology"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Dermatology"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Phycology"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Neurology"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Ophthalmology"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Pathology"
                        });
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.TimeSlot", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("ScheduleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("TimeSlots", (string)null);
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Doctor", b =>
                {
                    b.HasBaseType("SimpleClinic.Infrastructure.Entities.ApplicationUser");

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OfficePhoneNumber")
                        .IsRequired()
                        .HasMaxLength(99)
                        .HasColumnType("nvarchar(99)");

                    b.Property<decimal>("PricePerAppointment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProfilePictureFilename")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("SpecialityId")
                        .HasColumnType("int");

                    b.HasIndex("SpecialityId");

                    b.HasDiscriminator().HasValue("Doctor");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Patient", b =>
                {
                    b.HasBaseType("SimpleClinic.Infrastructure.Entities.ApplicationUser");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<bool>("FormsCompleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("HasInsurance")
                        .HasColumnType("bit");

                    b.Property<string>("MedicalHistoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NextOfKinId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("NextOfKinId");

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleClinic.Infrastructure.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.DoctorAppointment", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleClinic.Infrastructure.Entities.Patient", null)
                        .WithMany("DoctorAppointments")
                        .HasForeignKey("PatientId");

                    b.HasOne("SimpleClinic.Infrastructure.Entities.TimeSlot", "TimeSlot")
                        .WithMany()
                        .HasForeignKey("TimeSlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("TimeSlot");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.MedicalHistory", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.Patient", "Patient")
                        .WithOne("MedicalHistory")
                        .HasForeignKey("SimpleClinic.Infrastructure.Entities.MedicalHistory", "PatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Medicament", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.MedicalHistory", null)
                        .WithMany("Medicaments")
                        .HasForeignKey("MedicalHistoryId");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.NextOfKin", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Prescription", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.Doctor", "Doctor")
                        .WithMany("Prescriptions")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SimpleClinic.Infrastructure.Entities.Medicament", "Medicament")
                        .WithMany()
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleClinic.Infrastructure.Entities.Patient", "Patient")
                        .WithMany("Prescriptions")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Medicament");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Schedule", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.Doctor", "Doctor")
                        .WithMany("Schedules")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleClinic.Infrastructure.Entities.Service", "Service")
                        .WithMany("Schedules")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.ServiceAppointment", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.Patient", null)
                        .WithMany("ServiceAppointments")
                        .HasForeignKey("PatientId");

                    b.HasOne("SimpleClinic.Infrastructure.Entities.Service", "Service")
                        .WithMany("Appointments")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleClinic.Infrastructure.Entities.TimeSlot", "TimeSlot")
                        .WithMany()
                        .HasForeignKey("TimeSlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("TimeSlot");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.TimeSlot", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.Schedule", null)
                        .WithMany("TimeSlots")
                        .HasForeignKey("ScheduleId");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Doctor", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.Speciality", "Speciality")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Patient", b =>
                {
                    b.HasOne("SimpleClinic.Infrastructure.Entities.NextOfKin", "NextOfKin")
                        .WithMany()
                        .HasForeignKey("NextOfKinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NextOfKin");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.MedicalHistory", b =>
                {
                    b.Navigation("Medicaments");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Schedule", b =>
                {
                    b.Navigation("TimeSlots");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Service", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Speciality", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Doctor", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Prescriptions");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("SimpleClinic.Infrastructure.Entities.Patient", b =>
                {
                    b.Navigation("DoctorAppointments");

                    b.Navigation("MedicalHistory")
                        .IsRequired();

                    b.Navigation("Prescriptions");

                    b.Navigation("ServiceAppointments");
                });
#pragma warning restore 612, 618
        }
    }
}
