namespace SimpleClinic.Common.Constants;

/// <summary>
///  Entity constants
/// </summary>
public static class DataConstants
{
    /// <summary>
    /// Application user constants
    /// </summary>
    public static class ApplicationUserConstants 
    {
        /// <summary>
        /// Both first and last name max letters count
        /// </summary>
        public const int FirstAndLastNameMaxLength = 40;

        /// <summary>
        /// Both first and last name min letters count
        /// </summary>
        public const int FirstAndLastNameMinLength = 3;

        /// <summary>
        /// Address max letters count
        /// </summary>
        public const int AddressMaxLength = 150;
        /// <summary>
        /// Address min letters count
        /// </summary>
        public const int AddressMinLength = 5;
    }

    /// <summary>
    /// Doctor constants
    /// </summary>
    public static class DoctorConstants 
    {
        /// <summary>
        /// Doctor license number max letters count
        /// </summary>
        public const int LicenseNumberMaxLength = 50;
        /// <summary>
        /// Doctor license number min letters count
        /// </summary>
        public const int LicenseNumberMinLength = 5;

        /// <summary>
        /// Doctor biography max letters count
        /// </summary>
        public const int BiographyMaxLength = 500;
        /// <summary>
        /// Doctor biography min letters count
        /// </summary>
        public const int BiographyMinLength = 5;

        /// <summary>
        /// Doctor office phone number max digits count
        /// </summary>
        public const int OfficePhoneNumberMaxLength = 99;
        /// <summary>
        /// Doctor office phone number min digits count
        /// </summary>
        public const int OfficePhoneNumberMinLength = 3;

        /// <summary>
        /// Doctor appoitment price max value
        /// </summary>
        public const string PricePerAppointmentMaxValue = "99999";
        /// <summary>
        /// Doctor appoitment price min value
        /// </summary>
        public const string PricePerAppointmentMinValue = "0";
    }

    /// <summary>
    /// Speciality constants
    /// </summary>
    public static class SpecialityConstants 
    {
        /// <summary>
        /// Speciality name max letters count
        /// </summary>
        public const int NameMaxLength = 50;
        /// <summary>
        /// Speciality name min letters count
        /// </summary>
        public const int NameMinLength = 3;

    }

    /// <summary>
    /// Service constants
    /// </summary>
    public static class ServiceConstants 
    {
        /// <summary>
        /// Service name max letters count
        /// </summary>
        public const int NameMaxLength = 50;

        /// <summary>
        /// Service name min letters count
        /// </summary>
        public const int NameMinLength = 3;

        /// <summary>
        /// Service picture url max letters count
        /// </summary>
        public const int PictureMaxLength = 500;

        /// <summary>
        /// Service picture url min letters count
        /// </summary>
        public const int PictureMinLength = 5;

        /// <summary>
        /// Service appoitment price max value
        /// </summary>
        public const string PriceMaxValue = "99999";

        /// <summary>
        /// Service appoitment price min value
        /// </summary>
        public const string PriceMinValue = "0";
    }

    /// <summary>
    /// Medical history constants
    /// </summary>
    public static class MedicalHistoryConstants 
    {
        /// <summary>
        /// Surgeries description max letters count
        /// </summary>
        public const int SurgeryDescrMaxLength = 500;

        /// <summary>
        /// Surgeries description min letters count
        /// </summary>
        public const int SurgeryDescrMinLength = 5;

        /// <summary>
        /// Medical conditions description max letters count
        /// </summary>
        public const int MedicalConditionsDescrMaxLength = 500;

        /// <summary>
        /// Medical conditions description max letters count
        /// </summary>
        public const int MedicalConditionsDescrMinLength = 5;
    }

    public static class MedicamentConstants 
    {
        /// <summary>
        /// Medicament name max letters count
        /// </summary>
        public const int NameMaxLength = 50;

        /// <summary>
        /// Medicament name min letters count
        /// </summary>
        public const int NameMinLength = 3;

        /// <summary>
        /// Max milligrams value
        /// </summary>
        public const int QuantityPerdayMaxValue = 1000;

        /// <summary>
        /// Min milligrams value
        /// </summary>
        public const int QuantityPerdayMinValue = 1;
    }

    /// <summary>
    /// Next of kin constants
    /// </summary>
    public static class NextOfKinConstants 
    {
        /// <summary>
        /// Next of kin name max letters count
        /// </summary>
        public const int NameMaxLength = 50;

        /// <summary>
        /// Next of kin name min letters count
        /// </summary>
        public const int NameMinLength = 3;

        /// <summary>
        /// Phone number max digits count
        /// </summary>
        public const int PhoneNumberMaxLength = 99;

        /// <summary>
        /// Phone number min digits count
        /// </summary>
        public const int PhoneNumberMinLength = 1;

        /// <summary>
        /// Address max letters count
        /// </summary>
        public const int AddressMaxLength = 150;

        /// <summary>
        /// Address min letters count
        /// </summary>
        public const int AddressMinLength = 5;
    }

    /// <summary>
    /// Prescription constants
    /// </summary>
    public static class PrescriptionConstants 
    {
        /// <summary>
        /// Instructions conditions description max letters count
        /// </summary>
        public const int InstructionsMaxLength = 500;

        /// <summary>
        /// Instructions conditions description max letters count
        /// </summary>
        public const int InstructionsMinLength = 5;
    }
}
