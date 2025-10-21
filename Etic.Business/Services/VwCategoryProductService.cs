using Etic.Data.Abstract;
using Etic.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Business.Services
{
    public class VwCategoryProductService : IVwCategoryProductService
    {
        IVwCategoryProductDal _vwCategoryProductDal;
        ISettingService _settingService;

        public VwCategoryProductService(IVwCategoryProductDal vwCategoryProductDal, ISettingService settingService)
        {
            _vwCategoryProductDal = vwCategoryProductDal;
            _settingService = settingService;
        }

        public int CaculatePage(string catSeoname)
        {
            // bu kategori altındaki ürünleri çekelim.
            var products = _vwCategoryProductDal.GetAll(x => x.SeoLinkCat == catSeoname);

            // int adı : pageproductCount biR DEĞİŞKEN İÇERİSİNE bu ürünlerin sayısını atalım
            int pageproductCount = products.Count();
            // settings tablosundan max-category-product değerini çekelim.
            var maxCategoryProduct = _settingService.GetSetting("max-category-product").SettingValue.ToInt32();

            var pageCount = 0;

            var mod = pageproductCount % maxCategoryProduct;
            if (mod == 0)
            {
               return   pageproductCount / maxCategoryProduct;
            }
            else
            {
                var lastProduct = pageproductCount % maxCategoryProduct;
                return ((pageproductCount - lastProduct) / maxCategoryProduct) + 1;
                // 43 ürün sayısı
                // 9 sayfadaki ürün sayısı
                // 43 % 9 = 7 fazla ürün
                // ((43 - 7) / 9)+1
            }
        }

        public int CaculatePageSearch(string query)
        {
            // bu kategori altındaki ürünleri çekelim.
            var products = _vwCategoryProductDal.GetAll(x => x.Name.ToUpper().Contains(query.ToUpper()) || x.ProductDescription.ToUpper().Contains(query.ToUpper()));

            // int adı : pageproductCount biR DEĞİŞKEN İÇERİSİNE bu ürünlerin sayısını atalım
            int pageproductCount = products.Count();
            // settings tablosundan max-category-product değerini çekelim.
            var maxCategoryProduct = _settingService.GetSetting("max-category-product").SettingValue.ToInt32();

            var pageCount = 0;

            var mod = pageproductCount % maxCategoryProduct;
            if (mod == 0)
            {
                return pageproductCount / maxCategoryProduct;
            }
            else
            {
                var lastProduct = pageproductCount % maxCategoryProduct;
                return ((pageproductCount - lastProduct) / maxCategoryProduct) + 1;
                // 43 ürün sayısı
                // 9 sayfadaki ürün sayısı
                // 43 % 9 = 7 fazla ürün
                // ((43 - 7) / 9)+1
            }
        }

        public IList<VwCategoryProduct> List(string catSeoName, int page = 1)
        {
            var maxCategoryProduct = _settingService.GetSetting("max-category-product").SettingValue.ToInt32();


            var products = _vwCategoryProductDal.GetAll(x => x.SeoLinkCat == catSeoName);

            var result = products.Skip((page * maxCategoryProduct) - maxCategoryProduct).Take(maxCategoryProduct).ToList();
            return result;
        }

        public IList<VwCategoryProduct> Search(string query, int page = 1)
        {
            var maxCategoryProduct = _settingService.GetSetting("max-category-product").SettingValue.ToInt32();


            var products = _vwCategoryProductDal.GetAll(x => x.Name.Contains(query) || x.ProductDescription.Contains(query));

            var result = products.Skip((page * maxCategoryProduct) - maxCategoryProduct).Take(maxCategoryProduct).ToList();
            return result;
        }
    }
    public interface IVwCategoryProductService
    {
        public IList<VwCategoryProduct> List(string catSeoName,int page = 1);
        public int CaculatePage(string catSeoname);
        public int CaculatePageSearch(string query);
        public IList<VwCategoryProduct> Search(string query,int page = 1);
    }
}
