using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;
using Telerik.WinControls.UI;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using VisitaJayaPerkasa.Entities;

namespace VisitaJayaPerkasa.Control.Supplier
{
    public partial class SupplierEdit : UserControl
    {
        private VisitaJayaPerkasa.Entities.Supplier supplier;
        private bool wantToCreateVessel, isSaveMasterSupplierDetail;
        private List<VisitaJayaPerkasa.Entities.SupplierDetail> listSupplierDetail;
        private SqlCategoryRepository sqlCategoryRepository;
        private SqlPaymentRepository sqlPaymentRepository;
        private SqlCityRepository sqlCityRepository;
        public Guid newGuid;

        public SupplierEdit(VisitaJayaPerkasa.Entities.Supplier supplier)
        {
            InitializeComponent();
            sqlCategoryRepository = new SqlCategoryRepository();
            sqlPaymentRepository = new SqlPaymentRepository();
            sqlCityRepository = new SqlCityRepository();
            List<Category> listCategory = sqlCategoryRepository.GetCategories();
            List<Payment> listPayment = sqlPaymentRepository.GetListPayment();
            List<Entities.City> listCity = sqlCityRepository.GetCity();

            cboCategory.DataSource = listCategory;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "ID";

            cbTypeOfPayment.DataSource = listPayment;
            cbTypeOfPayment.DisplayMember = "Name";
            cbTypeOfPayment.ValueMember = "ID";

            cboDestination.DataSource = listCity;
            cboDestination.DisplayMember = "CityName";
            cboDestination.ValueMember = "ID";
            cboDestination.SelectedIndex = -1;
            cboDestination.Text = Constant.VisitaJayaPerkasaApplication.cboDefaultText;

            this.supplier = supplier;
            this.isSaveMasterSupplierDetail = false;
            sqlCityRepository = null;
            sqlPaymentRepository = null;

            if (supplier == null)
            {
                if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                wantToCreateVessel = true;
                listSupplierDetail = new List<Entities.SupplierDetail>();

                cboCategory.SelectedIndex = -1;
                cboCategory.Text = "-- Choose --";

                cbTypeOfPayment.SelectedIndex = -1;
                cbTypeOfPayment.Text = Constant.VisitaJayaPerkasaApplication.cboDefaultText;
            }
            else
            {
                wantToCreateVessel = false;
                etSupplierName.Text = supplier.SupplierName;
                cboCategory.SelectedItem = supplier.CategoryName;

                if (supplier.TypeOfPayment == null)
                {
                    cbTypeOfPayment.SelectedIndex = -1;
                    cbTypeOfPayment.Text = Constant.VisitaJayaPerkasaApplication.cboDefaultText;
                }
                else
                    cbTypeOfPayment.SelectedValue = supplier.TypeOfPayment;

                etTermOfPayment.Text = supplier.TermOfPayment;
                etAddress.Text = supplier.Address;
                etEmail.Text = supplier.Email;
                etPhone.Text = supplier.Phone;
                etFax.Text = supplier.Fax;
                etContactPerson.Text = supplier.ContactPerson;

                SqlSupplierRepository sqlSupplierRepository = new SqlSupplierRepository();
                listSupplierDetail = sqlSupplierRepository.ListSupplierDetail(supplier.Id);

                if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                {
                    MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (listSupplierDetail != null)
                    supplierDetailGridView.DataSource = listSupplierDetail;
                else
                    listSupplierDetail = new List<VisitaJayaPerkasa.Entities.SupplierDetail>();


                if (supplier.CategoryName.Equals("Trucking"))
                { 
                    List<string> listTemp = sqlSupplierRepository.ListTruckingNumber(supplier.Id);

                    if(listTemp != null)
                        for (int i = 0; i < listTemp.Count; i++)
                            gvTrucking.Rows.Add(listTemp.ElementAt(i));

                    listTemp = null;
                }
                else if (supplier.CategoryName.Equals("Dooring Agent"))
                {
                    List<Entities.City> listTemp = sqlSupplierRepository.ListCitySupplier(supplier.Id);
                    groupBoxDestination.Visible = true;

                    if (listTemp != null)
                        for (int i = 0; i < listTemp.Count; i++)
                        {
                            gvDestination.Rows.AddNew();

                            GridViewDataRowInfo gridViewRow = gvDestination.Rows[i];
                            gridViewRow.Cells["ID"].Value = listTemp.ElementAt(i).ID.ToString();
                            gridViewRow.Cells["CityName"].Value = listTemp.ElementAt(i).CityName;
                        }

                    listTemp = null;
                }

                sqlSupplierRepository = null;
            }
        }

        private void cboCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (etFirstName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill first name", "Information");
            //else if (!Regex.Match(etDetailPhone.Text.Trim(), @"(^\d{5}$)|(^\d{5}-\d{4}$)").Success)
            //    MessageBox.Show(this, "Invalid phone number", "Information");
            //else if (!Regex.Match(etDetailMobile.Text.Trim(), @"(^\d{5}$)|(^\d{5}-\d{4}$)").Success)
            //    MessageBox.Show(this, "Invalid mobile number", "Information");
            else
            {
                VisitaJayaPerkasa.Entities.SupplierDetail supplierDetail = new Entities.SupplierDetail();

                supplierDetail.FirstName = etFirstName.Text.Trim();
                supplierDetail.LastName = etLastName.Text.Trim();
                supplierDetail.SupplierDetailPhone = etDetailPhone.Text.Trim();
                supplierDetail.SupplierDetailMobilePhone = etDetailMobile.Text.Trim();
                supplierDetail.SupplierDetailAddress = etDetailAddress.Text.Trim();
                supplierDetail.SupplierMobileExt = etExt.Text.Trim();
                supplierDetail.SupplierDetailEmail = etDetailEmail.Text.Trim();
                supplierDetail.SupplierDetailDivision = etDivision.Text.Trim();

                for (int i = 0; i < listSupplierDetail.Count; i++)
                {
                    if (listSupplierDetail.ElementAt(i).FirstName == supplierDetail.FirstName &&
                        listSupplierDetail.ElementAt(i).LastName == supplierDetail.LastName)
                    {
                        MessageBox.Show(this, "Data has already exist", "Information");
                        return;
                    }
                }


                listSupplierDetail.Add(supplierDetail);
                supplierDetailGridView.DataSource = typeof(List<VisitaJayaPerkasa.Entities.SupplierDetail>);
                supplierDetailGridView.DataSource = listSupplierDetail;

                supplierDetail = null;
                ResetDetailData();
            }
        }

        private void ResetDetailData()
        {
            etFirstName.Text = "";
            etLastName.Text = "";
            etDetailAddress.Text = "";
            etDetailMobile.Text = "";
            etDetailPhone.Text = "";
            etDivision.Text = "";
            etDetailEmail.Text = "";
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            listSupplierDetail.Clear();
            supplierDetailGridView.DataSource = null;
        }

        private void btnRemoveGrid_Click(object sender, EventArgs e)
        {
            List<VisitaJayaPerkasa.Entities.SupplierDetail> listTemp = new List<VisitaJayaPerkasa.Entities.SupplierDetail>();

            if (supplierDetailGridView.SelectedRows.Count == 1)
            {
                int n = 0;
                for (int i = 0; i < listSupplierDetail.Count; i++)
                {
                    GridViewRowInfo gridInfo = supplierDetailGridView.SelectedRows.First();
                    string firstName = gridInfo.Cells[0].Value.ToString();
                    string lastName = (gridInfo.Cells[1].Value != null) ? gridInfo.Cells[1].Value.ToString() : null;

                    if (listSupplierDetail.ElementAt(i).FirstName == firstName &&
                        listSupplierDetail.ElementAt(i).LastName == lastName)
                        n = i;
                    else
                        listTemp.Add(listSupplierDetail.ElementAt(i));
                }

                listSupplierDetail.RemoveAt(n);
                listSupplierDetail = null;
                listSupplierDetail = listTemp;

                supplierDetailGridView.DataSource = typeof(List<VisitaJayaPerkasa.Entities.SupplierDetail>);
                supplierDetailGridView.DataSource = listSupplierDetail;
            }
            else
                MessageBox.Show(this, "Please select row who will be removed", "Information");
        }

        private bool CheckTrucking() {
            if (gvTrucking.Rows.Count == 0)
            {
                MessageBox.Show(this, "Please add trucking number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else return true;
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (etSupplierName.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "Please fill supplier name", "Information");
            }
            else
            {
                if (!etEmail.Text.Trim().Equals(""))
                {
                    if (!Regex.Match(etEmail.Text.Trim(), @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                    + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                    + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                    + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$").Success)
                    {
                        MessageBox.Show(this, "Invalid email", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                bool successValidation = true;
                if (groupBoxTrucking.Visible) {
                    if (!CheckTrucking())
                        successValidation = false;
                }

                if (successValidation)
                {
                    if (supplierDetailGridView.RowCount == 0)
                    {
                        DialogResult dResult = MessageBox.Show(this, "Are you sure want save this data without supplier detail ? ", "Confirmation", MessageBoxButtons.YesNo);
                        if (dResult == DialogResult.Yes)
                            SaveData();
                    }
                    else
                        SaveData();
                }
            }
        }

        private void SaveData()
        {
            SqlSupplierRepository sqlSupplierRepository = null;

            if (wantToCreateVessel)
            {
                sqlSupplierRepository = new SqlSupplierRepository();
                newGuid = Guid.NewGuid();

                string[] strSqlParam = getStringSqlParameter();
                object[] objSqlParam = GetObjSqlParameter(newGuid);
                SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(strSqlParam, objSqlParam);

                if (sqlSupplierRepository.CheckSupplier(sqlParam, Guid.Empty, true))
                {
                    DialogResult dResult = MessageBox.Show(this, "Supplier has already deleted. Do you want to activate ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dResult == DialogResult.Yes)
                    {
                        if (sqlSupplierRepository.ActivateSupplier(sqlParam))
                        {
                            MessageBox.Show(this, "Success Activate Supplier", "Information");
                            radButtonElement2.PerformClick();
                        }
                        else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                            MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show(this, "Cannot Activate Supplier", "Information");

                        sqlParam = null;
                    }
                    return;
                }
                else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                {
                    MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (sqlSupplierRepository.CheckSupplier(sqlParam, Guid.Empty))
                {
                    MessageBox.Show(this, "Supplier has already exists", "Information");
                    return;
                }
                else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                {
                    MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (sqlSupplierRepository.CreateSupplier(sqlParam, (groupBoxTrucking.Visible) ? gvTrucking.RowCount : 0, (groupBoxDestination.Visible) ? gvDestination.RowCount : 0))
                {
                    MessageBox.Show(this, "Success insert supplier data", "Information");

                    if (isSaveMasterSupplierDetail) {
                        SqlPelayaranRepository sqlPelayaranRepository = new SqlPelayaranRepository();

                        VisitaJayaPerkasa.Entities.Pelayaran pelayaran = sqlPelayaranRepository.GetPelayaranBySupplierID((wantToCreateVessel) ? newGuid.ToString() : this.supplier.Id.ToString());
                        UserControl controllers = new Pelayaran.PelayaranEdit(pelayaran, this);
                        Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);

                        sqlPelayaranRepository = null;
                    }
                    else
                        radButtonElement2.PerformClick();
                }
                else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MessageBox.Show(this, "Cannot insert supplier data", "Information");
                }
                sqlSupplierRepository = null;
                strSqlParam = null;
                objSqlParam = null;
                sqlParam = null;
            }
            else
            {
                sqlSupplierRepository = new SqlSupplierRepository();
                string[] strSqlParam = getStringSqlParameter();
                object[] objSqlParam = GetObjSqlParameter(supplier.Id);
                SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(strSqlParam, objSqlParam);

                if (sqlSupplierRepository.CheckSupplier(sqlParam, this.supplier.Id))
                {
                    MessageBox.Show(this, "supplier has already exist. if it has already deleted. you must activate it with create new data", "Information");
                    return;
                }
                else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                {
                    MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (sqlSupplierRepository.EditSupplier(sqlParam, (groupBoxTrucking.Visible) ? gvTrucking.RowCount : 0, (groupBoxDestination.Visible) ? gvDestination.RowCount : 0))
                {
                    MessageBox.Show(this, "Success edit supplier data", "Information");

                    if (isSaveMasterSupplierDetail)
                    {
                        SqlPelayaranRepository sqlPelayaranRepository = new SqlPelayaranRepository();

                        VisitaJayaPerkasa.Entities.Pelayaran pelayaran = sqlPelayaranRepository.GetPelayaranBySupplierID(this.supplier.Id.ToString());
                        UserControl controllers = new Pelayaran.PelayaranEdit(pelayaran, this);
                        Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);

                        sqlPelayaranRepository = null;
                    }
                    else
                        radButtonElement2.PerformClick();
                }
                else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MessageBox.Show(this, "Cannot edit supplier data", "Information");
                }

                sqlSupplierRepository = null;
                strSqlParam = null;
                objSqlParam = null;
                sqlParam = null;
            }
        }

        private string[] getStringSqlParameter()
        {
            //rowcount * 11 is number of field of supplier detail
            // + 11 is number of field of supplier
            // + 3 is number of field of trucking number or destination supplier
            string[] strSqlParameter;
            if(groupBoxTrucking.Visible)
                strSqlParameter = new string[((supplierDetailGridView.RowCount * 11) + 11) + (gvTrucking.RowCount * 3)];
            else if(groupBoxDestination.Visible)
                strSqlParameter = new string[((supplierDetailGridView.RowCount * 11) + 11) + (gvDestination.RowCount * 3)];
            else
                strSqlParameter = new string[(supplierDetailGridView.RowCount * 11) + 11];
                
            
            strSqlParameter[0] = "supplier_id";
            strSqlParameter[1] = "category_id";
            strSqlParameter[2] = "supplier_name";
            strSqlParameter[3] = "address";
            strSqlParameter[4] = "phone";
            strSqlParameter[5] = "fax";
            strSqlParameter[6] = "email";
            strSqlParameter[7] = "contact_person";
            strSqlParameter[8] = "deleted";
            strSqlParameter[9] = "TypeOfPaymentId";
            strSqlParameter[10] = "TermOfPayment";

            int j = 11;
            for (int i = 0; i < supplierDetailGridView.RowCount; i++)
            {
                strSqlParameter[j++] = "supplier_detail_id";
                strSqlParameter[j++] = "supplier_id";
                strSqlParameter[j++] = "first_name";
                strSqlParameter[j++] = "last_name";
                strSqlParameter[j++] = "phone";
                strSqlParameter[j++] = "mobile_phone";
                strSqlParameter[j++] = "address";
                strSqlParameter[j++] = "deleted";
                strSqlParameter[j++] = "Extention";
                strSqlParameter[j++] = "Email";
                strSqlParameter[j++] = "Division";
            }


            if (gvTrucking.Visible) {
                for (int i = 0; i < gvTrucking.RowCount; i++)
                {
                    strSqlParameter[j++] = "supplier_trucking_id";
                    strSqlParameter[j++] = "supplier_id";
                    strSqlParameter[j++] = "trucking_no";
                }
            }
            else if (gvDestination.Visible) {
                for (int i = 0; i < gvDestination.RowCount; i++)
                {
                    strSqlParameter[j++] = "supplier_destination_id";
                    strSqlParameter[j++] = "supplier_id";
                    strSqlParameter[j++] = "destination_id";
                }
            }

            return strSqlParameter;
        }

        private object[] GetObjSqlParameter(Guid id)
        {
            //rowcount * 11 is number of field of supplier detail
            // + 11 is number of field of supplier
            // + 3 is number of field of trucking or destination supplier
            object[] obj;
            if(groupBoxTrucking.Visible)
                obj = new object[((supplierDetailGridView.RowCount * 11) + 11) + (gvTrucking.RowCount * 3)];
            else if(groupBoxDestination.Visible)
                obj = new object[((supplierDetailGridView.RowCount * 11) + 11) + (gvDestination.RowCount * 3)];
            else 
                obj = new object[(supplierDetailGridView.RowCount * 11) + 11];
            
            obj[0] = id;
            obj[1] = SqlUtility.isDBNULL(cboCategory.SelectedValue.ToString().Trim());
            obj[2] = etSupplierName.Text.Trim();
            obj[3] = SqlUtility.isDBNULL(etAddress.Text.Trim());
            obj[4] = SqlUtility.isDBNULL(etPhone.Text.Trim());
            obj[5] = SqlUtility.isDBNULL(etFax.Text.Trim());
            obj[6] = SqlUtility.isDBNULL(etEmail.Text.Trim());
            obj[7] = SqlUtility.isDBNULL(etContactPerson.Text.Trim());
            obj[8] = 0;
            obj[9] = (cbTypeOfPayment.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText)) ? DBNull.Value : cbTypeOfPayment.SelectedValue;
            obj[10] = etTermOfPayment.Text;

            int i = 11;
            for (int j = 0; j < supplierDetailGridView.RowCount; j++)
            {
                obj[i++] = Guid.NewGuid();
                obj[i++] = id;
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[0].Value + "");
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[1].Value + "");
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[3].Value + "");
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[4].Value + "");
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[5].Value + "");
                obj[i++] = 0;
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[2].Value + "");
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[6].Value + "");
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[7].Value + "");
            }


