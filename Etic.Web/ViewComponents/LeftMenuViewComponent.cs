using Etic.Business.ComponentHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Etic.Web.ViewComponents
{
    public class LeftMenuViewComponent : ViewComponent
    {
        private ILeftMenuComponentHandler _leftMenuComponentHandler;
        public LeftMenuViewComponent(ILeftMenuComponentHandler leftMenuComponentHandler)
        {
            _leftMenuComponentHandler = leftMenuComponentHandler;
        }
        public ViewViewComponentResult Invoke()
        {
            var result = _leftMenuComponentHandler.GetCategories();
            return View(result);
        }
    }
}
