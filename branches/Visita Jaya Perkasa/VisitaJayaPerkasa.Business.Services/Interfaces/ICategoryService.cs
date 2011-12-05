using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public interface ICategoryService
    {
        void SaveCategory(Category category);
        void DeleteCategory(string categoryCode);
        IEnumerable<Category> GetCategories();
        Category GetCategoryByID(string categoryCode);
    }
}
