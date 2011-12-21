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

namespace VisitaJayaPerkasa.Control.Supplier
{
    public partial class SupplierEdit : UserControl
    {
        private VisitaJayaPerkasa.Entities.Supplier supplier;
        private bool wantToCreateVessel;
        private List<VisitaJayaPerkasa.Entities.SupplierDetail> listSupplierDetail;
        private SqlCategoryRepository sqlCategoryRepository;

        public SupplierEdit(VisitaJayaPerkasa.Entities.Supplier supplier)
        {
            InitializeComponent();
            sqlCategoryRepository = new SqlCategoryRepository();
            List<VisitaJayaPerkasa.Entities.Category> listCategory = sqlCategoryRepository.GetCategories();
            cboCategory.DataSource = listCategory;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "ID";
            this.supplier = supplier;

            if (supplier == null)
            {
                wantToCreateVessel = true;
                listSupplierDetail = new List<Entities.SupplierDetail>();
            }
            else
            {
                wantToCreateVessel = false;
                etSupplierName.Text = supplier.SupplierName;
                cboCategory.SelectedItem = supplier.CategoryId;
                etAddress.Text = supplier.Address;
                etEmail.Text = supplier.Email;
                etPhone.Text = supplier.Phone;
                etFax.Text = supplier.Fax;
                etContactPerson.Text = supplier.ContactPerson;

                SqlSupplierRepository sqlSupplierRepository = new SqlSupplierRepository();
                listSupplierDetail = sqlSupplierRepository.ListSupplierDetail(supplier.Id);

                if (listSupplierDetail != null)
                    supplierDetailGridView.DataSource = listSupplierDetail;
                else
                    listSupplierDetail = new List<VisitaJayaPerkasa.Entities.SupplierDetail>();

                sqlSupplierRepository = null;
            }
        }

        private void cboCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(8);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (etFirstName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill first name", "Information");
            else if (!Regex.Match(etDetailPhone.Text.Trim(), @"(^\d{5}$)|(^\d{5}-\d{4}$)").Success)
                MessageBox.Show(this, "Invalid phone number", "Information");
            else if (!Regex.Match(etDetailMobile.Text.Trim(), @"(^\d{5}$)|(^\d{5}-\d{4}$)").Success)
                MessageBox.Show(this, "Invalid mobile number", "Information");
            else
            {
                VisitaJayaPerkasa.Entities.SupplierDetail supplierDetail = new Entities.SupplierDetail();

                supplierDetail.FirstName = etFirstName.Text.Trim();
                supplierDetail.LastName = etLastName.Text.Trim();
                supplierDetail.SupplierDetailPhone = etDetailPhone.Text.Trim();
                supplierDetail.SupplierDetailMobilePhone = etDetailMobile.Text.Trim();
                supplierDetail.SupplierDetailAddress = etDetailAddress.Text.Trim();

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
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
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

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (etSupplierName.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "Please fill supplier name", "Information");
            }
            else if (!Regex.Match(etEmail.Text.Trim(), @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$").Success)
                MessageBox.Show(this, "Invalid email", "Information");
            else if (!Regex.Match(etPhone.Text.Trim(), @"(^\d{5}$)|(^\d{5}-\d{4}$)").Success)
                MessageBox.Show(this, "Invalid phone number", "Information");
            else if (!Regex.Match(etFax.Text.Trim(), @"(^\d{5}$)|(^\d{5}-\d{4}$)").Success)
                MessageBox.Show(this, "Invalid fax number", "Information");
            else
            {
                if (supplierDetailGridView.RowCount == 0)
                {
                    DialogResult dResult = MessageBox.Show(this, "Are you sure want save this data without supplier detail ? ", "Confirmation", MessageBoxButtons.YesNo);
                    if (dResult == DialogResult.Yes)
                    {
                        SaveData();
                    }
                }
                else
                    SaveData();
            }
        }

        private void SaveData()
        {
            SqlSupplierRepository sqlSupplierRepository = null;

            if (wantToCreateVessel)
            {
                sqlSupplierRepository = new SqlSupplierRepository();
                Guid newGuid = Guid.NewGuid();

                string[] strSqlParam = getStringSqlParameter();
                object[] objSqlParam = GetObjSqlParameter(newGuid);
                SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(strSqlParam, objSqlParam);

                if (sqlSupplierRepository.CreateSupplier(sqlParam))
                {
                    MessageBox.Show(this, "Success insert supplier data", "Information");
                    radButtonElement2.PerformClick();
                }
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

                if (sqlSupplierRepository.EditSupplier(sqlParam))
                {
                    MessageBox.Show(this, "Success edit supplier data", "Information");
                    radButtonElement2.PerformClick();
                }
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
            //rowcount * 8 is number of field of customer detail
            // + 9 is number of field of customer
            string[] strSqlParameter = new string[(supplierDetailGridView.RowCount * 8) + 9];
            strSqlParameter[0] = "supplier_id";
            strSqlParameter[1] = "category_id";
            strSqlParameter[2] = "supplier_name";
            strSqlParameter[3] = "address";
            strSqlParameter[4] = "phone";
            strSqlParameter[5] = "fax";
            strSqlParameter[6] = "email";
            strSqlParameter[7] = "contact_person";
            strSqlParameter[8] = "deleted";

            int j = 9;
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
            }

            return strSqlParameter;
        }

        private object[] GetObjSqlParameter(Guid id)
        {
            //rowcount * 8 is number of field of customer detail
            // + 9 is number of field of customer
            object[] obj = new object[(supplierDetailGridView.RowCount * 8) + 9];
            obj[0] = id;
            obj[1] = SqlUtility.isDBNULL(cboCategory.SelectedValue.ToString().Trim());
            obj[2] = etSupplierName.Text.Trim();
            obj[3] = SqlUtility.isDBNULL(etAddress.Text.Trim());
            obj[4] = SqlUtility.isDBNULL(etPhone.Text.Trim());
            obj[5] = SqlUtility.isDBNULL(etFax.Text.Trim());
            obj[6] = SqlUtility.isDBNULL(etEmail.Text.Trim());
            obj[7] = SqlUtility.isDBNULL(etContactPerson.Text.Trim());
            obj[8] = 0;

            int i = 9;
            for (int j = 0; j < supplierDetailGridView.RowCount; j++)
            {
                obj[i++] = Guid.NewGuid();
                obj[i++] = id;
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[0].Value + "");
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[1].Value + "");
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[2].Value + "");
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[3].Value + "");
                obj[i++] = SqlUtility.isDBNULL(supplierDetailGridView.Rows[j].Cells[4].Value + "");
                obj[i++] = 0;
            }

            return obj;
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new SupplierList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
