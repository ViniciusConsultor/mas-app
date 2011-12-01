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

        public bool CreateCategory(Category category)
        {
            return _categoryRepository.CreateCategory(category);
        }

        public bool EditCategory(Category category)
        {
            return _categoryRepository.EditCategory(category);
        }

        public bool DeleteCategory(string categoryCode)
        {
            return _categoryRepository.DeleteCategory(categoryCode);
        }

        public IEnumerable<Category> GetCategory()
        {
            return _categoryRepository.GetCategory();
        }

        public Category GetCategoryByID(string categoryCode)
        {
            return _categoryRepository.GetCategoryByID(categoryCode);
        }
    }
}
