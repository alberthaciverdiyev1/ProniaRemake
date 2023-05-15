using Microsoft.AspNetCore.Mvc;

namespace _3rdBackendProject.Controllers
{
    public class WishListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