            if (groupBoxTrucking.Visible) {
                for(int j = 0; j < gvTrucking.RowCount; j++)
                {
                    obj[i++] = Guid.NewGuid();
                    obj[i++] = id;
                    obj[i++] = gvTrucking.Rows[j].Cells[0].Value;
                }
            }
            else if (groupBoxDestination.Visible) {
                for (int j = 0; j < gvDestination.RowCount; j++)
                {
                    obj[i++] = Guid.NewGuid();
                    obj[i++] = id;
                    obj[i++] = gvDestination.Rows[j].Cells[0].Value;
                }
            }

            return obj;
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new SupplierList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.Text.Equals("Trucking"))
            {
                groupBoxTrucking.Visible = true;
                groupBoxDestination.Visible = false;
                btnPelayaran.Visible = false;
            }
            else if (cboCategory.Text.Equals("Shipping Lines")) 
            {
                btnPelayaran.Visible = true;
                groupBoxDestination.Visible = false;
                groupBoxTrucking.Visible = false;
            }
            else if (cboCategory.Text.Equals("Dooring Agent"))
            {
                groupBoxTrucking.Visible = false;
                groupBoxDestination.Visible = true;
                btnPelayaran.Visible = false;
            }
            else
            {
                groupBoxTrucking.Visible = false;
                groupBoxDestination.Visible = false;
                btnPelayaran.Visible = false;
            }
        }

        private void btnAddTrucking_Click(object sender, EventArgs e)
        {
            if (txtTruckingNumber.Text.Trim().Equals(""))
                MessageBox.Show(this, "Please fill trucking number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else 
            {
                gvTrucking.Rows.Add(txtTruckingNumber.Text.Trim());
                txtTruckingNumber.Text = "";
            } 
        }

        private void btnRemoveTrucking_Click(object sender, EventArgs e)
        {
            if(gvTrucking.RowCount > 0 && gvTrucking.CurrentCell != null)
                gvTrucking.Rows.RemoveAt(gvTrucking.CurrentCell.RowIndex);
        }

        private void btnPelayaran_Click(object sender, EventArgs e)
        {
            DialogResult dResult = MessageBox.Show(this, "You must save master supplier, before create detail vessel of supplier.\n Do you want to save ?", "Confirmation", MessageBoxButtons.YesNo);
            if (dResult == DialogResult.Yes){
                isSaveMasterSupplierDetail = true;
                radButtonElement1.PerformClick();
            }
        }

        private void cbTypeOfPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void btnAddDestination_Click(object sender, EventArgs e)
        {
            if (cboDestination.Text.Trim().Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please select city", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (gvDestination.RowCount > 0)
                {
                    for (int i = 0; i < gvDestination.RowCount; i++) {
                        GridViewDataRowInfo info = gvDestination.Rows[i];

                        if (info.Cells["CityName"].Value.Equals(cboDestination.Text))
                        {
                            MessageBox.Show(this, "City has already exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }




                gvDestination.Rows.AddNew();
                int rowIndex = gvDestination.RowCount - 1;

                GridViewDataRowInfo gridViewRow = gvDestination.Rows[rowIndex];
                gridViewRow.Cells["ID"].Value = cboDestination.SelectedValue.ToString();
                gridViewRow.Cells["CityName"].Value = cboDestination.Text;
            } 
        }

        private void btnRemoveDestination_Click(object sender, EventArgs e)
        {
            if (gvDestination.RowCount > 0 && gvDestination.CurrentCell != null)
                gvDestination.Rows.RemoveAt(gvDestination.CurrentCell.RowIndex);
        }

        private void cboDestination_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }


    }
}
