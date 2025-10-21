using Etic.Business.ComponentHandler;
using Etic.Business.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Etic.Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private IHeaderComponentHandler _headerComponentHandler;

        public HeaderViewComponent(IHeaderComponentHandler headerComponentHandler)
        {
            _headerComponentHandler = headerComponentHandler;
        }

        public ViewViewComponentResult Invoke()
        {
            var result = _headerComponentHandler.GetUserData(null, Request);

            return View(result);
        }
    }
}
