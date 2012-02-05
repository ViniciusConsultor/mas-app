﻿using System;
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
using VisitaJayaPerkasa.Form;
using Telerik.WinControls.Enumerations;

namespace VisitaJayaPerkasa.Control.PriceList
{
    public partial class PriceList : UserControl
    {
        private SqlPriceListRepository sqlPriceListRepository;
        private SqlSupplierRepository sqlSupplierRepository;
        private SqlCityRepository sqlCityRepository;
        private SqlTypeContRepository sqlTypeContRepository;
        private SqlConditionRepository sqlConditionRepository;
        private SqlCustomerRepository sqlCustomerRepository;
        private SqlRecipientRepository sqlRecipientRepository;
        private SqlWareHouseRepository sqlWareHouseRepository;

        private List<VisitaJayaPerkasa.Entities.PriceList> listPriceList;
        private List<VisitaJayaPerkasa.Entities.Category> listTypeOfSupplier;
        private List<VisitaJayaPerkasa.Entities.Supplier> listSupplier; 
        private List<VisitaJayaPerkasa.Entities.City> listCity;
        private List<VisitaJayaPerkasa.Entities.TypeCont> listType;
        private List<VisitaJayaPerkasa.Entities.Condition> listCondition;
        private List<VisitaJayaPerkasa.Entities.Customer> listCustomer;
        private List<VisitaJayaPerkasa.Entities.Recipient> listRecipient;
        private List<WareHouse> listWarehouse;
        private List<Guid> listPriceDeleteExistsData;

        //this variable used for result from search on customer text
        private VisitaJayaPerkasa.Entities.Customer searchResultCustomer;

        public PriceList()
        {
            InitializeComponent();
            sqlPriceListRepository = new SqlPriceListRepository();
            sqlCityRepository = new SqlCityRepository();
            sqlCustomerRepository = new SqlCustomerRepository();
            sqlRecipientRepository = new SqlRecipientRepository();
            sqlWareHouseRepository = new SqlWareHouseRepository();

            listPriceDeleteExistsData = new List<Guid>();

            listTypeOfSupplier = sqlPriceListRepository.GetTypeOfSupplier();
            cboTypeSupplier.SelectedValueChanged -= new EventHandler(cboTypeSupplier_SelectedValueChanged);
            cboTypeSupplier.DataSource = listTypeOfSupplier;
            cboTypeSupplier.DisplayMember = "CategoryName";
            cboTypeSupplier.ValueMember = "ID";
            cboTypeSupplier.SelectedIndex = -1;
            cboTypeSupplier.SelectedText = Constant.VisitaJayaPerkasaApplication.cboDefaultText;
            cboTypeSupplier.SelectedValueChanged += new EventHandler(cboTypeSupplier_SelectedValueChanged);

            listCity = sqlCityRepository.GetCity();
            cbDestination.DataSource = listCity;
            cbDestination.DisplayMember = "CityName";
            cbDestination.ValueMember = "ID";
            cbDestination.SelectedIndex = -1;
            cbDestination.SelectedText = Constant.VisitaJayaPerkasaApplication.cboDefaultText;



            listRecipient = sqlRecipientRepository.GetRecipient();
            cboRecipient.DataSource = listRecipient;
            cboRecipient.DisplayMember = "Name";
            cboRecipient.ValueMember = "ID";
            cboRecipient.SelectedIndex = -1;
            cboRecipient.SelectedText = Constant.VisitaJayaPerkasaApplication.cboDefaultText;

            listWarehouse = sqlWareHouseRepository.GetWareHouse();
            cboStuffingPlace.DataSource = listWarehouse;
            cboStuffingPlace.DisplayMember = "Address";
            cboStuffingPlace.ValueMember = "Id";
            cboStuffingPlace.SelectedIndex = -1;
            cboStuffingPlace.SelectedText = Constant.VisitaJayaPerkasaApplication.cboDefaultText;

            cbSupplier.Enabled = false;
            cboRecipient.Enabled = false;
            cbDislayAll.Visible = false;
            cbDislayAll.Enabled = false;
            sqlPriceListRepository = null;
            PriceListGridView.Enabled = false;
            sqlCityRepository = null;

            pickerFrom.Value = DateTime.Today;
            pickerTo.Value = DateTime.Today;
        }

