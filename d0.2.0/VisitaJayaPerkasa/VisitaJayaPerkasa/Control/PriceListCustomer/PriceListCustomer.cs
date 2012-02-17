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
        private List<VisitaJayaPerkasa.Entities.TypeCont> ListTypeCont;
        private SqlTypeContRepository sqlTypeContRepository;

        private List<VisitaJayaPerkasa.Entities.City> listCity;
        private SqlCityRepository sqlCityRepository;

        private List<VisitaJayaPerkasa.Entities.PriceList> listPrice;
        private VisitaJayaPerkasa.Entities.Customer searchResultCustomer;

        private SqlPriceListRepository sqlPriceListRepository;

        private List<Guid> listPriceDeleteExistsData;
        private List<int> listIndexPriceDeleteExistsData;


        public PriceListCustomer()
        {
            InitializeComponent();

            listPriceDeleteExistsData = new List<Guid>();
            listIndexPriceDeleteExistsData = new List<int>();

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

            pickerFrom.Value = DateTime.Now;
            pickerTo.Value = DateTime.Now;
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
            /*
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
            */

            SqlConditionRepository sqlConditionRepository = new SqlConditionRepository();
            List<VisitaJayaPerkasa.Entities.Condition> listTempCondition = sqlConditionRepository.GetConditions();
            ((GridViewComboBoxColumn)this.radGridView1.Columns[6]).DataSource = listTempCondition;
            ((GridViewComboBoxColumn)this.radGridView1.Columns[6]).DisplayMember = "ConditionName";
            ((GridViewComboBoxColumn)this.radGridView1.Columns[6]).ValueMember = "ID";


            listTempCondition = null;
            sqlConditionRepository = null;
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

                    id = (radGridView1.Rows[i].Cells[1].Value.ToString().Equals("")) ? Utility.Utility.DefaultDateTime().ToString() : radGridView1.Rows[i].Cells[1].Value.ToString();
                    objPriceList.DateFrom = Utility.Utility.ConvertStringToDate(id);
                    if (objPriceList.DateFrom.ToString().Equals(Utility.Utility.DefaultDateTime().ToString()))
                    {
                        MessageBox.Show(this, "Please fill date from in line " + i, "Information");
                        return;
                    }

                    id = (radGridView1.Rows[i].Cells[2].Value.ToString().Equals("")) ? Utility.Utility.DefaultDateTime().ToString() : radGridView1.Rows[i].Cells[2].Value.ToString();
                    objPriceList.DateTo = Utility.Utility.ConvertStringToDate(id);
                    if (objPriceList.DateTo.ToString().Equals(Utility.Utility.DefaultDateTime().ToString()))
                    {
                        MessageBox.Show(this, "Please fill date to in line " + i, "Information");
                        return;
                    }

                    /*
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
                    */
                    id = (radGridView1.Rows[i].Cells[6].Value.ToString().Equals("")) ? Guid.Empty.ToString() : radGridView1.Rows[i].Cells[6].Value.ToString();
                    objPriceList.ConditionID = Utility.Utility.ConvertToUUID(id);
                    if (objPriceList.ConditionID.ToString().Equals(Guid.Empty.ToString()))
                    {
                        MessageBox.Show(this, "Please fill condition in line " + i, "Information");
                        return;
                    }

                    id = (radGridView1.Rows[i].Cells[7].Value.ToString().Equals("")) ? "-1" : radGridView1.Rows[i].Cells[7].Value.ToString();
                    objPriceList.PriceCustomer = Utility.Utility.ConvertStringToDecimal(id);
                    if (objPriceList.PriceCustomer.ToString().Equals("-1"))
                    {
                        MessageBox.Show(this, "Please fill price in line " + i, "Information");
                        return;
                    }


                    if (objPriceList.DateFrom.Date > objPriceList.DateTo.Date)
                    {
                        MessageBox.Show(this, "Please correct datefrom and dateto in line " + i, "Information");
                        return;
                    }

                    tempPriceList.Add(objPriceList);
                    objPriceList = null;
                }



                for (int i = 0; i < tempPriceList.Count; i++)
                {
                    for (int j = i + 1; j < tempPriceList.Count; j++)
                    {
                        if (
                            (
                                tempPriceList.ElementAt(i).ConditionID.ToString().Equals(tempPriceList.ElementAt(j).ConditionID.ToString())
                            )
                            &&
                            (
                                (
                                    tempPriceList.ElementAt(i).DateFrom <= tempPriceList.ElementAt(j).DateFrom &&
                                    tempPriceList.ElementAt(i).DateTo >= tempPriceList.ElementAt(j).DateFrom
                                )
                                ||
                                (
                                    tempPriceList.ElementAt(i).DateFrom >= tempPriceList.ElementAt(j).DateFrom &&
                                    tempPriceList.ElementAt(i).DateFrom <= tempPriceList.ElementAt(j).DateTo
                                )
                            )
                            )
                        {
                            MessageBox.Show(this, "Please fix record - " + i + " and record -" + j + " have same record of date and condition. Please remove one", "Information");
                            return;
                        }
                    }
                }


                sqlPriceListRepository = new SqlPriceListRepository();
                listPriceDeleteExistsData.Clear();
                listIndexPriceDeleteExistsData.Clear();
                List<int> indexDeleted = new List<int>();

                for (int i = 0; i < tempPriceList.Count; i++)
                {
                    Guid exists = Guid.Empty;

                    exists = sqlPriceListRepository.GetPriceMenuCustomer(tempPriceList.ElementAt(i).DateFrom,
                                                tempPriceList.ElementAt(i).DateTo,
                                                tempPriceList.ElementAt(i).ConditionID.ToString(),
                                                cboType.SelectedValue.ToString(),
                                                cbDestination.SelectedValue.ToString(),
                                                searchResultCustomer.ID.ToString());

                    if (!exists.ToString().Equals(Guid.Empty.ToString()))
                    {
                        DialogResult dResult = MessageBox.Show(this, "Record - " + i + " has already exist. \n If you don't want to override this data, so your data not will be save. \n Do you want to override ?", "Confirmation", MessageBoxButtons.YesNo);
                        if (dResult == DialogResult.Yes)
                        {
                            listPriceDeleteExistsData.Add(exists);
                            listIndexPriceDeleteExistsData.Add(i);
                        }
                        else
                            indexDeleted.Add(i);
                    }
                }


                for (int i = indexDeleted.Count - 1; i >= 0; i--)
                    tempPriceList.RemoveAt(indexDeleted.ElementAt(i));

                for (int i = 0; i < listPriceDeleteExistsData.Count; i++)
                {
                    for (int j = i + 1; j < listPriceDeleteExistsData.Count; j++)
                    {
                        if (listPriceDeleteExistsData.ElementAt(i) == listPriceDeleteExistsData.ElementAt(j))
                        {
                            MessageBox.Show(this, "Please fix record -" + listIndexPriceDeleteExistsData.ElementAt(i) + " and record -" + listIndexPriceDeleteExistsData.ElementAt(j) + " have same record of date, type and condition. Please remove one", "Information");
                            return;
                        }
                    }
                }

                sqlPriceListRepository = null;


                SqlParameter[] sqlParamDeletedPrice = null;
                SqlParameter[] sqlParamInsert = null;
                if (listPriceDeleteExistsData.Count > 0)
                {
                    string[] key = new string[listPriceDeleteExistsData.Count];
                    object[] value = new object[listPriceDeleteExistsData.Count];

                    for (int j = 0; j < listPriceDeleteExistsData.Count; j++)
                    {
                        key[j] = "priceID";
                        value[j] = listPriceDeleteExistsData.ElementAt(j);
                    }

                    sqlParamDeletedPrice = SqlUtility.SetSqlParameter(key, value);
                }

                if (tempPriceList.Count > 0)
                {
                    //14 is field who any in below for
                    string[] key = new string[tempPriceList.Count * 14];
                    object[] value = new object[tempPriceList.Count * 14];

                    int nn = 0;
                    for (int j = 0; j < tempPriceList.Count; j++)
                    {
                        key[nn] = "price_id";
                        value[nn++] = Guid.NewGuid();

                        key[nn] = "dateFrom";
                        value[nn++] = tempPriceList.ElementAt(j).DateFrom;

                        key[nn] = "dateTo";
                        value[nn++] = tempPriceList.ElementAt(j).DateTo;

                        key[nn] = "supplier_id";
                        value[nn++] = Guid.Empty;

                        key[nn] = "destination";
                        if (cbDestination.SelectedIndex >= 0)
                            value[nn++] = listCity[cbDestination.SelectedIndex].ID;
                        else value[nn++] = Guid.Empty;

                        key[nn] = "type_cont_id";
                        if (cboType.SelectedIndex >= 0)
                            value[nn++] = ListTypeCont[cboType.SelectedIndex].ID;
                        else value[nn++] = Guid.Empty;

                        key[nn] = "condition_id";
                        value[nn++] = tempPriceList.ElementAt(j).ConditionID;

                        key[nn] = "price_supplier";
                        value[nn++] = DBNull.Value;

                        key[nn] = "customer_id";
                        if (searchResultCustomer != null)
                            value[nn++] = searchResultCustomer.ID;
                        else value[nn++] = Guid.Empty;

                        key[nn] = "price_customer";
                        value[nn++] = tempPriceList.ElementAt(j).PriceCustomer;

                        key[nn] = "stuffing_id";
                        value[nn++] = Guid.Empty;

                        key[nn] = "recipient_id";
                        value[nn++] = Guid.Empty;

                        key[nn] = "price_courier";
                        value[nn++] = DBNull.Value;

                        key[nn] = "item";
                        value[nn++] = DBNull.Value;
                    }

                    sqlParamInsert = SqlUtility.SetSqlParameter(key, value);
                    if (sqlPriceListRepository == null)
                        sqlPriceListRepository = new SqlPriceListRepository();
                    if (sqlPriceListRepository.SavePriceList(
                        (sqlParamDeletedPrice == null) ? null : sqlParamDeletedPrice,
                        sqlParamInsert))
                    {
                        MessageBox.Show(this, "Success saving !", "Information");
                        listPriceDeleteExistsData.Clear();
                        listIndexPriceDeleteExistsData.Clear();
                    }
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
            btnDisplayData.Enabled = true;
            btnSearch.Enabled = true;
            cboType.Enabled = true;
            cbDestination.Enabled = true;
            pickerFrom.Enabled = true;
            pickerTo.Enabled = true;

            radGridView1.Rows.Clear();
            radGridView1.Enabled = false;
        }

        private void cbDestination_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void btnDisplayData_Click(object sender, EventArgs e)
        {
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
            else if (pickerFrom.Value.Date > pickerTo.Value.Date) {
                MessageBox.Show(this, "Date from must greather than date to", "Information");
            }
            else
            {
                btnDisplayData.Enabled = false;
                cboType.Enabled = false;
                cbDestination.Enabled = false;

                LoadGridViewData();
                LoadData();
            }
        }

    }
}
