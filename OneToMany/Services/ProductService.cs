using Microsoft.EntityFrameworkCore;
using OneToMany.Data;
using OneToMany.Models;
using OneToMany.Services.Interfaces;

namespace OneToMany.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.Include(m => m.ProductImage).Where(m => !m.SoftDeleted).ToListAsync();

        public async Task<Product> GetByIdAsync(int? id) => await _context.Products.FindAsync(id);

        public async Task<Product> GetByIdWithImages(int? id) => await _context.Products.Include(m => m.ProductImage).FirstOrDefaultAsync(m => m.Id == id);


    }
}
