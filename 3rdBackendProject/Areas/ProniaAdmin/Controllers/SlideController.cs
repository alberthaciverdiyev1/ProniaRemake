using _3rdBackendProject.DAL;
using _3rdBackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3rdBackendProject.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SlideController : Controller
    {
        private readonly AppDbContext _context;

        public SlideController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Slide> slides = await _context.Slides.ToListAsync(); 


            return View(slides);
        }
        public IActionResult Create() {



            return View();
        }
    }
}
