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
            .AddFilter("searchWord", searchWord, a => a.FirstName.ToLower().Contains(searchWord.ToLower()) ||
                a.LastName.ToLower().Contains(searchWord.ToLower()) ||
                a.Username.ToLower().Contains(searchWord.ToLower()))
            //.AddFilter("roleId", roleId, a => a. == artistId, _userService.GetArtists(), "Name")
            .Setup();

            return View(pagedViewModel);
        }


        [Authorize]
        public ActionResult DeletedIndex(string searchWord, int? roleId, GridSortOptions gridSortOptions, int? page)
        {
            IEnumerable<User> temp = _userService.GetAllDeletedUsers();
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
        [HttpGet]
        public ActionResult AddUser()
        {
            var viewModel = new UserModel();

            var roles = _roleService.GetListRole();
            var roleList = (from o in roles select new SelectListItem { Text = o.Name, Value = o.Name }).ToList();
            roleList.Insert(0, new SelectListItem { Selected = true, Text = "-- Select Role  --", Value = "" });
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
        [HttpPost]
        public ActionResult AddUser(UserModel userModel)
        {
            var roles = _roleService.GetListRole();
            var roleList = (from o in roles select new SelectListItem { Text = o.Name, Value = o.Name }).ToList();
            roleList.Insert(0, new SelectListItem { Selected = true, Text = "-- Select Role  --", Value = "" });
            userModel.Roles = roleList.ToList();

            userModel.MaritalStatuses = new List<SelectListItem>();
            userModel.MaritalStatuses.Add(new SelectListItem { Selected = true, Text = "Unmarried", Value = "Unmarried" });
            userModel.MaritalStatuses.Add(new SelectListItem { Selected = false, Text = "Married", Value = "Married" });

            userModel.Genders = new List<SelectListItem>();
            userModel.Genders.Add(new SelectListItem { Selected = true, Text = "Male", Value = "Male" });
            userModel.Genders.Add(new SelectListItem { Selected = false, Text = "Female", Value = "Female" });

            if (string.IsNullOrWhiteSpace(userModel.Password))
                userModel.Password = "12345";

            if (string.IsNullOrWhiteSpace(userModel.SelectedRoleName))
                ModelState.AddModelError("SelectedRoleName", "Role must be selected");

            if (string.IsNullOrWhiteSpace(userModel.Username))
                ModelState.AddModelError("Username", "Username must be filled");

            if (ModelState.IsValid)
            {
                userModel.Id = Guid.NewGuid();
                User user = new User
                {
                    Id = userModel.Id,
                    Username = userModel.Username,
                    Password = userModel.Password,
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
                };
                List<UserRole> userRoles = new List<UserRole>();
                userRoles.Add(new UserRole
                {
                    UserRoleId = Guid.NewGuid(),
                    UserId = userModel.Id,
                    RoleName = userModel.SelectedRoleName,
                    Deleted = 0
                });

                _userService.SaveUser(user, userRoles);

                return RedirectToAction("Index");
            }

            return View(userModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditUser(Guid id)
        {
            var viewModel = new UserModel();

            var roles = _roleService.GetListRole();
            var roleList = (from o in roles select new SelectListItem { Text = o.Name, Value = o.Name }).ToList();
            roleList.Insert(0, new SelectListItem { Selected = true, Text = "-- Select Role  --", Value = "" });
            viewModel.Roles = roleList.ToList();

            viewModel.MaritalStatuses = new List<SelectListItem>();
            viewModel.MaritalStatuses.Add(new SelectListItem { Selected = true, Text = "Unmarried", Value = "Unmarried" });
            viewModel.MaritalStatuses.Add(new SelectListItem { Selected = false, Text = "Married", Value = "Married" });

            viewModel.Genders = new List<SelectListItem>();
            viewModel.Genders.Add(new SelectListItem { Selected = true, Text = "Male", Value = "Male" });
            viewModel.Genders.Add(new SelectListItem { Selected = false, Text = "Female", Value = "Female" });

            User model = _userService.GetUser(id);
            List<Role> userRoles = _userService.GetRolesByUserId(id);
            string selectedRoleName = "";
            if (userRoles != null)
            {
                foreach (Role role in userRoles)
                    selectedRoleName += role.Name + ",";
                selectedRoleName = selectedRoleName.TrimEnd(',');
            }
            else
            {
                selectedRoleName = "No Role";
            }

            viewModel.SelectedRoleName = selectedRoleName;
            viewModel.Id = model.Id;
            viewModel.Username = model.Username;
            viewModel.Password = model.Password;
            viewModel.Salt = model.Salt;
            viewModel.LastName = model.LastName;
            viewModel.FirstName = model.FirstName;
            viewModel.MiddleInitial = model.MiddleInitial;
            viewModel.Email = model.Email;
            viewModel.PhoneNumber = model.PhoneNumber;
            viewModel.MobilePhoneNumber = model.MobilePhoneNumber;
            viewModel.Nik = model.Nik;
            viewModel.Address = model.Address;
            viewModel.DateOfBirth = model.DateOfBirth;
            viewModel.MaritalStatus = model.MaritalStatus;
            viewModel.Gender = model.Gender;
            viewModel.Deleted = model.Deleted;
            viewModel.StartingDate = model.StartingDate;
            viewModel.SalaryPerMonth = model.SalaryPerMonth;
            viewModel.UmkPerDay = model.UmkPerDay;
            
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditUser(UserModel userModel)
        {
            var roles = _roleService.GetListRole();
            var roleList = (from o in roles select new SelectListItem { Text = o.Name, Value = o.Name }).ToList();
            roleList.Insert(0, new SelectListItem { Selected = true, Text = "-- Select Role  --", Value = "" });
            userModel.Roles = roleList.ToList();

            userModel.MaritalStatuses = new List<SelectListItem>();
            userModel.MaritalStatuses.Add(new SelectListItem { Selected = true, Text = "Unmarried", Value = "Unmarried" });
            userModel.MaritalStatuses.Add(new SelectListItem { Selected = false, Text = "Married", Value = "Married" });

            userModel.Genders = new List<SelectListItem>();
            userModel.Genders.Add(new SelectListItem { Selected = true, Text = "Male", Value = "Male" });
            userModel.Genders.Add(new SelectListItem { Selected = false, Text = "Female", Value = "Female" });

            User user = _userService.GetUser(userModel.Id);
            userModel.Username = user.Username;

            if (ModelState.IsValid)
            {
                user.LastName = userModel.LastName;
                user.FirstName = userModel.FirstName;
                user.MiddleInitial = userModel.MiddleInitial;
                user.Email = userModel.Email;
                user.PhoneNumber = userModel.PhoneNumber;
                user.MobilePhoneNumber = userModel.MobilePhoneNumber;
                user.Nik = userModel.Nik;
                user.Address = userModel.Address;
                user.DateOfBirth = userModel.DateOfBirth;
                user.MaritalStatus = userModel.MaritalStatus;
                user.Gender = userModel.Gender;
                user.StartingDate = userModel.StartingDate;
                user.SalaryPerMonth = userModel.SalaryPerMonth;
                user.UmkPerDay = userModel.UmkPerDay;

                List<UserRole> userRoles = new List<UserRole>();

                _userService.SaveUser(user, userRoles);

                return RedirectToAction("Index");
            }

            return View(userModel);
        }

        [Authorize]
        public ActionResult Delete(Guid id)
        {
            _userService.DeleteUser(id);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Undelete(Guid id)
        {
            _userService.UndeleteUser(id);

            return RedirectToAction("DeletedIndex");
        }
    }
}
