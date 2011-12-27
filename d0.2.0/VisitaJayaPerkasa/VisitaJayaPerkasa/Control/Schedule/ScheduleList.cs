using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;
using Telerik.WinControls.UI;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Control.Schedule
{
    public partial class ScheduleList : UserControl
    {
        private List<VisitaJayaPerkasa.Entities.Schedule> schedules;
        private List<VisitaJayaPerkasa.Entities.Schedule> showShedule;
        private SqlScheduleRepository sqlScheduleRepository;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        private BackgroundWorker backgroundWorker;

        public ScheduleList()
        {
            InitializeComponent();
            actionBarDateBegin.Text = Utility.Utility.ConvertDateToString(DateTime.Now);
            actionBarDateEnd.Text = Utility.Utility.ConvertDateToString(DateTime.Now);

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);

            cboValueSearch.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            txtRoSearch.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;

            pageSize = 15;
            LoadData();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            this.LoadDataInBackground();

            if (bw.CancellationPending)
                e.Cancel = true;
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show("Operation was cancelled");
            else if (e.Error != null)
            {
                string msg = String.Format("An error occurred: {0}", e.Error.Message);
                MessageBox.Show(msg);
            }
            else
                this.RefreshGrid();
        }


        private void radCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            radCalendarBegin.Visible = false;
            actionBarDateBegin.Text = Utility.Utility.ConvertDateToString(radCalendarBegin.SelectedDate);
        }

        private void radImageButtonElement2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData() {
            Constant.VisitaJayaPerkasaApplication.pBarForm = new Form.PBarDialog();
            backgroundWorker.RunWorkerAsync();
            Constant.VisitaJayaPerkasaApplication.pBarForm.ShowDialog();
        }

        private void LoadDataInBackground()
        {
            sqlScheduleRepository = new SqlScheduleRepository();
            schedules = null;

                    
            if(cboKeySearch.Text.Equals("Destination"))
                schedules = sqlScheduleRepository.ListSchedule(actionBarDateBegin.Text,
                    actionBarDateEnd.Text, cboValueSearch.SelectedValue.ToString(), "", ""
                    );           
            else if(cboKeySearch.Text.Equals("Vessel"))
                schedules = sqlScheduleRepository.ListSchedule(actionBarDateBegin.Text,
                    actionBarDateEnd.Text, "", cboValueSearch.SelectedValue.ToString(), ""
                    );
            else if(cboKeySearch.Text.Equals("VOY"))
                schedules = sqlScheduleRepository.ListSchedule(actionBarDateBegin.Text,
                    actionBarDateEnd.Text, "", "", txtRoSearch.Text.Trim()
                    );
            else
                schedules = sqlScheduleRepository.ListSchedule(actionBarDateBegin.Text,
                    actionBarDateEnd.Text, "", "", ""
                    );   


            if (schedules != null)
            {
                showShedule = schedules;
            }
            else
                showShedule = null;

            if (showShedule != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(showShedule.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;
        }

        private void radButtonElementCreate_Click(object sender, EventArgs e)
        {
            VisitaJayaPerkasa.Entities.Schedule tempSchedule = null;
            UserControl controllers = new ScheduleEdit(tempSchedule);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
        
        }

        private void radButtonElementPrev_Click(object sender, EventArgs e)
        {
            if (currentPage != 1)
            {
                currentPage--;
                RefreshGrid();
            }
        }

        private void radButtonElementNext_Click(object sender, EventArgs e)
        {
            if (totalPage > currentPage)
            {
                currentPage++;
                RefreshGrid();
            }
        }

        public void RefreshGrid()
        {
            if (totalPage != 0)
                showShedule = showShedule.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<VisitaJayaPerkasa.Entities.Schedule>();
            else
                showShedule = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            ScheduleGridView.DataSource = showShedule;

            Constant.VisitaJayaPerkasaApplication.pBarForm.Invoke
            (
                (MethodInvoker)delegate()
                {
                    Constant.VisitaJayaPerkasaApplication.pBarForm.Close();
                    Constant.VisitaJayaPerkasaApplication.pBarForm.Dispose();
                    Constant.VisitaJayaPerkasaApplication.pBarForm = null;
                }
            );
        }

        private void radButtonElementRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void radButtonElementRemove_Click(object sender, EventArgs e)
        {
            if (showShedule != null)
            {
                sqlScheduleRepository = new SqlScheduleRepository();
                DialogResult dResult = MessageBox.Show(this, "Are you sure want delete this data ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    GridViewRowInfo gridInfo = ScheduleGridView.SelectedRows.First();
                    string id = gridInfo.Cells[0].Value.ToString();
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "schedule_id" }, new object[] { id });

                    if (sqlScheduleRepository.DeleteSchedule(sqlParam))
                    {
                        MessageBox.Show("Data Deleted !");
                        LoadData();
                    }
                    else
                        MessageBox.Show("Cannot Delete Data !");

                    sqlParam = null;
                    gridInfo = null;
                }
            }
        }

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {
            if (showShedule != null)
            {
                GridViewRowInfo gridInfo = ScheduleGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.Schedule tempSchedule = showShedule.Where(c => c.ID.ToString() == id).SingleOrDefault();

                UserControl controllers = new ScheduleEdit(tempSchedule);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }

        private void radImageButtonElement4_Click(object sender, EventArgs e)
        {
            if (Utility.Utility.ConvertStringToDate(actionBarDateBegin.Text) > Utility.Utility.ConvertStringToDate(actionBarDateEnd.Text))
                MessageBox.Show(this, "Date begin greather than date end", "Information");
            else
                LoadData();
        }

        private void radCalendarEnd_SelectionChanged(object sender, EventArgs e)
        {
            actionBarDateEnd.Text = Utility.Utility.ConvertDateToString(radCalendarEnd.SelectedDate);
            radCalendarEnd.Visible = false;
        }

        private void radCalendarBegin_SelectionChanged(object sender, EventArgs e)
        {
            actionBarDateBegin.Text = Utility.Utility.ConvertDateToString(radCalendarBegin.SelectedDate);
            radCalendarBegin.Visible = false;
        }

        private void btnCalendarEnd_Click(object sender, EventArgs e)
        {
            radCalendarEnd.Visible = true;
        }

        private void btnCalendarBegin_Click(object sender, EventArgs e)
        {
            radCalendarBegin.Visible = true;
        }

        private void cboKeySearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboValueSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboKeySearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKeySearch.Text.Equals("Destination"))
            {
                SqlCityRepository sqlCityRepository = new SqlCityRepository();
                List<VisitaJayaPerkasa.Entities.City> listCity = sqlCityRepository.GetCity();
                cboValueSearch.DataSource = null;

                cboValueSearch.DataSource = listCity;
                cboValueSearch.DisplayMember = "CityName";
                cboValueSearch.ValueMember = "ID";

                txtRoSearch.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                cboValueSearch.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                listCity = null;
                sqlCityRepository = null;
            }
            else if (cboKeySearch.Text.Equals("Vessel"))
            {
                SqlVesselRepository sqlVesselRepository = new SqlVesselRepository();
                List<VisitaJayaPerkasa.Entities.Vessel> listVessel = sqlVesselRepository.GetVessels();
                cboValueSearch.DataSource = null;

                cboValueSearch.DataSource = listVessel;
                cboValueSearch.DisplayMember = "VesselName";
                cboValueSearch.ValueMember = "ID";

                txtRoSearch.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                cboValueSearch.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                sqlVesselRepository = null;
                listVessel = null;
            }
            else if (cboKeySearch.Text.Equals("VOY"))
            {
                cboValueSearch.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                txtRoSearch.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            }
            else if (cboKeySearch.Text.Equals("All"))
            {
                cboValueSearch.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                txtRoSearch.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            }
        }

    }
}
