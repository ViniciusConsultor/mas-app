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
using System.Threading;

namespace VisitaJayaPerkasa.Control.CategoryControl
{
    public partial class CategoryList : UserControl
    {
        private SqlCategoryRepository sqlCategoryRepository;
        private List<Category> categories;
        private List<Category> ShowCategories;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        private BackgroundWorker backgroundWorker;

        public CategoryList()
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

        private void LoadData() {
            Constant.VisitaJayaPerkasaApplication.pBarForm = new Form.PBarDialog();
            backgroundWorker.RunWorkerAsync();
            Constant.VisitaJayaPerkasaApplication.pBarForm.ShowDialog();
        }

        public void LoadDataInBackground()
        {
            sqlCategoryRepository = new SqlCategoryRepository();
            categories = null;

            string searchValue = radTextBoxElementSearchWord.Text.ToLower();
            string searchKey = radComboBoxElement.Text;

            categories = sqlCategoryRepository.GetCategories();
            if (categories != null)
            {
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchKey))
                {
                    switch (searchKey)
                    {
                        case "Code":
                            ShowCategories = categories.Where(c => c.CategoryCode.ToLower().Contains(searchValue)).ToList<Category>();
                            break;
                        case "Name":
                            ShowCategories = categories.Where(c => c.CategoryName.ToLower().Contains(searchValue)).ToList<Category>();
                            break;
                    }

                }
                else
                    ShowCategories = categories;
            }
            else
                ShowCategories = null;

            if (ShowCategories != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ShowCategories.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;
        }

        public void RefreshGrid()
        {
            if (totalPage != 0)
                ShowCategories = ShowCategories.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<Category>();
            else
                ShowCategories = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            CategoryGridView.DataSource = ShowCategories;

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
            Category category = null;
            UserControl controllers = new CategoryEdit(category);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
        }

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {
            if (ShowCategories != null)
            {
                GridViewRowInfo gridInfo = CategoryGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                Category category = categories.Where(c => c.ID.ToString().Equals(id)).FirstOrDefault();

                UserControl controllers = new CategoryEdit(category);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }

        }

        private void radButtonElementRemove_Click(object sender, EventArgs e)
        {
            if (ShowCategories != null)
            {
                sqlCategoryRepository = new SqlCategoryRepository();
                DialogResult dResult = MessageBox.Show(this, "Are you sure want delete this data ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    GridViewRowInfo gridInfo = CategoryGridView.SelectedRows.First();
                    string id = gridInfo.Cells[0].Value.ToString();
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "category_id" }, new object[] { id });

                    if (sqlCategoryRepository.DeleteCategory(sqlParam))
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

        private void CategoryGridView_DoubleClick(object sender, EventArgs e)
        {
            if (ShowCategories != null)
            {
                GridViewRowInfo gridInfo = CategoryGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                Category category = categories.Where(c => c.ID.ToString().Equals(id)).FirstOrDefault();

                UserControl controllers = new CategoryView(category);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }
        
    }
}
