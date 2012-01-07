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
            List<VisitaJayaPerkasa.Entities.City> listOrigin = sqlCityRepository.GetCity();
            List<VisitaJayaPerkasa.Entities.City> listDestination = sqlCityRepository.GetCity();
            List<VisitaJayaPerkasa.Entities.PelayaranDetail> listPelayaran = sqlPelayaranRepository.GetVessels();
            List<VisitaJayaPerkasa.Entities.Condition> listCondition = sqlConditionRepository.GetConditions();

            cboCustomer.DataSource = listCustomer;
            cboCustomer.DisplayMember = "CustomerName";
            cboCustomer.ValueMember = "ID";

            cboType.DataSource = listType;
            cboType.DisplayMember = "TypeName";
            cboType.ValueMember = "ID";

            cboOrigin.DataSource = listOrigin;
            cboOrigin.DisplayMember = "CityName";
            cboOrigin.ValueMember = "ID";

            cboDestination.DataSource = listDestination;
            cboDestination.DisplayMember = "CityName";
            cboDestination.ValueMember = "ID";

            cboPelayaranDetail.DataSource = listPelayaran;
            cboPelayaranDetail.DisplayMember = "VesselName";
            cboPelayaranDetail.ValueMember = "PelayaranDetailID";

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
            else if(cboPelayaranDetail.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose pelayaran", "Information");
            else if (cboOrigin.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose origin", "Information");
            else if (cboDestination.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose destination", "Information");
            else if (cboCondition.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose condition", "Information");
            else if(etTruckNo.Text.Trim().Equals(""))
                MessageBox.Show(this, "Please fill truck number", "Information");
            else if (etVoy.Text.Trim().Equals(""))
                MessageBox.Show(this, "Please fill Voyage", "Information");
            else if (etSeal.Text.Trim().Equals(""))
                MessageBox.Show(this, "Please fill no seal", "Information");
            else
            {
                VisitaJayaPerkasa.Entities.CustomerTransDetail objCustTransDetail = new Entities.CustomerTransDetail();
                objCustTransDetail.CustomerDetailTransID = Guid.NewGuid();

                objCustTransDetail.TypeID = Utility.Utility.ConvertToUUID(cboType.SelectedValue.ToString());
                objCustTransDetail.TypeName = cboType.Text;

                objCustTransDetail.PelayaranDetailID = Utility.Utility.ConvertToUUID(cboPelayaranDetail.SelectedValue.ToString());
                objCustTransDetail.VesselName = cboPelayaranDetail.Text;

                objCustTransDetail.Origin = Utility.Utility.ConvertToUUID(cboOrigin.SelectedValue.ToString());
                objCustTransDetail.OriginName = cboOrigin.Text;

                objCustTransDetail.Destination = Utility.Utility.ConvertToUUID(cboDestination.SelectedValue.ToString());
                objCustTransDetail.DestinationName = cboDestination.Text;

                objCustTransDetail.ConditionID = Utility.Utility.ConvertToUUID(cboCondition.SelectedValue.ToString());
                objCustTransDetail.ConditionName = cboCondition.Text;

                objCustTransDetail.NoSeal = etSeal.Text.Trim();
                objCustTransDetail.TruckNo = etTruckNo.Text.Trim();
                objCustTransDetail.Voyage = etVoy.Text.Trim();

                objCustTransDetail.StuffingDate = dtpStuffingDate.Value;
                objCustTransDetail.StuffingPlace = etStuffingPlace.Text.Trim();

                objCustTransDetail.ETD = dtpETD.Value;
                objCustTransDetail.TD = dtpTD.Value;
                objCustTransDetail.ETA = dtpETA.Value;
                objCustTransDetail.TA = dtpTA.Value;
                objCustTransDetail.Unloading = dtpUnloading.Value;

                listCustomerTransDetail.Add(objCustTransDetail);
                CustomerTransDetailGridView.DataSource = typeof(List<VisitaJayaPerkasa.Entities.CustomerTransDetail>);
                CustomerTransDetailGridView.DataSource = listCustomerTransDetail;
                objCustTransDetail = null;

                etSeal.Text = "";
                etTruckNo.Text = "";
                etVoy.Text = "";
                etStuffingPlace.Text = "";
                cboCondition.SelectedIndex = 0;
                cboOrigin.SelectedIndex = 0;
                cboDestination.SelectedIndex = 0;
                cboPelayaranDetail.SelectedIndex = 0;
                cboType.SelectedIndex = 0;

                dtpStuffingDate.Value = DateTime.Now;
                dtpETD.Value = DateTime.Now;
                dtpTD.Value = DateTime.Now;
                dtpETA.Value = DateTime.Now;
                dtpTA.Value = DateTime.Now;
                dtpUnloading.Value = DateTime.Now;
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
                    string[] key = new string[listCustomerTransDetail.Count * 18];
                    object[] value = new object[listCustomerTransDetail.Count * 18];

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

                        key[k] = "pelayaran_detail_id";
                        value[k++] = listCustomerTransDetail.ElementAt(i).PelayaranDetailID;

                        key[k] = "origin";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Origin;

                        key[k] = "destination";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Destination;

                        key[k] = "condition_id";
                        value[k++] = listCustomerTransDetail.ElementAt(i).ConditionID;

                        key[k] = "no_seal";
                        value[k++] = listCustomerTransDetail.ElementAt(i).NoSeal;

                        key[k] = "stuffing_date";
                        value[k++] = listCustomerTransDetail.ElementAt(i).StuffingDate;

                        key[k] = "stuffing_place";
                        value[k++] = listCustomerTransDetail.ElementAt(i).StuffingPlace;

                        key[k] = "truck_number";
                        value[k++] = listCustomerTransDetail.ElementAt(i).TruckNo;

                        key[k] = "voy";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Voyage;

                        key[k] = "etd";
                        value[k++] = listCustomerTransDetail.ElementAt(i).ETD;

                        key[k] = "td";
                        value[k++] = listCustomerTransDetail.ElementAt(i).TD;

                        key[k] = "eta";
                        value[k++] = listCustomerTransDetail.ElementAt(i).ETA;

                        key[k] = "ta";
                        value[k++] = listCustomerTransDetail.ElementAt(i).TA;

                        key[k] = "unloading";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Unloading;

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

        private void btnShowHideEditor_Click(object sender, EventArgs e)
        {
            splitPanel1.Collapsed = !splitPanel1.Collapsed;
            if (splitPanel1.Collapsed)
                btnShowHideEditor.Text = "Show Editor";
            else
                btnShowHideEditor.Text = "Collapse Editor";

        }
    }
}
