using OneToMany.Models;
using OneToMany.ViewModels;

namespace OneToMany.Services.Interfaces
{
    public interface IBasketService
    {
        void AddProduct(List<BasketVM> basket, Product product);
        int GetCount();
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
