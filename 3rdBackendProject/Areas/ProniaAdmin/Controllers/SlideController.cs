using _3rdBackendProject.DAL;
using _3rdBackendProject.Models;
using _3rdBackendProject.Utilities.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3rdBackendProject.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SlideController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlideController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slide> slides = await _context.Slides.ToListAsync();

            return View(slides);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slide slide)
        {
            if (slide.Photo == null)
            {
                ModelState.AddModelError("Photo", "Shekil Yeri Bos Ola Bilmez");
                return View();
            }
            if (!slide.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Sechilmis Fayl Duzgun Formatda Deyil");
                return View();
            }

            if (slide.Photo.CheckFileSize(500))
            {
                ModelState.AddModelError("Photo", "Secilmis Seklin Hecmi 200 KB-dan Boyukdur");
            }

            slide.Image = await slide.Photo.CreateFile(_env.WebRootPath, "assets/images/website-images");

            await _context.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) BadRequest();


            Slide slide = await _context.Slides.FirstOrDefaultAsync(x => x.Id == id);
            if (slide == null) NotFound();

            slide.Image.DeleteItem(_env.WebRootPath, "assets/images/website-images");

            _context.Slides.Remove(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id, Slide slide)
        {
            if (id == null || id < 1) BadRequest();


            Slide existed = await _context.Slides.FirstOrDefaultAsync(x => x.Id == id);
            if (slide == null) NotFound();
            if (slide.Photo != null)
            {
                if (!slide.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Sechilmis Fayl Duzgun Formatda Deyil");
                    return View(existed);
                }

                if (slide.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Secilmis Seklin Hecmi 200 KB-dan Boyukdur");
                }
                existed.Image.DeleteItem(_env.WebRootPath, "assets/images/website-images");
                existed.Image=await slide.Photo.CreateFile(_env.WebRootPath, "assets/images/website-images");

            }

            existed.Title=slide.Title;
            existed.Description=slide.Description;
            existed.SubTitle=slide.SubTitle;
            existed.Order=slide.Order;
            return View(existed);
        }
    }
}
