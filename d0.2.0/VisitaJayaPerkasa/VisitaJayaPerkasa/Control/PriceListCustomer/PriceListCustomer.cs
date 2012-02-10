using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.Form;
using VisitaJayaPerkasa.SqlRepository;
using Telerik.WinControls.UI;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Control.PriceListCustomer
{
    public partial class PriceListCustomer : UserControl
    {
        private VisitaJayaPerkasa.Entities.Customer searchResultCustomer;

        private List<VisitaJayaPerkasa.Entities.TypeCont> ListTypeCont;
        private SqlTypeContRepository sqlTypeContRepository;

        private List<VisitaJayaPerkasa.Entities.City> listCity;
        private SqlCityRepository sqlCityRepository;

        private List<VisitaJayaPerkasa.Entities.PriceList> listPrice;

        public PriceListCustomer()
        {
            InitializeComponent();

            sqlTypeContRepository = new SqlTypeContRepository();
            ListTypeCont = sqlTypeContRepository.GetTypeCont();

            cboType.DataSource = ListTypeCont;
            cboType.DisplayMember = "TypeName";
            cboType.ValueMember = "ID";
            cboType.SelectedIndex = -1;
            cboType.SelectedText = Constant.VisitaJayaPerkasaApplication.cboDefaultText;

            sqlTypeContRepository = null;


            sqlCityRepository = new SqlCityRepository();
            listCity = sqlCityRepository.GetCity();
            cbDestination.DataSource = listCity;
            cbDestination.DisplayMember = "CityName";
            cbDestination.ValueMember = "ID";
            cbDestination.SelectedIndex = -1;
            cbDestination.SelectedText = Constant.VisitaJayaPerkasaApplication.cboDefaultText;

            sqlCityRepository = null;
        }

        private void cbDestination_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            new SearchCustomer().ShowDialog();
            if (Constant.VisitaJayaPerkasaApplication.objGetOtherView != null)
            {
                searchResultCustomer = (VisitaJayaPerkasa.Entities.Customer)Constant.VisitaJayaPerkasaApplication.objGetOtherView;
                txtCustomer.Text = searchResultCustomer.CustomerName;
            }

            Constant.VisitaJayaPerkasaApplication.objGetOtherView = null;
        }

        private void LoadData() {
            SqlPriceListRepository sqlPriceListRepository = new SqlPriceListRepository();

            listPrice = sqlPriceListRepository.GetPriceListByCriteria(pickerFrom.Value.Date, pickerTo.Value.Date,
                "", cbDestination.SelectedValue.ToString(), searchResultCustomer.ID.ToString(),
                "", "", 0, cboType.SelectedValue.ToString());

            radGridView1.Enabled = true;
            if (listPrice != null)
            {
                for (int i = 0; i < listPrice.Count(); i++)
                {
                    object[] obj = {listPrice.ElementAt(i).ID, 
                        listPrice.ElementAt(i).DateFrom,
                        listPrice.ElementAt(i).DateTo,
                        listPrice.ElementAt(i).CustomerID,
                        listPrice.ElementAt(i).Destination,
                        listPrice.ElementAt(i).TypeID, 
                        listPrice.ElementAt(i).ConditionID,
                        listPrice.ElementAt(i).PriceCustomer
                                   };
                    radGridView1.Rows.Add(obj);
                }
            }

            sqlPriceListRepository = null;
        }

        private void LoadGridViewData() {
            List<VisitaJayaPerkasa.Entities.City> listTempCity = new List<Entities.City>();
            listTempCity.Add(listCity.ElementAt(cbDestination.SelectedIndex));

            ((GridViewComboBoxColumn)this.radGridView1.Columns[3]).DataSource = listTempCity;
            ((GridViewComboBoxColumn)this.radGridView1.Columns[3]).DisplayMember = "CityName";
            ((GridViewComboBoxColumn)this.radGridView1.Columns[3]).ValueMember = "ID";


            List<VisitaJayaPerkasa.Entities.TypeCont> listTempTypeCont = new List<Entities.TypeCont>();
            listTempTypeCont.Add(ListTypeCont.ElementAt(cboType.SelectedIndex));
            ((GridViewComboBoxColumn)this.radGridView1.Columns[4]).DataSource = listTempTypeCont;
            ((GridViewComboBoxColumn)this.radGridView1.Columns[4]).DisplayMember = "TypeName";
            ((GridViewComboBoxColumn)this.radGridView1.Columns[4]).ValueMember = "ID";

            SqlCustomerRepository sqlCustomerRepository = new SqlCustomerRepository();
            List<VisitaJayaPerkasa.Entities.Customer> listTempCustomer = sqlCustomerRepository.ListCustomers();
            ((GridViewComboBoxColumn)this.radGridView1.Columns[2]).DataSource = listTempCustomer;
            ((GridViewComboBoxColumn)this.radGridView1.Columns[2]).DisplayMember = "CustomerName";
            ((GridViewComboBoxColumn)this.radGridView1.Columns[2]).ValueMember = "ID";


            SqlConditionRepository sqlConditionRepository = new SqlConditionRepository();
            List<VisitaJayaPerkasa.Entities.Condition> listTempCondition = sqlConditionRepository.GetConditions();
            ((GridViewComboBoxColumn)this.radGridView1.Columns[5]).DataSource = listTempCondition;
            ((GridViewComboBoxColumn)this.radGridView1.Columns[5]).DisplayMember = "ConditionName";
            ((GridViewComboBoxColumn)this.radGridView1.Columns[5]).ValueMember = "ID";


            listTempCity = null;
            listTempCustomer = null;
            listTempCondition = null;
            listTempCity = null;

            sqlConditionRepository = null;
            sqlCustomerRepository = null;
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            radGridView1.Rows.Clear();

            if (txtCustomer.Text.Trim().Equals(""))
            {
                MessageBox.Show(this, "Please select customer", "Information");
            }
            else if (cbDestination.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText)) {
                MessageBox.Show(this, "Please choose destination", "Information");
            }
            else if (cboType.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText)) {
                MessageBox.Show(this, "Please choose type", "Information");
            }
            else
            {
                LoadGridViewData();
                LoadData();
            }
        }

        private void btnSaveGrid_Click(object sender, EventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {
                //var listID is used for fill id who has already inserted
                //this listID is used to delete all id and then write again (override)
                List<VisitaJayaPerkasa.Entities.PriceList> tempPriceList = new List<VisitaJayaPerkasa.Entities.PriceList>();
                List<string> listID = new List<string>();

                    for (int i = 0; i < radGridView1.RowCount; i++)
                    {
                        VisitaJayaPerkasa.Entities.PriceList objPriceList = new VisitaJayaPerkasa.Entities.PriceList();

                        string id = (radGridView1.Rows[i].Cells[0].Value.ToString().Equals("")) ? Guid.Empty.ToString() : radGridView1.Rows[i].Cells[0].Value.ToString();
                        objPriceList.ID = Utility.Utility.ConvertToUUID(id);

                        if (!id.Equals(Guid.Empty.ToString()))
                            listID.Add(objPriceList.ID.ToString());

                        id = (radGridView1.Rows[i].Cells[1].Value.ToString().Equals("")) ? Utility.Utility.DefaultDateTime().ToString() : radGridView1.Rows[i].Cells[1].Value.ToString();
                        objPriceList.DateFrom = Utility.Utility.ConvertStringToDate(id);
                        if (objPriceList.DateFrom.ToString().Equals(Utility.Utility.DefaultDateTime().ToString()))
                        {
                            MessageBox.Show(this, "Please fill date in line " + i, "Information");
                            return;
                        }

                        id = (radGridView1.Rows[i].Cells[2].Value.ToString().Equals("")) ? Guid.Empty.ToString() : radGridView1.Rows[i].Cells[2].Value.ToString();
                        objPriceList.CustomerID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.CustomerID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill customer in line " + i, "Information");
                            return;
                        }

                        id = (radGridView1.Rows[i].Cells[3].Value.ToString().Equals("")) ? Guid.Empty.ToString() : radGridView1.Rows[i].Cells[3].Value.ToString();
                        objPriceList.Destination = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.Destination.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill destination in line " + i, "Information");
                            return;
                        }

                        id = (radGridView1.Rows[i].Cells[4].Value.ToString().Equals("")) ? Guid.Empty.ToString() : radGridView1.Rows[i].Cells[4].Value.ToString();
                        objPriceList.TypeID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.TypeID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill type in line " + i, "Information");
                            return;
                        }

                        id = (radGridView1.Rows[i].Cells[5].Value.ToString().Equals("")) ? Guid.Empty.ToString() : radGridView1.Rows[i].Cells[5].Value.ToString();
                        objPriceList.ConditionID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.ConditionID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill condition in line " + i, "Information");
                            return;
                        }

                        id = (radGridView1.Rows[i].Cells[6].Value.ToString().Equals("")) ? "-1" : radGridView1.Rows[i].Cells[6].Value.ToString();
                        objPriceList.PriceCustomer = Utility.Utility.ConvertStringToDecimal(id);
                        if (objPriceList.PriceCustomer.ToString().Equals("-1"))
                        {
                            MessageBox.Show(this, "Please fill price supplier in line and check your price in line " + i, "Information");
                            return;
                        }

                        tempPriceList.Add(objPriceList);
                        objPriceList = null;
                    }


                SqlParameter[] sqlParamDeleted = null;
                SqlParameter[] sqlParamInsert = null;
                if (listID.Count > 0)
                {
                    string[] key = new string[listID.Count];
                    object[] value = new object[listID.Count];

                    for (int j = 0; j < listID.Count; j++)
                    {
                        key[j] = "priceID";
                        value[j] = listID.ElementAt(j);
                    }

                    sqlParamDeleted = SqlUtility.SetSqlParameter(key, value);
                }

                if (tempPriceList.Count > 0)
                {
                    //11 is field who any in below for
                    string[] key = new string[tempPriceList.Count * 11];
                    object[] value = new object[tempPriceList.Count * 11];

                    int nn = 0;
                    for (int j = 0; j < tempPriceList.Count; j++)
                    {
                        key[nn] = "price_id";
                        value[nn++] = Guid.NewGuid();

                        key[nn] = "dateFrom";
                        value[nn++] = tempPriceList.ElementAt(j).DateFrom;

                        key[nn] = "supplier_id";
                        value[nn++] = tempPriceList.ElementAt(j).SupplierID;

                        key[nn] = "destination";
                        value[nn++] = tempPriceList.ElementAt(j).Destination;

                        key[nn] = "type_cont_id";
                        value[nn++] = tempPriceList.ElementAt(j).TypeID;

                        key[nn] = "condition_id";
                        value[nn++] = tempPriceList.ElementAt(j).ConditionID;

                        key[nn] = "price_supplier";
                        value[nn++] = tempPriceList.ElementAt(j).PriceSupplier;

                        key[nn] = "customer_id";
                        value[nn++] = tempPriceList.ElementAt(j).CustomerID;

                        key[nn] = "price_customer";
                        value[nn++] = tempPriceList.ElementAt(j).PriceCustomer;

                        key[nn] = "stuffing_id";
                        value[nn++] = tempPriceList.ElementAt(j).StuffingID;

                        key[nn] = "recipient_id";
                        value[nn++] = tempPriceList.ElementAt(j).Recipient;
                    }

                    sqlParamInsert = SqlUtility.SetSqlParameter(key, value);
                    SqlPriceListRepository sqlPriceListRepository = new SqlPriceListRepository();
                    if (sqlPriceListRepository.SavePriceList(
                        (sqlParamDeleted == null) ? null : sqlParamDeleted,
                        sqlParamInsert))
                        MessageBox.Show(this, "Success saving !", "Information");
                    else
                    {
                        MessageBox.Show(this, "Failed save data !", "Information");
                        return;
                    }
                }

                radGridView1.Rows.Clear();
                radGridView1.Enabled = false;
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = true;
            cboType.Enabled = true;
            cbDestination.Enabled = true;
            pickerFrom.Enabled = true;
            pickerTo.Enabled = true;

            radGridView1.Enabled = false;
        }

    }
}
