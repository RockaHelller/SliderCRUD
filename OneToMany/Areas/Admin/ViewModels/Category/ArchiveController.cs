using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToMany.Data;

namespace OneToMany.Areas.Admin.ViewModels.Category
{
    [Area("Admin")]
    public class ArchiveController : Controller
    {
        private readonly AppDbContext _context;
        public ArchiveController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Categories()
        {
            List<CategoryVM> list = new();

            var datas = await _context.Categories.IgnoreQueryFilters().Where(m=>m.SoftDeleted == true).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new CategoryVM
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExtractCategory(int id)
        {
            var existCategory = await _context.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);

            existCategory.SoftDeleted = false;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Categories));
        }
    }
}
