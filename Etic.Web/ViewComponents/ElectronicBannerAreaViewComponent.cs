using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Etic.Web.ViewComponents
{
    public class ElectronicBannerAreaViewComponent : ViewComponent
    {
        public ElectronicBannerAreaViewComponent()
        {

        }
        public ViewViewComponentResult Invoke()
        {
            return View();
        }
    }
}
