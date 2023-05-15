using Microsoft.AspNetCore.Mvc;

namespace _3rdBackendProject.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
