using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SimpleClinic.Controllers
{
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
}
