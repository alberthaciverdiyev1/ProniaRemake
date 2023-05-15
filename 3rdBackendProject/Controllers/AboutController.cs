using Microsoft.AspNetCore.Mvc;

namespace _3rdBackendProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
