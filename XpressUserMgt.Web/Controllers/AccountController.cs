using Microsoft.AspNetCore.Mvc;

namespace XpressUserMgt.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
