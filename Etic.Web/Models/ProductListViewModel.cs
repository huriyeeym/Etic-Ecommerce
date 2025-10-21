using Etic.Entities;

namespace Etic.Web.Models
{
    public class ProductListViewModel
    {
        public IList<VwCategoryProduct> Products { get; set; }
        public int PageCount { get; set; }

        public int CurrentPage { get; set; }
        public string CurrentCategory { get; set; }
    }
}
