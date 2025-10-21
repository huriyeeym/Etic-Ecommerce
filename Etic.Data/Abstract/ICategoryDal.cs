using Etic.Core.DataAcses;
using Etic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Data.Abstract
{
    // 2. adım tablo eklendikten sonra dal interface oluşturulur.
    public interface ICategoryDal : IEfEntityRepositoryBase<Category>
    {
    }
}
