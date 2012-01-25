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
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Form.Report
{
    public partial class RptTransForm : Telerik.WinControls.UI.RadForm
    {
        private string customerName;
        private Guid ID;
        //private SqlCustomerTransRepository sqlCustomerTransRepository;
        RptCustomerTrans objRpt;

        public RptTransForm(string customerName, Guid ID)
        {
            InitializeComponent();
            this.customerName = customerName.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText) ? "" : customerName;
            this.ID = ID;
        }

        private void RptTransForm_Load(object sender, EventArgs e)
        {
            //// Set path report.
            //string strPath = Application.StartupPath + @"\Form\Report\RptTrans.rpt";
            //// Object for load report.
            //ReportDocument rpt = new ReportDocument();
            //rpt.Load(strPath);

            // //Set Parameter
            //DataTable dt = null;
            //sqlCustomerTransRepository = new SqlCustomerTransRepository();
            //dt = sqlCustomerTransRepository.ReportCustomerTransDetail(ID);

            //ParameterFields pfields = new ParameterFields();
            //ParameterField pfield = new ParameterField();
            //ParameterDiscreteValue disValue = new ParameterDiscreteValue();
            //pfield = new ParameterField();
            //pfield.Name = "customerName";
            //disValue = new ParameterDiscreteValue();
            //disValue.Value = customerName;
            //pfield.CurrentValues.Add(disValue);
            //pfields.Add(pfield);
            //rpt.SetDataSource(dt);

            //// Set source of report.
            //crystalReportViewer1.ParameterFieldInfo = pfields;
            //crystalReportViewer1.ReportSource = rpt;
            //crystalReportViewer1.Refresh();
        }

        private void chkAll_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkAll.Checked)
            {
                chkTgl.Checked = true;
                chkPengirim.Checked = true;
                chkRecipientName.Checked = true;
                chkTypeName.Checked = true;
                chkDestination.Checked = true;
                chkJenisBarang.Checked = true;
                chkCondition.Checked = true;
                chkStuffingPlace.Checked = true;
                chkTruckNumber.Checked = true;
                chkNoContainer.Checked = true;
                chkNoSeal.Checked = true;
                chkQuantity.Checked = true;
                chkSJ1.Checked = true;
                chkSJ2.Checked = true;
                chkSJ3.Checked = true;
                chkSJ4.Checked = true;
                chkSJ5.Checked = true;
                chkSJ6.Checked = true;
                chkSJ7.Checked = true;
                chkSJ8.Checked = true;
                chkSJ9.Checked = true;
                chkSJ10.Checked = true;
                chkSJ11.Checked = true;
                chkSJ12.Checked = true;
                chkSJ13.Checked = true;
                chkSJ14.Checked = true;
                chkSJ15.Checked = true;
                chkSJ16.Checked = true;
                chkSJ17.Checked = true;
                chkSJ18.Checked = true;
                chkSJ19.Checked = true;
                chkSJ20.Checked = true;
                chkSJ21.Checked = true;
                chkSJ22.Checked = true;
                chkSJ23.Checked = true;
                chkSJ24.Checked = true;
                chkSJ25.Checked = true;
                chkVesselName.Checked = true;
                chkVoy.Checked = true;
                chkEtd.Checked = true;
                chkTD.Checked = true;
                chkEta.Checked = true;
                chkTA.Checked = true;
                chkUnloading.Checked = true;
                chkTerimaToko.Checked = true;
                chkKeterangan.Checked = true;
                chkNoBa.Checked = true;
            }
            else
            {
                chkTgl.Checked = false;
                chkPengirim.Checked = false;
                chkRecipientName.Checked = false;
                chkTypeName.Checked = false;
                chkDestination.Checked = false;
                chkJenisBarang.Checked = false;
                chkCondition.Checked = false;
                chkStuffingPlace.Checked = false;
                chkTruckNumber.Checked = false;
                chkNoContainer.Checked = false;
                chkNoSeal.Checked = false;
                chkQuantity.Checked = false;
                chkSJ1.Checked = false;
                chkSJ2.Checked = false;
                chkSJ3.Checked = false;
                chkSJ4.Checked = false;
                chkSJ5.Checked = false;
                chkSJ6.Checked = false;
                chkSJ7.Checked = false;
                chkSJ8.Checked = false;
                chkSJ9.Checked = false;
                chkSJ10.Checked = false;
                chkSJ11.Checked = false;
                chkSJ12.Checked = false;
                chkSJ13.Checked = false;
                chkSJ14.Checked = false;
                chkSJ15.Checked = false;
                chkSJ16.Checked = false;
                chkSJ17.Checked = false;
                chkSJ18.Checked = false;
                chkSJ19.Checked = false;
                chkSJ20.Checked = false;
                chkSJ21.Checked = false;
                chkSJ22.Checked = false;
                chkSJ23.Checked = false;
                chkSJ24.Checked = false;
                chkSJ25.Checked = false;
                chkVesselName.Checked = false;
                chkVoy.Checked = false;
                chkEtd.Checked = false;
                chkTD.Checked = false;
                chkEta.Checked = false;
                chkTA.Checked = false;
                chkUnloading.Checked = false;
                chkTerimaToko.Checked = false;
                chkKeterangan.Checked = false;
                chkNoBa.Checked = false;
            }
        }

        private string CreateSelectQueryAndParameters()
        {
            ReportDocument reportDocument;
            ParameterFields paramFields;

            ParameterField paramField;
            ParameterDiscreteValue paramDiscreteValue;

            reportDocument = new ReportDocument();
            paramFields = new ParameterFields();

            string query = "SELECT ";
            int columnNo = 0;

            if (chkTgl.Checked)
            {
                columnNo++;
                query = query.Insert(query.Length, "REPLACE(CONVERT(VARCHAR(11), ct.tgl_transaksi, 106), ' ', '-') as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Tanggal";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkPengirim.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "c.customer_name as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Customer Name";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkRecipientName.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "r.recipient_name as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Recipient Name";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkTypeName.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "tc.type_name as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Type";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkDestination.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "cd.city_name as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Destination";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkJenisBarang.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.jenis_barang as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Jenis Barang";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkCondition.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "cnd.condition_name as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Condition";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkStuffingPlace.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "w.address as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Stuffing Place";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkTruckNumber.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.truck_number as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Truck No";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkNoContainer.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.no_container as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "No Container";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkNoSeal.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.no_seal as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "No Seal";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ1.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj1 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 1";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ2.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj2 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 2";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ3.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj3 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 3";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ4.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj4 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 4";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ5.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj5 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 5";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ6.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj6 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 6";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ7.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj7 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 7";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ8.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj8 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 8";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ9.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj9 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 9";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ10.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj10 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 10";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ11.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj11 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 11";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ12.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj12 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 12";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ13.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj13 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 13";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ14.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj14 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 14";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ15.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj15 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 15";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ16.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj16 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 16";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ17.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj17 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 17";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ18.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj18 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 18";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ19.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj19 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 19";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ20.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj20 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 20";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ21.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj21 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 21";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ22.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj22 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 22";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ23.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj23 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 23";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ24.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj24 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 24";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkSJ25.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.sj25 as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "SJ 25";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkVesselName.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "pd.vessel_name as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Vessel";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkVoy.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.voy as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Voy";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkEtd.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "REPLACE(CONVERT(VARCHAR(11), ctd.etd, 106), ' ', '-') as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "ETD";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkTD.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "REPLACE(CONVERT(VARCHAR(11), ctd.td, 106), ' ', '-') as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "TD";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkEta.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "REPLACE(CONVERT(VARCHAR(11), ctd.eta, 106), ' ', '-') as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "ETA";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkTA.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "REPLACE(CONVERT(VARCHAR(11), ctd.ta, 106), ' ', '-') as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "TA";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkUnloading.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "REPLACE(CONVERT(VARCHAR(11), ctd.unloading, 106), ' ', '-') as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Unloading";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkTerimaToko.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "REPLACE(CONVERT(VARCHAR(11), ctd.terima_toko, 106), ' ', '-') as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Terima Toko";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkKeterangan.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.keterangan as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "Keterangan";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }
            if (chkNoBa.Checked)
            {
                columnNo++;
                if (query.Contains("Column"))
                {
                    query = query.Insert(query.Length, ", ");
                }
                query = query.Insert(query.Length, "ctd.no_ba as Column" + columnNo.ToString());


                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "No BA";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }

            for (int i = columnNo; i < 47; i++)
            {
                columnNo++;
                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }

            crystalReportViewer1.ParameterFieldInfo = paramFields;

            query += " FROM [Customer_Trans_Detail] ctd " +
                        "INNER JOIN [Type_Cont] tc ON tc.type_id = ctd.type_id " +
                        "INNER JOIN [Pelayaran_Detail] pd ON pd.pelayaran_detail_id = ctd.pelayaran_detail_id " +
                        "INNER JOIN [Pelayaran] p ON p.pelayaran_id = pd.pelayaran_id " +
                        "INNER JOIN [City] co ON co.city_id = ctd.origin " +
                        "INNER JOIN [City] cd ON cd.city_id = ctd.destination " +
                        "INNER JOIN [Condition] cnd ON cnd.condition_id = ctd.condition_id " +
                        "INNER JOIN [RECIPIENT] r ON r.recipient_id = ctd.recipient_id " +
                        "INNER JOIN [CUSTOMER_TRANS] ct ON ct.id = ctd.customer_trans_id " +
                        "INNER JOIN [CUSTOMER] c ON c.customer_id = ct.customer_id " +
                        "INNER JOIN [Warehouse] w ON w.stuffing_place_id = ctd.stuffing_place " +
                        "Where (ctd.deleted is null OR ctd.deleted = '0') " +
                        "AND ctd.customer_trans_id = '" + ID + "'";
            return query;
        }

        private void btnShowReport_Click(object sender, EventArgs e)
        {
            objRpt = new RptCustomerTrans();

            string query = CreateSelectQueryAndParameters();

            if (!query.Contains("Column"))
            {
                MessageBox.Show("No selection to display!");
                return;
            }

            try
            {
                SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);

                SqlDataAdapter da = new SqlDataAdapter(query, VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);
                ShippingMainDataSet ds = new ShippingMainDataSet();
                da.Fill(ds, "CUSTOMER_TRANS");

                objRpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = objRpt;
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
}
