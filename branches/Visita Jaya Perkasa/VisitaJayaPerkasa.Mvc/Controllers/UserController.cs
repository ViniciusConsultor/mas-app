using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitaJayaPerkasa.Business.Services;
using MvcContrib.UI.Grid;
using VisitaJayaPerkasa.Mvc.Models;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Mvc.Controllers
{
    public class UserController : VisitaJayaPerkasa.Web.Mvc.Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [Authorize]
        public ActionResult Index(string searchWord, int? roleId, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<User> temp = _userService.GetAllUsers();
            IEnumerable<UserModel> list = AutoMapper.Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(temp);
            var pagedViewModel = new PagedViewModel<UserModel>
            {
                ViewData = ViewData,
                Query = list,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "Name",
                Page = page,
                PageSize = 10,
            }
            .AddFilter("searchWord", searchWord, a => a.FirstName.Contains(searchWord) || a.LastName.Contains(searchWord) || a.Username.Contains(searchWord))
            //.AddFilter("roleId", roleId, a => a. == artistId, _userService.GetArtists(), "Name")
            .Setup();

            return View(pagedViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddUser(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                userModel.Id = Guid.NewGuid();

                _userService.SaveUser(new User
                {
                    Id = userModel.Id,
                    Username = userModel.Username,
                    Password = "12345",
                    Salt = "saltnyaboongan:(",
                    LastName = userModel.LastName,
                    FirstName = userModel.FirstName,
                    MiddleInitial = userModel.MiddleInitial,
                    Email = userModel.Email,
                    PhoneNumber = userModel.PhoneNumber,
                    MobilePhoneNumber = userModel.MobilePhoneNumber,
                    Nik = userModel.Nik,
                    Address = userModel.Address,
                    DateOfBirth = userModel.DateOfBirth,
                    MaritalStatus = userModel.MaritalStatus,
                    Gender = userModel.Gender,
                    Deleted = 0,
                    StartingDate = userModel.StartingDate,
                    SalaryPerMonth = userModel.SalaryPerMonth,
                    UmkPerDay = userModel.UmkPerDay
                }, new UserRole 
                {
                    UserRoleId = Guid.NewGuid(),
                    UserId = userModel.Id,
                    RoleName = userModel.SelectedRoleName,
                    Deleted = 0
                }
                );

                return RedirectToAction("Index");
            }

            return View(userModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddUser()
        {
            var roles = _roleService.GetListRole();
            var roleList = (from o in roles select new SelectListItem { Text = o.Name, Value = o.Name }).ToList();
            roleList.Insert(0, new SelectListItem { Selected = true, Text = "-- Select Role  --", Value = "" });
            var viewModel = new UserModel();
            viewModel.Roles = roleList.ToList();
            viewModel.MaritalStatuses = new List<SelectListItem>();
            viewModel.MaritalStatuses.Add(new SelectListItem { Selected = true, Text = "Unmarried", Value = "Unmarried" });
            viewModel.MaritalStatuses.Add(new SelectListItem { Selected = false, Text = "Married", Value = "Married" });
            viewModel.Genders = new List<SelectListItem>();
            viewModel.Genders.Add(new SelectListItem { Selected = true, Text = "Male", Value = "Male" });
            viewModel.Genders.Add(new SelectListItem { Selected = false, Text = "Female", Value = "Female" });

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Delete(Guid id)
        {
            _userService.DeleteUser(id);

            return RedirectToAction("Index");
        }
    }
}
