using Etic.Entities;

namespace Etic.Web.Models
{
    public class ElectroProductWrapperModel
    {
        public IList<ProductCategory> ProductCategories { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<ProductImage> ProductImages { get; set; }
    }
}
