using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using VisitaJayaPerkasa.SqlRepository;

namespace VisitaJayaPerkasa.Form.Report.Delivery
{
    public partial class RptDeliveryControl : UserControl
    {
        private SqlCustomerRepository sqlCustomerRepository;
        private RptDelivery rptDelivery;

        public RptDeliveryControl()
        {
            InitializeComponent();

            sqlCustomerRepository = new SqlCustomerRepository();

            DateTime datetime = DateTime.Now;
            cboMonth.SelectedIndex = datetime.Month-1;
            spnYear.Value = datetime.Year;

            cboMonth.SelectedIndex = 11;
            spnYear.Value = 2011;

            List<VisitaJayaPerkasa.Entities.Customer> listCustomer = sqlCustomerRepository.ListCustomers();

            cboCustomer.DataSource = listCustomer;
            cboCustomer.DisplayMember = "CustomerName";
            cboCustomer.ValueMember = "ID";
            cboCustomer.SelectedIndex = -1;
            cboCustomer.Text = "-- Choose --";

            cboCustomer.SelectedIndex = 0;
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

            //MessageBox.Show("start: " + startDate.ToShortDateString() + "\nend: " + endDate.ToShortDateString() + "\ncid: " + customerId);

            //build query
            string query = "SELECT ct.id, ct.customer_id, c.customer_name, ct.tgl_transaksi, " +
                           "       ctd.stuffing_date, w.address, des.city_name, ctd.no_container, " +
                           "       ctd.voy, ctd.td, pd.vessel_name, ctd.ta, " +
                           "       ctd.unloading, ctd.terima_toko, r.recipient_name " +
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
                MessageBox.Show(dt.Rows.Count.ToString());

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
                //con.Close();
            }
        }
    }
}
