using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _3rdBackendProject.DAL;
using _3rdBackendProject.Models;
using _3rdBackendProject.ViewModels;

namespace _3rdBackendProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {



            List<Slide> slides = _context.Slides.OrderBy(s => s.Order).Take(3).ToList();

            List<Product> products = _context.Products.Include(p => p.Category).Include(p => p.ProductImages).ToList();

            HomeVM homeVM = new HomeVM
            {
                Sliders = slides,
                Products = products
            };

            return View(homeVM);
        }


        public IActionResult Detail(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Product product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductTags).ThenInclude(pt => pt.Tag)
                .FirstOrDefault(p => p.Id == id);


            if (product == null) return NotFound();

            return View(product);
        }
    
    }
}
