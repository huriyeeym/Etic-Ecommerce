using Etic.Business.ComponentHandler;
using Etic.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Etic.Web.ViewComponents
{
    public class ElectroProductWrapperViewComponent : ViewComponent
    {
        private IElectroProductWrapperComponentHandler _electroProductWrapperComponentHandler;
        //ctor
        public ElectroProductWrapperViewComponent(IElectroProductWrapperComponentHandler electroProductWrapperComponentHandler)
        {
            _electroProductWrapperComponentHandler = electroProductWrapperComponentHandler;
        }

        public ViewViewComponentResult Invoke()
        {
            ElectroProductWrapperModel model = new ElectroProductWrapperModel();
            model.Categories = _electroProductWrapperComponentHandler.GetCategories().Where(x => x.ParentId == 0)
                .ToList();
            model.ProductCategories = _electroProductWrapperComponentHandler.GetProductCategories();
            model.Products = _electroProductWrapperComponentHandler.GetProducts();
            model.ProductImages = _electroProductWrapperComponentHandler.GetProductImages();
            return View(model);
        }
    }
}
