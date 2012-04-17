using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;

using VisitaJayaPerkasa.Entities;
using VisitaJayaPerkasa.SqlRepository;

namespace VisitaJayaPerkasa.Form.Report.Delivery
{
    public partial class RptDeliveryControl : UserControl
    {
        private SqlCustomerRepository sqlCustomerRepository;
        private SqlCustomerTransRepository sqlCustomerTransRepository;
        private RptDelivery rptDelivery;
        private RptBeritaAcaraDelivery rptBeritaAcara;

        public RptDeliveryControl()
        {
            InitializeComponent();

            sqlCustomerRepository = new SqlCustomerRepository();
            sqlCustomerTransRepository = new SqlCustomerTransRepository();

            DateTime datetime = DateTime.Now;
            cboMonth.SelectedIndex = datetime.Month-1;
            spnYear.Value = datetime.Year;

            cboMonth.SelectedIndex = datetime.Month - 1;
            spnYear.Value = datetime.Year;

            List<VisitaJayaPerkasa.Entities.Customer> listCustomer = sqlCustomerRepository.ListCustomers();
            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            cboCustomer.DataSource = listCustomer;
            cboCustomer.DisplayMember = "CustomerName";
            cboCustomer.ValueMember = "ID";
            cboCustomer.SelectedIndex = -1;
            cboCustomer.Text = "-- Choose --";

            cboCustomer.SelectedIndex = 0;

            cboCustomer2.DataSource = listCustomer;
            cboCustomer2.DisplayMember = "CustomerName";
            cboCustomer2.ValueMember = "ID";
            cboCustomer2.SelectedIndex = -1;
            cboCustomer2.Text = "-- Choose --";

            cboTransDate.SelectedIndex = -1;
            cboTransDate.Text = "-- Choose --";

            cboTransDetail.SelectedIndex = -1;
            cboTransDetail.Text = "-- Choose --";

            cboCustomer2.SelectedIndexChanged += new EventHandler(Customer2_SelectedIndexChanged);

            cboCustomer2.SelectedIndex = 0;
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            //if no customer selected, alert and return
            if (cboCustomer.SelectedIndex < 0)
            {
                MessageBox.Show("Please select customer.");
                return;
            }

            //prepare parameter data
            DateTime startDate = new DateTime((int)spnYear.Value, cboMonth.SelectedIndex + 1, 1);
            int endMonth = cboMonth.SelectedIndex + 2;
            int endYear = (int)spnYear.Value;
            if (endMonth > 12)
            {
                endMonth = endMonth - 12;
                endYear++;
            }
            DateTime endDate = new DateTime(endYear, endMonth, 1);
            string customerId = cboCustomer.SelectedValue.ToString();

            //build query
            string query = "SELECT ct.id, ct.customer_id AS CustomerId, c.customer_name AS CustomerName, " +
                           "       ct.tgl_transaksi AS TanggalTransaksi, ctd.stuffing_date AS StuffingDate, " +
                           "       w.address AS stuffingplace, des.city_name AS Tujuan, " +
                           "       ctd.no_container AS ContainerNo, ctd.voy, ctd.td, " +
                           "       pd.vessel_name AS VesselName, ctd.ta, ctd.unloading, " +
                           "       ctd.terima_toko AS DateReceived, r.recipient_name AS Recipient " +
                           "FROM   CUSTOMER_TRANS AS ct " +
                           "       INNER JOIN CUSTOMER AS c ON c.customer_id = ct.customer_id " +
                           "       LEFT OUTER JOIN CUSTOMER_TRANS_DETAIL AS ctd ON ctd.customer_trans_id = ct.id " +
                           "       LEFT OUTER JOIN WAREHOUSE AS w ON w.stuffing_place_id = ctd.stuffing_place " +
                           "       LEFT OUTER JOIN PELAYARAN_DETAIL AS pd ON pd.pelayaran_detail_id = ctd.pelayaran_detail_id " +
                           "       LEFT OUTER JOIN CITY AS des ON des.city_id = ctd.destination " +
                           "       LEFT OUTER JOIN RECIPIENT AS r ON r.recipient_id = ctd.recipient_id " +
                           "WHERE ct.tgl_transaksi >= '" + startDate + "' " +
                           "AND ct.tgl_transaksi < '" + endDate + "' " +
                           "AND ct.customer_id = '" + customerId + "' ";
            
            rptDelivery = new RptDelivery();

            SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);
            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(query, VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);

                ShippingMainDataSet ds = new ShippingMainDataSet();
                da.Fill(ds, "DELIVERY");

                DataTable dt = ds.Tables["DELIVERY"];
                //MessageBox.Show(dt.Rows.Count.ToString());

                ParameterFields pluralParameter = new ParameterFields();
                ParameterField singleParameter;
                ParameterDiscreteValue parameterDiscreteValue;

