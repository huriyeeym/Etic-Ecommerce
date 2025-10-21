using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etic.Entities;

namespace Etic.Business.Models
{
    public class ProductControllerHandlerModel
    {
        public Product Product { get; set; }
        public IList<ProductImage> Images { get; set; }
    }
}
