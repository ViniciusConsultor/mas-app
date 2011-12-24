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

namespace VisitaJayaPerkasa.Control.Transaction
{
    public partial class CustomerTransEdit : UserControl
    {
        private bool wantToCreateVessel;
        
        private VisitaJayaPerkasa.Entities.CustomerTrans customerTrans;
        private List<VisitaJayaPerkasa.Entities.CustomerTransDetail> listCustomerTransDetail;
        
        private SqlCustomerTransRepository sqlCustomerTransRepository;
        private SqlCustomerRepository sqlCustomerRepository;
        private SqlTypeContRepository sqlTypeContRepository;
        private SqlCityRepository sqlCityRepository;
        private SqlPelayaranRepository sqlPelayaranRepository;
        private SqlConditionRepository sqlConditionRepository;

        public CustomerTransEdit(VisitaJayaPerkasa.Entities.CustomerTrans customerTrans)
        {
            InitializeComponent();
            this.customerTrans = customerTrans;
            
            sqlCustomerRepository = new SqlCustomerRepository();
            sqlTypeContRepository = new SqlTypeContRepository();
            sqlCityRepository = new SqlCityRepository();
            sqlPelayaranRepository = new SqlPelayaranRepository();
            sqlConditionRepository = new SqlConditionRepository();

            List<VisitaJayaPerkasa.Entities.Customer> listCustomer = sqlCustomerRepository.ListCustomers();
            List<VisitaJayaPerkasa.Entities.TypeCont> listType = sqlTypeContRepository.GetTypeCont();
            List<VisitaJayaPerkasa.Entities.City> listCity = sqlCityRepository.GetCity();
            List<VisitaJayaPerkasa.Entities.Pelayaran> listPelayaran = sqlPelayaranRepository.GetPelayaran();
            List<VisitaJayaPerkasa.Entities.Condition> listCondition = sqlConditionRepository.GetConditions();

            cboCustomer.DataSource = listCustomer;
            cboCustomer.DisplayMember = "CustomerName";
            cboCustomer.ValueMember = "ID";

            cboType.DataSource = listType;
            cboType.DisplayMember = "TypeName";
            cboType.ValueMember = "ID";

            cboOrigin.DataSource = listCity;
            cboOrigin.DisplayMember = "CityName";
            cboOrigin.ValueMember = "ID";

            cboDestination.DataSource = listCity;
            cboDestination.DisplayMember = "CityName";
            cboDestination.ValueMember = "ID";

            cboPelayaran.DataSource = listPelayaran;
            cboPelayaran.DisplayMember = "Name";
            cboPelayaran.ValueMember = "ID";

            cboCondition.DataSource = listCondition;
            cboCondition.DisplayMember = "ConditionName";
            cboCondition.ValueMember = "ID";

            if (customerTrans == null)
            {
                wantToCreateVessel = true;
                listCustomerTransDetail = new List<Entities.CustomerTransDetail>();
            }
            else
            {
                wantToCreateVessel = false;
                cboCustomer.SelectedValue = customerTrans.CustomerID;

                SqlCustomerTransRepository sqlCustomerTransRepository = new SqlCustomerTransRepository();
                listCustomerTransDetail = sqlCustomerTransRepository.ListCustomerTransDetail(customerTrans.CustomerTransID);

                if (listCustomerTransDetail != null)
                    CustomerTransDetailGridView.DataSource = listCustomerTransDetail;
                else
                    listCustomerTransDetail = new List<VisitaJayaPerkasa.Entities.CustomerTransDetail>();

                sqlCustomerTransRepository = null;
            }

            sqlCustomerRepository = null;
            sqlTypeContRepository = null;
            sqlCityRepository = null;
            sqlPelayaranRepository = null;
            sqlConditionRepository = null;
        }

