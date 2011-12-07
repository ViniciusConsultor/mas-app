using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitaJayaPerkasa.Business.Services;
using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Mvc.Models;
using MvcContrib.UI.Grid;

namespace VisitaJayaPerkasa.Mvc.Controllers
{
    public class CategoryController : VisitaJayaPerkasa.Web.Mvc.Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }


        [Authorize]
        [HttpGet]
        public ActionResult Index(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<Category> temp = _categoryService.GetCategoryBySearch(searchWord);
            IEnumerable<CategoryModel> listCategory = AutoMapper.Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryModel>>(temp);

            var pagedViewModel = new PagedViewModel<CategoryModel>
            {
                ViewData = ViewData,
                Query = listCategory,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "CategoryName",
                Page = page,
                PageSize = 10,
            }
         .Setup();

            return View(pagedViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddCategory(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                _categoryService.SaveCategory(new Category
                {
                    CategoryCode = categoryModel.CategoryCode,
                    CategoryName = categoryModel.CategoryName
                });
                return RedirectToAction("Index");
            }

            return View(categoryModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditCategory(string ID)
        {
            Category category = _categoryService.GetCategoryByID(ID);
            CategoryModel categoryModel = AutoMapper.Mapper.Map<Category, CategoryModel>(category);

            return View(categoryModel);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditCategory(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                _categoryService.SaveCategory(new Category
                {
                    CategoryCode = categoryModel.CategoryCode,
                    CategoryName = categoryModel.CategoryName
                });
                return RedirectToAction("Index");
            }

            return View(categoryModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteCategory(string ID)
        {
            Category category = _categoryService.GetCategoryByID(ID);
            category.Deleted = 1;

            _categoryService.SaveCategory(category);

            return RedirectToAction("Index");
        }
    }
}
