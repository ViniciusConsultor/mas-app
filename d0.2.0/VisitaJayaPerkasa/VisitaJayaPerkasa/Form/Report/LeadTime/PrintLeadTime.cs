using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using VisitaJayaPerkasa.Entities;
using VisitaJayaPerkasa.SqlRepository;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Form.Report.LeadTime
{
    public partial class PrintLeadTime : Telerik.WinControls.UI.RadForm
    {
        Surat objSurat;
        SqlCustomerTransRepository sqlCustomerTransRepository;
        ReportDocument reportDocument;
        ParameterFields paramFields;

        ParameterField paramField;
        ParameterDiscreteValue paramDiscreteValue;
        CrtLeadTime crtLeadTime;

        public PrintLeadTime(Surat objSurat)
        {
            InitializeComponent();
            this.objSurat = objSurat;
            sqlCustomerTransRepository = new SqlCustomerTransRepository();
            reportDocument = new ReportDocument();
            paramFields = new ParameterFields();
            crtLeadTime = new CrtLeadTime();


            //Execute Query
            String strQry = "Select [CUSTOMER_TRANS_DETAIL].no_container as Column1, " + 
                "(Select Top 1 address From WareHouse Where stuffing_place_id = stuffing_place) as Column2, " + 
                "[CUSTOMER_TRANS_DETAIL].terima_toko as Column3 From [CUSTOMER_TRANS_DETAIL] " + 
                "Join [Customer_Trans] On " + 
                "[Customer_Trans].tgl_transaksi = cast('" + Utility.Utility.ConvertDateToString(objSurat.Tgl) + "' as date) And " + 
                "[Customer_Trans].Customer_id = '" + objSurat.CustomerID + "' And " + 
                "([Customer_Trans].deleted is not null Or [Customer_Trans].deleted = 0) " +
                "And [Customer_Trans].id = [Customer_Trans_Detail].customer_trans_id";

            int columnNo = 1;
            paramField = new ParameterField();
            paramField.Name = "col" + columnNo.ToString();
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Container";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);
            columnNo++;

            paramField = new ParameterField();
            paramField.Name = "col" + columnNo.ToString();
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Tgl penerimaan Container";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);
            columnNo++;

            paramField = new ParameterField();
            paramField.Name = "col" + columnNo.ToString();
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Gudang";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "noSurat";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = objSurat.NoSurat;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "parameterReceive";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = objSurat.CustomerName;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);
            crystalReportViewer1.ParameterFieldInfo = paramFields;


            SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);
            SqlDataAdapter da = new SqlDataAdapter(strQry, VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);
            ShippingMainDataSet ds = new ShippingMainDataSet();
            da.Fill(ds, "Surat_Lead_Time");

            crtLeadTime.SetDataSource(ds);
            crystalReportViewer1.ReportSource = crtLeadTime;
        }
    }
}
