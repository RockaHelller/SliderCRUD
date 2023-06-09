using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OneToMany.Data;
using OneToMany.Models;
using OneToMany.Services.Interfaces;
using OneToMany.ViewModels;

namespace OneToMany.Services
{
    public class BasketService : IBasketService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public BasketService(AppDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public void AddProduct(List<BasketVM> basket, Product product)
        {

        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(m => m.ProductImage).ToListAsync();
        }

        public int GetCount()
        {
            List<BasketVM> basket;
            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket.Sum(m => m.Count);
        }
    }
}
