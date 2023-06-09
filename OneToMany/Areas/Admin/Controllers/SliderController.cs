using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToMany.Areas.Admin.ViewModels.Slider;
using OneToMany.Data;
using OneToMany.Helpers;
using OneToMany.Models;
using System.IO;

namespace OneToMany.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SliderVM> sliderList = new();

            List<Slider> sliders = await _context.Sliders.ToListAsync();

            foreach (Slider slider in sliders)
            {
                SliderVM model = new()
                {
                    Id = slider.Id,
                    Image = slider.Image,
                    Status = slider.Status,
                    CreateDate = slider.CreatedDate.ToString("dd-MM-yyyy")
                };

                sliderList.Add(model);
            }

            return View(sliderList);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (dbSlider is null) return NotFound();

            SliderDetailVM model = new()
            {
                Image = dbSlider.Image,
                CreateDate = dbSlider.CreatedDate.ToString("dd-MM-yyyy"),
                Status = dbSlider.Status,
            };

            return View(model);


        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Please select only image file");
                    return View();
                }

                if (item.CheckFileSize(500))
                {
                    ModelState.AddModelError("Image", "Image size must be max 500 KB");
                    return View();
                }
            }

            foreach (var item in request.Images)
            {
                string filename = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(filename, _env.WebRootPath, "img");

                Slider slider = new()
                {
                    Image = filename
                };

                await _context.Sliders.AddAsync(slider);

            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "img", slider.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (dbSlider is null) return NotFound();

            return View(new SliderEditVM { Image = dbSlider.Image });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (id is null) return BadRequest();

            Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (dbSlider is null) return NotFound();

            if (request.NewImage is null) return RedirectToAction(nameof(Index));

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Please select only image file");
                request.Image = dbSlider.Image;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(500))
            {
                ModelState.AddModelError("Image", "Image size must be max 500 KB");
                request.Image = dbSlider.Image;
                return View(request);
            }

            string oldPath = Path.Combine(_env.WebRootPath, "img", dbSlider.Image);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            string filename = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;

            await request.NewImage.SaveFileAsync(filename, _env.WebRootPath, "img");

            dbSlider.Image = filename;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


    }
}