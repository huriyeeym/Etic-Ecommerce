using Etic.Entities;

namespace Etic.Web.Models
{
    public class ProductDetailModel
    {
        Product Product { get; set; }
        List<ProductImage> Images { get; set; }
    }
}
