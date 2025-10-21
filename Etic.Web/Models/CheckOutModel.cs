using Etic.Entities;

namespace Etic.Web.Models
{
    public class CheckOutModel
    {
        public IList<VwBasketProductList> BasketProducts { get; set; }
        public UserAddress UserAddress { get; set; }
        public User User { get; set; }
    }

}
