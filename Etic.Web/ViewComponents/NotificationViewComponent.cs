using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Etic.Web.ViewComponents
{
    public class NotificationViewComponent : ViewComponent
    {
        public NotificationViewComponent()
        {

        }
        public ViewViewComponentResult Invoke()
        {
            return View();
        }
    }
}
