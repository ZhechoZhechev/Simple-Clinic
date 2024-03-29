﻿namespace SimpleClinic.Core.Models.PatientModels
{
    public class DoctorServiceModel
    {
        public string Id { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string OfficePhoneNumber { get; set; } = null!;

        public string ProfilePictureFilename { get; set; } = null!;

        public string Biography { get; set; } = null!;

        public string  PricePerHour { get; set; } = null!;

        public string Speciality { get; set; } = null!;
    }
}