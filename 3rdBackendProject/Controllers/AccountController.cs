using Microsoft.AspNetCore.Mvc;

namespace _3rdBackendProject.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
