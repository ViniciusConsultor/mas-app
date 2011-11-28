using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Business.Entities;
using Shipping.Data;

namespace Shipping.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }
    }
}
