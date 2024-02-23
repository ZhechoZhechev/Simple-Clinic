namespace SimpleClinic.Common.Constants;

/// <summary>
/// General constants
/// </summary>
public static class GeneralApplicationConstants
{
    /// <summary>
    /// Year of releasing the app
    /// </summary>
    public const int ReleaseYear = 2022;
    
    /// <summary>
    /// Memory cache settings for home/AllDepartments endpoint
    /// </summary>
    public const string AllDepsMemoryCacheKey = "AllDepsKey";
    public const int AllDepsMemoryCacheExpTime = 10;

    /// <summary>
    /// Memory Cache settings for home/DoctorDetails endpoint
    /// </summary>
    public const string DoctorDetailsCacheKey = "DocDetailsKey";
    public const int DocDetailsCacheExpTime = 15;
}
