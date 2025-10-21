using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etic.Business.Models;
using Etic.Business.Services;
using Etic.Entities;

namespace Etic.Business.ControllerHandler
{
    public class ProductControllerHandler : IProductControllerHandler
    {
        IProductService _productService;
        IProductImageService _productImageService;

        public ProductControllerHandler(IProductService productService, IProductImageService productImageService)
        {
            _productService = productService;
            _productImageService = productImageService;
        }

        public ProductControllerHandlerModel Get(string name)
        {
            #region Data Çekme İşlemleri
            Product productDetail = _productService.GetBySeoUrl(name);
            var productImages = _productImageService.GetAllByProductId(productDetail.Id);
          
            #endregion

            #region Model İçini Doldurduk
            ProductControllerHandlerModel model = new ProductControllerHandlerModel();
            model.Images = productImages;
            model.Product = productDetail;
            #endregion
            return model;
        }
    }
    public interface IProductControllerHandler
    {
        ProductControllerHandlerModel Get(string name);
    }
}
