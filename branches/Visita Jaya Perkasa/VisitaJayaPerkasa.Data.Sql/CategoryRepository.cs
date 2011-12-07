using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VisitaJayaPerkasa.Business.Entities;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Data.Sql
{
    public class CategoryRepository : ICategoryRepository
    {
        private string _mainConnectionString;

        public CategoryRepository(string mainConnectionString)
        {
            _mainConnectionString = mainConnectionString;
        }

        public void SaveCategory(Category category)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            using (var scope = repo.GetTransaction())
            {
                if (GetCategoryByID(category.CategoryCode) == null)
                {
                    //Create new
                    repo.Insert(category);
                }
                else
                {
                    //Update it

                    repo.Update("Category", "category_code", category);
                }

                scope.Complete();
            }

            repo.CloseSharedConnection();
        }

        public void DeleteCategory(string categoryCode)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();
            Category category = GetCategoryByID(categoryCode);
            repo.Delete("Category", "category_code", category);
            repo.CloseSharedConnection();  
        }

        public IEnumerable<Category> GetCategories()
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<Category> categories = repo.Fetch<Category>("SELECT * FROM [Category] WHERE (deleted is null OR deleted = '0')").ToList<Category>();

            repo.CloseSharedConnection();

            return categories;
        }

        public Category GetCategoryByID(string categoryCode)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            Category category = repo.SingleOrDefault<Category>("SELECT * FROM CATEGORY WHERE category_code=@0 AND (deleted is null OR deleted = '0')", categoryCode);

            repo.CloseSharedConnection();

            return category;
        }


        public IEnumerable<Category> GetCategoryBySearch(string searchWord)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();

            IEnumerable<Category> listCategory = repo.Fetch<Category>(
                "SELECT * FROM CATEGORY WHERE (deleted is null OR deleted = '0') AND (category_name like '%" + searchWord + "%' OR category_code like '%" + searchWord + "%')"
            ).ToList<Category>();

            repo.CloseSharedConnection();
            return listCategory;
        }
    }
}
