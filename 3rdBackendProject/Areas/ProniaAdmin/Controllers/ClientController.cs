using _3rdBackendProject.DAL;
using _3rdBackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3rdBackendProject.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Client> client = await _context.Clients.Include(x=>x.Profession).ToListAsync();
            return View(client);
        }

        public async Task<IActionResult> Create()
        {ViewBag.Professions=await _context.Professions.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(Client client)
        {
            if(!ModelState.IsValid) 
             return View(); 
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
