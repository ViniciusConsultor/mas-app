using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.Control;
using VisitaJayaPerkasa.Entities;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using VisitaJayaPerkasa.Control.UserControls;
using VisitaJayaPerkasa.Control.VesselControls;
using VisitaJayaPerkasa.Control.Customer;
using VisitaJayaPerkasa.Control.Category;
using VisitaJayaPerkasa.Control.City;
using VisitaJayaPerkasa.Control.Condition;
using VisitaJayaPerkasa.Control.LeadTime;
using VisitaJayaPerkasa.Control.Pelayaran;
using VisitaJayaPerkasa.Control.Supplier;
using VisitaJayaPerkasa.Control.TypeCont;
using VisitaJayaPerkasa.Control.Warehouse;

namespace VisitaJayaPerkasa.Form
{
    public partial class MainForm : Telerik.WinControls.UI.RadRibbonForm
    {
        public UserControl controllers;

        public MainForm()
        {
            InitializeComponent();
            Constant.VisitaJayaPerkasaApplication.mainForm = this;

            if (! UserProfile.user.RoleObj.RoleName.ToLower().Equals(Constant.VisitaJayaPerkasaApplication.roleAdmin)) {
                radRibbonBarGroupUser.Visibility = ElementVisibility.Collapsed;
                radRibbonBarGroupCategory.Visibility = ElementVisibility.Collapsed;

                radMenuItemUser.Visibility = ElementVisibility.Collapsed;
                radMenuItemRole.Visibility = ElementVisibility.Collapsed;
            }

            string temp = ((! Utility.Utility.IsStringNullorEmpty(UserProfile.user.FirstName)) && (! Utility.Utility.IsStringNullorEmpty(UserProfile.user.LastName))) ? (UserProfile.user.FirstName + ", " + UserProfile.user.LastName) : (UserProfile.user.FirstName + UserProfile.user.LastName);
            radLabelElementWelcome.Text = "Welcome: " + temp;
            temp = null;
        }

        public void ShowUserControl(object uc) {
            controllers = (UserControl)uc;
            controllers.Dock = DockStyle.Fill;
            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(controllers);            
        }

        private void radImageButtonUser_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UserList());
        }

        private void radImageButtonRole_Click(object sender, EventArgs e)
        {
            ShowUserControl(new VesselList());
        }

        private void radImageButtonVessel_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UserList());
        }

        private void radMenuItemUser_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UserList());
        }

        private void radMenuItemRole_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UserList());
        }

        private void radMenuItemVessel_Click(object sender, EventArgs e)
        {
            ShowUserControl(new VesselList());
        }

        private void radMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
            VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.loginForm.Close();
        }

        private void radMenuItemLogOut_Click(object sender, EventArgs e)
        {
            UserProfile.user = null;
            this.Close();
            VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.loginForm.Show();
        }
        
        private void radImageButtonElementCustomer_Click(object sender, EventArgs e)
        {
            ShowUserControl(new CustomerList());
        }

        private void radImageButtonElementCategory_Click(object sender, EventArgs e)
        {
            ShowUserControl(new CategoryList());
        }

        private void radImageButtonElementVessel_Click(object sender, EventArgs e)
        {
            ShowUserControl(new VesselList());
        }

        private void radImageButtonElementCity_Click(object sender, EventArgs e)
        {
            ShowUserControl(new CityList());
        }

        private void radImageButtonElementCondition_Click(object sender, EventArgs e)
        {
            ShowUserControl(new ConditionList());
        }

        private void radImageButtonElementLeadTime_Click(object sender, EventArgs e)
        {
            ShowUserControl(new LeadTimeList());
        }

        private void radImageButtonElementPelayaran_Click(object sender, EventArgs e)
        {
            ShowUserControl(new PelayaranList());
        }

        private void radImageButtonElementSupplier_Click(object sender, EventArgs e)
        {
            ShowUserControl(new SupplierList());
        }

        private void radImageButtonElementTypeCont_Click(object sender, EventArgs e)
        {
            ShowUserControl(new TypeContList());
        }

        private void radImageButtonElementWareHouse_Click(object sender, EventArgs e)
        {
            ShowUserControl(new WareHouseList());
        }


    }
}