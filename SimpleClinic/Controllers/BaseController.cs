namespace SimpleClinic.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


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
