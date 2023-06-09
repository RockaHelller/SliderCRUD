using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToMany.Data;
using OneToMany.Models;
using OneToMany.Services.Interfaces;
using OneToMany.ViewModels;

namespace OneToMany.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public SliderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.Where(m => !m.SoftDeleted).ToListAsync();
            SliderInfo sliderInfo = await _context.SliderInfos.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();

            SliderVM model = new()
            {
                Sliders = sliders,
                SliderInfo = sliderInfo
            };

            return await Task.FromResult(View(model));
        }
    }
}
