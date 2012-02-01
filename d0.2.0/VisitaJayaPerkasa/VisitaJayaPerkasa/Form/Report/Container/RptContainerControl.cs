﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using VisitaJayaPerkasa.SqlRepository;
using VisitaJayaPerkasa.Entities;

namespace VisitaJayaPerkasa.Form.Report.Container
{
    public partial class RptContainerControl : UserControl
    {
        RptContainer objReportContainer;
        List<PelayaranDetail> listPelayaran;

        public RptContainerControl()
        {
            InitializeComponent();
            pickerATD.Value = DateTime.Today;

            SqlPelayaranRepository pelayaranRepository = new SqlPelayaranRepository();
            listPelayaran = pelayaranRepository.GetVessels();
            cbVessel.DataSource = listPelayaran;
            cbVessel.DisplayMember = "VesselName";
            cbVessel.ValueMember = "PelayaranDetailID";

            cbVessel.SelectedIndex = -1;
            cbVessel.Text = Constant.VisitaJayaPerkasaApplication.cboDefaultText;
        }

        private void cbAll_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (cbAll.Checked)
            {
                cbPengirim.Checked = true;
                cbPenerima.Checked = true;
                cbNoContainer.Checked = true;
                cbSeal.Checked = true;
                cbColly.Checked = true;
                cbCont.Checked = true;
            }
            else
            {
                cbPengirim.Checked = false;
                cbPenerima.Checked = false;
                cbNoContainer.Checked = false;
                cbSeal.Checked = false;
                cbColly.Checked = false;
                cbCont.Checked = false;
            }
        }


        private string CreateSelectQueryAndParameters()
        {
            //ReportDocument reportDocument = new ReportDocument();
            ParameterFields pluralParameter = new ParameterFields();
            ParameterField singleParameter;
            ParameterDiscreteValue parameterDiscreteValue;
            string query = "SELECT ";
            int colNumber = 0;

            if (cbPengirim.Checked)
            {
                colNumber++;
                query = query.Insert(query.Length, "c.customer_name as Pengirim");

                singleParameter = new ParameterField();
                singleParameter.Name = "col" + colNumber;

                parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = "Pengirim";

                singleParameter.CurrentValues.Add(parameterDiscreteValue);
                pluralParameter.Add(singleParameter);
            }
            
            if (cbPenerima.Checked)
            {
                colNumber++;

                if (query.Length > "SELECT ".Length)
                    query = query.Insert(query.Length, ", ");
                query = query.Insert(query.Length, "r.recipient_name as Penerima");

                singleParameter = new ParameterField();
                singleParameter.Name = "col" + colNumber;

                parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = "Penerima";
                singleParameter.CurrentValues.Add(parameterDiscreteValue);
                pluralParameter.Add(singleParameter);
            }
            
            if (cbNoContainer.Checked)
            {
                colNumber++;

                if (query.Length > "SELECT ".Length)
                    query = query.Insert(query.Length, ", ");
                query = query.Insert(query.Length, "ctd.no_container as NoContainer");

                singleParameter = new ParameterField();
                singleParameter.Name = "col" + colNumber;

                parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = "No Container";
                singleParameter.CurrentValues.Add(parameterDiscreteValue);
                pluralParameter.Add(singleParameter);
            }
            
            if (cbSeal.Checked)
            {
                colNumber++;

                if (query.Length > "SELECT ".Length)
                    query = query.Insert(query.Length, ", ");
                query = query.Insert(query.Length, "ctd.no_seal as Seal");

                singleParameter = new ParameterField();
                singleParameter.Name = "col" + colNumber;

                parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = "No Seal";
                singleParameter.CurrentValues.Add(parameterDiscreteValue);
                pluralParameter.Add(singleParameter);
            }
            
            if (cbColly.Checked)
            {
                colNumber++;

                if (query.Length > "SELECT ".Length)
                    query = query.Insert(query.Length, ", ");
                query = query.Insert(query.Length, "ctd.colly as Colly");

                singleParameter = new ParameterField();
                singleParameter.Name = "col" + colNumber;

                parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = "Colly";
                singleParameter.CurrentValues.Add(parameterDiscreteValue);
                pluralParameter.Add(singleParameter);
            }
            
            if (cbCont.Checked)
            {
                colNumber++;

                if (query.Length > "SELECT ".Length)
                    query = query.Insert(query.Length, ", ");
                query = query.Insert(query.Length, "tc.type_name as Cont");

                singleParameter = new ParameterField();
                singleParameter.Name = "col" + colNumber;

                parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = "Cont";
                singleParameter.CurrentValues.Add(parameterDiscreteValue);
                pluralParameter.Add(singleParameter);
            }


            for (int i = colNumber; i < 6; i++)
            {
                colNumber++;
                singleParameter = new ParameterField();
                singleParameter.Name = "col" + colNumber;

                parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = "";
                singleParameter.CurrentValues.Add(parameterDiscreteValue);
                pluralParameter.Add(singleParameter);
            }

            crystalReportViewerContainer.ParameterFieldInfo = pluralParameter;

            query += " FROM [Customer_Trans_Detail] ctd " +
                        "INNER JOIN [Customer_Trans] ct ON ct.id = ctd.customer_trans_id " +
                        "INNER JOIN [Type_Cont] tc ON tc.type_id = ctd.type_id " +
                        "INNER JOIN [RECIPIENT] r ON r.recipient_id = ctd.recipient_id " +
                        "INNER JOIN [CUSTOMER] c ON ct.customer_id = c.customer_id " +
                        "Where (ctd.deleted is null OR ctd.deleted = '0') " +
                        "AND ctd.pelayaran_detail_id = '" + cbVessel.SelectedValue.ToString() + "' " + 
                        "AND ctd.voy = '" + etVoy.Text.Trim() + "' " +
                        "AND convert(varchar(10), ctd.etd, 101) = '" + Utility.Utility.ConvertDateToString(pickerATD.Value) + "' ";

            return query;
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            if (cbVessel.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose vessel", "Information");
            else if(etVoy.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill voy", "Information");
            else if (pickerATD.Value.Date > DateTime.Now.Date)
                MessageBox.Show(this, "ATD must be lower from today", "Information");
            else
            {
                string query = CreateSelectQueryAndParameters();

                if (query.Length == "SELECT ".Length)
                {
                    MessageBox.Show("No selection to display!");
                    return;
                }

                objReportContainer = new RptContainer();
                try
                {
                    SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataSetContainer dsContainer = new DataSetContainer();

                    da.Fill(dsContainer, "DataTableContainer");
                    objReportContainer.SetDataSource(dsContainer);
                    crystalReportViewerContainer.ReportSource = objReportContainer;
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void cbVessel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

    }
}
