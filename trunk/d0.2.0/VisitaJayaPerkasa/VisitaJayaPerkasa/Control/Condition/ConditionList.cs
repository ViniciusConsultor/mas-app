using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Telerik.WinControls.UI;

using VisitaJayaPerkasa.SqlRepository;
using VisitaJayaPerkasa.Entities;

namespace VisitaJayaPerkasa.Control.ConditionControl
{
    public partial class ConditionList : UserControl
    {
        private SqlConditionRepository sqlConditionRepository;
        private List<Condition> conditions;
        private List<Condition> ShowConditions;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        private BackgroundWorker backgroundWorker;

        public ConditionList()
        {
            InitializeComponent();
            pageSize = 15;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);

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

        private void LoadData()
        {
            Constant.VisitaJayaPerkasaApplication.pBarForm = new Form.PBarDialog();
            backgroundWorker.RunWorkerAsync();
            Constant.VisitaJayaPerkasaApplication.pBarForm.ShowDialog();
        }

        public void LoadDataInBackground()
        {
            sqlConditionRepository = new SqlConditionRepository();
            conditions = null;

            string searchValue = radTextBoxElementSearchWord.Text.ToLower();
            string searchKey = radComboBoxElement.Text;

            conditions = sqlConditionRepository.GetConditions();
            if (conditions != null)
            {
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchKey))
                {
                    switch (searchKey)
                    {
                        case "Code":
                            ShowConditions = conditions.Where(c => c.ConditionCode.ToLower().Contains(searchValue)).ToList<Condition>();
                            break;
                        case "Name":
                            ShowConditions = conditions.Where(c => c.ConditionName.ToLower().Contains(searchValue)).ToList<Condition>();
                            break;
                    }

                }
                else
                    ShowConditions = conditions;
            }
            else
                ShowConditions = null;

            if (ShowConditions != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ShowConditions.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;
        }

        public void RefreshGrid()
        {
            if (totalPage != 0)
                ShowConditions = ShowConditions.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<Condition>();
            else
                ShowConditions = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            ConditionGridView.DataSource = ShowConditions;

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

        private void radButtonElementRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void radButtonElementBtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void radButtonElementCreate_Click(object sender, EventArgs e)
        {
            Condition condition = null;
            UserControl controllers = new ConditionEdit(condition);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
        }

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {
            if (ShowConditions != null)
            {
                GridViewRowInfo gridInfo = ConditionGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                Condition condition = conditions.Where(c => c.ID.ToString().Equals(id)).FirstOrDefault();

                UserControl controllers = new ConditionEdit(condition);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }

        private void radButtonElementRemove_Click(object sender, EventArgs e)
        {
            if (ShowConditions != null)
            {
                sqlConditionRepository = new SqlConditionRepository();
                DialogResult dResult = MessageBox.Show(this, "Are you sure want delete this data ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    GridViewRowInfo gridInfo = ConditionGridView.SelectedRows.First();
                    string id = gridInfo.Cells[0].Value.ToString();
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "condition_id" }, new object[] { id });

                    if (sqlConditionRepository.DeleteCondition(sqlParam))
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

        private void ConditionGridView_DoubleClick(object sender, EventArgs e)
        {
            if (ShowConditions != null)
            {
                GridViewRowInfo gridInfo = ConditionGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                Condition condition = conditions.Where(c => c.ID.ToString().Equals(id)).FirstOrDefault();

                UserControl controllers = new ConditionView(condition);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }

    }
}
