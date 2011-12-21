using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisitaJayaPerkasa.Control.Warehouse
{
    public partial class WareHouseView : UserControl
    {
        public WareHouseView(Entities.WareHouse warehouse)
        {
            InitializeComponent();
            lblAddress.Text = Utility.Utility.DisplayNullValues(warehouse.Address);
            lblPhone.Text = Utility.Utility.DisplayNullValues(warehouse.Phone);
            lblFax.Text = Utility.Utility.DisplayNullValues(warehouse.Fax);
            lblEmail.Text = Utility.Utility.DisplayNullValues(warehouse.Email);
            lblContact.Text = Utility.Utility.DisplayNullValues(warehouse.ContactPerson);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new WareHouseList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
