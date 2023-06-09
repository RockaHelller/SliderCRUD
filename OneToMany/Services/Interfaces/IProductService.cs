using OneToMany.Models;

namespace OneToMany.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int? id);
        Task<Product> GetByIdWithImages(int? id);
    }
}
