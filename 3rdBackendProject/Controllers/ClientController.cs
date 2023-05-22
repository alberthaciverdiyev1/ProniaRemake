using Microsoft.AspNetCore.Mvc;

namespace _3rdBackendProject.Controllers
{  [Area("ProniaAdmin")]
    public class ClientController : Controller
    {
      
        public IActionResult Index()
        {

            return View();
        }
    }
}
