using _3rdBackendProject.DAL;
using _3rdBackendProject.Models;
using _3rdBackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace _3rdBackendProject.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;

        public BasketController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            List<BasketItemsVM> basketItems = new List<BasketItemsVM>();
            if (Request.Cookies["Basket"] != null)
            {
                List<BasketCookiesItemVM> basket = JsonConvert.DeserializeObject<List<BasketCookiesItemVM>>(Request.Cookies["Basket"]);
                for (int i = 0; i < basket.Count; i++)
                {
                    Product product = await _context.Products.Include(p => p.ProductImages.Where(p => p.IsPrimary == true)).FirstOrDefaultAsync(x => x.Id == basket[i].Id);
                    if (product != null)
                    {
                        basketItems.Add(new BasketItemsVM
                        {
                            Name = product.Name,
                            Price = product.Price,
                            Count = basket[i].Count,
                            Image = product.ProductImages[0].Image
                        });
                    }
                    else
                    {
                        basketItems.Remove(basketItems[i]);
                    }
                }
            }
            return View(basketItems);
        }
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            if (Request.Cookies == null) return NotFound();

            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null) return NotFound();

            List<BasketCookiesItemVM> basket = new List<BasketCookiesItemVM>();

            if (Request.Cookies["Basket"] == null)
            {

                basket.Add(new BasketCookiesItemVM
                {
                    Id = product.Id,
                    Count = 1
                });

            }
            else
            {
                basket = JsonConvert.DeserializeObject<List<BasketCookiesItemVM>>(Request.Cookies["Basket"]);

                BasketCookiesItemVM existed = basket.FirstOrDefault(x => x.Id == id);
                if (existed != null) { existed.Count++; }
                else
                {
                    basket.Add(new BasketCookiesItemVM
                    {
                        Id = product.Id,
                        Count = 1
                    });
                }
            }


            string json = JsonConvert.SerializeObject(basket);
            Response.Cookies.Append("Basket", json);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult GetBasket()
        {
            if (Request.Cookies["Basket"] == null)
            {

                return Json(new List<BasketCookiesItemVM>());
            }
            List<BasketCookiesItemVM> basket = JsonConvert.DeserializeObject<List<BasketCookiesItemVM>>(Request.Cookies["Basket"]);
            return Json(basket);
        }
    }
}
