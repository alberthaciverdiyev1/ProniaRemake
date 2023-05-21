using _3rdBackendProject.DAL;
using _3rdBackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3rdBackendProject.ViewComponents
{
    public class FooterViewComponents:ViewComponent
    {
        private readonly AppDbContext _context;

        public FooterViewComponents(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
    

          Dictionary<string,string> settings= await _context.Settings.ToDictionaryAsync(x=>x.Key,x=>x.Value);
            return View(await Task.FromResult(settings));
        
        }
    }
}
