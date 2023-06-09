using Microsoft.Build.Framework;

namespace OneToMany.Areas.Admin.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