        private void LoadCboGridView() {
            sqlSupplierRepository = new SqlSupplierRepository();
            sqlCityRepository = new SqlCityRepository();
            sqlTypeContRepository = new SqlTypeContRepository();
            sqlConditionRepository = new SqlConditionRepository();

                List<VisitaJayaPerkasa.Entities.Supplier> listTempSupplier = new List<Entities.Supplier>();
                listTempSupplier.Add(listSupplier.ElementAt(cbSupplier.SelectedIndex));


                ((GridViewComboBoxColumn)this.PriceListGridView.Columns[2]).DataSource = listTempSupplier;
                ((GridViewComboBoxColumn)this.PriceListGridView.Columns[2]).DisplayMember = "SupplierName";
                ((GridViewComboBoxColumn)this.PriceListGridView.Columns[2]).ValueMember = "Id";


                if (cboTypeSupplier.Text.ToLower().Equals("shipping lines") ||
                   cboTypeSupplier.Text.ToLower().Equals("dooring agent")
                    )
                {
                    List<VisitaJayaPerkasa.Entities.City> listTempCity = new List<Entities.City>();
                    listTempCity.Add(listCity.ElementAt(cbDestination.SelectedIndex));

                    ((GridViewComboBoxColumn)this.PriceListGridView.Columns[3]).DataSource = listTempCity;
                    ((GridViewComboBoxColumn)this.PriceListGridView.Columns[3]).DisplayMember = "CityName";
                    ((GridViewComboBoxColumn)this.PriceListGridView.Columns[3]).ValueMember = "ID";

                    listTempCity = null;
                }

                listType = sqlTypeContRepository.GetTypeCont();
                ((GridViewComboBoxColumn)this.PriceListGridView.Columns[4]).DataSource = listType;
                ((GridViewComboBoxColumn)this.PriceListGridView.Columns[4]).DisplayMember = "TypeName";
                ((GridViewComboBoxColumn)this.PriceListGridView.Columns[4]).ValueMember = "ID";


                if (cboTypeSupplier.Text.ToLower().Equals("shipping lines"))
                {
                    listCondition = sqlConditionRepository.GetConditions();
                    ((GridViewComboBoxColumn)this.PriceListGridView.Columns[5]).DataSource = listCondition;
                    ((GridViewComboBoxColumn)this.PriceListGridView.Columns[5]).DisplayMember = "ConditionName";
                    ((GridViewComboBoxColumn)this.PriceListGridView.Columns[5]).ValueMember = "ID";
                }

                if (cboTypeSupplier.Text.ToLower().Equals("shipping lines") ||
                    cboTypeSupplier.Text.ToLower().Equals("trucking")
                    ) {
                    listCustomer = sqlCustomerRepository.ListCustomers();
                    ((GridViewComboBoxColumn)this.PriceListGridView.Columns[9]).DataSource = listCustomer;
                    ((GridViewComboBoxColumn)this.PriceListGridView.Columns[9]).DisplayMember = "CustomerName";
                    ((GridViewComboBoxColumn)this.PriceListGridView.Columns[9]).ValueMember = "ID";
                }

                listTempSupplier = null;


            if (cboTypeSupplier.Text.ToLower().Equals("dooring agent")) {
                List<VisitaJayaPerkasa.Entities.Recipient> tempListRecipient = sqlRecipientRepository.GetRecipient();
                ((GridViewComboBoxColumn)this.PriceListGridView.Columns[7]).DataSource = tempListRecipient;
                ((GridViewComboBoxColumn)this.PriceListGridView.Columns[7]).DisplayMember = "Name";
                ((GridViewComboBoxColumn)this.PriceListGridView.Columns[7]).ValueMember = "ID";

                tempListRecipient = null;
            }

            if (cboTypeSupplier.Text.ToLower().Equals("trucking"))
            {
                List<VisitaJayaPerkasa.Entities.WareHouse> tempListWarehouse = new List<Entities.WareHouse>();
                tempListWarehouse.Add(listWarehouse.ElementAt(cboStuffingPlace.SelectedIndex)); 
                
                ((GridViewComboBoxColumn)this.PriceListGridView.Columns[6]).DataSource = tempListWarehouse;
                ((GridViewComboBoxColumn)this.PriceListGridView.Columns[6]).DisplayMember = "Address";
                ((GridViewComboBoxColumn)this.PriceListGridView.Columns[6]).ValueMember = "Id";

                tempListWarehouse = null;
            }

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


        private void RefreshTypeSupplier() {
            PriceListGridView.Rows.Clear();
            PriceListGridView.DataSource = null;
            PriceListGridView.Enabled = false;

            sqlPriceListRepository = new SqlPriceListRepository();
            listSupplier = sqlPriceListRepository.GetSupplier(Utility.Utility.ConvertToUUID(cboTypeSupplier.SelectedValue.ToString()));

            cbSupplier.Enabled = true;
            cbSupplier.DataSource = listSupplier;
            cbSupplier.DisplayMember = "SupplierName";
            cbSupplier.ValueMember = "Id";
            cbSupplier.SelectedIndex = -1;
            cbSupplier.Text = "-- Choose --";

            sqlPriceListRepository = null;
        }

        private void SetTypeOfSupplierDooringAgent() {
            lblRecipient.Visible = true;
            cboRecipient.Visible = true;
            cboRecipient.Enabled = true;
            cbRecipientDisplay.Visible = true;
            cbRecipientDisplay.Enabled = true;


            lblStuffing.Visible = false;
            cboStuffingPlace.Visible = false;

            lblCustomer.Visible = false;
            txtCustomer.Visible = false;
            btnSearch.Visible = false;

            cbDislayAll.Visible = false;
            cbDislayAll.Enabled = false;

            lblDestination.Visible = true;
            cbDestination.Visible = true;
            cbDestination.Enabled = true;
        }

        private void cboTypeSupplier_SelectedValueChanged(object sender, EventArgs e)
        { 
            if ((!cboTypeSupplier.Text.Equals("-- Choose --")) && (!cboTypeSupplier.Text.Equals("")))
            {
                RefreshTypeSupplier();

                if (cboTypeSupplier.Text.ToLower().Equals("shipping lines")) {
                    lblRecipient.Visible = false;
                    cboRecipient.Visible = false;

                    cbRecipientDisplay.Visible = false;
                    cbRecipientDisplay.Enabled = false;

                    lblStuffing.Visible = false;
                    cboStuffingPlace.Visible = false;

                    lblCustomer.Visible = true;
                    txtCustomer.Visible = true;
                    btnSearch.Visible = true;
                    btnSearch.Enabled = true;

                    lblDestination.Visible = true;
                    cbDestination.Visible = true;
                    cbDestination.Enabled = true;

                    cbDislayAll.Visible = true;
                    cbDislayAll.Enabled = true;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("dooring agent")) {
                    SetTypeOfSupplierDooringAgent();
                }
                else if(cboTypeSupplier.Text.ToLower().Equals("trucking")){
                    lblRecipient.Visible = false;
                    cboRecipient.Visible = false;

                    cbRecipientDisplay.Visible = false;
                    cbRecipientDisplay.Enabled = false;

                    lblStuffing.Visible = true;
                    cboStuffingPlace.Visible = true;
                    cboStuffingPlace.Enabled = true;

                    lblCustomer.Visible = true;
                    txtCustomer.Visible = true;
                    btnSearch.Visible = true;
                    btnSearch.Enabled = true;

                    lblDestination.Visible = false;
                    cbDestination.Visible = false;

                    cbDislayAll.Visible = true;
                    cbDislayAll.Enabled = true;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("general")) {
                    lblRecipient.Visible = false;
                    cboRecipient.Visible = false;
                    cboRecipient.Enabled = false;

                    cbRecipientDisplay.Visible = false;
                    cbRecipientDisplay.Enabled = false;

                    lblStuffing.Visible = false;
                    cboStuffingPlace.Visible = false;
                    cboStuffingPlace.Enabled = false;

                    lblCustomer.Visible = false;
                    txtCustomer.Visible = false;
                    btnSearch.Visible = false;
                    btnSearch.Enabled = false;

                    lblDestination.Visible = false;
                    cbDestination.Visible = false;
                    cbDestination.Enabled = false;
                    cbDislayAll.Visible = false;
                    cbDislayAll.Enabled = false;
                }
            }
        }

        private void LoadData() {
            sqlPriceListRepository = new SqlPriceListRepository();
            PriceListGridView.ReadOnly = false;

            if (cboTypeSupplier.Text.ToLower().Equals("shipping lines"))
            {
                listPriceList = sqlPriceListRepository.GetPriceListByCriteria(pickerFrom.Value.Date, pickerTo.Value.Date,
                    cbSupplier.SelectedValue.ToString(), cbDestination.SelectedValue.ToString(), (! cbDislayAll.Checked) ? searchResultCustomer.ID.ToString() : "", "", "", 1, "");

                if(cbDislayAll.Checked)
                    PriceListGridView.ReadOnly = true;
            }
            else if (cboTypeSupplier.Text.ToLower().Equals("dooring agent")) {
                listPriceList = sqlPriceListRepository.GetPriceListByCriteria(pickerFrom.Value.Date, pickerTo.Value.Date,
                    cbSupplier.SelectedValue.ToString(), cbDestination.SelectedValue.ToString(), "", (! cbRecipientDisplay.Checked) ? cboRecipient.SelectedValue.ToString() : "", "", 1, "");

                if (cbRecipientDisplay.Checked)
                    PriceListGridView.ReadOnly = true;
            }
            else if (cboTypeSupplier.Text.ToLower().Equals("trucking")) {
                listPriceList = sqlPriceListRepository.GetPriceListByCriteria(pickerFrom.Value.Date, pickerTo.Value.Date,
                    cbSupplier.SelectedValue.ToString(), "", (!cbDislayAll.Checked) ? searchResultCustomer.ID.ToString() : "", "", cboStuffingPlace.SelectedValue.ToString(), 1, "");

                if (cbDislayAll.Checked)
                    PriceListGridView.ReadOnly = true;
            }
            else if (cboTypeSupplier.Text.ToLower().Equals("general")) {
                listPriceList = sqlPriceListRepository.GetPriceListByCriteria(pickerFrom.Value.Date, pickerTo.Value.Date,
                    cbSupplier.SelectedValue.ToString(), "", "", 
                    "", "", 1, "");
            }



            PriceListGridView.Enabled = true;
            if (listPriceList != null)
            {
                for (int i = 0; i < listPriceList.Count(); i++)
                {
                    object[] obj = {listPriceList.ElementAt(i).ID, 
                        listPriceList.ElementAt(i).Date, 
                        listPriceList.ElementAt(i).SupplierID, 
                        listPriceList.ElementAt(i).Destination,
                        listPriceList.ElementAt(i).TypeID, 
                        listPriceList.ElementAt(i).ConditionID,
                        listPriceList.ElementAt(i).StuffingID,
                        listPriceList.ElementAt(i).Recipient,
                        listPriceList.ElementAt(i).PriceSupplier,
                        listPriceList.ElementAt(i).CustomerID,
                        listPriceList.ElementAt(i).PriceCustomer,
                        listPriceList.ElementAt(i).PriceCourier
                    };
                    PriceListGridView.Rows.Add(obj);
                }
            }

            sqlPriceListRepository = null;
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            PriceListGridView.Rows.Clear();
            radButton2.Enabled = false;

            if (cboTypeSupplier.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose type of supplier", "Information");
            else if (cbSupplier.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose supplier", "Information");
            else if ((cboTypeSupplier.Text.ToLower().Equals("shipping lines") || cboTypeSupplier.Text.ToLower().Equals("dooring agent")) 
                && cbDestination.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose destination", "Information");
            else if (pickerFrom.Value.Date > pickerTo.Value.Date)
                MessageBox.Show(this, "Date from don't be greather than date to", "Information");
            else
            {
                /*Arrange of field grid                  
0 id
1 date
2 supplier
3 tujuan
4 type
5 condition
6 stufing place
7 recipient
8 price supplier
9 customer
10 price customer
11 price courier
*/

                if (cboTypeSupplier.Text.ToLower().Equals("shipping lines")) {
                    if (txtCustomer.Text.Equals("") && (! cbDislayAll.Checked)) {
                        MessageBox.Show(this, "Please choose customer", "Information");
                        return;
                    }

                    if(cbDislayAll.Checked)
                        PriceListGridView.Columns[9].IsVisible = true;
                    else
                        PriceListGridView.Columns[9].IsVisible = false;

                    cbDestination.Enabled = false;
                    btnSearch.Enabled = false;
                    cbDislayAll.Enabled = false;


                    PriceListGridView.Columns[1].IsVisible = true;
                    PriceListGridView.Columns[2].IsVisible = false;
                    PriceListGridView.Columns[3].IsVisible = false;
                    PriceListGridView.Columns[4].IsVisible = true;
                    PriceListGridView.Columns[5].IsVisible = true;
                    PriceListGridView.Columns[6].IsVisible = false;
                    PriceListGridView.Columns[7].IsVisible = false;
                    PriceListGridView.Columns[8].IsVisible = true;
                    PriceListGridView.Columns[10].IsVisible = false;
                    PriceListGridView.Columns[11].IsVisible = false;
                    PriceListGridView.Enabled = true;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("dooring agent")) {
                    if (cboRecipient.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText) && (!cbRecipientDisplay.Checked))
                    {
                        MessageBox.Show(this, "Please choose recipient", "Information");
                        return;
                    }

                    if (cbRecipientDisplay.Checked)
                        PriceListGridView.Columns[7].IsVisible = true;
                    else
                        PriceListGridView.Columns[7].IsVisible = false;

                    cbDestination.Enabled = false;
                    cboRecipient.Enabled = false;
                    cbRecipientDisplay.Enabled = false;

                    PriceListGridView.Columns[1].IsVisible = true;
                    PriceListGridView.Columns[2].IsVisible = false;
                    PriceListGridView.Columns[3].IsVisible = false;
                    PriceListGridView.Columns[4].IsVisible = true;
                    PriceListGridView.Columns[5].IsVisible = false;
                    PriceListGridView.Columns[6].IsVisible = false;
                    PriceListGridView.Columns[8].IsVisible = true;
                    PriceListGridView.Columns[9].IsVisible = false;
                    PriceListGridView.Columns[10].IsVisible = false;
                    PriceListGridView.Columns[11].IsVisible = false;
                    PriceListGridView.Enabled = true;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("trucking")) {
                    if (txtCustomer.Text.Equals("") && (! cbDislayAll.Checked))
                    {
                        MessageBox.Show(this, "Please choose customer", "Information");
                        return;
                    }
                    else if(cboStuffingPlace.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                    {
                        MessageBox.Show(this, "Please choose stuffing place", "Information");
                        return;
                    }

                    if (cbDislayAll.Checked)
                        PriceListGridView.Columns[9].IsVisible = true;
                    else
                        PriceListGridView.Columns[9].IsVisible = false;

                    cboStuffingPlace.Enabled = false;
                    cbDislayAll.Enabled = false;

                    PriceListGridView.Columns[1].IsVisible = true;
                    PriceListGridView.Columns[2].IsVisible = false;
                    PriceListGridView.Columns[3].IsVisible = false;
                    PriceListGridView.Columns[4].IsVisible = true;
                    PriceListGridView.Columns[5].IsVisible = false;
                    PriceListGridView.Columns[6].IsVisible = false;
                    PriceListGridView.Columns[7].IsVisible = false;
                    PriceListGridView.Columns[8].IsVisible = true;
                    PriceListGridView.Columns[10].IsVisible = false;
                    PriceListGridView.Columns[11].IsVisible = true;
                    PriceListGridView.Enabled = true;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("general")) {
                    if (cbSupplier.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                    {
                        MessageBox.Show(this, "Please choose supplier", "Information");
                        return;
                    }

                    PriceListGridView.Columns[1].IsVisible = true;
                    PriceListGridView.Columns[2].IsVisible = false;
                    PriceListGridView.Columns[3].IsVisible = false;
                    PriceListGridView.Columns[4].IsVisible = false;
                    PriceListGridView.Columns[5].IsVisible = false;
                    PriceListGridView.Columns[6].IsVisible = false;
                    PriceListGridView.Columns[7].IsVisible = false;
                    PriceListGridView.Columns[8].IsVisible = true;
                    PriceListGridView.Columns[9].IsVisible = false;
                    PriceListGridView.Columns[10].IsVisible = false;
                    PriceListGridView.Columns[11].IsVisible = false;
                    PriceListGridView.Enabled = true;
                }

                cboTypeSupplier.Enabled = false;
                cbSupplier.Enabled = false;
                pickerFrom.Enabled = false;
                pickerTo.Enabled = false;

                LoadCboGridView();
                LoadData();
            }
        }

        private void btnSaveGrid_Click(object sender, EventArgs e)
        {
            if (PriceListGridView.RowCount > 0)
            {
                List<VisitaJayaPerkasa.Entities.PriceList> tempPriceList = new List<VisitaJayaPerkasa.Entities.PriceList>();

                if (cboTypeSupplier.Text.ToLower().Equals("shipping lines"))
                {
                    for (int i = 0; i < PriceListGridView.RowCount; i++)
                    {
                        VisitaJayaPerkasa.Entities.PriceList objPriceList = new VisitaJayaPerkasa.Entities.PriceList();

                        string id = (PriceListGridView.Rows[i].Cells[0].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[0].Value.ToString();
                        objPriceList.ID = Utility.Utility.ConvertToUUID(id);

                        id = (PriceListGridView.Rows[i].Cells[1].Value.ToString().Equals("")) ? Utility.Utility.DefaultDateTime().ToString() : Utility.Utility.ChangeDateMMDD(PriceListGridView.Rows[i].Cells[1].Value.ToString());
                        objPriceList.Date = Utility.Utility.ConvertStringToDate(id);
                        if (objPriceList.Date.ToString().Equals(Utility.Utility.DefaultDateTime().ToString()))
                        {
                            MessageBox.Show(this, "Please fill date in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[4].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[4].Value.ToString();
                        objPriceList.TypeID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.TypeID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill type in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[5].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[5].Value.ToString();
                        objPriceList.ConditionID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.ConditionID.ToString().Equals(Guid.Empty))
                        {
                            MessageBox.Show(this, "Please fill condition in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[8].Value.ToString().Equals("")) ? "-1" : PriceListGridView.Rows[i].Cells[8].Value.ToString();
                        objPriceList.PriceSupplier = Utility.Utility.ConvertStringToDecimal(id);
                        if (objPriceList.PriceSupplier.ToString().Equals("-1"))
                        {
                            MessageBox.Show(this, "Please fill price supplier in line and check your price in line " + i, "Information");
                            return;
                        }

                        tempPriceList.Add(objPriceList);
                        objPriceList = null;
                    }



                    for (int i = 0; i < tempPriceList.Count; i++) {
                        for (int j = i + 1; j < tempPriceList.Count; j++) {
                            if (
                                PriceListGridView.Rows[i].Cells[1].Value.ToString().Equals(PriceListGridView.Rows[j].Cells[1].Value.ToString())
                                && PriceListGridView.Rows[i].Cells[4].Value.ToString().Equals(PriceListGridView.Rows[j].Cells[4].Value.ToString())
                                && PriceListGridView.Rows[i].Cells[5].Value.ToString().Equals(PriceListGridView.Rows[j].Cells[5].Value.ToString())
                                ) {
                                    MessageBox.Show(this, "Record -" + i + " and record -" + j + " have same record of date, type and condition. Please remove one", "Information");
                                    return;
                            }
                        }
                    }


                    sqlPriceListRepository = new SqlPriceListRepository();
                    listPriceDeleteExistsData.Clear();
                    List<int> indexDeleted = new List<int>();

                    for (int i = 0; i < tempPriceList.Count; i++) {
                        Guid exists = Guid.Empty;
                        exists = sqlPriceListRepository.GetPriceCustomerByShippingLines(tempPriceList.ElementAt(i).Date, 
                                                    tempPriceList.ElementAt(i).TypeID.ToString(),
                                                    tempPriceList.ElementAt(i).ConditionID.ToString());

                        if (! exists.ToString().Equals(Guid.Empty.ToString())) { 
                            DialogResult dResult = MessageBox.Show(this, "Record - " + i + " has already exist. \n If you don't want to override this data, so your data not will be save. \n Do you want to override ?", "Confirmation", MessageBoxButtons.YesNo);
                            if (dResult == DialogResult.Yes)
                                listPriceDeleteExistsData.Add(exists);
                            else
                                indexDeleted.Add(i);
                        }
                    }


                    for (int i = 0; i < indexDeleted.Count; i++)
                        tempPriceList.RemoveAt(indexDeleted.ElementAt(i));

                    sqlPriceListRepository = null;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("dooring agent")) {
                    for (int i = 0; i < PriceListGridView.RowCount; i++)
                    {
                        VisitaJayaPerkasa.Entities.PriceList objPriceList = new VisitaJayaPerkasa.Entities.PriceList();

                        string id = (PriceListGridView.Rows[i].Cells[0].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[0].Value.ToString();
                        objPriceList.ID = Utility.Utility.ConvertToUUID(id);

                        id = (PriceListGridView.Rows[i].Cells[1].Value.ToString().Equals("")) ? Utility.Utility.DefaultDateTime().ToString() : Utility.Utility.ChangeDateMMDD(PriceListGridView.Rows[i].Cells[1].Value.ToString());
                        objPriceList.Date = Utility.Utility.ConvertStringToDate(id);
                        if (objPriceList.Date.ToString().Equals(Utility.Utility.DefaultDateTime().ToString()))
                        {
                            MessageBox.Show(this, "Please fill date in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[4].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[4].Value.ToString();
                        objPriceList.TypeID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.TypeID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill type in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[8].Value.ToString().Equals("")) ? "-1" : PriceListGridView.Rows[i].Cells[8].Value.ToString();
                        objPriceList.PriceSupplier = Utility.Utility.ConvertStringToDecimal(id);
                        if (objPriceList.PriceSupplier.ToString().Equals("-1"))
                        {
                            MessageBox.Show(this, "Please fill price supplier in line and check your price in line " + i, "Information");
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
                                PriceListGridView.Rows[i].Cells[1].Value.ToString().Equals(PriceListGridView.Rows[j].Cells[1].Value.ToString())
                                && PriceListGridView.Rows[i].Cells[4].Value.ToString().Equals(PriceListGridView.Rows[j].Cells[4].Value.ToString())
                                )
                            {
                                MessageBox.Show(this, "Record -" + i + " and record -" + j + " has same record of date and type. Please remove one", "Information");
                                return;
                            }
                        }
                    }


                    sqlPriceListRepository = new SqlPriceListRepository();
                    listPriceDeleteExistsData.Clear();
                    List<int> indexDeleted = new List<int>();

                    for (int i = 0; i < tempPriceList.Count; i++)
                    {
                        Guid exists = Guid.Empty;
                        exists = sqlPriceListRepository.GetPriceCustomerByDAgentANDTrucking(tempPriceList.ElementAt(i).Date,
                                                    tempPriceList.ElementAt(i).TypeID.ToString());
                        if (!exists.ToString().Equals(Guid.Empty.ToString()))
                        {
                            DialogResult dResult = MessageBox.Show(this, "Record - " + i + " has already exist. \n If you don't want to override this data, so your data not will be save. \n Do you want to override ?", "Confirmation", MessageBoxButtons.YesNo);
                            if (dResult == DialogResult.Yes)
                                listPriceDeleteExistsData.Add(exists);
                            else
                                indexDeleted.Add(i);
                        }
                    }


                    for (int i = 0; i < indexDeleted.Count; i++)
                        tempPriceList.RemoveAt(indexDeleted.ElementAt(i));

                    sqlPriceListRepository = null;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("trucking"))
                {
                    for (int i = 0; i < PriceListGridView.RowCount; i++)
                    {
                        VisitaJayaPerkasa.Entities.PriceList objPriceList = new VisitaJayaPerkasa.Entities.PriceList();

                        string id = (PriceListGridView.Rows[i].Cells[0].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[0].Value.ToString();
                        objPriceList.ID = Utility.Utility.ConvertToUUID(id);

                        id = (PriceListGridView.Rows[i].Cells[1].Value.ToString().Equals("")) ? Utility.Utility.DefaultDateTime().ToString() : Utility.Utility.ChangeDateMMDD(PriceListGridView.Rows[i].Cells[1].Value.ToString());
                        objPriceList.Date = Utility.Utility.ConvertStringToDate(id);
                        if (objPriceList.Date.ToString().Equals(Utility.Utility.DefaultDateTime().ToString()))
                        {
                            MessageBox.Show(this, "Please fill date in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[4].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[4].Value.ToString();
                        objPriceList.TypeID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.TypeID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill type in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[8].Value.ToString().Equals("")) ? "-1" : PriceListGridView.Rows[i].Cells[8].Value.ToString();
                        objPriceList.PriceSupplier = Utility.Utility.ConvertStringToDecimal(id);
                        if (objPriceList.PriceSupplier.ToString().Equals("-1"))
                        {
                            MessageBox.Show(this, "Please fill price supplier in line and check your price in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[11].Value.ToString().Equals("")) ? "-1" : PriceListGridView.Rows[i].Cells[11].Value.ToString();
                        objPriceList.PriceCourier = Utility.Utility.ConvertStringToDecimal(id);
                        if (objPriceList.PriceCourier.ToString().Equals("-1"))
                        {
                            MessageBox.Show(this, "Please fill price courier in line and check your price in line " + i, "Information");
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
                                PriceListGridView.Rows[i].Cells[1].Value.ToString().Equals(PriceListGridView.Rows[j].Cells[1].Value.ToString())
                                && PriceListGridView.Rows[i].Cells[4].Value.ToString().Equals(PriceListGridView.Rows[j].Cells[4].Value.ToString())
                                )
                            {
                                MessageBox.Show(this, "Record -" + i + " and record -" + j + " have same record of date and type. Please remove one", "Information");
                                return;
                            }
                        }
                    }


                    sqlPriceListRepository = new SqlPriceListRepository();
                    listPriceDeleteExistsData.Clear();
                    List<int> indexDeleted = new List<int>();

                    for (int i = 0; i < tempPriceList.Count; i++)
                    {
                        Guid exists = Guid.Empty;
                        exists = sqlPriceListRepository.GetPriceCustomerByDAgentANDTrucking(tempPriceList.ElementAt(i).Date,
                                                    tempPriceList.ElementAt(i).TypeID.ToString());
                        if (!exists.ToString().Equals(Guid.Empty.ToString()))
                        {
                            DialogResult dResult = MessageBox.Show(this, "Record - " + i + " has already exist. \n If you don't want to override this data, so your data not will be save. \n Do you want to override ?", "Confirmation", MessageBoxButtons.YesNo);
                            if (dResult == DialogResult.Yes)
                                listPriceDeleteExistsData.Add(exists);
                            else
                                indexDeleted.Add(i);
                        }
                    }


                    for (int i = 0; i < indexDeleted.Count; i++)
                        tempPriceList.RemoveAt(indexDeleted.ElementAt(i));

                    sqlPriceListRepository = null;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("general"))
                {
                    for (int i = 0; i < PriceListGridView.RowCount; i++)
                    {
                        VisitaJayaPerkasa.Entities.PriceList objPriceList = new VisitaJayaPerkasa.Entities.PriceList();

                        string id = (PriceListGridView.Rows[i].Cells[0].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[0].Value.ToString();
                        objPriceList.ID = Utility.Utility.ConvertToUUID(id);

                        id = (PriceListGridView.Rows[i].Cells[1].Value.ToString().Equals("")) ? Utility.Utility.DefaultDateTime().ToString() : Utility.Utility.ChangeDateMMDD(PriceListGridView.Rows[i].Cells[1].Value.ToString());
                        objPriceList.Date = Utility.Utility.ConvertStringToDate(id);
                        if (objPriceList.Date.ToString().Equals(Utility.Utility.DefaultDateTime().ToString()))
                        {
                            MessageBox.Show(this, "Please fill date in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[8].Value.ToString().Equals("")) ? "-1" : PriceListGridView.Rows[i].Cells[8].Value.ToString();
                        objPriceList.PriceSupplier = Utility.Utility.ConvertStringToDecimal(id);
                        if (objPriceList.PriceSupplier.ToString().Equals("-1"))
                        {
                            MessageBox.Show(this, "Please fill price supplier in line and check your price in line " + i, "Information");
                            return;
                        }


                        tempPriceList.Add(objPriceList);
                        objPriceList = null;
                    }


                    for (int i = 0; i < tempPriceList.Count; i++)
                    {
                        for (int j = i + 1; j < tempPriceList.Count; j++)
                        {
                            if (PriceListGridView.Rows[i].Cells[1].Value.ToString().Equals(PriceListGridView.Rows[j].Cells[1].Value.ToString()))
                            {
                                MessageBox.Show(this, "Record -" + i + " and record -" + j + " have same record of date. Please remove one", "Information");
                                return;
                            }

                        }
                    }
                }


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
                    //11 is field who any in below for
                    string[] key = new string[tempPriceList.Count * 12];
                    object[] value = new object[tempPriceList.Count * 12];

                    int nn = 0;
                    for (int j = 0; j < tempPriceList.Count; j++)
                    {
                        key[nn] = "price_id";
                        value[nn++] = Guid.NewGuid();

                        key[nn] = "Date";
                        value[nn++] = tempPriceList.ElementAt(j).Date;

                        key[nn] = "supplier_id";
                        if (cbSupplier.SelectedIndex >= 0)
                            value[nn++] = listSupplier[cbSupplier.SelectedIndex].Id;
                        else value[nn++] = Guid.Empty;

                        key[nn] = "destination";
                        if (cbDestination.SelectedIndex >= 0)
                            value[nn++] = listCity[cbDestination.SelectedIndex].ID;
                        else value[nn++] = Guid.Empty;

                        key[nn] = "type_cont_id";
                        value[nn++] = tempPriceList.ElementAt(j).TypeID;

                        key[nn] = "condition_id";
                        value[nn++] = tempPriceList.ElementAt(j).ConditionID;

                        key[nn] = "price_supplier";
                        value[nn++] = tempPriceList.ElementAt(j).PriceSupplier;

                        key[nn] = "customer_id";
                        if (searchResultCustomer != null)
                            value[nn++] = searchResultCustomer.ID;
                        else value[nn++] = Guid.Empty;

                        key[nn] = "price_customer";
                        value[nn++] = tempPriceList.ElementAt(j).PriceCustomer;

                        key[nn] = "stuffing_id";
                        if (cboStuffingPlace.SelectedIndex >= 0)
                            value[nn++] = listWarehouse[cboStuffingPlace.SelectedIndex].Id;
                        else value[nn++] = Guid.Empty;

                        key[nn] = "recipient_id";
                        if (cboRecipient.SelectedIndex >= 0)
                            value[nn++] = listRecipient[cboRecipient.SelectedIndex].ID;
                        else value[nn++] = Guid.Empty;

                        key[nn] = "price_courier";
                        value[nn++] = tempPriceList.ElementAt(j).PriceCourier;
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
                    }
                    else
                    {
                        MessageBox.Show(this, "Failed save data !", "Information");
                        return;
                    }
                }

                PriceListGridView.Rows.Clear();
                PriceListGridView.Enabled = false;
            }
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

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            listPriceDeleteExistsData.Clear();
            radButton2.Enabled = true;

            pickerTo.Enabled = true;
            pickerFrom.Enabled = true;
            cboTypeSupplier.Enabled = true;
            searchResultCustomer = null;
            txtCustomer.Text = "";

            if(cboTypeSupplier.Text.ToLower().Equals("dooring agent")){
                RefreshTypeSupplier();
                SetTypeOfSupplierDooringAgent();
                return;
            }


            cboTypeSupplier.SelectedItem = cboTypeSupplier.Items.ElementAt(0);
        }


        /*
        private bool validateDate(VisitaJayaPerkasa.Entities.PriceList objPriceList, List<String> listID, List<VisitaJayaPerkasa.Entities.PriceList> priceList)
        {
            bool result = true;

            if (sqlPriceListRepository == null)
                sqlPriceListRepository = new SqlPriceListRepository();
            //query the db
            string customerID;
            if (searchResultCustomer != null)
                customerID = searchResultCustomer.ID.ToString();
            else
                customerID = Guid.Empty.ToString();
            int count = sqlPriceListRepository.FindPriceByDateSupplierCustomer(objPriceList.Date, listSupplier[cbSupplier.SelectedIndex].Id.ToString(), customerID);
            if (count > 0)
            {
                VisitaJayaPerkasa.Entities.PriceList oldPriceList = sqlPriceListRepository.GetPriceByDateSupplierCustomer(objPriceList.Date, listSupplier[cbSupplier.SelectedIndex].Id.ToString(), customerID);
                //decimal oldPrice = sqlPriceListRepository.GetSupplierPriceByDateSupplierCustomer(objPriceList.Date, listSupplier[cbSupplier.SelectedIndex].Id.ToString(), customerID);

                DialogResult dlgRes = DialogResult.Yes;
                if (objPriceList.PriceSupplier != oldPriceList.PriceSupplier)
                    dlgRes = MessageBox.Show("We already have price list on " + objPriceList.Date.ToString("dd/MMMM/yyyy") + ". Do you want to override supplier price to "+ objPriceList.PriceSupplier.ToString("#,###") +"?", "Data Duplication!", MessageBoxButtons.YesNo);
                if (dlgRes == DialogResult.Yes)
                {
                    //query the price id we want to override and set the current data id to existing
                    objPriceList.ID = oldPriceList.ID;
                    //add ID to deleted list
                    if (!listID.Contains(objPriceList.ID.ToString()))
                        listID.Add(objPriceList.ID.ToString());
                    for (int i = priceList.Count-1; i >= 0; i--)
                    {
                        if (priceList[i].ID.Equals(objPriceList.ID))
                            priceList.Remove(priceList[i]);
                    }
                    result = true;
                }
                else if (dlgRes == DialogResult.No)
                {
                    //check to the list. if exist, let it be. if not, cancel delete id
                    bool exist = false;
                    for (int i = priceList.Count - 1; i >= 0; i--)
                    {
                        if (priceList[i].ID.Equals(objPriceList.ID))
                        {
                            exist = true;
                            break;
                        }
                    }
                    if (exist)
                        listID.Remove(objPriceList.ID.ToString());
                    //remove this data from list
                    result = false;
                }
            }
            return result;
        }
         */
         

        private bool validateTrucking(VisitaJayaPerkasa.Entities.PriceList objPriceList, List<String> listID, List<VisitaJayaPerkasa.Entities.PriceList> priceList)
        {
            bool result = true;

            if (sqlPriceListRepository == null)
                sqlPriceListRepository = new SqlPriceListRepository();
            //query the db
            string customerID;
            if (searchResultCustomer != null)
                customerID = searchResultCustomer.ID.ToString();
            else
                customerID = Guid.Empty.ToString();
            string msg = sqlPriceListRepository.FindPriceBySupplierCustomerStuffing(objPriceList.Date, listSupplier[cbSupplier.SelectedIndex].Id.ToString(), customerID, listWarehouse[cboStuffingPlace.SelectedIndex].Id.ToString(), objPriceList.TypeID.ToString());
            if (msg != "")
            {
                VisitaJayaPerkasa.Entities.PriceList oldPriceList = sqlPriceListRepository.GetPriceBySupplierCustomerStuffing(objPriceList.Date, listSupplier[cbSupplier.SelectedIndex].Id.ToString(), customerID, listWarehouse[cboStuffingPlace.SelectedIndex].Id.ToString(), objPriceList.TypeID.ToString());

                DialogResult dlgRes = DialogResult.Yes;
                if (objPriceList.PriceSupplier != oldPriceList.PriceSupplier || objPriceList.PriceCourier != oldPriceList.PriceCourier)
                    dlgRes = MessageBox.Show("We already have price list on:\n" + msg + ".\nDo you want to override supplier price to " + objPriceList.PriceSupplier.ToString("#,###") + " and courier price to " + objPriceList.PriceCourier.ToString("#,###") + "?", "Data Duplication!", MessageBoxButtons.YesNo);
                if (dlgRes == DialogResult.Yes)
                {
                    //query the price id we want to override and set the current data id to existing
                    objPriceList.ID = oldPriceList.ID;
                    //add ID to deleted list
                    if (!listID.Contains(objPriceList.ID.ToString()))
                        listID.Add(objPriceList.ID.ToString());
                    for (int i = priceList.Count - 1; i >= 0; i--)
                    {
                        if (priceList[i].ID.Equals(objPriceList.ID))
                            priceList.Remove(priceList[i]);
                    }
                    result = true;
                }
                else if (dlgRes == DialogResult.No)
                {
                    //check to the list. if exist, let it be. if not, cancel delete id
                    bool exist = false;
                    for (int i = priceList.Count - 1; i >= 0; i--)
                    {
                        if (priceList[i].ID.Equals(objPriceList.ID))
                        {
                            exist = true;
                            break;
                        }
                    }
                    if (exist)
                        listID.Remove(objPriceList.ID.ToString());
                    //remove this data from list
                    result = false;
                }
            }
            return result;
        }

        private void cbDislayAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                searchResultCustomer = null;
                txtCustomer.Text = "";
                btnSearch.Enabled = false;
            }
            else
                btnSearch.Enabled = true;
        }

        private void cbRecipient_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                cboRecipient.SelectedIndex = -1;
                cboRecipient.Text = Constant.VisitaJayaPerkasaApplication.cboDefaultText;
                cboRecipient.Enabled = false;
            }
            else
                cboRecipient.Enabled = true;
        }
    }
}
