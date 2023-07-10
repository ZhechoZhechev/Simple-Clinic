namespace SimpleClinic.Controllers;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Doctors controller
/// </summary>
public class DoctorController : Controller
{
    private readonly string directoryPath;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    public DoctorController(IConfiguration configuration)
    {
        this.directoryPath = configuration["UpploadSettings:ImageDir"];
    }

    /// <summary>
    /// End point fo all the IFormFile uploads
    /// </summary>
    /// <param name="files">the uploaded image</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult UploadFiles(List<IFormFile> files)
    {
        foreach (var file in files)
        {
            if (file != null && file.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(directoryPath, uniqueFileName);

                using var stream = System.IO.File.Create(filePath);
                file.CopyTo(stream);

            }
        }

        return Ok();
    }

}
