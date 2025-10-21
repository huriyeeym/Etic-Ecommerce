using Etic.Business.ControllerHandler;
using Etic.Entities;
using Etic.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Etic.Web.Controllers
{
    public class ApiController : Controller
    {
        private IApiControllerHandler _apiControllerHandler;

        public ApiController(IApiControllerHandler apiControllerHandler)
        {
            _apiControllerHandler = apiControllerHandler;
        }

        [HttpPost]
        public JsonResult AddToBasket(int productId, int quantity)
        {
            var result = _apiControllerHandler.CheckBasket(Request, Response);
            // urun eklicez
            _apiControllerHandler.AddBasketProduct(productId, quantity, result.ToString());
            BasketModel model = new BasketModel();
            model.BasketProducts = _apiControllerHandler.GetBasketProducts(result.ToString());
            model.Quantity = model.BasketProducts.Sum(x => x.Quantity);
            int[] ids = model.BasketProducts.Select(x => x.ProductId).ToArray();
            var basketProductList = _apiControllerHandler.GetBasketProductCalculatePrice(ids);

            decimal totalPrice = 0;
            foreach (var product in basketProductList)
            {
                var q = model.BasketProducts.FirstOrDefault(x => x.ProductId == product.Id).Quantity;
                totalPrice += product.Price * q;

            }
            model.TotalPrice = totalPrice;

            var productHtml = GenerateProducts(basketProductList, model);
            model.ProductHtml = productHtml;
            return Json(model);
        }

        [HttpPost]
        public JsonResult GetBasket()
        {
            var result = _apiControllerHandler.CheckBasket(Request, Response);

            BasketModel model = new BasketModel();
            model.BasketProducts = _apiControllerHandler.GetBasketProducts(result.ToString());
            model.Quantity = model.BasketProducts.Sum(x => x.Quantity);
            int[] ids = model.BasketProducts.Select(x => x.ProductId).ToArray();
            var basketProductList = _apiControllerHandler.GetBasketProductCalculatePrice(ids);

            decimal totalPrice = 0;
            foreach (var product in basketProductList)
            {
                var q = model.BasketProducts.FirstOrDefault(x => x.ProductId == product.Id).Quantity;
                totalPrice += product.Price * q;

            }
            model.TotalPrice = totalPrice;

            var productHtml = GenerateProducts(basketProductList, model);
            model.ProductHtml = productHtml;
            return Json(model);
        }

        public string GenerateProducts(IList<Product> products, BasketModel model)
        {
            string template = System.IO.File.ReadAllText(Environment.CurrentDirectory + "/TextTemplates/BasketProduct.txt");
            string temp = template;
            string result = null;
            foreach (var item in products)
            {
                template = template.Replace("[link]", "/product/" + item.SeoLink);
                template = template.Replace("[img]", _apiControllerHandler.GetProductImage(item.Id));
                template = template.Replace("[name]", item.Name);

                var q = model.BasketProducts.FirstOrDefault(x => x.ProductId == item.Id).Quantity;

                template = template.Replace("[total]", (item.Price).ToString("C"));
                template = template.Replace("[quantity]", q.ToString());


                result += template;
                template = temp;
            }
            return result;
        }
    }
}
