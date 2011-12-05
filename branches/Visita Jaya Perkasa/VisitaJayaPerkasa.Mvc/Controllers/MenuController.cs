using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using VisitaJayaPerkasa.Business.Services;
using VisitaJayaPerkasa.Configuration;
using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Mvc.Models;

namespace VisitaJayaPerkasa.Mvc.Controllers
{
    public class MenuController : VisitaJayaPerkasa.Web.Mvc.Controller
    {
        private IUserService _userService;
        private IConfigurationManager _configurationManager;


        public MenuController(IUserService userService, IConfigurationManager configurationManager)
        {
            _userService = userService;
            _configurationManager = configurationManager;
        }

        [Authorize]
        [ChildActionOnly]
        public ActionResult Menu()
        {
            MenuModel model = new MenuModel
            {
                Portal50Url = _configurationManager.GetString("VisitaJayaPerkasa.Portal.Mvc.Template.PortalUrl")
            };

            //Get the roles
            List<Role> userRoles = _userService.GetRolesByUserId(Context.User.Id);
            if (userRoles != null && userRoles.Count > 0)
            {
                Role isInAdmin = userRoles.Find(a => a.Name == "Admin");
                if (isInAdmin != null)
                    model.IsAdmin = true;

                Role isInUser = userRoles.Find(a => a.Name == "User");
                if (isInUser != null)
                    model.IsUser = true;
            }

            //Master Menu
            Menu menu = new Menu();

            //Test 1
            #region test1
            if (model.IsUser || model.IsAdmin)
            {
                MenuItem test1a = new MenuItem()
                {
                    //ImageUrl = Url.Content("~/Content/Images/VJP/menu-patients.gif"),
                    Url = String.Format(model.Portal50Url, "patient/default.aspx"),
                    Alt = "Test1",
                    Text = "Test"
                };

                MenuItem mi1 = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "patient/new_patient.aspx"),
                    Text = "m1"
                };
                test1a.Items.Add(mi1);

                MenuItem mi2 = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "patient/default.aspx?s=true"),
                    Text = "m2"
                };
                test1a.Items.Add(mi2);

                if (model.IsAdmin)
                {
                    MenuItem mi3 = new MenuItem()
                    {
                        Url = String.Format(model.Portal50Url, "patient/merge.aspx"),
                        Text = "m3"
                    };
                    test1a.Items.Add(mi3);
                }
                menu.Items.Add(test1a);
            }
            #endregion

            //Test 2
            #region Test2
            if (model.IsUser || model.IsAdmin)
            {
                MenuItem test2a = new MenuItem()
                {
                    //ImageUrl = Url.Content("~/Content/Images/VJP/menu-scheduling.gif"),
                    Url = String.Format(model.Portal50Url, "schedule/default.aspx"),
                    Alt = "Test2",
                    Text = "Test 2"
                };

                MenuItem mi1 = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "schedule/default.aspx?t=r&m=v"),
                    Text = "m1"
                };
                test2a.Items.Add(mi1);

                MenuItem mi2 = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "schedule/default.aspx?t=a&m=v"),
                    Text = "m2"
                };
                test2a.Items.Add(mi2);

                MenuItem mi3 = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "schedule/resourceview.aspx"),
                    Text = "m3"
                };
                test2a.Items.Add(mi3);

                menu.Items.Add(test2a);
            }
#endregion

            //Administration
            #region Administration
            if (model.IsAdmin)
            {
                MenuItem adminMenu = new MenuItem()
                {
                    //ImageUrl = Url.Content("~/Content/Images/VJP/menu-administration.gif"),
                    Url = String.Format(model.Portal50Url, "category"),
                    Alt = "Administration",
                    Text = "Administration"
                };

                MenuItem category = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "category"),
                    Text = "Category"
                };
                adminMenu.Items.Add(category);

                MenuItem city = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "city"),
                    Text = "City"
                };
                adminMenu.Items.Add(city);

                MenuItem condition = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "condition"),
                    Text = "Condition"
                };
                adminMenu.Items.Add(condition);

                MenuItem customer = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "customer"),
                    Text = "Customer"
                };
                adminMenu.Items.Add(customer);

                MenuItem leadTime = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "leadtime"),
                    Text = "LeadTime"
                };
                adminMenu.Items.Add(leadTime);

                MenuItem role = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "role"),
                    Text = "Role"
                };
                adminMenu.Items.Add(role);

                MenuItem supplier = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "supplier"),
                    Text = "Supplier"
                };
                adminMenu.Items.Add(supplier);

                MenuItem typeCont = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "typecont"),
                    Text = "TypeCont"
                };
                adminMenu.Items.Add(typeCont);

                MenuItem user = new MenuItem()
                {
                    Url = String.Format(model.Portal50Url, "user"),
                    Text = "User"
                };
                adminMenu.Items.Add(user);

                menu.Items.Add(adminMenu);
                
            }
            #endregion

            //Setting menu to model
            model.Menu = menu;

            return PartialView(model);
        }

        //
        // GET: /Header/

        public ActionResult Index()
        {
            return View();
        }

    }
}
