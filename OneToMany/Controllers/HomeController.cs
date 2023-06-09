using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToMany.Data;
using OneToMany.Models;
using OneToMany.Services;
using OneToMany.Services.Interfaces;
using OneToMany.ViewModels;
using System.Diagnostics;

namespace OneToMany.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        
        public HomeController(AppDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {

            IEnumerable<Blog> blogs = await _context.Blogs.Where(m=>!m.SoftDeleted).OrderByDescending(m=>m.Id).Take(3).ToListAsync();
            IEnumerable<Category> categories = await _context.Categories.Where(m => !m.SoftDeleted).ToListAsync();
            IEnumerable<Product> products = await _productService.GetAllAsync();
            IEnumerable<Expert> experts = await _context.Experts.Where(m => !m.SoftDeleted).ToListAsync();
            IEnumerable<Instagram> instagrams = await _context.Instagrams.Where(m => !m.SoftDeleted).ToListAsync();

            HomeVM model = new HomeVM()
            {
                Blogs = blogs,
                Categories = categories,
                Product = products,
                Experts = experts,
                Instagrams = instagrams
            };

            return View(model);
        }
    }
}