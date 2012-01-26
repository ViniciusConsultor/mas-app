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

                SqlParameter[] param = SqlUtility.SetSqlParameter(new string[] { "address", "email", "contact_person" }, new object[] { etAddress.Text.Trim(), etEmail.Text.Trim(), etContact.Text.Trim() });


                if (wantToCreateVessel)
                {
                    VisitaJayaPerkasa.Entities.WareHouse warehouse = new VisitaJayaPerkasa.Entities.WareHouse();
                    warehouse.Id = Guid.NewGuid();
                    warehouse.Phone = etPhone.Text.Trim();
                    warehouse.Fax = etFax.Text.Trim();
                    warehouse.Email = etEmail.Text.Trim();
                    warehouse.Address = etAddress.Text.Trim();
                    warehouse.ContactPerson = etContact.Text.Trim();
                    warehouse.Deleted = 0;

                    if (sqlWareHouseRepository.CheckWarehouse(param, Guid.Empty, true))
                    {
                        DialogResult dResult = MessageBox.Show(this, "stuffing place has already deleted. Do you want to activate ?", "Confirmation", MessageBoxButtons.YesNo);
                        if (dResult == DialogResult.Yes)
                        {
                            SqlParameter[] parameters = SqlUtility.SetSqlParameter(new string[] { "stuffing_place_id", "address", "phone", "fax", "email", "contact_person", "deleted" }
                            , new object[] { warehouse.Id, warehouse.Address, warehouse.Phone, warehouse.Fax, warehouse.Email, warehouse.ContactPerson, warehouse.Deleted });

                            if (sqlWareHouseRepository.ActivateWarehouse(parameters))
                            {
                                MessageBox.Show(this, "Success Activate Stuffing Place", "Information");
                                radButtonElement2.PerformClick();
                            }
                            else
                                MessageBox.Show(this, "Cannot Activate Stuffing Place", "Information");

                            parameters = null;
                        }
                        return;
                    }
                    else if (sqlWareHouseRepository.CheckWarehouse(param, Guid.Empty))
                    {
                        MessageBox.Show(this, "Stuffing Place has already exists", "Information");
                        return;
                    }

                    //Create user 
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "stuffing_place_id", "address", "phone", "fax", "email", "contact_person", "deleted" }
                            , new object[] { warehouse.Id, warehouse.Address, warehouse.Phone, warehouse.Fax, warehouse.Email, warehouse.ContactPerson, warehouse.Deleted });

                    if (sqlWareHouseRepository.CreateWareHouse(sqlParam))
                    {
                        MessageBox.Show(this, "Success create stuffing place", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Create stuffing place", "Information");
                    }
                }
                else
                {
                    warehouse.Id = this.warehouse.Id;
                    warehouse.Phone = etPhone.Text.Trim();
                    warehouse.Fax = etFax.Text.Trim();
                    warehouse.Email = etEmail.Text.Trim();
                    warehouse.Address = etAddress.Text.Trim();
                    warehouse.ContactPerson = etContact.Text.Trim();
                    warehouse.Deleted = 0;

                    if (sqlWareHouseRepository.CheckWarehouse(param, warehouse.Id))
                    {
                        MessageBox.Show(this, "Stuffing Place has already exist. if it has already deleted. you must activate it with create new data", "Information");
                        return;
                    }

                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "stuffing_place_id", "address", "phone", "fax", "email", "contact_person", "deleted" }
                            , new object[] { warehouse.Id, warehouse.Address, warehouse.Phone, warehouse.Fax, warehouse.Email, warehouse.ContactPerson, warehouse.Deleted });

                    if (sqlWareHouseRepository.EditWareHouse(sqlParam))
                    {
                        MessageBox.Show(this, "Success edit stuffing place", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot edit stuffing place", "Information");
                    }
                }


                //SqlParameter[] param;

                //if (wantToCreateVessel)
                //{
                //    //Check vessel code has already exists ?
                //    param = SqlUtility.SetSqlParameter(new string[] { "address" }, new object[] { etAddress.Text.Trim() });          
                //    param = null;
                //    param = SqlUtility.SetSqlParameter(new string[] { "stuffing_place_id", "address", "phone", "fax", "email", "contact_person", "deleted" }, new object[] { Guid.NewGuid(), etAddress.Text.Trim(), etPhone.Text.Trim(), etFax.Text.Trim(), etEmail.Text.Trim(), etContact.Text.Trim(),  0 });

                //    if (sqlWareHouseRepository.CheckWarehouseAddress(param, true))
                //        MessageBox.Show(this, "Address already exist", "Information");
                //    else
                //    {
                //        if (sqlWareHouseRepository.CreateWareHouse(param))
                //        {
                //            MessageBox.Show(this, "Success create warehouse", "Information");
                //            radButtonElement2.PerformClick();
                //        }
                //        else
                //        {
                //            MessageBox.Show(this, "Cannot Create warehouse", "Information");
                //        }
                //    }
                    
                //}
                //else
                //{
                //    param = SqlUtility.SetSqlParameter(new string[] { "stuffing_place_id", "address" }, new object[] { warehouse.Id, etAddress.Text.Trim()});
                //    if (sqlWareHouseRepository.CheckWarehouseAddress(param, true, true))
                //    {
                //        MessageBox.Show(this, "Address already exist", "Information");
                //        return;
                //    }

                //    param = SqlUtility.SetSqlParameter(new string[] { "address", "phone", "fax", "email", "contact_person", "stuffing_place_id" }, new object[] { etAddress.Text.Trim(), etPhone.Text.Trim(), etFax.Text.Trim(), etEmail.Text.Trim(), etContact.Text.Trim(), warehouse.Id });
                //    if (sqlWareHouseRepository.EditWareHouse(param))
                //    {
                //        MessageBox.Show(this, "Success Edit warehouse", "Information");
                //        radButtonElement2.PerformClick();
                //    }
                //    else
                //    {
                //        MessageBox.Show(this, "Cannot Edit warehouse", "Information");
                //    }
                //}

                //param = null;
                //sqlWareHouseRepository = null;
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new WareHouseList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
