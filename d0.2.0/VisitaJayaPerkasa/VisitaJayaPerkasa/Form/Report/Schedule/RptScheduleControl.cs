using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.Utility.Log;
using System.Data.SqlClient;
using CrystalDecisions.Shared;

namespace VisitaJayaPerkasa.Form.Report.Schedule
{
    public partial class RptScheduleControl : UserControl
    {
        rptSchedule rptSchedule;

        public RptScheduleControl()
        {
            InitializeComponent();
            
            rbBeginDate.Text = Utility.Utility.ConvertDateToString(DateTime.Now.Date);
            rbEndDate.Text = Utility.Utility.ConvertDateToString(DateTime.Now.Date);
        }

        private void rbBeginDate_Click(object sender, EventArgs e)
        {
            radCalendarBegin.Visible = true;
        }

        private void radCalendarEnd_SelectionChanged(object sender, EventArgs e)
        {
            rbEndDate.Text = Utility.Utility.ConvertDateToString(radCalendarEnd.SelectedDate);
            radCalendarEnd.Visible = false;
        }

        private void radCalendarBegin_SelectionChanged(object sender, EventArgs e)
        {
            rbBeginDate.Text = Utility.Utility.ConvertDateToString(radCalendarBegin.SelectedDate);
            radCalendarBegin.Visible = false;
        }

        private void rbEndDate_Click(object sender, EventArgs e)
        {
            radCalendarEnd.Visible = true;
        }


        private void SetParameter() {
            ParameterFields paramFields = new ParameterFields();
            ParameterField paramField;
            ParameterDiscreteValue paramDiscreteValue;

            paramField = new ParameterField();
            paramField.Name = "col1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Tujuan";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);


            paramField = null;
            paramDiscreteValue = null;
            paramField = new ParameterField();
            paramField.Name = "col2";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Pelayaran";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);


            paramField = null;
            paramDiscreteValue = null;
            paramField = new ParameterField();
            paramField.Name = "col3";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Vessel";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);


            paramField = null;
            paramDiscreteValue = null;
            paramField = new ParameterField();
            paramField.Name = "col4";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Voy";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);


            paramField = null;
            paramDiscreteValue = null;
            paramField = new ParameterField();
            paramField.Name = "col5";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Tgl closing";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);


            paramField = null;
            paramDiscreteValue = null;
            paramField = new ParameterField();
            paramField.Name = "col6";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "ETD";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);


            paramField = null;
            paramDiscreteValue = null;
            paramField = new ParameterField();
            paramField.Name = "col7";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Keterangan";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);


