using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;
using VisitaJayaPerkasa.Entities;
using Telerik.WinControls.UI;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Control.PriceList
{
    public partial class PriceList : UserControl
    {
        private SqlPriceListRepository sqlPriceListRepository;
        private SqlSupplierRepository sqlSupplierRepository;
        private SqlCityRepository sqlCityRepository;
        private SqlTypeContRepository sqlTypeContRepository;
        private SqlConditionRepository sqlConditionRepository;

        private List<VisitaJayaPerkasa.Entities.PriceList> listPriceList;
        private List<VisitaJayaPerkasa.Entities.Category> listTypeOfSupplier;
        private List<VisitaJayaPerkasa.Entities.Supplier> listSupplier; 
        private List<VisitaJayaPerkasa.Entities.City> listCity;
        private List<VisitaJayaPerkasa.Entities.TypeCont> listType;
        private List<VisitaJayaPerkasa.Entities.Condition> listCondition;

        private string lastValueTypeSupplier;

        public PriceList()
        {
            InitializeComponent();
            sqlPriceListRepository = new SqlPriceListRepository();
            sqlCityRepository = new SqlCityRepository();

            listTypeOfSupplier = sqlPriceListRepository.GetTypeOfSupplier();
            cboTypeSupplier.DataSource = listTypeOfSupplier;
            cboTypeSupplier.DisplayMember = "CategoryName";
            cboTypeSupplier.ValueMember = "ID";
            cboTypeSupplier.SelectedIndex = -1;
            cboTypeSupplier.SelectedText = Constant.VisitaJayaPerkasaApplication.cboDefaultText;

            listCity = sqlCityRepository.GetCity();
            cbDestination.DataSource = listCity;
            cbDestination.DisplayMember = "CityName";
            cbDestination.ValueMember = "ID";
            cbDestination.SelectedIndex = -1;
            cbDestination.SelectedText = Constant.VisitaJayaPerkasaApplication.cboDefaultText;

            cbSupplier.Enabled = false;
            cboRecipient.Enabled = false;
            sqlPriceListRepository = null;
            PriceListGridView.Enabled = false;

            sqlPriceListRepository = null;
            sqlCityRepository = null;

            lastValueTypeSupplier = cboTypeSupplier.Text;
        }

        private void LoadCboGridView() {
            sqlSupplierRepository = new SqlSupplierRepository();
            sqlCityRepository = new SqlCityRepository();
            sqlTypeContRepository = new SqlTypeContRepository();
            sqlConditionRepository = new SqlConditionRepository();

            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[2]).DataSource = listSupplier;
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[2]).DisplayMember = "SupplierName";
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[2]).ValueMember = "Id";

            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[3]).DataSource = listCity;
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[3]).DisplayMember = "CityName";
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[3]).ValueMember = "ID";

            listType = sqlTypeContRepository.GetTypeCont();
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[4]).DataSource = listType;
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[4]).DisplayMember = "TypeName";
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[4]).ValueMember = "ID";

            listCondition = sqlConditionRepository.GetConditions();
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[5]).DataSource = listCondition;
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[5]).DisplayMember = "ConditionName";
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[5]).ValueMember = "ID";

            sqlSupplierRepository = null;
            sqlCityRepository = null;
            sqlTypeContRepository = null;
            sqlConditionRepository = null;
        }

        private void cboTypeSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cbSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cbDestination_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboRecipient_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboStuffingPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void DisableAndEnableTypeSupplier() {
            this.cboTypeSupplier.SelectedValueChanged -= new System.EventHandler(this.cboTypeSupplier_SelectedValueChanged);
            cboTypeSupplier.SelectedItem = lastValueTypeSupplier;
            this.cboTypeSupplier.SelectedValueChanged += new System.EventHandler(this.cboTypeSupplier_SelectedValueChanged);
        }

        private void cboTypeSupplier_SelectedValueChanged(object sender, EventArgs e)
        { 
            if ((!cboTypeSupplier.SelectedText.Equals("-- Choose --")) && (!cboTypeSupplier.SelectedText.Equals("")))
            {
                if (PriceListGridView.RowCount > 0) {
                    DialogResult dResult = MessageBox.Show(this, "Changing your data grid will not be save \n Do you want to save data in grid? ", "Confirmation", MessageBoxButtons.YesNo);

                    if (dResult == DialogResult.Yes)
                    {
                        List<VisitaJayaPerkasa.Entities.PriceList> tempPriceList = new List<VisitaJayaPerkasa.Entities.PriceList>();
                        List<string> listID = new List<string>();

                        for (int i = 0; i < PriceListGridView.RowCount; i++)
                        {
                            VisitaJayaPerkasa.Entities.PriceList objPriceList = new VisitaJayaPerkasa.Entities.PriceList();

                            string id = (PriceListGridView.Rows[i].Cells[0].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[0].Value.ToString();
                            objPriceList.ID = Utility.Utility.ConvertToUUID(id);

                            if (!id.Equals(Guid.Empty.ToString()))
                                listID.Add(objPriceList.ID.ToString());

                            id = (PriceListGridView.Rows[i].Cells[1].Value.ToString().Equals("")) ? Utility.Utility.DefaultDateTime().ToString() : PriceListGridView.Rows[i].Cells[1].Value.ToString();
                            objPriceList.Date = Utility.Utility.ConvertStringToDate(id);
                            if (objPriceList.Date.ToString().Equals(Utility.Utility.DefaultDateTime().ToString()))
                            {
                                MessageBox.Show(this, "Please fill date in line " + i, "Information");
                                DisableAndEnableTypeSupplier();
                                return;
                            }

                            id = (PriceListGridView.Rows[i].Cells[2].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[2].Value.ToString();
                            objPriceList.SupplierID = Utility.Utility.ConvertToUUID(id);
                            if (objPriceList.SupplierID.ToString().Equals(Guid.Empty.ToString()))
                            {
                                MessageBox.Show(this, "Please fill supplier in line " + i, "Information");
                                DisableAndEnableTypeSupplier();
                                return;
                            }

                            id = (PriceListGridView.Rows[i].Cells[3].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[3].Value.ToString();
                            objPriceList.Destination = Utility.Utility.ConvertToUUID(id);
                            if (objPriceList.Destination.ToString().Equals(Guid.Empty.ToString()))
                            {
                                MessageBox.Show(this, "Please fill destination in line " + i, "Information");
                                DisableAndEnableTypeSupplier();
                                return;
                            }

                            id = (PriceListGridView.Rows[i].Cells[4].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[4].Value.ToString();
                            objPriceList.TypeID = Utility.Utility.ConvertToUUID(id);
                            if (objPriceList.TypeID.ToString().Equals(Guid.Empty.ToString()))
                            {
                                MessageBox.Show(this, "Please fill type in line " + i, "Information");
                                DisableAndEnableTypeSupplier();
                                return;
                            }

                            id = (PriceListGridView.Rows[i].Cells[5].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[5].Value.ToString();
                            objPriceList.ConditionID = Utility.Utility.ConvertToUUID(id);
                            if (objPriceList.ConditionID.ToString().Equals(Guid.Empty))
                            {
                                MessageBox.Show(this, "Please fill condition in line " + i, "Information");
                                DisableAndEnableTypeSupplier();
                                return;
                            }

                            id = (PriceListGridView.Rows[i].Cells[6].Value.ToString().Equals("")) ? "-1" : PriceListGridView.Rows[i].Cells[6].Value.ToString();
                            objPriceList.Price = Utility.Utility.ConvertStringToDecimal(id);
                            if (objPriceList.Price.ToString().Equals("-1"))
                            {
                                MessageBox.Show(this, "Please fill price in line and check your price" + i, "Information");
                                DisableAndEnableTypeSupplier();
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
                            //7 is field who any in below for
                            string[] key = new string[tempPriceList.Count * 7];
                            object[] value = new object[tempPriceList.Count * 7];

                            int nn = 0;
                            for (int j = 0; j < tempPriceList.Count; j++)
                            {
                                key[nn] = "price_id";
                                value[nn++] = Guid.NewGuid();

                                key[nn] = "Date";
                                value[nn++] = tempPriceList.ElementAt(j).Date;

                                key[nn] = "supplier";
                                value[nn++] = tempPriceList.ElementAt(j).SupplierID;

                                key[nn] = "tujuan";
                                value[nn++] = tempPriceList.ElementAt(j).Destination;

                                key[nn] = "type";
                                value[nn++] = tempPriceList.ElementAt(j).TypeID;

                                key[nn] = "condition";
                                value[nn++] = tempPriceList.ElementAt(j).ConditionID;

                                key[nn] = "price";
                                value[nn++] = tempPriceList.ElementAt(j).Price;
                            }

                            sqlParamInsert = SqlUtility.SetSqlParameter(key, value);
                            sqlPriceListRepository = new SqlPriceListRepository();
                            if (sqlPriceListRepository.SavePriceList(
                                (sqlParamDeleted == null) ? null : sqlParamDeleted,
                                sqlParamInsert))
                                MessageBox.Show(this, "Success saving !", "Information");
                            else
                            {
                                MessageBox.Show(this, "Failed save data !", "Information");
                                DisableAndEnableTypeSupplier();
                                return;
                            }
                        }

                        PriceListGridView.Rows.Clear();
                        PriceListGridView.Enabled = false;
                    }
                    else
                    {
                        DisableAndEnableTypeSupplier();
                        return;
                    }
                }

                sqlPriceListRepository = new SqlPriceListRepository();
                listSupplier = sqlPriceListRepository.GetSupplier(Utility.Utility.ConvertToUUID(cboTypeSupplier.SelectedValue.ToString()));

                cbSupplier.Enabled = true;
                cbSupplier.DataSource = listSupplier;
                cbSupplier.DisplayMember = "SupplierName";
                cbSupplier.ValueMember = "Id";
                cbSupplier.SelectedIndex = -1;
                cbSupplier.Text = "-- Choose --";

                lastValueTypeSupplier = cboTypeSupplier.Text;
                sqlPriceListRepository = null;
            }
        }

        private void LoadData() {
            sqlPriceListRepository = new SqlPriceListRepository();
            listPriceList = sqlPriceListRepository.GetPriceListByCriteria(pickerFrom.Value.Date, pickerTo.Value.Date, cbSupplier.SelectedValue.ToString(), cbDestination.SelectedValue.ToString());

            for (int i = 0; i < listPriceList.Count(); i++)
            {
                object[] obj = {listPriceList.ElementAt(i).ID, listPriceList.ElementAt(i).Date, listPriceList.ElementAt(i).SupplierID, listPriceList.ElementAt(i).Destination, listPriceList.ElementAt(i).TypeID, listPriceList.ElementAt(i).ConditionID, listPriceList.ElementAt(i).Price};
                PriceListGridView.Rows.Add(obj);
            }

            sqlPriceListRepository = null;
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            PriceListGridView.Rows.Clear();

            if (cboTypeSupplier.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose type of supplier", "Information");
            else if (cbSupplier.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose supplier", "Information");
            else if (cbDestination.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose destination", "Information");
            else if (pickerFrom.Value.Date > pickerTo.Value.Date)
                MessageBox.Show(this, "Date from don't be greather than date to", "Information");
            else
            {
                PriceListGridView.Enabled = true;
                LoadCboGridView();
                LoadData();
            }
        }
    }
}
