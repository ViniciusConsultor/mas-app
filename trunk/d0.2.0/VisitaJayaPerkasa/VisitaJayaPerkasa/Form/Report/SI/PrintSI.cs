using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace VisitaJayaPerkasa.Form.Report.SI
{
    public partial class PrintSI : Telerik.WinControls.UI.RadForm
    {
        ReportDocument reportDocument;
        ParameterFields paramFields;

        ParameterField paramField;
        ParameterDiscreteValue paramDiscreteValue;

        public PrintSI(string Supplier, string city, string kapal, string date)
        {
            InitializeComponent();
            reportDocument = new ReportDocument();
            paramFields = new ParameterFields();

            paramField = new ParameterField();
            paramField.Name = "supplier";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = Supplier;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "kapal";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = kapal;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "tujuan";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = city;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "ATD";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = date;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);
            crystalReportViewer1.ParameterFieldInfo = paramFields;
        }

        private void PrintSI_Load(object sender, EventArgs e)
        {
        }
    }
}
