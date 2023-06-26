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
        public const string PricePerAppointmentMinValue = "1";
    }
}
