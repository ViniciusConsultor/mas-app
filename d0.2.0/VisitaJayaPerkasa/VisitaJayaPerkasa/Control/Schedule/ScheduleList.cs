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

        public ScheduleList()
        {
            InitializeComponent();
            actionBarDate.Text = Utility.Utility.ConvertDateToString(DateTime.Now);

            pageSize = 15;
            LoadData();
        }

        private void radCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            radCalendar1.Visible = false;
            actionBarDate.Text = Utility.Utility.ConvertDateToString(radCalendar1.SelectedDate);
        }

        private void radImageButtonElement1_Click(object sender, EventArgs e)
        {
            radCalendar1.Visible = true;
        }

        private void radImageButtonElement2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData() {
            sqlScheduleRepository = new SqlScheduleRepository();
            schedules = null;

            schedules = sqlScheduleRepository.ListSchedule(actionBarDate.Text);
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

            RefreshGrid();
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

        private void radCalendar1_SelectionChanged_1(object sender, EventArgs e)
        {
            actionBarDate.Text = Utility.Utility.ConvertDateToString(radCalendar1.SelectedDate);
            radCalendar1.Visible = false;
        }

    }
}
