using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using VisitaJayaPerkasa.Entities;
using VisitaJayaPerkasa.SqlRepository;
using Telerik.WinControls.UI;

namespace VisitaJayaPerkasa.Form.Report.LeadTime
{
    public partial class rptLeadTimeControl : UserControl
    {
        List<Surat> listSurat = null;
        SqlSuratRepository sqlSuratRepository;
        BackgroundWorker backgroundWorker;

        public rptLeadTimeControl()
        {
            InitializeComponent();

            rtsiHal.Visibility = ElementVisibility.Collapsed;
            rtsiCustomer.Visibility = ElementVisibility.Collapsed;
            radCalendarBegin.Visible = false;
            radCalendarEnd.Visible = false;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);


            lblDateBegin.Text = Utility.Utility.ConvertDateToString(DateTime.Now);
            lblDateEnd.Text = Utility.Utility.ConvertDateToString(DateTime.Now);
            LoadData();
        }


        private void LoadDataInBackground() 
        {
            Search("first");
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
            {
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

        }


        public void LoadData() 
        {
            Constant.VisitaJayaPerkasaApplication.pBarForm = new Form.PBarDialog();
            backgroundWorker.RunWorkerAsync();
            Constant.VisitaJayaPerkasaApplication.pBarForm.ShowDialog();
        }

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearch.Text.ToLower().Equals("all")) 
            {
                rtsiHal.Visibility = ElementVisibility.Collapsed;
                rtsiCustomer.Visibility = ElementVisibility.Collapsed;
            }
            else if (cbSearch.Text.ToLower().Equals("hal"))
            {
                rtsiHal.Visibility = ElementVisibility.Visible;
                rtsiCustomer.Visibility = ElementVisibility.Collapsed;


                sqlSuratRepository = new SqlSuratRepository();
                listSurat = sqlSuratRepository.ListSurat(EnumSurat.LeadTime);
                cbHal.DataSource = listSurat;
                cbHal.DisplayMember = "NoSurat";

                sqlSuratRepository = null;
            }
            else if (cbSearch.Text.ToLower().Equals("customer"))
            {
                rtsiHal.Visibility = ElementVisibility.Collapsed;
                rtsiCustomer.Visibility = ElementVisibility.Visible;

                SqlCustomerRepository sqlCustomerRepository = new SqlCustomerRepository();
                List<Customer> listCustomer = sqlCustomerRepository.listCustomerForPriceList();

                cboCustomer.DataSource = listCustomer;
                cboCustomer.DisplayMember = "CustomerName";
                cboCustomer.ValueMember = "ID";

                sqlCustomerRepository = null;
                listCustomer = null;
            }

        }


        public void Search(string criteria) {
            sqlSuratRepository = new SqlSuratRepository();

            if (criteria.ToLower().Equals("hal")) 
            {
                if (cbHal.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                {
                    MessageBox.Show(this, "Please choose hal", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sqlSuratRepository = null;
                    return;
                }
                else
                    listSurat = sqlSuratRepository.ListSuratByCriteria(EnumSurat.LeadTime, lblDateBegin.Text, lblDateEnd.Text, cbHal.Text, null);
            }
            else if (criteria.ToLower().Equals("customer"))
            {
                if (cboCustomer.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                {
                    MessageBox.Show(this, "Please choose customer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sqlSuratRepository = null;
                    return;
                }
                else
                    listSurat = sqlSuratRepository.ListSuratByCriteria(EnumSurat.LeadTime, lblDateBegin.Text, lblDateEnd.Text, null, cboCustomer.SelectedValue.ToString());
            }
            else if (criteria.ToLower().Equals("all"))
                listSurat = sqlSuratRepository.ListSuratByCriteria(EnumSurat.LeadTime, lblDateBegin.Text, lblDateEnd.Text, null, null);
            else
                listSurat = sqlSuratRepository.ListSurat(EnumSurat.LeadTime);


            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                radGridView1.DataSource = listSurat;

            sqlSuratRepository = null;
        }


        private void radImageButtonSearch_Click(object sender, EventArgs e)
        {
            if (cbSearch.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText)) 
                MessageBox.Show(this, "Please choose search by", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (Utility.Utility.ConvertStringToDate(lblDateBegin.Text) > Utility.Utility.ConvertStringToDate(lblDateEnd.Text))
                MessageBox.Show(this, "Date begin greather than date end", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (rtsiHal.Visibility == ElementVisibility.Visible)
                {
                    if (cbHal.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                        MessageBox.Show(this, "Please choose hal", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        Search("hal");
                }
                else if (rtsiCustomer.Visibility == ElementVisibility.Visible)
                {
                    if (cboCustomer.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                        MessageBox.Show(this, "Please choose Customer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        Search("customer");
                }
                else
                    Search("all");
            }
        }

        private void radCalendarBegin_SelectionChanged(object sender, EventArgs e)
        {
            lblDateBegin.Text = Utility.Utility.ConvertDateToString(radCalendarBegin.SelectedDate);
            radCalendarBegin.Visible = false;
        }

        private void radCalendarEnd_SelectionChanged(object sender, EventArgs e)
        {
            lblDateEnd.Text = Utility.Utility.ConvertDateToString(radCalendarEnd.SelectedDate);
            radCalendarEnd.Visible = false;
        }

        private void imageButtonDateEnd_Click(object sender, EventArgs e)
        {
            radCalendarEnd.Visible = true;
        }

        private void imageButtonDateStart_Click(object sender, EventArgs e)
        {
            radCalendarBegin.Visible = true;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (radGridView1.SelectedRows.Count == 1)
            {
                GridViewRowInfo gridInfo = radGridView1.SelectedRows.First();
                string id = gridInfo.Cells[1].Value.ToString();
                Surat tempSurat = listSurat.Where(c => c.NoSurat == id).SingleOrDefault();

                new PrintLeadTime(tempSurat).ShowDialog();
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            new newLeadTime(null, this).ShowDialog();
        }

        private void radCalendarBegin_SelectionChanged_1(object sender, EventArgs e)
        {
            lblDateBegin.Text = Utility.Utility.ConvertDateToString(radCalendarBegin.SelectedDate);
            radCalendarBegin.Visible = false;
        }

        private void radCalendarEnd_SelectionChanged_1(object sender, EventArgs e)
        {
            lblDateEnd.Text = Utility.Utility.ConvertDateToString(radCalendarEnd.SelectedDate);
            radCalendarEnd.Visible = false;
        }

        private void cbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cbHal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (radGridView1.SelectedRows.Count == 1)
            {
                GridViewRowInfo gridInfo = radGridView1.SelectedRows.First();
                string id = gridInfo.Cells[1].Value.ToString();
                Surat tempSurat = listSurat.Where(c => c.NoSurat == id).SingleOrDefault();

                new newLeadTime(tempSurat, this).ShowDialog();
            }
        }


    }
}
