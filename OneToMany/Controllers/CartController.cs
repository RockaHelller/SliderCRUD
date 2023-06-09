using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.ContentModel;
using OneToMany.Data;
using OneToMany.Models;
using OneToMany.Services;
using OneToMany.Services.Interfaces;
using OneToMany.ViewModels;

namespace OneToMany.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;

        public CartController(AppDbContext context, IHttpContextAccessor accessor, IProductService productService)
        {
            _context = context;
            _accessor = accessor;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            List<BasketDetailVM> basketList = new();

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                List<BasketVM> basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);

                foreach (var item in basketDatas)
                {
                    var dbProduct = await _productService.GetByIdWithImages(item.Id);

                    if (dbProduct != null)
                    {
                        BasketDetailVM basketDetail = new()
                        {
                            Id = dbProduct.Id,
                            Name = dbProduct.Name,
                            Image = dbProduct.ProductImage.Where(m => m.IsMain).FirstOrDefault().Image,
                            Count = item.Count,
                            Price = dbProduct.Price,
                            TotalPrice = dbProduct.Price * item.Count
                        };

                        basketList.Add(basketDetail);

                    }

                }
            }

            return View(basketList);
        }


        [HttpPost]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _context.Products.FindAsync(id);

            if (product == null) return NotFound();

            List<BasketVM> basket = GetBasketDatas();

            AddProdcutToBasket(basket, product);

            //return RedirectToAction(nameof(Index));
            return Ok(basket.Sum(m=>m.Count));
        }

        private List<BasketVM> GetBasketDatas()
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

            return basket;
        }

        private void AddProdcutToBasket(List<BasketVM> basket, Product product)
        {
            BasketVM existProduct = basket.FirstOrDefault(m => m.Id == product.Id);

            if (existProduct is null)
            {
                basket.Add(new BasketVM
                {
                    Id = product.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }




            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteProductFromBasket(int? id)
        {
            List<BasketVM> basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);

            var data = basketDatas.FirstOrDefault(m => m.Id == id);

            basketDatas.Remove(data);

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketDatas));

            return RedirectToAction(nameof(Index));

        }

    }
}
