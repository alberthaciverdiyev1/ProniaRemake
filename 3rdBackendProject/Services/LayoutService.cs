using _3rdBackendProject.DAL;
using _3rdBackendProject.Models;
using _3rdBackendProject.ViewModels;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace _3rdBackendProject.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _http;

        public LayoutService(AppDbContext context,IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }


        public async Task<Dictionary<string, string>> GetSettingAsync()
        {
            Dictionary<string,string> setting = await _context.Settings.ToDictionaryAsync(x=>x.Key, x=>x.Value);
            return setting;
        }
        public async Task<List<BasketItemsVM>> GetBasket()
        {
            List<BasketItemsVM> basketItems = new List<BasketItemsVM>();
            if (_http.HttpContext.Request.Cookies["Basket"] != null)
            {
                List<BasketCookiesItemVM> basket = JsonConvert.DeserializeObject<List<BasketCookiesItemVM>>(_http.HttpContext.Request.Cookies["Basket"]);
                for (int i = 0; i < basket.Count; i++)
                {
                    Product product = await _context.Products.Include(p => p.ProductImages.Where(p => p.IsPrimary == true)).FirstOrDefaultAsync(x => x.Id == basket[i].Id);
                    basketItems.Add(new BasketItemsVM
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Count = basket[i].Count,
                        Image = product.ProductImages[0].Image
                    });
                }
            }
            return basketItems;
        }
    }
}
