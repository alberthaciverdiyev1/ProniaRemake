using _3rdBackendProject.DAL;
using _3rdBackendProject.Models;
using _3rdBackendProject.Utilities.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace _3rdBackendProject.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]


    public class ClientController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ClientController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            List<Client> clients = await _context.Clients.Include(c => c.Profession).ToListAsync();
            return View(clients);
        }
        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Professions = await _context.Professions.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Professions = await _context.Professions.ToListAsync();
                return View();
            }
            ViewBag.Professions = await _context.Professions.ToListAsync();
            if (client.ProfessionId == 0)
            {
                ModelState.AddModelError("ProfessionId", "Enter Position ID");
            }
            bool result = await _context.Professions.AnyAsync(p => p.Id == client.ProfessionId);
            if (!result)
            {
                ModelState.AddModelError("ProfessionId", "We Couldnt Find Anything!");
                ViewBag.Professions = await _context.Professions.ToListAsync();
                return View();
            }
            if (client.Photo == null)
            {
                ModelState.AddModelError("Photo", "Image Cant Empty Here");
                ViewBag.Professions = await _context.Professions.ToListAsync();
                return View();
            }
            if (!client.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Please Add Image");
                return View();
            }
            if (!client.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Fayl hecmi 200 kb'den boyuk olmamalidir");
                return View();
            }
            bool orderCheck = await _context.Slides.AnyAsync(s => s.Order == client.Order);
            if (orderCheck)
            {
                ModelState.AddModelError("Order", "This Order Already Added");
                return View();
            }

            client.Name.Capitalize();
            client.Surname.Capitalize();
            client.Image = await client.Photo.CreateFileAsync(_env.WebRootPath, "assets/images/website-images");


            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Client existed = await _context.Clients.FirstOrDefaultAsync(s => s.Id == id);
            if (existed == null) return NotFound();
            ViewBag.Professions = await _context.Professions.ToListAsync();
            return View(existed);
        }
        [HttpPost]
  
        public async Task<IActionResult> Update(int? id, Client client)
        {

            if (id == null || id < 1) return BadRequest();

            Client existed = await _context.Clients.FirstOrDefaultAsync(s => s.Id == id);
            if (existed == null) return NotFound();
            //if (!ModelState.IsValid) return View(existed);
            bool result = await _context.Professions.AnyAsync(p => p.Id == client.ProfessionId);
            if (!result)
            {
                ModelState.AddModelError("ProfessionId", "We Cant Find This Position!");
                ViewBag.Professions = await _context.Professions.ToListAsync();
                return View(existed);
            }
            if (client.Photo != null)
            {
                if (client.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Image Not Null Here");
                    return View(existed);
                }
                if (!client.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Please Add Image");
                    return View(existed);
                }
                if (!client.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Fayl hecmi 200 kb'den boyuk olmamalidir");
                    return View(existed);
                }
                existed.Image.DeleteFile(_env.WebRootPath, "assets/images/website-images");
                existed.Image = await client.Photo.CreateFileAsync(_env.WebRootPath, "assets/images/website-images");
            }


            existed.Fullname = client.Fullname;
            existed.Thinks = client.Thinks;
            existed.Order = client.Order;



            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Client client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (client == null) return NotFound();

            client.Image.DeleteFile(_env.WebRootPath, "assets/images/website-images");

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        } }
}


#endregion