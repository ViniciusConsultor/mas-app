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
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.Control.Transaction
{
    public partial class CustomerTransEdit : UserControl
    {
        private bool wantToCreateVessel;
        public Guid ID;
        public IEnumerable<int> days;
        public int selectedIdx; 
        
        private VisitaJayaPerkasa.Entities.CustomerTrans customerTrans;
        private List<VisitaJayaPerkasa.Entities.CustomerTransDetail> listCustomerTransDetail;
        
        private SqlCustomerTransRepository sqlCustomerTransRepository;
        private SqlCustomerRepository sqlCustomerRepository;
        private SqlTypeContRepository sqlTypeContRepository;
        private SqlCityRepository sqlCityRepository;
        private SqlPelayaranRepository sqlPelayaranRepository;
        private SqlConditionRepository sqlConditionRepository;
        private SqlPriceListRepository sqlPriceListRepository;
        private SqlWareHouseRepository sqlWarehouseRepository;
        private SqlRecipientRepository sqlRecipientRepository;
        private SqlScheduleRepository sqlScheduleRepository;
        private SqlTruckingRepository sqlTruckingRepository;

        public CustomerTransEdit(VisitaJayaPerkasa.Entities.CustomerTrans customerTrans)
        {
            InitializeComponent();
            this.customerTrans = customerTrans;
            
            sqlCustomerRepository = new SqlCustomerRepository();
            sqlTypeContRepository = new SqlTypeContRepository();
            sqlCityRepository = new SqlCityRepository();
            sqlPelayaranRepository = new SqlPelayaranRepository();
            sqlConditionRepository = new SqlConditionRepository();
            sqlPriceListRepository = new SqlPriceListRepository();
            sqlWarehouseRepository = new SqlWareHouseRepository();
            sqlRecipientRepository = new SqlRecipientRepository();
            sqlScheduleRepository = new SqlScheduleRepository();
            sqlTruckingRepository = new SqlTruckingRepository();

            List<VisitaJayaPerkasa.Entities.Customer> listCustomer = sqlCustomerRepository.ListCustomers();
            List<VisitaJayaPerkasa.Entities.TypeCont> listType = sqlTypeContRepository.GetTypeCont();
            List<VisitaJayaPerkasa.Entities.City> listOrigin = sqlCityRepository.GetCity();
            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
            {
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<VisitaJayaPerkasa.Entities.City> listDestination = sqlCityRepository.GetCity();
            List<VisitaJayaPerkasa.Entities.PelayaranDetail> listPelayaran = sqlPelayaranRepository.GetVessels();
            List<VisitaJayaPerkasa.Entities.Schedule> listSchedule = sqlScheduleRepository.ListSchedule();
            List<VisitaJayaPerkasa.Entities.Condition> listCondition = sqlConditionRepository.GetConditions();
            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
            {
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<VisitaJayaPerkasa.Entities.WareHouse> listWarehouse = sqlWarehouseRepository.GetWareHouse();
            List<VisitaJayaPerkasa.Entities.Recipient> listRecipient = sqlRecipientRepository.GetRecipient();
            List<VisitaJayaPerkasa.Entities.Trucking> listTrucking = sqlTruckingRepository.ListTrucking();
            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
            {
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cboCustomer.DataSource = listCustomer;
            cboCustomer.DisplayMember = "CustomerName";
            cboCustomer.ValueMember = "ID";
            cboCustomer.SelectedIndex = -1;
            cboCustomer.Text = "-- Choose --";
            
            cboType.DataSource = listType;
            cboType.DisplayMember = "TypeName";
            cboType.ValueMember = "ID";
            cboType.SelectedIndex = -1;
            cboType.Text = "-- Choose --";

            cboOrigin.DataSource = listOrigin;
            cboOrigin.DisplayMember = "CityName";
            cboOrigin.ValueMember = "ID";
            cboOrigin.SelectedIndex = -1;
            cboOrigin.Text = "-- Choose --";

            cboDestination.DataSource = listDestination;
            cboDestination.DisplayMember = "CityName";
            cboDestination.ValueMember = "ID";
            cboDestination.SelectedIndex = -1;
            cboDestination.Text = "-- Choose --";

            days = from p in listDestination 
                   select p.Days;

            cboPelayaranDetail.DataSource = listSchedule;
            cboPelayaranDetail.DisplayMember = "namaKapal";
            //cboPelayaranDetail.ValueMember = "ID";
            cboPelayaranDetail.SelectedIndex = -1;
            cboPelayaranDetail.Text = "-- Choose --";

            cboCondition.DataSource = listCondition;
            cboCondition.DisplayMember = "ConditionName";
            cboCondition.ValueMember = "ID";
            cboCondition.SelectedIndex = -1;
            cboCondition.Text = "-- Choose --";

            cboStuffingPlace.DataSource = listWarehouse;
            cboStuffingPlace.DisplayMember = "Address";
            cboStuffingPlace.ValueMember = "Id";
            cboStuffingPlace.SelectedIndex = -1;
            cboStuffingPlace.Text = "-- Choose --";

            cboRecipient.DataSource = listRecipient;
            cboRecipient.DisplayMember = "Name";
            cboRecipient.ValueMember = "ID";
            cboRecipient.SelectedIndex = -1;
            cboRecipient.Text = "-- Choose --";

            cboTrucking.DataSource = listTrucking;
            cboTrucking.DisplayMember = "TruckNo";
            cboTrucking.ValueMember = "ID";
            cboTrucking.SelectedIndex = -1;
            cboTrucking.Text = "-- Choose --";

            if (customerTrans == null)
            {
                wantToCreateVessel = true;
                listCustomerTransDetail = new List<Entities.CustomerTransDetail>();
            }
            else
            {
                wantToCreateVessel = false;
                cboCustomer.SelectedValue = customerTrans.CustomerID;
                cboCustomer.Enabled = false;

                SqlCustomerTransRepository sqlCustomerTransRepository = new SqlCustomerTransRepository();
                listCustomerTransDetail = sqlCustomerTransRepository.ListCustomerTransDetail(customerTrans.CustomerTransID);
                ID = customerTrans.CustomerTransID;

                if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (listCustomerTransDetail != null)
                    CustomerTransDetailGridView.DataSource = listCustomerTransDetail;
                else
                    listCustomerTransDetail = new List<VisitaJayaPerkasa.Entities.CustomerTransDetail>();

                sqlCustomerTransRepository = null;
            }



            cboCustomer.SelectedIndexChanged += new EventHandler(cboSelected_SelectedIndexChanged);
            cboType.SelectedIndexChanged += new EventHandler(cboSelected_SelectedIndexChanged);
            cboDestination.SelectedIndexChanged += new EventHandler(cboSelected_SelectedIndexChanged);
            cboCondition.SelectedIndexChanged += new EventHandler(cboSelected_SelectedIndexChanged);


            sqlCustomerRepository = null;
            sqlTypeContRepository = null;
            sqlCityRepository = null;
            sqlPelayaranRepository = null;
            sqlConditionRepository = null;
            sqlRecipientRepository = null;

            if (wantToCreateVessel)
            {
                dtpTD.Visible = false;
                dtpETA.Visible = false;
                dtpTA.Visible = false;
                dtpUnloading.Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboCustomer.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select Customer", "Information");
                return;
            }
            else if (cboType.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select Cont Type", "Information");
                return;
            }
            else if (cboPelayaranDetail.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select Vessel", "Information");
                return;
            }
            else if (cboOrigin.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select Origin", "Information");
                return;
            }
            else if (cboDestination.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select Destionation", "Information");
                return;
            }
            else if (cboCondition.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select Condition", "Information");
                return;
            }
            else if (cboStuffingPlace.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select Stuffing Place", "Information");
                return;
            }
            else if(cboRecipient.SelectedIndex == -1){
                MessageBox.Show(this, "Please select Recipient", "Information");
                return;
            }
            else if (cboTrucking.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select truck number", "Information");
                return;
            }
            else if (etVoy.Text.Trim().Equals(""))
            {
                MessageBox.Show(this, "Please fill Voyage", "Information");
                return;
            }
            else if (etSeal.Text.Trim().Equals(""))
            {
                MessageBox.Show(this, "Please fill no seal", "Information");
                return;
            }
            else if (etPrice.Text.Trim().Equals(""))
            {
                MessageBox.Show(this, "Please fill set price", "Information");
                return;
            }
            else if (dtpTD.Value <= dtpETD.Value)
            {
                MessageBox.Show(this, "Please select td greater than etd", "Information");
                return;
            }
            else
            {
                try
                {
                    VisitaJayaPerkasa.Entities.CustomerTransDetail objCustTransDetail = new Entities.CustomerTransDetail();
                    objCustTransDetail.CustomerDetailTransID = Guid.NewGuid();

                    objCustTransDetail.TypeID = Utility.Utility.ConvertToUUID(cboType.SelectedValue.ToString());
                    objCustTransDetail.TypeName = cboType.Text;

                    VisitaJayaPerkasa.Entities.Schedule schedule = cboPelayaranDetail.SelectedValue as VisitaJayaPerkasa.Entities.Schedule;
                    if (schedule != null)
                    {
                        objCustTransDetail.PelayaranDetailID = schedule.ID;
                        objCustTransDetail.VesselName = schedule.namaKapal;
                    }

                    objCustTransDetail.Origin = Utility.Utility.ConvertToUUID(cboOrigin.SelectedValue.ToString());
                    objCustTransDetail.OriginName = cboOrigin.Text;

                    objCustTransDetail.Destination = Utility.Utility.ConvertToUUID(cboDestination.SelectedValue.ToString());
                    objCustTransDetail.DestinationName = cboDestination.Text;

                    objCustTransDetail.ConditionID = Utility.Utility.ConvertToUUID(cboCondition.SelectedValue.ToString());
                    objCustTransDetail.ConditionName = cboCondition.Text;

                    objCustTransDetail.NoSeal = etSeal.Text.Trim();
                    objCustTransDetail.TruckNo = cboTrucking.Text.Trim();
                    objCustTransDetail.Voyage = etVoy.Text.Trim();

                    objCustTransDetail.StuffingDate = dtpStuffingDate.Value;
                    objCustTransDetail.StuffingPlace = Utility.Utility.ConvertToUUID(cboStuffingPlace.SelectedValue.ToString());
                    objCustTransDetail.WarehouseName = cboStuffingPlace.Text;

                    objCustTransDetail.ETD = dtpETD.Value;
                    objCustTransDetail.TD = dtpTD.Value;
                    objCustTransDetail.ETA = dtpETA.Value;
                    objCustTransDetail.TA = dtpTA.Value;
                    objCustTransDetail.Unloading = dtpUnloading.Value;
                    objCustTransDetail.Price = Decimal.Parse(etPrice.Text);

                    objCustTransDetail.RecipientID = Utility.Utility.ConvertToUUID(cboRecipient.SelectedValue.ToString());
                    objCustTransDetail.RecipientName = cboRecipient.Text;
                    objCustTransDetail.JenisBarang = etJenisBarang.Text.Trim();
                    objCustTransDetail.NoContainer = etNoContainer.Text.Trim();
                    objCustTransDetail.Quantity = etQty.Text.Trim();
                    objCustTransDetail.NoBA = etBA.Text.Trim();
                    objCustTransDetail.TerimaToko = dtpTerimaToko.Value;
                    objCustTransDetail.Keterangan = etKeterangan.Text.Trim();
                    objCustTransDetail.Sj1 = etSJ1.Text.Trim();
                    objCustTransDetail.Sj2 = etSJ2.Text.Trim();
                    objCustTransDetail.Sj3 = etSJ3.Text.Trim();
                    objCustTransDetail.Sj4 = etSJ4.Text.Trim();
                    objCustTransDetail.Sj5 = etSJ5.Text.Trim();
                    objCustTransDetail.Sj6 = etSJ6.Text.Trim();
                    objCustTransDetail.Sj7 = etSJ7.Text.Trim();
                    objCustTransDetail.Sj8 = etSJ8.Text.Trim();
                    objCustTransDetail.Sj9 = etSJ9.Text.Trim();
                    objCustTransDetail.Sj10 = etSJ10.Text.Trim();
                    objCustTransDetail.Sj11 = etSJ11.Text.Trim();
                    objCustTransDetail.Sj12 = etSJ12.Text.Trim();
                    objCustTransDetail.Sj13 = etSJ13.Text.Trim();
                    objCustTransDetail.Sj14 = etSJ14.Text.Trim();
                    objCustTransDetail.Sj15 = etSJ15.Text.Trim();
                    objCustTransDetail.Sj16 = etSJ16.Text.Trim();
                    objCustTransDetail.Sj17 = etSJ17.Text.Trim();
                    objCustTransDetail.Sj18 = etSJ18.Text.Trim();
                    objCustTransDetail.Sj19 = etSJ19.Text.Trim();
                    objCustTransDetail.Sj20 = etSJ20.Text.Trim();
                    objCustTransDetail.Sj21 = etSJ21.Text.Trim();
                    objCustTransDetail.Sj22 = etSJ22.Text.Trim();
                    objCustTransDetail.Sj23 = etSJ23.Text.Trim();
                    objCustTransDetail.Sj24 = etSJ24.Text.Trim();
                    objCustTransDetail.Sj25 = etSJ25.Text.Trim();

                    listCustomerTransDetail.Add(objCustTransDetail);
                    CustomerTransDetailGridView.DataSource = typeof(List<VisitaJayaPerkasa.Entities.CustomerTransDetail>);
                    CustomerTransDetailGridView.DataSource = listCustomerTransDetail;
                    objCustTransDetail = null;

                    etSeal.Text = "";
                    etVoy.Text = "";
                    etJenisBarang.Text = "";
                    etNoContainer.Text = "";
                    etQty.Text = "";
                    etBA.Text = "";
                    etKeterangan.Text = "";
                    etSJ1.Text = "";
                    etSJ2.Text = "";
                    etSJ3.Text = "";
                    etSJ4.Text = "";
                    etSJ5.Text = "";
                    etSJ6.Text = "";
                    etSJ7.Text = "";
                    etSJ8.Text = "";
                    etSJ9.Text = "";
                    etSJ10.Text = "";
                    etSJ11.Text = "";
                    etSJ12.Text = "";
                    etSJ13.Text = "";
                    etSJ14.Text = "";
                    etSJ15.Text = "";
                    etSJ16.Text = "";
                    etSJ17.Text = "";
                    etSJ18.Text = "";
                    etSJ19.Text = "";
                    etSJ20.Text = "";
                    etSJ21.Text = "";
                    etSJ22.Text = "";
                    etSJ23.Text = "";
                    etSJ24.Text = "";
                    etSJ25.Text = "";


                    cboCustomer.SelectedIndexChanged -= new EventHandler(cboSelected_SelectedIndexChanged);
                    cboType.SelectedIndexChanged -= new EventHandler(cboSelected_SelectedIndexChanged);
                    cboDestination.SelectedIndexChanged -= new EventHandler(cboSelected_SelectedIndexChanged);
                    cboCondition.SelectedIndexChanged -= new EventHandler(cboSelected_SelectedIndexChanged);


                    cboStuffingPlace.SelectedIndex = -1;
                    cboStuffingPlace.Text = "-- Choose --";
                    cboCondition.SelectedIndex = -1;
                    cboCondition.Text = "-- Choose --";
                    cboOrigin.SelectedIndex = -1;
                    cboOrigin.Text = "-- Choose --";
                    cboDestination.SelectedIndex = -1;
                    cboDestination.Text = "-- Choose --";
                    cboPelayaranDetail.SelectedIndex = -1;
                    cboPelayaranDetail.Text = "-- Choose --";
                    cboType.SelectedIndex = -1;
                    cboType.Text = "-- Choose --";
                    cboRecipient.SelectedIndex = -1;
                    cboRecipient.Text = "-- Choose --";
                    cboTrucking.SelectedIndex = -1;
                    cboTrucking.Text = "-- Choose --";


                    dtpStuffingDate.Value = DateTime.Now;
                    dtpETD.Value = DateTime.Now;
                    dtpTD.Value = DateTime.Now;
                    dtpETA.Value = DateTime.Now;
                    dtpTA.Value = DateTime.Now;
                    dtpUnloading.Value = DateTime.Now;
                    dtpTerimaToko.Value = DateTime.Now;


                    cboCustomer.SelectedIndexChanged += new EventHandler(cboSelected_SelectedIndexChanged);
                    cboType.SelectedIndexChanged += new EventHandler(cboSelected_SelectedIndexChanged);
                    cboDestination.SelectedIndexChanged += new EventHandler(cboSelected_SelectedIndexChanged);
                    cboCondition.SelectedIndexChanged += new EventHandler(cboSelected_SelectedIndexChanged);
                }
                catch (NullReferenceException ex)
                {
                    Logging.Error("CustomerTransEdit.cs - btnAddClick '" + ex.Message + "'");
                }
            }
            if (listCustomerTransDetail.Count > 0)
                cboCustomer.Enabled = false;
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            listCustomerTransDetail.Clear();
            CustomerTransDetailGridView.DataSource = typeof(List<VisitaJayaPerkasa.Entities.CustomerTransDetail>);
            CustomerTransDetailGridView.DataSource = listCustomerTransDetail;
            if (listCustomerTransDetail.Count == 0 && wantToCreateVessel)
                cboCustomer.Enabled = true;
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
            if (listCustomerTransDetail.Count == 0 && wantToCreateVessel)
                cboCustomer.Enabled = true;
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new CustomerTransList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (CustomerTransDetailGridView.Rows.Count > 0) {
                sqlCustomerTransRepository = new SqlCustomerTransRepository();
                if (cboCustomer.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                    MessageBox.Show(this, "Please choose customer name", "Information");
                else { 

                    //51 is number of field below
                    int k = 0;
                    string[] key = new string[listCustomerTransDetail.Count * 51];
                    object[] value = new object[listCustomerTransDetail.Count * 51];

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

                        key[k] = "price";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Price;

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

                        key[k] = "recipient_id";
                        value[k++] = listCustomerTransDetail.ElementAt(i).RecipientID;

                        key[k] = "jenis_barang";
                        value[k++] = listCustomerTransDetail.ElementAt(i).JenisBarang;

                        key[k] = "no_container";
                        value[k++] = listCustomerTransDetail.ElementAt(i).NoContainer;

                        key[k] = "quantity";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Quantity;

                        key[k] = "sj1";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj1;

                        key[k] = "sj2";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj2;

                        key[k] = "sj3";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj3;

                        key[k] = "sj4";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj4;

                        key[k] = "sj5";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj5;

                        key[k] = "sj6";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj6;

                        key[k] = "sj7";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj7;

                        key[k] = "sj8";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj8;

                        key[k] = "sj9";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj9;

                        key[k] = "sj10";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj10;

                        key[k] = "sj11";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj11;

                        key[k] = "sj12";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj12;

                        key[k] = "sj13";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj13;

                        key[k] = "sj14";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj14;

                        key[k] = "sj15";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj15;

                        key[k] = "sj16";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj16;

                        key[k] = "sj17";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj17;

                        key[k] = "sj18";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj18;

                        key[k] = "sj19";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj19;

                        key[k] = "sj20";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj20;

                        key[k] = "sj21";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj21;

                        key[k] = "sj22";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj22;

                        key[k] = "sj23";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj23;

                        key[k] = "sj24";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj24;

                        key[k] = "sj25";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Sj25;

                        key[k] = "terima_toko";
                        value[k++] = listCustomerTransDetail.ElementAt(i).TerimaToko;

                        key[k] = "keterangan";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Keterangan;

                        key[k] = "no_ba";
                        value[k++] = listCustomerTransDetail.ElementAt(i).NoBA;

                        key[k] = "deleted";
                        value[k++] = listCustomerTransDetail.ElementAt(i).Deleted;
                    }


                    SqlParameter[] sqlParameterInsert = SqlUtility.SetSqlParameter(key, value);
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
                            ID = TransID;
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Form.Report.RptTransForm rptTrans = new Form.Report.RptTransForm(cboCustomer.Text.ToString(), ID);
            rptTrans.Show();
        }

        private void dtpTD_ValueChanged(object sender, EventArgs e)
        {
            dtpETA.Value = (cboDestination.Text == "-- Choose --") ? DateTime.Now : dtpTD.Value.AddDays(days.ElementAt(selectedIdx));
        }


        private void cboSelected_SelectedIndexChanged(object sender, EventArgs e) {
            sqlPelayaranRepository = new SqlPelayaranRepository();

            try
            {
                RadComboBox cbo = sender as RadComboBox;

                if (cbo.Tag != null)
                {
                    if (cbo.Tag.ToString() == "111")
                    {
                        List<VisitaJayaPerkasa.Entities.Schedule> listSchedule = sqlScheduleRepository.ListScheduleByDestination((Guid)cbo.SelectedValue);
                        if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                            MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        cboPelayaranDetail.DataSource = listSchedule;
                        cboPelayaranDetail.DisplayMember = "namaKapal";
                        //cboPelayaranDetail.ValueMember = "PelayaranDetailID";
                        cboPelayaranDetail.SelectedIndex = -1;
                        cboPelayaranDetail.Text = "-- Choose --";


                        if (listSchedule == null)
                        {
                            MessageBox.Show(this, "No schedule in list", "Information");
                            return;
                        }

                        selectedIdx = cbo.SelectedIndex;
                        dtpETA.Value = (dtpTD.Value.ToShortDateString() == DateTime.Today.ToShortDateString()) ? DateTime.Now : dtpTD.Value.AddDays(days.ElementAt(selectedIdx));

                    }
                }

                String custID, destID, typeID, condID;
                custID = destID = typeID = condID = null;

                if (!cboCustomer.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                    custID = cboCustomer.SelectedValue.ToString();
                if (!cboDestination.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                    destID = cboDestination.SelectedValue.ToString();
                if (!cboType.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                    typeID = cboType.SelectedValue.ToString();
                if (!cboCondition.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                    condID = cboCondition.SelectedValue.ToString();

                if (custID != null && destID != null && typeID != null && condID != null)
                {
                    etPrice.Text = sqlPriceListRepository.SearchPriceList(DateTime.Now, custID, destID, typeID, condID).ToString();
                    if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (etPrice.Text.Equals("0"))
                        etPrice.Text = sqlPriceListRepository.SearchPriceListGeneral(DateTime.Now, destID, typeID, condID).ToString();

                    if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (etPrice.Text.Equals("0"))
                        MessageBox.Show(this, "No price for this customer", "Information");
                }
            }
            catch (NullReferenceException ex)
            {
                etPrice.Text = "0";
                Logging.Error("CustomerTransEdit.cs - CBSelected_Changed '" + ex.Message + "'");
            }

            sqlPelayaranRepository = null;
        }

        private void cbo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboPelayaranDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPelayaranDetail.SelectedIndex < 0)
            {
                etVoy.Text = "";
                return;
            }
            VisitaJayaPerkasa.Entities.Schedule schedule = cboPelayaranDetail.SelectedValue as VisitaJayaPerkasa.Entities.Schedule;
            etVoy.Text = schedule.voy;
        }

        /*
        private void CustomerTransDetailGridView_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo gridInfo = CustomerTransDetailGridView.SelectedRows.First();
            string id = gridInfo.Cells[0].Value.ToString();
            VisitaJayaPerkasa.Entities.CustomerTransDetail customerTrans = listCustomerTransDetail.Where(c => c.CustomerTransID.ToString().Equals(id)).FirstOrDefault();

            cboType.SelectedValue = customerTrans.TypeID.ToString();
            cboType.Text = customerTrans.TypeName;

            cboPelayaranDetail.SelectedValue = customerTrans.PelayaranDetailID.ToString();
            cboPelayaranDetail.Text = customerTrans.VesselName;

            cboOrigin.SelectedValue = customerTrans.Origin.ToString();
            cboOrigin.Text = customerTrans.OriginName;

            cboDestination.SelectedValue = customerTrans.Destination.ToString();
            cboDestination.Text = customerTrans.DestinationName;
            
            cboCondition.SelectedValue = customerTrans.ConditionID.ToString();
            cboCondition.Text = customerTrans.ConditionName;

            etSeal.Text = customerTrans.NoSeal;
            etTruckNo.Text = customerTrans.TruckNo;
            etVoy.Text = customerTrans.Voyage;

            dtpStuffingDate.Value = customerTrans.StuffingDate;

            cboStuffingPlace.SelectedValue = customerTrans.StuffingPlace.ToString();

            dtpETD.Value = customerTrans.ETD;
            dtpTD.Value = customerTrans.TD;
            dtpETA.Value = customerTrans.ETA;
            dtpTA.Value = customerTrans.TA;
            dtpUnloading.Value = customerTrans.Unloading;
            etPrice.Text = customerTrans.Price.ToString();

            cboRecipient.SelectedValue = customerTrans.RecipientID.ToString();
            cboRecipient.Text = customerTrans.RecipientName;
            etJenisBarang.Text = customerTrans.JenisBarang;
            etNoContainer.Text = customerTrans.NoContainer;
            etQty.Text = customerTrans.Quantity.ToString();
            etBA.Text = customerTrans.NoBA;
            dtpTerimaToko.Value = customerTrans.TerimaToko.Value;
            etKeterangan.Text = customerTrans.Keterangan;
            etSJ1.Text = customerTrans.Sj1;
            etSJ2.Text = customerTrans.Sj2;
            etSJ3.Text = customerTrans.Sj3;
            etSJ4.Text = customerTrans.Sj4;
            etSJ5.Text = customerTrans.Sj5;
            etSJ6.Text = customerTrans.Sj6;
            etSJ7.Text = customerTrans.Sj7;
            etSJ8.Text = customerTrans.Sj8;
            etSJ9.Text = customerTrans.Sj9;
            etSJ10.Text = customerTrans.Sj10;
            etSJ11.Text = customerTrans.Sj11;
            etSJ12.Text = customerTrans.Sj12;
            etSJ13.Text = customerTrans.Sj13;
            etSJ14.Text = customerTrans.Sj14;
            etSJ15.Text = customerTrans.Sj15;
            etSJ16.Text = customerTrans.Sj16;
            etSJ17.Text = customerTrans.Sj17;
            etSJ18.Text = customerTrans.Sj18;
            etSJ19.Text = customerTrans.Sj19;
            etSJ20.Text = customerTrans.Sj20;
            etSJ21.Text = customerTrans.Sj21;
            etSJ22.Text = customerTrans.Sj22;
            etSJ23.Text = customerTrans.Sj23;
            etSJ24.Text = customerTrans.Sj24;
            etSJ25.Text = customerTrans.Sj25;
        }*/
    }
}