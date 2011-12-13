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

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {

        }


    }
}