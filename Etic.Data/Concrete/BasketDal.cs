using Etic.Core.DataAcses;
using Etic.Data.Abstract;
using Etic.Entities;

namespace Etic.Data.Concrete
{
    public class BasketDal : EfEntityRepositoryBase<Basket, EticContext>, IBasketDal

    {
    }
}
