using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Business.Entities;
namespace Shipping.Data
{
    public interface ICategoryRepository
    {
        bool CreateCategory(Category category);
        bool EditCategory(Category category);
        bool DeleteCategory(string categoryCode);
        IEnumerable<Category> GetCategory();
        Category GetCategoryByID(string categoryCode);
        List<Category> GetCategories();
    }
}
