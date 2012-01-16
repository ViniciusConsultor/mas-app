using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using VisitaJayaPerkasa.SqlRepository;

namespace VisitaJayaPerkasa.Form.Report
{
    public partial class RptTransForm : Telerik.WinControls.UI.RadForm
    {
        private string customerName;
        private Guid ID;
        private SqlCustomerTransRepository sqlCustomerTransRepository;

        public RptTransForm(string customerName, Guid ID)
        {
            InitializeComponent();
            this.customerName = customerName.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText) ? "" : customerName;
            this.ID = ID;
        }

        private void RptTransForm_Load(object sender, EventArgs e)
        {
            // Set path report.
            string strPath = Application.StartupPath + @"\Form\Report\RptTrans.rpt";
            // Object for load report.
            ReportDocument rpt = new ReportDocument();
            rpt.Load(strPath);

             //Set Parameter
            DataTable dt = null;
            sqlCustomerTransRepository = new SqlCustomerTransRepository();
            dt = sqlCustomerTransRepository.ReportCustomerTransDetail(ID);

            ParameterFields pfields = new ParameterFields();
            ParameterField pfield = new ParameterField();
            ParameterDiscreteValue disValue = new ParameterDiscreteValue();
            pfield = new ParameterField();
            pfield.Name = "customerName";
            disValue = new ParameterDiscreteValue();
            disValue.Value = customerName;
            pfield.CurrentValues.Add(disValue);
            pfields.Add(pfield);
            rpt.SetDataSource(dt);

            // Set source of report.
            crystalReportViewer1.ParameterFieldInfo = pfields;
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}
