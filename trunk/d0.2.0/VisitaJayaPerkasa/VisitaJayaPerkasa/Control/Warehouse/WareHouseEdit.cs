using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Control.Warehouse
{
    public partial class WareHouseEdit : UserControl
    {
        private bool wantToCreateVessel;
        private Entities.WareHouse warehouse;

        public WareHouseEdit(Entities.WareHouse warehouse)
        {
            InitializeComponent();
            this.warehouse = warehouse;

            if (warehouse == null)
            {
                wantToCreateVessel = true;
            }
            else
            {
                wantToCreateVessel = false;
                etAddress.Text = warehouse.Address;
                etPhone.Text = warehouse.Phone;
                etFax.Text = warehouse.Fax;
                etEmail.Text = warehouse.Email;
                etContact.Text = warehouse.ContactPerson;
            }
        }

        private void WareHouseEdit_Load(object sender, EventArgs e)
        {

        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (etAddress.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill address", "Information");
            else
            {
                SqlWareHouseRepository sqlWareHouseRepository = new SqlWareHouseRepository();
                SqlParameter[] param;

                if (wantToCreateVessel)
                {
                    //Check vessel code has already exists ?
                    param = SqlUtility.SetSqlParameter(new string[] { "address" }, new object[] { etAddress.Text.Trim() });          
                    param = null;
                    param = SqlUtility.SetSqlParameter(new string[] { "stuffing_place_id", "address", "phone", "fax", "email", "contact_person", "deleted" }, new object[] { Guid.NewGuid(), etAddress.Text.Trim(), etPhone.Text.Trim(), etFax.Text.Trim(), etEmail.Text.Trim(), etContact.Text.Trim(),  0 });

                    if (sqlWareHouseRepository.CreateWareHouse(param))
                    {
                        MessageBox.Show(this, "Success create warehouse", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Create warehouse", "Information");
                    }
                }
                else
                {
                    param = SqlUtility.SetSqlParameter(new string[] { "address", "phone", "fax", "email", "contact_person", "stuffing_place_id" }, new object[] { etAddress.Text.Trim(), etPhone.Text.Trim(), etFax.Text.Trim(), etEmail.Text.Trim(), etContact.Text.Trim(), warehouse.Id });

                    if (sqlWareHouseRepository.EditWareHouse(param))
                    {
                        MessageBox.Show(this, "Success Edit warehouse", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Edit warehouse", "Information");
                    }
                }

                param = null;
                sqlWareHouseRepository = null;
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new WareHouseList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
