using OneToMany.Models;

namespace OneToMany.ViewModels
{
    public class SliderVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SliderInfo SliderInfo { get; set; }
    }
}