        private void cboCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboPelayaran_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboOrigin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboDestination_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboCondition_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(cboType.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose type", "Information");
            else if(cboPelayaran.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose pelayaran", "Information");
            else if (cboOrigin.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose origin", "Information");
            else if (cboDestination.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose destination", "Information");
            else if (cboCondition.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose condition", "Information");
            else if(etPrice.Text.Trim().Equals(""))
                MessageBox.Show(this, "Please fill price", "Information");
            else if(etSeal.Text.Trim().Equals(""))
                MessageBox.Show(this, "Please fill no seal", "Information");
            else
            {
                VisitaJayaPerkasa.Entities.CustomerTransDetail objCustTransDetail = new Entities.CustomerTransDetail();
                objCustTransDetail.CustomerDetailTransID = Guid.NewGuid();

                objCustTransDetail.TypeID = Utility.Utility.ConvertToUUID(cboType.SelectedValue.ToString());
                objCustTransDetail.TypeName = cboType.Text;

                objCustTransDetail.PelayaranID = Utility.Utility.ConvertToUUID(cboPelayaran.SelectedValue.ToString());
                objCustTransDetail.PelayaranName = cboPelayaran.Text;

                objCustTransDetail.Origin = Utility.Utility.ConvertToUUID(cboOrigin.SelectedValue.ToString());
                objCustTransDetail.OriginName = cboOrigin.Text;

                objCustTransDetail.Destination = Utility.Utility.ConvertToUUID(cboDestination.SelectedValue.ToString());
                objCustTransDetail.DestinationName = cboDestination.Text;

                objCustTransDetail.ConditionID = Utility.Utility.ConvertToUUID(cboCondition.SelectedValue.ToString());
                objCustTransDetail.ConditionName = cboCondition.Text;

                objCustTransDetail.Price = Utility.Utility.ConvertStringToDecimal(etPrice.Text.Trim());
                objCustTransDetail.NoSeal = etSeal.Text.Trim();

                listCustomerTransDetail.Add(objCustTransDetail);
                CustomerTransDetailGridView.DataSource = typeof(List<VisitaJayaPerkasa.Entities.CustomerTransDetail>);
                CustomerTransDetailGridView.DataSource = listCustomerTransDetail;
                objCustTransDetail = null;
            }
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            listCustomerTransDetail.Clear();
            CustomerTransDetailGridView.DataSource = typeof(List<VisitaJayaPerkasa.Entities.CustomerTransDetail>);
            CustomerTransDetailGridView.DataSource = listCustomerTransDetail;
        }

        private void btnRemoveGrid_Click(object sender, EventArgs e)
        {
            if (CustomerTransDetailGridView.SelectedRows.Count > 0) {
                GridViewRowInfo gridInfo = CustomerTransDetailGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();

                for (int i = 0; i < listCustomerTransDetail.Count; i++)
                    if (listCustomerTransDetail.ElementAt(i).CustomerDetailTransID.ToString().Equals(id))
                    {
                        listCustomerTransDetail.RemoveAt(i);
                        break;
                    }

                CustomerTransDetailGridView.DataSource = typeof(List<VisitaJayaPerkasa.Entities.CustomerTransDetail>);
                CustomerTransDetailGridView.DataSource = listCustomerTransDetail;
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new CustomerTransList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (CustomerTransDetailGridView.Rows.Count > 0) {
                if (cboCustomer.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                {
                    MessageBox.Show(this, "Please choose customer name", "Information");
                }
                else { 

                    //10 is number of field below
                    int k = 0;
                    string[] key = new string[listCustomerTransDetail.Count * 10];
                    object[] value = new object[listCustomerTransDetail.Count * 10];

                    Guid TransID;
                    if (wantToCreateVessel)
                        TransID = Guid.NewGuid();
                    else
                        TransID = customerTrans.CustomerTransID;

                    for (int i = 0; i < listCustomerTransDetail.Count; i++) {
                        key[k] = "id";
                        value[k++] = listCustomerTransDetail.ElementAt(i).CustomerDetailTransID;

                        key[k] = "customer_trans_id";
                        value[k++] = TransID;

                        key[k] = "type_id";
                        value[k++] = listCustomerTransDetail.ElementAt(i).TypeID;

                        key[k] = "pelayaran_id";
                        value[k++] = listCustomerTransDetail.ElementAt(i).PelayaranID;

                        key[k] = "origin";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Origin;

                        key[k] = "destination";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Destination;

                        key[k] = "price";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Price;

                        key[k] = "condition_id";
                        value[k++] = listCustomerTransDetail.ElementAt(i).ConditionID;

                        key[k] = "no_seal";
                        value[k++] = listCustomerTransDetail.ElementAt(i).NoSeal;

                        key[k] = "deleted";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Deleted;
                    }

                    SqlParameter[] sqlParameterInsert = SqlUtility.SetSqlParameter(key, value);
                    sqlCustomerTransRepository = new SqlCustomerTransRepository();

                    if (wantToCreateVessel)
                    {
                        SqlParameter[] sqlParameterMaster = SqlUtility.SetSqlParameter(
                            new string[] { "id", "customer_id", "tgl_transaksi", "deleted" },
                            new object[] { TransID, cboCustomer.SelectedValue, DateTime.Today, 0 }
                        );

                        if (sqlCustomerTransRepository.CreateCustomerTrans(sqlParameterInsert, sqlParameterMaster)) {
                            MessageBox.Show(this, "Success save data", "Information");
                            radButtonElement2.PerformClick();

                            sqlParameterMaster = null;
                        }
                        else
                            MessageBox.Show(this, "Cannot save data", "Information");
                    }
                    else {
                        SqlParameter[] sqlParameterMaster = SqlUtility.SetSqlParameter(
                            new string[] { "id", "customer_id" },
                            new object[] { TransID, cboCustomer.SelectedValue }
                        );

                        if (sqlCustomerTransRepository.EditCustomerTrans(sqlParameterInsert, sqlParameterMaster)) {                    
                            MessageBox.Show(this, "Success edit data", "Information");
                            radButtonElement2.PerformClick();

                            sqlParameterMaster = null;
                        }
                        else
                            MessageBox.Show(this, "Cannot edit data", "Information");
                    }

                        sqlCustomerTransRepository = null;
                        sqlParameterInsert = null;
                }
            }
            else
                MessageBox.Show(this, "Please fill transaction detail", "Information");

        }
    }
}
