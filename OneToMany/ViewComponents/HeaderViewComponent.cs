using Microsoft.AspNetCore.Mvc;
using OneToMany.Data;
using OneToMany.Services.Interfaces;

namespace OneToMany.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        
        public HeaderViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = _layoutService.GetAllDatas();
            return await Task.FromResult(View(datas));
        }
    }
}
