using Etic.Core.DataAcses;
using Etic.Data.Abstract;
using Etic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Data.Concrete
{
    public class VwBasketProductListDal : EfEntityRepositoryBase<VwBasketProductList, EticContext>, IVwBasketProductListDal
    {
    }
}
