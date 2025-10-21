using Etic.Business.ComponentHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Etic.Web.ViewComponents
{
    public class MenuSliderWrapperViewComponent : ViewComponent
    {
        private IMenuSliderWrapperComponentHandler _menuSliderWrapperComponentHandler;
        public MenuSliderWrapperViewComponent(IMenuSliderWrapperComponentHandler menuSliderWrapperComponentHandler)
        {
            _menuSliderWrapperComponentHandler = menuSliderWrapperComponentHandler;
        }

        public ViewViewComponentResult Invoke()
        {
            var result = _menuSliderWrapperComponentHandler.GetSlider();
            return View(result.OrderBy(x => x.Sort).ToList());
        }
    }
}
