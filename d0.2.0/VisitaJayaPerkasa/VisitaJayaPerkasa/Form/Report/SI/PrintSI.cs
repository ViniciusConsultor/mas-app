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
        public PrintSI()
        {
            InitializeComponent();
            reportDocument = new ReportDocument();
            paramFields = new ParameterFields();

            paramField = new ParameterField();
            paramField.Name = "col";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Container";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "col";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Tgl penerimaan Container";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "col";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Gudang";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "noSurat";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "parameterReceive";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);
            crystalReportViewer1.ParameterFieldInfo = paramFields;
        }

        private void PrintSI_Load(object sender, EventArgs e)
        {
        }
    }
}
