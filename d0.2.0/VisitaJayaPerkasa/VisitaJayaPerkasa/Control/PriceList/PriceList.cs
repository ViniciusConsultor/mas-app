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
using VisitaJayaPerkasa.Form;

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
            sqlPriceListRepository = null;
            PriceListGridView.Enabled = false;

            sqlPriceListRepository = null;
            sqlCityRepository = null;
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

            lblStuffing.Visible = false;
            cboStuffingPlace.Visible = false;

            lblCustomer.Visible = false;
            txtCustomer.Visible = false;
            btnSearch.Visible = false;

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

                    lblStuffing.Visible = false;
                    cboStuffingPlace.Visible = false;

                    lblCustomer.Visible = true;
                    txtCustomer.Visible = true;
                    btnSearch.Visible = true;
                    btnSearch.Enabled = true;

                    lblDestination.Visible = true;
                    cbDestination.Visible = true;
                    cbDestination.Enabled = true;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("dooring agent")) {
                    SetTypeOfSupplierDooringAgent();
                }
                else if(cboTypeSupplier.Text.ToLower().Equals("trucking")){
                    lblRecipient.Visible = false;
                    cboRecipient.Visible = false;

                    lblStuffing.Visible = true;
                    cboStuffingPlace.Visible = true;
                    cboStuffingPlace.Enabled = true;

                    lblCustomer.Visible = true;
                    txtCustomer.Visible = true;
                    btnSearch.Visible = true;
                    btnSearch.Enabled = true;

                    lblDestination.Visible = false;
                    cbDestination.Visible = false;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("general")) {
                    lblRecipient.Visible = false;
                    cboRecipient.Visible = false;
                    cboRecipient.Enabled = false;

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
                }
            }
        }

        private void LoadData() {
            sqlPriceListRepository = new SqlPriceListRepository();

            if (cboTypeSupplier.Text.ToLower().Equals("shipping lines"))
            {
                listPriceList = sqlPriceListRepository.GetPriceListByCriteria(pickerFrom.Value.Date, pickerTo.Value.Date,
                    cbSupplier.SelectedValue.ToString(), cbDestination.SelectedValue.ToString(), searchResultCustomer.ID.ToString(), "", "", 1, "");
            }
            else if (cboTypeSupplier.Text.ToLower().Equals("dooring agent")) {
                listPriceList = sqlPriceListRepository.GetPriceListByCriteria(pickerFrom.Value.Date, pickerTo.Value.Date,
                    cbSupplier.SelectedValue.ToString(), cbDestination.SelectedValue.ToString(), "", cboRecipient.SelectedValue.ToString(), "", 1, "");
            }
            else if (cboTypeSupplier.Text.ToLower().Equals("trucking")) {
                listPriceList = sqlPriceListRepository.GetPriceListByCriteria(pickerFrom.Value.Date, pickerTo.Value.Date,
                    cbSupplier.SelectedValue.ToString(), "", searchResultCustomer.ID.ToString(), "", cboStuffingPlace.SelectedValue.ToString(), 1, "");
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
                    if (txtCustomer.Text.Equals("")) {
                        MessageBox.Show(this, "Please choose customer", "Information");
                        return;
                    }

                    cbDestination.Enabled = false;
                    btnSearch.Enabled = false;


                    PriceListGridView.Columns[1].IsVisible = true;
                    PriceListGridView.Columns[2].IsVisible = true;
                    PriceListGridView.Columns[3].IsVisible = true;
                    PriceListGridView.Columns[4].IsVisible = true;
                    PriceListGridView.Columns[5].IsVisible = true;
                    PriceListGridView.Columns[6].IsVisible = false;
                    PriceListGridView.Columns[7].IsVisible = false;
                    PriceListGridView.Columns[8].IsVisible = true;
                    PriceListGridView.Columns[9].IsVisible = true;
                    PriceListGridView.Columns[10].IsVisible = false;
                    PriceListGridView.Enabled = true;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("dooring agent")) {
                    if (cboRecipient.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText)) {
                        MessageBox.Show(this, "Please choose recipient", "Information");
                        return;
                    }

                    cbDestination.Enabled = false;
                    cboRecipient.Enabled = false;

                    PriceListGridView.Columns[1].IsVisible = true;
                    PriceListGridView.Columns[2].IsVisible = true;
                    PriceListGridView.Columns[3].IsVisible = true;
                    PriceListGridView.Columns[4].IsVisible = true;
                    PriceListGridView.Columns[5].IsVisible = false;
                    PriceListGridView.Columns[6].IsVisible = false;
                    PriceListGridView.Columns[7].IsVisible = true;
                    PriceListGridView.Columns[8].IsVisible = true;
                    PriceListGridView.Columns[9].IsVisible = false;
                    PriceListGridView.Columns[10].IsVisible = false;
                    PriceListGridView.Enabled = true;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("trucking")) {
                    if (txtCustomer.Text.Equals(""))
                    {
                        MessageBox.Show(this, "Please choose customer", "Information");
                        return;
                    }
                    else if(cboStuffingPlace.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                    {
                        MessageBox.Show(this, "Please choose stuffing place", "Information");
                        return;
                    }

                    PriceListGridView.Columns[1].IsVisible = true;
                    PriceListGridView.Columns[2].IsVisible = true;
                    PriceListGridView.Columns[3].IsVisible = false;
                    PriceListGridView.Columns[4].IsVisible = true;
                    PriceListGridView.Columns[5].IsVisible = false;
                    PriceListGridView.Columns[6].IsVisible = true;
                    PriceListGridView.Columns[7].IsVisible = false;
                    PriceListGridView.Columns[8].IsVisible = true;
                    PriceListGridView.Columns[9].IsVisible = true;
                    PriceListGridView.Columns[10].IsVisible = false;
                    PriceListGridView.Enabled = true;
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("general")) {
                    if (cbSupplier.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                    {
                        MessageBox.Show(this, "Please choose supplier", "Information");
                        return;
                    }

                    PriceListGridView.Columns[1].IsVisible = true;
                    PriceListGridView.Columns[2].IsVisible = true;
                    PriceListGridView.Columns[3].IsVisible = false;
                    PriceListGridView.Columns[4].IsVisible = false;
                    PriceListGridView.Columns[5].IsVisible = false;
                    PriceListGridView.Columns[6].IsVisible = false;
                    PriceListGridView.Columns[7].IsVisible = false;
                    PriceListGridView.Columns[8].IsVisible = true;
                    PriceListGridView.Columns[9].IsVisible = false;
                    PriceListGridView.Columns[10].IsVisible = false;
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
                //var listID is used for fill id who has already inserted
                //this listID is used to delete all id and then write again (override)
                List<VisitaJayaPerkasa.Entities.PriceList> tempPriceList = new List<VisitaJayaPerkasa.Entities.PriceList>();
                List<string> listID = new List<string>();

                if (cboTypeSupplier.Text.ToLower().Equals("shipping lines"))
                {
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
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[2].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[2].Value.ToString();
                        objPriceList.SupplierID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.SupplierID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill supplier in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[3].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[3].Value.ToString();
                        objPriceList.Destination = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.Destination.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill destination in line " + i, "Information");
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

                        id = (PriceListGridView.Rows[i].Cells[9].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[9].Value.ToString();
                        objPriceList.CustomerID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.Destination.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill customer in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[11].Value.ToString().Equals("")) ? "-1" : PriceListGridView.Rows[i].Cells[11].Value.ToString();
                        objPriceList.PriceCourier = Utility.Utility.ConvertStringToDecimal(id);
                        if (objPriceList.PriceCourier.ToString().Equals("-1"))
                        {
                            MessageBox.Show(this, "Please fill price courier in line and check your price in line " + i, "Information");
                            return;
                        }

                        if (!validateDate(objPriceList.Date, objPriceList.SupplierID, objPriceList.CustomerID, objPriceList, listID))
                            continue;

                        tempPriceList.Add(objPriceList);
                        objPriceList = null;
                    }
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("dooring agent")) {
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
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[2].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[2].Value.ToString();
                        objPriceList.SupplierID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.SupplierID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill supplier in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[3].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[3].Value.ToString();
                        objPriceList.Destination = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.Destination.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill destination in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[4].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[4].Value.ToString();
                        objPriceList.TypeID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.TypeID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill type in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[7].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[7].Value.ToString();
                        objPriceList.Recipient = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.Recipient.ToString().Equals(Guid.Empty))
                        {
                            MessageBox.Show(this, "Please fill recipient in line " + i, "Information");
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

                        if (!validateDate(objPriceList.Date, objPriceList.SupplierID, objPriceList.CustomerID, objPriceList, listID))
                            continue;

                        tempPriceList.Add(objPriceList);
                        objPriceList = null;
                    }
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("trucking"))
                {
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
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[2].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[2].Value.ToString();
                        objPriceList.SupplierID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.SupplierID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill supplier in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[4].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[4].Value.ToString();
                        objPriceList.TypeID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.TypeID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill type in line " + i, "Information");
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[9].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[9].Value.ToString();
                        objPriceList.CustomerID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.CustomerID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill customer in line " + i, "Information");
                            return;
                        }
                        
                        id = (PriceListGridView.Rows[i].Cells[6].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[6].Value.ToString();
                        objPriceList.StuffingID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.StuffingID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill stuffing in line " + i, "Information");
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

                        //if (!validateDate(objPriceList.Date, objPriceList.SupplierID, objPriceList.CustomerID, objPriceList, listID))
                        //    continue;

                        if (!validateTrucking(objPriceList.Date, objPriceList.SupplierID, objPriceList.CustomerID, objPriceList.StuffingID, objPriceList, listID))
                            continue;

                        tempPriceList.Add(objPriceList);
                        objPriceList = null;
                    }
                }
                else if (cboTypeSupplier.Text.ToLower().Equals("general"))
                {
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
                            return;
                        }

                        id = (PriceListGridView.Rows[i].Cells[2].Value.ToString().Equals("")) ? Guid.Empty.ToString() : PriceListGridView.Rows[i].Cells[2].Value.ToString();
                        objPriceList.SupplierID = Utility.Utility.ConvertToUUID(id);
                        if (objPriceList.SupplierID.ToString().Equals(Guid.Empty.ToString()))
                        {
                            MessageBox.Show(this, "Please fill supplier in line " + i, "Information");
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

                        if (!validateDate(objPriceList.Date, objPriceList.SupplierID, objPriceList.CustomerID, objPriceList, listID))
                            continue;

                        tempPriceList.Add(objPriceList);
                        objPriceList = null;
                    }
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

                        key[nn] = "price_courier";
                        value[nn++] = tempPriceList.ElementAt(j).PriceCourier;
                    }

                    sqlParamInsert = SqlUtility.SetSqlParameter(key, value);
                    if (sqlPriceListRepository == null)
                        sqlPriceListRepository = new SqlPriceListRepository();
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

        private bool validateDate(DateTime date, Guid supplierID, Guid customerID, VisitaJayaPerkasa.Entities.PriceList objPriceList, List<String> listID)
        {
            bool result = true;

            if (sqlPriceListRepository == null)
                sqlPriceListRepository = new SqlPriceListRepository();
            //query the db
            int count = sqlPriceListRepository.FindPriceByDateSupplierCustomer(date, supplierID.ToString(), customerID.ToString());
            if (count > 0)
            {
                DialogResult dlgRes = MessageBox.Show("We already have price list on " + date.ToString("dd/MMMM/yyyy") + ". Do you want to override?", "Data Duplication!", MessageBoxButtons.YesNo);
                if (dlgRes == DialogResult.Yes)
                {
                    //query the price id we want to override and set the current data id to existing
                    objPriceList.ID = (sqlPriceListRepository.GetPriceByDateSupplierCustomer(date, supplierID.ToString(), customerID.ToString()));
                    //add ID to deleted list
                    listID.Add(objPriceList.ID.ToString());
                    result = true;
                }
                else if (dlgRes == DialogResult.No)
                {
                    //remove this data from list
                    result = false;
                }
            }
            return result;
        }

        private bool validateTrucking(DateTime date, Guid supplierID, Guid customerID, Guid stuffingID, VisitaJayaPerkasa.Entities.PriceList objPriceList, List<String> listID)
        {
            bool result = true;

            if (sqlPriceListRepository == null)
                sqlPriceListRepository = new SqlPriceListRepository();
            //query the db
            string msg = sqlPriceListRepository.FindPriceBySupplierCustomerStuffing(date, supplierID.ToString(), customerID.ToString(), stuffingID.ToString());
            if (msg != "")
            {
                DialogResult dlgRes = MessageBox.Show("We already have price list on " + msg + ". Do you want to override?", "Data Duplication!", MessageBoxButtons.YesNo);
                if (dlgRes == DialogResult.Yes)
                {
                    //query the price id we want to override and set the current data id to existing
                    objPriceList.ID = (sqlPriceListRepository.GetPriceBySupplierCustomerStuffing(date, supplierID.ToString(), customerID.ToString(), stuffingID.ToString()));
                    //add ID to deleted list
                    listID.Add(objPriceList.ID.ToString());
                    result = true;
                }
                else if (dlgRes == DialogResult.No)
                {
                    //remove this data from list
                    result = false;
                }
            }
            return result;
        }
    }
}
