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
    //3. adım tablo eklendi sonra interface sonrada class dal
    public class CategoryDal : EfEntityRepositoryBase<Category, EticContext>, ICategoryDal
    {
    }
}
