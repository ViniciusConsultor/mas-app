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
using VisitaJayaPerkasa.Control.Customer;
using VisitaJayaPerkasa.Control.City;
using VisitaJayaPerkasa.Control.ConditionControl;
using VisitaJayaPerkasa.Control.Pelayaran;
using VisitaJayaPerkasa.Control.Supplier;
using VisitaJayaPerkasa.Control.TypeCont;
using VisitaJayaPerkasa.Control.Warehouse;
using VisitaJayaPerkasa.Control.Schedule;
using VisitaJayaPerkasa.Control.Transaction;
using VisitaJayaPerkasa.Control.Recipient;
using VisitaJayaPerkasa.Control.PriceListCustomer;
using VisitaJayaPerkasa.Form.Report.Container;
using VisitaJayaPerkasa.Form.Report.Delivery;
using VisitaJayaPerkasa.Form.Report.Invoice;
using VisitaJayaPerkasa.Form.Report.Schedule;
using VisitaJayaPerkasa.Form.Report.Request_Price;

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
                RBGUser.Visibility = ElementVisibility.Collapsed;

                radMenuItemUser.Visibility = ElementVisibility.Collapsed;
            }

            string temp = ((! Utility.Utility.IsStringNullorEmpty(UserProfile.user.FirstName)) && (! Utility.Utility.IsStringNullorEmpty(UserProfile.user.LastName))) ? (UserProfile.user.FirstName + ", " + UserProfile.user.LastName) : (UserProfile.user.FirstName + UserProfile.user.LastName);
            radLabelElementWelcome.Text = "Welcome: " + temp;
            temp = null;
            this.Closed += new System.EventHandler(this.mainForm_Closed);
        }

        public void ShowUserControl(object uc) {
            controllers = (UserControl)uc;
            controllers.Dock = DockStyle.Fill;
            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(controllers);            
        }

        private void radMenuItemUser_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UserList());
        }

        private void radMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radMenuItemLogOut_Click(object sender, EventArgs e)
        {
            UserProfile.user = null;
            VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.loginForm.Show();
            this.Close();
        }

        private void mainForm_Closed(object sender, EventArgs e)
        {
            if (UserProfile.user != null)
                VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.loginForm.Close();
        }

        private void radMenuItemCity_Click(object sender, EventArgs e)
        {
            ShowUserControl(new CityList());
        }

        private void radMenuItemCondition_Click(object sender, EventArgs e)
        {
            ShowUserControl(new ConditionList());
        }

        private void radMenuItemCustomer_Click(object sender, EventArgs e)
        {
            ShowUserControl(new CustomerList());
        }

        private void radMenuItemPelayaran_Click(object sender, EventArgs e)
        {
            ShowUserControl(new PelayaranList());
        }

        private void radMenuItemSupplier_Click(object sender, EventArgs e)
        {
            ShowUserControl(new SupplierList());
        }

        private void radMenuItemTypeCont_Click(object sender, EventArgs e)
        {
            ShowUserControl(new TypeContList());
        }

        private void radMenuItemWareHouse_Click(object sender, EventArgs e)
        {
            ShowUserControl(new WareHouseList());
        }

        private void radMenuItemRecipient_Click(object sender, EventArgs e)
        {
            ShowUserControl(new RecipientList());
        }

        private void radMenuItemPriceList_Click(object sender, EventArgs e)
        {
            ShowUserControl(new VisitaJayaPerkasa.Control.PriceList.PriceList());
        }

        private void radMenuItemSchedule_Click(object sender, EventArgs e)
        {
            ShowUserControl(new ScheduleList());
        }

        private void radMenuItemPriceListCustomer_Click(object sender, EventArgs e)
        {
            ShowUserControl(new PriceListCustomer());
        }

        private void radMenuItemOrderList_Click(object sender, EventArgs e)
        {
            ShowUserControl(new CustomerTransList());
        }


        private void SetEnableDisableGroup(RadRibbonBarGroup rbg) {
            if (Constant.VisitaJayaPerkasaApplication.RBGroup != null)
                Constant.VisitaJayaPerkasaApplication.RBGroup.Enabled = true;

            Constant.VisitaJayaPerkasaApplication.RBGroup = rbg;
            Constant.VisitaJayaPerkasaApplication.RBGroup.Enabled = false;
        }


        //Group Item
        private void radImageButtonUser_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGUser);
            ShowUserControl(new UserList());
        }

        private void radImageButtonElementCity_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGCity);
            ShowUserControl(new CityList());
        }

        private void radImageButtonElementCondition_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGCondition);
            ShowUserControl(new ConditionList());
        }

        private void radImageButtonElementCustomer_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGCustomer);
            ShowUserControl(new CustomerList());
        }

        private void radImageButtonElementPelayaran_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGPelayaran);
            ShowUserControl(new PelayaranList());
        }

        private void radImageButtonElementSupplier_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGSupplier);
            ShowUserControl(new SupplierList());
        }

        private void radImageButtonElementTypeCont_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGTypeCont);
            ShowUserControl(new TypeContList());
        }

        private void radImageButtonElementWareHouse_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGWarehouse);
            ShowUserControl(new WareHouseList());
        }

        private void radRibbonBarGroupRecipient_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGRecipient);
            ShowUserControl(new RecipientList());
        }

        private void radImageButtonPriceList_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGPriceList);
            ShowUserControl(new VisitaJayaPerkasa.Control.PriceList.PriceList());
        }

        private void radImageButtonSchedule_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGSchedule);
            ShowUserControl(new ScheduleList());
        }

        private void radImageButtonPriceListCust_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGPriceListCust);
            ShowUserControl(new PriceListCustomer());
        }

        private void radImageButtonOrderList_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGTransaction);
            ShowUserControl(new CustomerTransList());
        }

        private void radImageButtonContainer_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGContainer);
            ShowUserControl(new RptContainerControl());
        }

        private void radImageButtonDelivery_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGDelivery);
            ShowUserControl(new RptDeliveryControl());
        }

        private void radImageButtonATK_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGATK);
            ShowUserControl(new VisitaJayaPerkasa.Control.ATK.AtkList());
        }

        private void radImageButtonInvoice_Click(object sender, EventArgs e)
        {
            string temp = ((!Utility.Utility.IsStringNullorEmpty(UserProfile.user.FirstName)) && (!Utility.Utility.IsStringNullorEmpty(UserProfile.user.LastName))) ? (UserProfile.user.FirstName + ", " + UserProfile.user.LastName) : (UserProfile.user.FirstName + UserProfile.user.LastName);
            SetEnableDisableGroup(RBGInvoice);
            ShowUserControl(new RptInvoiceControl(temp));
        }

        private void radImageButtonElement1_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGRptSchedule);
            ShowUserControl(new RptSchedule());
        }

        private void radImageButtonElement2_Click(object sender, EventArgs e)
        {
            SetEnableDisableGroup(RBGSupplyPrice);
            ShowUserControl(new rptSupplyPriceControl());
        }

    }
}