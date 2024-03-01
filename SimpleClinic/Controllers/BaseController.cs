namespace SimpleClinic.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

/// <summary>
/// extends Controller with method that returns user Id
/// </summary>
public class BaseController : Controller
{
    protected string GetCurrnetUserId(ITempDataDictionary tempdata) 
    {
        var userId = tempdata.Peek("CurrentUserId");
        if (userId != null)
        {
            return userId.ToString();
        }
        return null;
    }
}
