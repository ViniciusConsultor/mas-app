using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipping.Business.Services;
using Shipping.Business.Entities;
using Shipping.Mvc.Models.Category;

namespace Shipping.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Category> temp = _categoryService.GetCategory();
            IEnumerable<CategoryModel> listCategory = AutoMapper.Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryModel>>(temp);


            return View(listCategory);
        }


        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                Category category = AutoMapper.Mapper.Map<CategoryModel, Category>(categoryModel);
                bool isSuccess = _categoryService.CreateCategory(category);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your category data and try again");
                    return View(categoryModel);
                }
            }

            return View(categoryModel);
        }


        [HttpGet]
        public ActionResult EditCategory(string ID)
        {
            Category category = _categoryService.GetCategoryByID(ID);
            CategoryModel categoryModel = AutoMapper.Mapper.Map<Category, CategoryModel>(category);

            return View(categoryModel);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                Category category = AutoMapper.Mapper.Map<CategoryModel, Category>(categoryModel);
                bool isSuccess = _categoryService.EditCategory(category);

                if (isSuccess)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Please check your category data and try again");
                    return View(categoryModel);
                }
            }
            else
                return View(categoryModel);
        }


        [HttpGet]
        public ActionResult DeleteCategory(string ID)
        {
            bool isSuccess = _categoryService.DeleteCategory(ID);
            if (isSuccess)
                return RedirectToAction("Index");
            else
            {
                ModelState.AddModelError(string.Empty, "Cannot delete category");
                return RedirectToAction("Index");
            }
        }

        public ActionResult FilterRecordCategoryPartial()
        {
            IEnumerable<Category> temp = _categoryService.GetCategory();
            IEnumerable<CategoryModel> listCategory = AutoMapper.Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryModel>>(temp);

            return PartialView("FilterRecordCategoryPartial", listCategory);
        }

    }
}
