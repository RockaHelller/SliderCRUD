using OneToMany.Data;
using OneToMany.Services.Interfaces;
using OneToMany.ViewModels;

namespace OneToMany.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        private readonly IBasketService _basketService;

        public LayoutService(AppDbContext context, IBasketService basketService)
        {
            _context = context;
            _basketService = basketService;
        }

        public LayoutVM GetAllDatas()
        {
            int count = _basketService.GetCount();
            var datas = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
            return new LayoutVM { BasketCount = count, SettingData = datas };
        }
    }
}
