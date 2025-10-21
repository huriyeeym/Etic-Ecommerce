using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etic.Data.Abstract;
using Etic.Entities;

namespace Etic.Business.Services
{
    //5. adım service yazılır.
    public class CategoryService : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IList<Category> GetAllCategories()
        {
            return _categoryDal.GetAll();
        }
    }

    public interface ICategoryService
    {
        IList<Category> GetAllCategories();
    }
}
