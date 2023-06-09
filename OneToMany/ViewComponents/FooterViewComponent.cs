using Microsoft.AspNetCore.Mvc;
using OneToMany.Services.Interfaces;

namespace OneToMany.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly ILayoutService _layoutService;

        public FooterViewComponent(ILayoutService layoutService)
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
