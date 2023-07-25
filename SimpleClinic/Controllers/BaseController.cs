using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SimpleClinic.Controllers
{
    public class BaseController : Controller
    {
        protected string GetCurrnetUserId(ITempDataDictionary tempdata) 
        {
            if (tempdata.TryGetValue("CurrentUserId", out var userId))
            {
                return userId.ToString();
            }

            return null;
        }
    }
}
