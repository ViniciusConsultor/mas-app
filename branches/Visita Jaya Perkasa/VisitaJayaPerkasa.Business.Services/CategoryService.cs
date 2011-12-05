using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Data;

namespace VisitaJayaPerkasa.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void SaveCategory(Category category)
        {
            _categoryRepository.SaveCategory(category);
        }

        public void DeleteCategory(string categoryCode)
        {
            _categoryRepository.DeleteCategory(categoryCode);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public Category GetCategoryByID(string categoryCode)
        {
            return _categoryRepository.GetCategoryByID(categoryCode);
        }
    }
}
