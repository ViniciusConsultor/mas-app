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

namespace VisitaJayaPerkasa.Control.Recipient
{
    public partial class RecipientList : UserControl
    {
        private SqlRecipientRepository sqlRecipientRepository;
        private List<VisitaJayaPerkasa.Entities.Recipient> recipient;
        private List<VisitaJayaPerkasa.Entities.Recipient> showRecipient;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        private BackgroundWorker backgroundWorker;

        public RecipientList()
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
            sqlRecipientRepository = new SqlRecipientRepository();
            recipient = null;

            string searchValue = radTextBoxElementSearchWord.Text;
            string searchKey = radComboBoxElement.Text;

            recipient = sqlRecipientRepository.GetRecipient();
            if (recipient != null)
            {
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchKey))
                {
                    switch (searchKey)
                    {
                        case "Name":
                            showRecipient = recipient.Where(c => c.Name.Contains(searchValue)).ToList<VisitaJayaPerkasa.Entities.Recipient>();
                            break;
                        case "Address" :
                            showRecipient = recipient.Where(c => c.Address.Contains(searchValue)).ToList<VisitaJayaPerkasa.Entities.Recipient>();
                            break;
                    }
                }
                else
                    showRecipient = recipient;
            }
            else
                showRecipient = null;

            if (showRecipient != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(showRecipient.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;
        }


        public void RefreshGrid()
        {
            if (totalPage != 0)
                showRecipient = showRecipient.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<VisitaJayaPerkasa.Entities.Recipient>();
            else
                showRecipient = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            RecipientGridView.DataSource = showRecipient;

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
            if (RecipientGridView.SelectedRows.Count == 1)
            {
                sqlRecipientRepository = new SqlRecipientRepository();
                DialogResult dResult = MessageBox.Show(this, "Are you sure want delete this data ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    GridViewRowInfo gridInfo = RecipientGridView.SelectedRows.First();
                    string id = gridInfo.Cells[0].Value.ToString();
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "recipient_id" }, new object[] { id });

                    if (sqlRecipientRepository.DeleteRecipient(sqlParam))
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

        private void RecipientGridView_DoubleClick(object sender, EventArgs e)
        {
            if (RecipientGridView.SelectedRows.Count == 1)
            {
                GridViewRowInfo gridInfo = RecipientGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.Recipient recipient = showRecipient.Where(c => c.ID.ToString().Equals(id)).FirstOrDefault();


                UserControl controllers = new RecipientView(recipient);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }

        private void radButtonElementCreate_Click(object sender, EventArgs e)
        {
            VisitaJayaPerkasa.Entities.Recipient recipient = null;
            UserControl controllers = new RecipientEdit(recipient);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
        }

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {
            if (RecipientGridView.SelectedRows.Count == 1)
            {
                GridViewRowInfo gridInfo = RecipientGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.Recipient recipient = showRecipient.Where(c => c.ID.ToString() == id).SingleOrDefault();

                UserControl controllers = new RecipientEdit(recipient);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }
    }
}
