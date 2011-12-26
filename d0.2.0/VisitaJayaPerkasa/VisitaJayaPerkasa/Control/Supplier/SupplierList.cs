using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Entities;
using Telerik.WinControls.UI;

namespace VisitaJayaPerkasa.Control.Supplier
{
    public partial class SupplierList : UserControl
    {
        private SqlSupplierRepository sqlSupplierRepository;
        private List<VisitaJayaPerkasa.Entities.Supplier> Supliers;
        private List<VisitaJayaPerkasa.Entities.Supplier> Showsuppliers;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        private BackgroundWorker backgroundWorker;

        public SupplierList()
        {
            InitializeComponent();
            pageSize = 15;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);

            LoadData();
        }

        private void radButtonElementBtnSearch_Click(object sender, EventArgs e)
        {
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
            sqlSupplierRepository = new SqlSupplierRepository();
            Supliers = null;

            string searchValue = radTextBoxElementSearchWord.Text;
            string searchKey = radComboBoxElement.Text;

            Supliers = sqlSupplierRepository.ListSuppliers();
            if (Supliers != null)
            {
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchKey))
                {
                    SqlParameter[] sqlParam;

                    switch (searchKey)
                    {
                        case "Supplier Name":
                            sqlParam = SqlUtility.SetSqlParameter(new string[] { "first_name" }, new object[] { searchValue });
                            Supliers = Supliers.Where(c => c.SupplierName.Contains(searchValue)).ToList<Entities.Supplier>();
                            break;
                    }

                    sqlParam = null;
                }
            }

            if (Supliers != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Supliers.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;
        }


        public void RefreshGrid()
        {
            if (totalPage != 0)
                Showsuppliers = Supliers.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<Entities.Supplier>();
            else
                Showsuppliers = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            SupplierGridView.DataSource = Showsuppliers;

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

        private void radButtonElementNext_Click(object sender, EventArgs e)
        {
            if (totalPage > currentPage)
            {
                currentPage++;
                RefreshGrid();
            }
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

        private void radButtonElementRemove_Click(object sender, EventArgs e)
        {
            if (Showsuppliers != null)
            {
                sqlSupplierRepository = new SqlSupplierRepository();
                DialogResult dResult = MessageBox.Show(this, "Are you sure want delete master and detail this supplier ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    GridViewRowInfo gridInfo = SupplierGridView.SelectedRows.First();
                    string id = gridInfo.Cells[0].Value.ToString();
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "supplier_id" }, new object[] { id });

                    if (sqlSupplierRepository.DeleteSupplier(sqlParam))
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
            VisitaJayaPerkasa.Entities.Supplier supplier = null;
            UserControl controllers = new SupplierEdit(supplier);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
        }

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {
            if (Showsuppliers != null)
            {
                GridViewRowInfo gridInfo = SupplierGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.Supplier supplier = Showsuppliers.Where(c => c.Id.ToString() == id).SingleOrDefault();

                UserControl controllers = new SupplierEdit(supplier);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }

        private void SupplierGridView_DoubleClick(object sender, EventArgs e)
        {
            if (Showsuppliers != null)
            {
                GridViewRowInfo gridInfo = SupplierGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.Supplier supplier = Showsuppliers.Where(c => c.Id.ToString().Equals(id)).FirstOrDefault();


                UserControl controllers = new SupplierView(supplier);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }
    }
}