                singleParameter = new ParameterField();
                singleParameter.Name = "strBulanTransaksi";
                parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = cboMonth.Text + " " + spnYear.Value.ToString();
                singleParameter.CurrentValues.Add(parameterDiscreteValue);
                pluralParameter.Add(singleParameter);

                reportViewer.ParameterFieldInfo = pluralParameter;
                rptDelivery.SetDataSource(ds);
                reportViewer.ReportSource = rptDelivery;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnViewBeritaAcara_Click(object sender, EventArgs e)
        {
            //if no customer selected, alert and return
            if (cboCustomer2.SelectedIndex < 0)
            {
                MessageBox.Show("Please select customer.");
                return;
            }

            //if no transaction selected, alert and return
            if (cboTransDate.SelectedIndex < 0)
            {
                MessageBox.Show("Please select transaction.");
                return;
            }

            //if no trans detail selected, alert and return
            if (cboTransDetail.SelectedIndex < 0)
            {
                MessageBox.Show("Please select transaction detail.");
                return;
            }

            //prepare parameter data
            //DateTime date = new DateTime(2011, 12, 24);
            string customerId = cboCustomer2.SelectedValue.ToString();
            string transDetailId = cboTransDetail.SelectedValue.ToString();

            //build query
            string query = "SELECT ct.id, ct.customer_id, ct.tgl_transaksi, " +
                           "       pd.vessel_name, ctd.voy, ctd.td, des.city_name, ctd.no_container, " +
                           "       ctd.no_seal, c.customer_name, r.recipient_name, tc.type_name, " +
                           "       ctd.jenis_barang, cnd.condition_name, " +
                           "       ctd.sj1, sj2, sj3, sj4, sj5, sj6, sj7, sj8, sj9, sj10, " +
                           "       ctd.sj11, sj12, sj13, sj14, sj15, sj16, sj17, sj18, sj19, sj20, " +
                           "       ctd.sj21, sj22, sj23, sj24, sj25 " +
                           "FROM   CUSTOMER_TRANS AS ct " +
                           "       INNER JOIN CUSTOMER AS c ON c.customer_id = ct.customer_id " +
                           "       LEFT OUTER JOIN CUSTOMER_TRANS_DETAIL AS ctd ON ctd.customer_trans_id = ct.id " +
                           "       LEFT OUTER JOIN PELAYARAN_DETAIL AS pd ON pd.pelayaran_detail_id = ctd.pelayaran_detail_id " +
                           "       LEFT OUTER JOIN CITY AS des ON des.city_id = ctd.destination " +
                           "       LEFT OUTER JOIN RECIPIENT AS r ON r.recipient_id = ctd.recipient_id " +
                           "       LEFT OUTER JOIN TYPE_CONT AS tc ON tc.type_id = ctd.type_id " +
                           "       LEFT OUTER JOIN CONDITION AS cnd ON cnd.condition_id = ctd.condition_id " +
                           "WHERE ((ctd.deleted is null OR ctd.deleted = '0') AND (ct.deleted is null OR ct.deleted = '0')) " + 
                           "AND ct.customer_id = '" + customerId + "' " +
                           "AND ctd.id = '" + transDetailId + "' ";

            SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();

                ParameterFields pluralParameter = new ParameterFields();
                ParameterField singleParameter;
                ParameterDiscreteValue parameterDiscreteValue;

                while (reader.Read())
                {
                    string kapal = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetValue(3).ToString();
                    string voy = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetValue(4).ToString();
                    //populate report params
                    singleParameter = new ParameterField();
                    singleParameter.Name = "strKapal";
                    parameterDiscreteValue = new ParameterDiscreteValue();
                    parameterDiscreteValue.Value = kapal + " / " + voy;
                    singleParameter.CurrentValues.Add(parameterDiscreteValue);
                    pluralParameter.Add(singleParameter);
                    
                    singleParameter = new ParameterField();
                    singleParameter.Name = "dtATD";
                    parameterDiscreteValue = new ParameterDiscreteValue();
                    parameterDiscreteValue.Value = reader.GetDateTime(5).Date;
                    singleParameter.CurrentValues.Add(parameterDiscreteValue);
                    pluralParameter.Add(singleParameter);

                    singleParameter = new ParameterField();
                    singleParameter.Name = "strTujuan";
                    parameterDiscreteValue = new ParameterDiscreteValue();
                    parameterDiscreteValue.Value = (Utility.Utility.IsDBNull(reader.GetValue(6))) ? null : reader.GetValue(6).ToString();
                    singleParameter.CurrentValues.Add(parameterDiscreteValue);
                    pluralParameter.Add(singleParameter);

                    singleParameter = new ParameterField();
                    singleParameter.Name = "strContainer";
                    parameterDiscreteValue = new ParameterDiscreteValue();
                    parameterDiscreteValue.Value = (Utility.Utility.IsDBNull(reader.GetValue(7))) ? null : reader.GetValue(7).ToString();
                    singleParameter.CurrentValues.Add(parameterDiscreteValue);
                    pluralParameter.Add(singleParameter);

                    singleParameter = new ParameterField();
                    singleParameter.Name = "strSegel";
                    parameterDiscreteValue = new ParameterDiscreteValue();
                    parameterDiscreteValue.Value = (Utility.Utility.IsDBNull(reader.GetValue(8))) ? null : reader.GetValue(8).ToString();
                    singleParameter.CurrentValues.Add(parameterDiscreteValue);
                    pluralParameter.Add(singleParameter);

                    singleParameter = new ParameterField();
                    singleParameter.Name = "strPengirim";
                    parameterDiscreteValue = new ParameterDiscreteValue();
                    parameterDiscreteValue.Value = (Utility.Utility.IsDBNull(reader.GetValue(9))) ? null : reader.GetValue(9).ToString();
                    singleParameter.CurrentValues.Add(parameterDiscreteValue);
                    pluralParameter.Add(singleParameter);

                    singleParameter = new ParameterField();
                    singleParameter.Name = "strPenerima";
                    parameterDiscreteValue = new ParameterDiscreteValue();
                    parameterDiscreteValue.Value = (Utility.Utility.IsDBNull(reader.GetValue(10))) ? null : reader.GetValue(10).ToString();
                    singleParameter.CurrentValues.Add(parameterDiscreteValue);
                    pluralParameter.Add(singleParameter);

                    singleParameter = new ParameterField();
                    singleParameter.Name = "strTypeCont";
                    parameterDiscreteValue = new ParameterDiscreteValue();
                    parameterDiscreteValue.Value = (Utility.Utility.IsDBNull(reader.GetValue(11))) ? null : reader.GetValue(11).ToString();
                    singleParameter.CurrentValues.Add(parameterDiscreteValue);
                    pluralParameter.Add(singleParameter);

                    singleParameter = new ParameterField();
                    singleParameter.Name = "strJenisBarang";
                    parameterDiscreteValue = new ParameterDiscreteValue();
                    parameterDiscreteValue.Value = (Utility.Utility.IsDBNull(reader.GetValue(12))) ? null : reader.GetValue(12).ToString();
                    singleParameter.CurrentValues.Add(parameterDiscreteValue);
                    pluralParameter.Add(singleParameter);

                    singleParameter = new ParameterField();
                    singleParameter.Name = "strKondisi";
                    parameterDiscreteValue = new ParameterDiscreteValue();
                    parameterDiscreteValue.Value = (Utility.Utility.IsDBNull(reader.GetValue(13))) ? null : reader.GetValue(13).ToString();
                    singleParameter.CurrentValues.Add(parameterDiscreteValue);
                    pluralParameter.Add(singleParameter);

                    for (int i = 1; i <= 25; i++)
                    {
                        string noDo = "strDO" + i.ToString();
                        singleParameter = new ParameterField();
                        singleParameter.Name = noDo;
                        parameterDiscreteValue = new ParameterDiscreteValue();
                        parameterDiscreteValue.Value = (Utility.Utility.IsDBNull(reader.GetValue(13 + i))) ? "0" : reader.GetValue(13 + i).ToString();
                        singleParameter.CurrentValues.Add(parameterDiscreteValue);
                        pluralParameter.Add(singleParameter);
                    }

                    break;
                }

                rptBeritaAcara = new RptBeritaAcaraDelivery();
                reportViewer.ParameterFieldInfo = pluralParameter;
                reportViewer.ReportSource = rptBeritaAcara;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void Customer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTransDate.SelectedIndexChanged -= TransDate_SelectedIndexChanged;
            //Alter the date list
            Guid customerID = (Guid) cboCustomer2.SelectedValue;
            List<CustomerTrans> transactionList = sqlCustomerTransRepository.ListCustomerTrans(customerID);
            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            cboTransDate.DataSource = transactionList;
            cboTransDate.DisplayMember = "TransDate";
            cboTransDate.ValueMember = "CustomerTransID";
            cboTransDate.SelectedIndex = -1;
            cboTransDate.Text = "-- Choose --";
            
            cboTransDate.SelectedIndexChanged += new EventHandler(TransDate_SelectedIndexChanged);
        }

        private void TransDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Alter the detail list
            Guid transID = (Guid)cboTransDate.SelectedValue;
            List<CustomerTransDetailSimplified> transDetail = sqlCustomerTransRepository.ListCustomerTransDetailSimplified(transID);
            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            cboTransDetail.DataSource = transDetail;
            cboTransDetail.DisplayMember = "StringRep";
            cboTransDetail.ValueMember = "CustomerDetailTransID";
            cboTransDetail.SelectedIndex = -1;
            cboTransDetail.Text = "-- Choose --";
        }
    }
}
