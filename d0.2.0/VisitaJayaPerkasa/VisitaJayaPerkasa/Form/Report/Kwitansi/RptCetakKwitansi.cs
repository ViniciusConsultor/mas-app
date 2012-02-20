using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace VisitaJayaPerkasa.Form.Report.Kwitansi
{
    public partial class RptCetakKwitansi : Telerik.WinControls.UI.RadForm
    {
        private string harga;
        private string ket;
        private string spell;

        public RptCetakKwitansi(string harga, string ket, string spell)
        {
            this.harga = harga;
            this.ket = ket;
            this.spell = spell;
            InitializeComponent();
        }

        private void RptCetakKwitansi_Load(object sender, EventArgs e)
        {
            string strPath =// @"C:\Program Files\Hewlett-Packard\Setup_Showroom\Showroom\rptKwitansi.rpt";

                 @"C:\Users\Jamaluddin Ahmad\Documents\Development\myProject\showRoom\prjShowroom\prjShowroom\Report\rptKwitansi.rpt";

            // Object for load report.
            ReportDocument rpt = new ReportDocument();
            rpt.Load(strPath);

            //Set Parameter
            ParameterFields pfields = new ParameterFields();
            ParameterField pfield = new ParameterField();
            ParameterDiscreteValue disValue = new ParameterDiscreteValue();
            pfield.Name = "spell";
            disValue.Value = spell;
            pfield.CurrentValues.Add(disValue);
            pfields.Add(pfield);

            pfield = new ParameterField();
            pfield.Name = "price";
            disValue = new ParameterDiscreteValue();
            disValue.Value = harga;
            pfield.CurrentValues.Add(disValue);
            pfields.Add(pfield);

            pfield = new ParameterField();
            pfield.Name = "keterangan";
            disValue = new ParameterDiscreteValue();
            disValue.Value = ket;
            pfield.CurrentValues.Add(disValue);
            pfields.Add(pfield);
            crystalReportViewer1.ParameterFieldInfo = pfields;

            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}
