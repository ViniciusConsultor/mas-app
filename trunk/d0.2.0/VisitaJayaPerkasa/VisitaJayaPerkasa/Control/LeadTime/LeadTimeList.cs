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

namespace VisitaJayaPerkasa.Control.LeadTime
{
    public partial class LeadTimeList : UserControl
    {
        private List<VisitaJayaPerkasa.Entities.LeadTime> leadTimes;
        private List<VisitaJayaPerkasa.Entities.LeadTime> showLeadTimes;
        private SqlLeadTimeRepository sqlLeadTimeRepository;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        public LeadTimeList()
        {
            InitializeComponent();
            pageSize = 15;
            LoadData();
        }

        public void LoadData()
        {
            sqlLeadTimeRepository = new SqlLeadTimeRepository();
            leadTimes = null;

            string searchValue = radTextBoxElementSearchWord.Text;
            string searchKey = radComboBoxElement.Text;

            leadTimes = sqlLeadTimeRepository.GetLeadTime();
            if (leadTimes != null)
            {
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchKey))
                {
                    switch (searchKey)
                    {
                        case "City Name":
                            showLeadTimes = leadTimes.Where(c => c.CityName.Contains(searchValue)).ToList<VisitaJayaPerkasa.Entities.LeadTime>();
                            break;
                    }
                }
                else
                    showLeadTimes = leadTimes;
            }
            else
                showLeadTimes = null;

            if (showLeadTimes != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(showLeadTimes.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;

            RefreshGrid();
        }

        public void RefreshGrid()
        {
            if (totalPage != 0)
                showLeadTimes = showLeadTimes.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<VisitaJayaPerkasa.Entities.LeadTime>();
            else
                showLeadTimes = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            LeadTimeGridView.DataSource = showLeadTimes;
        }

        private void radButtonElementPrev_Click(object sender, EventArgs e)
        {
            if (currentPage != 1)
            {
                currentPage--;
                RefreshGrid();
            }
        }

        private void radComboBoxElement_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void radButtonElementRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void PelayaranGridView_DoubleClick(object sender, EventArgs e)
        {
            if (showLeadTimes != null)
            {
                GridViewRowInfo gridInfo = LeadTimeGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.LeadTime leadTime = leadTimes.Where(c => c.ID.ToString().Equals(id)).FirstOrDefault();

                UserControl controllers = new LeadTimeView(leadTime);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }

        private void radButtonElementRemove_Click(object sender, EventArgs e)
        {
            if (showLeadTimes != null)
            {
                sqlLeadTimeRepository = new SqlLeadTimeRepository();
                DialogResult dResult = MessageBox.Show(this, "Are you sure want delete this data ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    GridViewRowInfo gridInfo = LeadTimeGridView.SelectedRows.First();
                    string id = gridInfo.Cells[0].Value.ToString();
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "lead_time_id" }, new object[] { id });

                    if (sqlLeadTimeRepository.DeleteLeadTime(sqlParam))
                    {
                        MessageBox.Show("Data Deleted !");
                        LoadData();
                    }
                    else
                        MessageBox.Show("Cannot Delete Data !");

                    sqlParam = null;
                }
            }
        }

        private void radButtonElementCreate_Click(object sender, EventArgs e)
        {
            VisitaJayaPerkasa.Entities.LeadTime leadTime = null;
            UserControl controllers = new LeadTimeEdit(leadTime);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
        }

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {
            if (showLeadTimes != null)
            {
                GridViewRowInfo gridInfo = LeadTimeGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.LeadTime leadTime = showLeadTimes.Where(c => c.ID.ToString() == id).SingleOrDefault();

                UserControl controllers = new LeadTimeEdit(leadTime);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }
    }
}
