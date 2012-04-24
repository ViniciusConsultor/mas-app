using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using VisitaJayaPerkasa.SqlRepository;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Form.Report.Invoice
{
    public partial class RptInvoiceControl : UserControl
    {
        private VisitaJayaPerkasa.Entities.CustomerTrans searchResultCustomerTrans;
        private string userName;
        private string customerName;
        private Guid ID;
        private string invoiceNo;
        private SqlCustomerTransRepository sqlCustomerTransRepository;
        private SqlInvoiceRepository sqlInvoiceRepository;

        public RptInvoiceControl(string userName)
        {
            this.userName = userName;
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            new SearchCustomerTrans().ShowDialog();
            if (Constant.VisitaJayaPerkasaApplication.objGetOtherView != null)
            {
                searchResultCustomerTrans = (VisitaJayaPerkasa.Entities.CustomerTrans)Constant.VisitaJayaPerkasaApplication.objGetOtherView;
                txtCustomer.Text = searchResultCustomerTrans.CustomerName;
                this.customerName = searchResultCustomerTrans.CustomerName;
            }

            Constant.VisitaJayaPerkasaApplication.objGetOtherView = null;
        }

        private void RptInvoiceControl_Load(object sender, EventArgs e)
        {
            Random random = new Random();
            this.invoiceNo = random.NextString(10);
            etNo.Text = this.invoiceNo;
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            // Set path report.
            string strPath = String.Empty;
            if (chkStatus.Checked)
                strPath = Application.StartupPath + @"\Form\Report\Invoice\RptInvoice.rpt";
            else
                strPath = Application.StartupPath + @"\Form\Report\Invoice\RptInvoicePPN.rpt";
            // Object for load report.
            ReportDocument rpt = new ReportDocument();
            rpt.Load(strPath);

            string query = "SELECT ROW_NUMBER() over (ORDER BY ct.tgl_transaksi) AS no_urut, " +
                           "       ctd.no_ba, ctd.no_container, tcnt.type_name, pd.vessel_name, ctd.ta, " +
                           "       ctd.terima_toko, des.city_name, ctd.price, ctd.unloading AS tgl_angkat, " +
                           "       ctd.no_seal AS seal, ctd.voy " +
                           "FROM   CUSTOMER_TRANS ct " +
                           "       INNER JOIN CUSTOMER c ON c.customer_id = ct.customer_id " +
                           "       LEFT OUTER JOIN CUSTOMER_TRANS_DETAIL ctd ON ctd.customer_trans_id = ct.id " +
                           "       LEFT OUTER JOIN TYPE_CONT tcnt ON tcnt.type_id = ctd.type_id " +
                           "       LEFT OUTER JOIN PELAYARAN_DETAIL AS pd ON pd.pelayaran_detail_id = ctd.pelayaran_detail_id " +
                           "       LEFT OUTER JOIN CITY AS des ON des.city_id = ctd.destination " +
                           "WHERE  ct.id = '" + searchResultCustomerTrans.CustomerTransID.ToString() + "'" +
                           "";

            SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);
            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(query, VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);

                ShippingMainDataSet ds = new ShippingMainDataSet();
                da.Fill(ds, "INVOICE_NONPPN");

                ParameterFields pluralParameter = new ParameterFields();
                ParameterField singleParameter;
                ParameterDiscreteValue parameterDiscreteValue;

                singleParameter = new ParameterField();
                singleParameter.Name = "userName";
                parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = userName;
                singleParameter.CurrentValues.Add(parameterDiscreteValue);
                pluralParameter.Add(singleParameter);

                singleParameter = new ParameterField();
                singleParameter.Name = "customerName";
                parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = customerName;
                singleParameter.CurrentValues.Add(parameterDiscreteValue);
                pluralParameter.Add(singleParameter);

                singleParameter = new ParameterField();
                singleParameter.Name = "invoiceNo";
                parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = invoiceNo;
                singleParameter.CurrentValues.Add(parameterDiscreteValue);
                pluralParameter.Add(singleParameter);

                DataTable dt = ds.Tables["INVOICE_NONPPN"];
                //MessageBox.Show(dt.Rows.Count.ToString());
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row[7].ToString() != "")
                        total += Convert.ToDecimal(row[7].ToString());
                    //total += 100000;//dr.
                }

                singleParameter = new ParameterField();
                singleParameter.Name = "totalTagihan";
                parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = total;
                singleParameter.CurrentValues.Add(parameterDiscreteValue);
                pluralParameter.Add(singleParameter);

                if (!chkStatus.Checked)
                {
                    singleParameter = new ParameterField();
                    singleParameter.Name = "ppn";
                    parameterDiscreteValue = new ParameterDiscreteValue();
                    parameterDiscreteValue.Value = total * (decimal)0.1;
                    singleParameter.CurrentValues.Add(parameterDiscreteValue);
                    pluralParameter.Add(singleParameter);

                    singleParameter = new ParameterField();
                    singleParameter.Name = "grandTotal";
                    parameterDiscreteValue = new ParameterDiscreteValue();
                    parameterDiscreteValue.Value = total * (decimal)1.1;
                    singleParameter.CurrentValues.Add(parameterDiscreteValue);
                    pluralParameter.Add(singleParameter);
                }

                crystalReportViewerContainer.ParameterFieldInfo = pluralParameter;
                rpt.SetDataSource(ds);
                crystalReportViewerContainer.ReportSource = rpt;
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

        private void radButton2_Click(object sender, EventArgs e)
        {
            sqlInvoiceRepository = new SqlInvoiceRepository();
            this.ID = searchResultCustomerTrans.CustomerTransID;
            this.customerName = searchResultCustomerTrans.CustomerName;
            SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "invoice_no", "id", "deleted" }
                        , new object[] { invoiceNo, searchResultCustomerTrans.CustomerTransID, false });

            if (sqlInvoiceRepository.CreateInvoice(sqlParam))
            {
                MessageBox.Show(this, "Success insert Invoice data", "Information");
            }
            else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                MessageBox.Show(this, "Cannot insert Invoice data", "Information");
            }
            sqlInvoiceRepository = null;
            sqlParam = null;
        }
    }

    public static class RandomString
    {
        private static Random random = new Random();

        public static string NextString(this Random r, int size)
        {
            var data = new byte[size];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)r.Next(0, 128);
            }
            var encoding = new ASCIIEncoding();
            return encoding.GetString(data);
        }
    }
}