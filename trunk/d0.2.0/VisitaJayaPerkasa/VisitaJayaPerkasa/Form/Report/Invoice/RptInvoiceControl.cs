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

            //Set Parameter
            DataTable dt = null;
            sqlCustomerTransRepository = new SqlCustomerTransRepository();
            dt = sqlCustomerTransRepository.ReportInvoice(ID);
            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            decimal total = decimal.Zero;
            string totalStr = String.Empty;
            decimal PPN = decimal.Zero;
            string ppnStr = String.Empty;
            decimal subTotal = decimal.Zero;
            string subTotalStr = String.Empty;
            if (!chkStatus.Checked)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[7].ToString() != "")
                        total += Convert.ToDecimal(row[7].ToString());
                }
                totalStr = total.ToString();
                subTotal = total - (total * (10 / 100));
                subTotalStr = subTotal.ToString();
                PPN = (total * (10 / 100));
                ppnStr = total.ToString();
            }

            ParameterFields pfields = new ParameterFields();
            ParameterField pfield = new ParameterField();
            ParameterDiscreteValue disValue = new ParameterDiscreteValue();
            pfield = new ParameterField();
            pfield.Name = "userName";
            disValue = new ParameterDiscreteValue();
            disValue.Value = userName;
            pfield.CurrentValues.Add(disValue);
            pfields.Add(pfield);

            pfield = new ParameterField();
            pfield.Name = "customerName";
            disValue = new ParameterDiscreteValue();
            disValue.Value = customerName;
            pfield.CurrentValues.Add(disValue);
            pfields.Add(pfield);

            pfield = new ParameterField();
            pfield.Name = "invoiceNo";
            disValue = new ParameterDiscreteValue();
            disValue.Value = invoiceNo;
            pfield.CurrentValues.Add(disValue);
            pfields.Add(pfield);

            if (!chkStatus.Checked)
            {
                pfield = new ParameterField();
                pfield.Name = "subTotal";
                disValue = new ParameterDiscreteValue();
                disValue.Value = subTotalStr;
                pfield.CurrentValues.Add(disValue);
                pfields.Add(pfield);

                pfield = new ParameterField();
                pfield.Name = "ppn";
                disValue = new ParameterDiscreteValue();
                disValue.Value = ppnStr;
                pfield.CurrentValues.Add(disValue);
                pfields.Add(pfield);

                pfield = new ParameterField();
                pfield.Name = "total";
                disValue = new ParameterDiscreteValue();
                disValue.Value = totalStr;
                pfield.CurrentValues.Add(disValue);
                pfields.Add(pfield);
            }

            rpt.SetDataSource(dt);

            // Set source of report.
            crystalReportViewerContainer.ParameterFieldInfo = pfields;
            crystalReportViewerContainer.ReportSource = rpt;
            crystalReportViewerContainer.Refresh();
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