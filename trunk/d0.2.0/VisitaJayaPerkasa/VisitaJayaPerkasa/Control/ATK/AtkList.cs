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

namespace VisitaJayaPerkasa.Control.ATK
{
    public partial class AtkList : UserControl
    {
        private SqlPriceListRepository sqlPriceRepository;
        private int currentPage;
        private int pageSize;
        private int totalPage;

        private BackgroundWorker backgroundWorker;
        private List<Entities.PriceList> listPrice;
        private List<Entities.PriceList> showListPrice;

        public AtkList()
        {
            InitializeComponent();
            pageSize = 15;
            
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);

            LoadData();
        }


        public void LoadDataInBackground()
        {
            sqlPriceRepository = new SqlPriceListRepository();
            listPrice = sqlPriceRepository.GetPriceListByCriteria(Utility.Utility.ConvertStringToDate("01/01/1990"), Utility.Utility.ConvertStringToDate("01/01/5555"),
                    "", "", "", "", "", 1, "");

            string searchValue = radTextBoxElementSearchWord.Text.ToLower().Trim();
            if (listPrice != null)
            {
                if (!string.IsNullOrEmpty(searchValue))
                    showListPrice = listPrice.Where(c => c.Item.ToLower().Contains(searchValue)).ToList<VisitaJayaPerkasa.Entities.PriceList>();
                else
                    showListPrice = listPrice;
            }
            else
                showListPrice = null;

            if (showListPrice != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(showListPrice.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;

            sqlPriceRepository = null;
        }

        public void RefreshGrid()
        {
            if (totalPage != 0)
                showListPrice = showListPrice.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<VisitaJayaPerkasa.Entities.PriceList>();
            else
                showListPrice = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            AtkGridView.DataSource = showListPrice;

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


        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            LoadDataInBackground();

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

        private void radButtonElementBtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void radButtonElementRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
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

        private void radButtonElementCreate_Click(object sender, EventArgs e)
        {
            VisitaJayaPerkasa.Entities.PriceList price = null;
            UserControl controllers = new Control.ATK.AtkEdit(price);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
        }

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {
            if (showListPrice != null)
            {
                GridViewRowInfo gridInfo = AtkGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.PriceList priceList = listPrice.Where(c => c.ID.ToString().Equals(id)).FirstOrDefault();

                UserControl controllers = new Control.ATK.AtkEdit(priceList);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }

        private void radButtonElementRemove_Click(object sender, EventArgs e)
        {
            if (AtkGridView.SelectedRows.Count == 1)
            {
                sqlPriceRepository = new SqlPriceListRepository();
                DialogResult dResult = MessageBox.Show(this, "Are you sure want delete this data ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    GridViewRowInfo gridInfo = AtkGridView.SelectedRows.First();
                    string id = gridInfo.Cells[0].Value.ToString();

                    if (sqlPriceRepository.DeletePrice(Utility.Utility.ConvertToUUID(id)))
                    {
                        MessageBox.Show("Data Deleted !");
                        LoadData();
                    }
                    else
                        MessageBox.Show("Cannot Delete Data !");
                }

                sqlPriceRepository = null;
            }
        }


    }
}