            paramField = null;
            paramDiscreteValue = null;
            paramField = new ParameterField();
            paramField.Name = "col8";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "DO";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);


            paramField = null;
            paramDiscreteValue = null;
            paramField = new ParameterField();
            paramField.Name = "BeginDate";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = rbBeginDate.Text;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);

            crystalReportViewer1.ParameterFieldInfo = paramFields;
        }

        private void radButtonElementBtnSearch_Click(object sender, EventArgs e)
        {
            if (Utility.Utility.ConvertStringToDate(Utility.Utility.ChangeDateMMDD(rbBeginDate.Text)) > Utility.Utility.ConvertStringToDate(Utility.Utility.ChangeDateMMDD(rbEndDate.Text)))
            {
                MessageBox.Show(this, "End date must be greather than begin date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else {
                List<ReportSchedule> listReportSchedules = GetReportSchedule(rbBeginDate.Text, rbEndDate.Text);
                if (listReportSchedules != null) {
                    ShippingMainDataSet ds = new ShippingMainDataSet();
                    rptSchedule = new rptSchedule();
                    SetParameter();


                    while(ds.Schedules.Rows.Count < listReportSchedules.Count)
                        ds.Schedules.Rows.Add();

                    for (int i = 0; i < listReportSchedules.Count; i++) 
                    {
                        if (i > 0) {
                            ReportSchedule obj0 = listReportSchedules.ElementAt(i - 1);
                            ReportSchedule obj1 = listReportSchedules.ElementAt(i);

                            if (obj0.tujuan.Equals(obj1.tujuan))
                                listReportSchedules.ElementAt(i).tujuan = "";
                            if (obj0.pelayaran.Equals(obj1.pelayaran) && listReportSchedules.ElementAt(i).tujuan.Equals(""))
                                listReportSchedules.ElementAt(i).pelayaran = "";

                            obj0 = null;
                            obj1 = null;
                        }    
                    }

                    for (int i = 0; i < listReportSchedules.Count; i++)
                    {
                        ReportSchedule obj = listReportSchedules.ElementAt(i);
                        ds.Schedules.Rows[i][0] = obj.tujuan;
                        ds.Schedules.Rows[i][1] = obj.pelayaran;
                        ds.Schedules.Rows[i][2] = obj.vessel;
                        ds.Schedules.Rows[i][3] = obj.voy;
                        ds.Schedules.Rows[i][4] = obj.tglClosing;
                        ds.Schedules.Rows[i][5] = obj.etd;
                        ds.Schedules.Rows[i][6] = "";
                        ds.Schedules.Rows[i][7] = "";

                        obj = null;
                    }

                    rptSchedule.SetDataSource(ds); 
                    crystalReportViewer1.ReportSource = rptSchedule;

                    listReportSchedules = null;
                }
                else
                    MessageBox.Show(this, "No data found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public List<ReportSchedule> GetReportSchedule(string beginDate, string endDate)
        {
            List<ReportSchedule> listReportSchedule = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "select 'Jakarta - ' + c.city_name as tujuan," + 
                        "( " + 
	                    "    select TOP 1 supplier_name FROM pelayaran_detail pd JOIN pelayaran p " + 
	                    "    ON 	p.pelayaran_id = pd.pelayaran_id " + 
	                    "    AND pd.pelayaran_detail_id = s.pelayaran_detail_id " + 
	                    "    JOIN supplier s " + 
	                    "    ON s.supplier_id = p.supplier_id " + 
                        ") as pelayaran, " + 
                        "(select TOP 1 vessel_name FROM pelayaran_detail pd WHERE pd.pelayaran_detail_id = s.pelayaran_detail_id) as vessel, " + 
                        "s.voy, s.tgl_closing, s.etd " + 
                        "FROM schedule s, city c " + 
                        "WHERE ta is null AND s.tujuan = c.city_id AND " + 
                        "(cast(s.etd as date) >= cast('" + beginDate + "' as date) " +  
                        "AND cast(s.etd as date) <= cast('" + endDate + "' as date)) " + 
                        "order by tujuan, pelayaran, vessel"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ReportSchedule rptSchedule = new ReportSchedule();
                            rptSchedule.tujuan = reader.GetString(0);
                            rptSchedule.pelayaran = reader.GetString(1);
                            rptSchedule.vessel = reader.GetString(2);
                            rptSchedule.voy = reader.GetString(3);
                            rptSchedule.tglClosing = Utility.Utility.GetDateOnly(reader.GetDateTime(4).ToString());
                            rptSchedule.etd = Utility.Utility.GetDateOnly(reader.GetDateTime(5).ToString());

                            if (listReportSchedule == null)
                                listReportSchedule = new List<ReportSchedule>();

                            listReportSchedule.Add(rptSchedule);
                            rptSchedule = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("RptScheduleControl.cs - GetReportSchedule() " + e.Message);
            }

            return listReportSchedule;
        }
    }


    public class ReportSchedule {
        public string tujuan { get; set; }
        public string pelayaran { get; set; }
        public string vessel { get; set; }
        public string voy { get; set; }
        public string tglClosing { get; set; }
        public string etd { get; set; }
    }
}








/*

select 'Jakarta - ' + c.city_name as tujuan,
(
	select TOP 1 supplier_name FROM pelayaran_detail pd JOIN pelayaran p 
	ON 	p.pelayaran_id = pd.pelayaran_id
	AND pd.pelayaran_detail_id = s.pelayaran_detail_id 
	JOIN supplier s
	ON s.supplier_id = p.supplier_id	
) as pelayaran,
(select TOP 1 vessel_name FROM pelayaran_detail pd WHERE pd.pelayaran_detail_id = s.pelayaran_detail_id) as vessel,
s.voy, s.tgl_closing, s.etd
FROM schedule s, city c
WHERE ta is null AND s.tujuan = c.city_id
*/