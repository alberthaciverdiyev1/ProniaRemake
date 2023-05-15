using _3rdBackendProject.DAL;
using _3rdBackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Cars.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()

        {
            List<Category> bodyTypes = await _context.Categories.Include(b => b.Products).ToListAsync();

            return View(bodyTypes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool result = await _context.Categories
                .AnyAsync(b => b.Name.Trim().ToLower() == category.Name.Trim().ToLower());
            if (result)
            {
                ModelState.AddModelError("Body", "Bu Adda Kateqoriya Artiq Movcuddur");
                return View();
            }
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null || id < 1) return BadRequest();
            Category existed = await _context.Categories.FirstOrDefaultAsync(e => e.Id == id);
            if (existed == null) return NotFound();

            return View(existed);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null || id < 1) return BadRequest();
            Category existed = await _context.Categories.FirstOrDefaultAsync(e => e.Id == id);
            if (existed == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(existed);
            }
            if (existed.Name == category.Name)
            {
                return RedirectToAction(nameof(Index));
            }
            bool result = await _context.Categories
              .AnyAsync(b => b.Name.Trim().ToLower() == category.Name.Trim().ToLower()&&b.Id!=existed.Id);
            if (result)
            {
                ModelState.AddModelError("Body", "Bu Adda Kateqoriya Artiq Movcuddur");
                return View(existed);
            }

            existed.Name = category.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Category existed = await _context.Categories.FirstOrDefaultAsync(e => e.Id == id);
            if (existed == null) return NotFound();

            _context.Categories.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
