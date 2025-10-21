using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etic.Business.Services;
using Etic.Entities;

namespace Etic.Business.ComponentHandler
{
    public class LeftMenuComponentHandler : ILeftMenuComponentHandler
    {
        private readonly  ICategoryService _categoryService;

        public LeftMenuComponentHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IList<Category> GetCategories()
        {
            return _categoryService.GetAllCategories();
        }
    }

    public interface ILeftMenuComponentHandler
    {
        IList<Category> GetCategories();
    }
}
