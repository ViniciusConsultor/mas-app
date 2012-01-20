using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.Data.SqlClient;
using VisitaJayaPerkasa.SqlRepository;

namespace VisitaJayaPerkasa.Control.Customer
{
    public partial class CustomerEdit : UserControl
    {
        private VisitaJayaPerkasa.Entities.Customer customer;
        private bool wantToCreateVessel;
        private List<VisitaJayaPerkasa.Entities.CustomerDetail> listCustomerDetail;

        public CustomerEdit(VisitaJayaPerkasa.Entities.Customer customer)
        {
            InitializeComponent();
            this.customer = customer;

            if (customer == null)
            {
                wantToCreateVessel = true;
                listCustomerDetail = new List<Entities.CustomerDetail>();
            }
            else
            {
                wantToCreateVessel = false;
                etCustomerName.Text = customer.CustomerName;
                etOffice.Text = customer.Office;
                etAddress.Text = customer.Address;
                etEmail.Text = customer.Email;
                etPhone.Text = customer.Phone;
                etFax.Text = customer.Fax;
                etContactPerson.Text = customer.ContactPerson;
                chkStatusPPN.Checked = Convert.ToBoolean(customer.StatusPPN);

                SqlCustomerRepository sqlCustomerRepository = new SqlCustomerRepository();
                listCustomerDetail = sqlCustomerRepository.ListCustomerDetail(customer.ID);

                if (listCustomerDetail != null)
                    CustomerDetailGridView.DataSource = listCustomerDetail;
                else
                    listCustomerDetail = new List<VisitaJayaPerkasa.Entities.CustomerDetail>();

                sqlCustomerRepository = null;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetDetailData();
        }

        private void ResetDetailData() {
            etFirstName.Text = "";
            etLastName.Text = "";
            etDetailAddress.Text = "";
            etDetailMobile.Text = "";
            etDetailPhone.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (etFirstName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill first name", "Information");
            else {
                VisitaJayaPerkasa.Entities.CustomerDetail custDetail = new Entities.CustomerDetail();

                custDetail.FirstName = etFirstName.Text.Trim();
                custDetail.LastName = etLastName.Text.Trim();
                custDetail.CustomerDetailAddress = etDetailAddress.Text.Trim();
                custDetail.CustomerDetailPhone = etDetailPhone.Text.Trim();
                custDetail.CustomerDetailMobilePhone = etDetailMobile.Text.Trim();



                for (int i = 0; i < listCustomerDetail.Count; i++)
                {
                    if (listCustomerDetail.ElementAt(i).FirstName == custDetail.FirstName &&
                        listCustomerDetail.ElementAt(i).LastName == custDetail.LastName)
                    {
                        MessageBox.Show(this, "Data has already exist", "Information");
                        return;
                    }
                }


                listCustomerDetail.Add(custDetail);
                CustomerDetailGridView.DataSource = typeof(List<VisitaJayaPerkasa.Entities.CustomerDetail>);
                CustomerDetailGridView.DataSource = listCustomerDetail;

                custDetail = null;
                ResetDetailData();
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new CustomerList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            listCustomerDetail.Clear();
            CustomerDetailGridView.DataSource = null;
        }

        private void btnRemoveGrid_Click(object sender, EventArgs e)
        {
            List<VisitaJayaPerkasa.Entities.CustomerDetail> listTemp = new List<VisitaJayaPerkasa.Entities.CustomerDetail>();

            if (CustomerDetailGridView.SelectedRows.Count == 1)
            {
                int n = 0;
                for (int i = 0; i < listCustomerDetail.Count; i++) { 
                    GridViewRowInfo gridInfo = CustomerDetailGridView.SelectedRows.First();
                    string firstName = gridInfo.Cells[0].Value.ToString();
                    string lastName = (gridInfo.Cells[1].Value != null)? gridInfo.Cells[1].Value.ToString() : null;

                    if (listCustomerDetail.ElementAt(i).FirstName == firstName &&
                        listCustomerDetail.ElementAt(i).LastName == lastName)
                        n = i;
                    else
                        listTemp.Add(listCustomerDetail.ElementAt(i));
                }

                listCustomerDetail.RemoveAt(n);
                listCustomerDetail = null;
                listCustomerDetail = listTemp;

                CustomerDetailGridView.DataSource = typeof(List<VisitaJayaPerkasa.Entities.CustomerDetail>);
                CustomerDetailGridView.DataSource = listCustomerDetail;
            }
            else
                MessageBox.Show(this, "Please select row who will be removed", "Information");
        }

        private string[] getStringSqlParameter() {
            //rowcount * 8 is number of field of customer detail
            // + 9 is number of field of customer
            string[] strSqlParameter = new string[(CustomerDetailGridView.RowCount * 8) + 10];
            strSqlParameter[0] = "customer_id";
            strSqlParameter[1] = "customer_name";
            strSqlParameter[2] = "office";
            strSqlParameter[3] = "address";
            strSqlParameter[4] = "phone";
            strSqlParameter[5] = "fax";
            strSqlParameter[6] = "email";
            strSqlParameter[7] = "contact_person";
            strSqlParameter[8] = "status_ppn";
            strSqlParameter[9] = "deleted";

            int j = 10;
            for (int i = 0; i < CustomerDetailGridView.RowCount; i++) {
                strSqlParameter[j++] = "customer_detail_id";
                strSqlParameter[j++] = "customer_id";
                strSqlParameter[j++] = "first_name";
                strSqlParameter[j++] = "last_name";
                strSqlParameter[j++] = "address";
                strSqlParameter[j++] = "phone";
                strSqlParameter[j++] = "mobile_phone";
                strSqlParameter[j++] = "deleted";
            }

            return strSqlParameter;
        }

        private object[] GetObjSqlParameter(Guid id)
        {
            //rowcount * 8 is number of field of customer detail
            // + 9 is number of field of customer
            object[] obj = new object[(CustomerDetailGridView.RowCount * 8) + 10];
            obj[0] = id;
            obj[1] = etCustomerName.Text.Trim();
            obj[2] = SqlUtility.isDBNULL(etOffice.Text.Trim());
            obj[3] = SqlUtility.isDBNULL(etAddress.Text.Trim());
            obj[4] = SqlUtility.isDBNULL(etPhone.Text.Trim());
            obj[5] = SqlUtility.isDBNULL(etFax.Text.Trim());
            obj[6] = SqlUtility.isDBNULL(etEmail.Text.Trim());
            obj[7] = SqlUtility.isDBNULL(etContactPerson.Text.Trim());
            obj[8] = chkStatusPPN.Checked;
            obj[9] = 0;

            int i = 10;
            for (int j = 0; j < CustomerDetailGridView.RowCount; j++)
            {
                obj[i++] = Guid.NewGuid();
                obj[i++] = id;
                obj[i++] = SqlUtility.isDBNULL(CustomerDetailGridView.Rows[j].Cells[0].Value + "");
                obj[i++] = SqlUtility.isDBNULL(CustomerDetailGridView.Rows[j].Cells[1].Value + "");
                obj[i++] = SqlUtility.isDBNULL(CustomerDetailGridView.Rows[j].Cells[2].Value + "");
                obj[i++] = SqlUtility.isDBNULL(CustomerDetailGridView.Rows[j].Cells[3].Value + "");
                obj[i++] = SqlUtility.isDBNULL(CustomerDetailGridView.Rows[j].Cells[4].Value + "");
                obj[i++] = 0;
            }

            return obj;
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (etCustomerName.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "Please fill customer name", "Information");
            }
            else
            {
                if (CustomerDetailGridView.RowCount == 0)
                {
                    DialogResult dResult = MessageBox.Show(this, "Are you sure want save this data without customer detail ? ", "Confirmation", MessageBoxButtons.YesNo);
                    if (dResult == DialogResult.Yes)
                    {
                        SaveData();
                    }
                }
                else
                    SaveData();
            }
        }

        private void SaveData() {
            SqlCustomerRepository sqlCustomerRepository = null;

            if (wantToCreateVessel)
            {
                sqlCustomerRepository = new SqlCustomerRepository();
                Guid newGuid = Guid.NewGuid();

                string[] strSqlParam = getStringSqlParameter();
                object[] objSqlParam = GetObjSqlParameter(newGuid);
                SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(strSqlParam, objSqlParam);

                if (sqlCustomerRepository.CreateCustomer(sqlParam))
                {
                    MessageBox.Show(this, "Success insert customer data", "Information");
                    radButtonElement2.PerformClick();
                }
                else
                {
                    MessageBox.Show(this, "Cannot insert customer data", "Information");
                }
                sqlCustomerRepository = null;
                strSqlParam = null;
                objSqlParam = null;
                sqlParam = null;
            }
            else
            {
                sqlCustomerRepository = new SqlCustomerRepository();
                string[] strSqlParam = getStringSqlParameter();
                object[] objSqlParam = GetObjSqlParameter(customer.ID);
                SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(strSqlParam, objSqlParam);

                if (sqlCustomerRepository.EditCustomer(sqlParam))
                {
                    MessageBox.Show(this, "Success edit customer data", "Information");
                    radButtonElement2.PerformClick();
                }
                else
                {
                    MessageBox.Show(this, "Cannot edit customer data", "Information");
                }

                sqlCustomerRepository = null;
                strSqlParam = null;
                objSqlParam = null;
                sqlParam = null;
            }

        }

    }
}
