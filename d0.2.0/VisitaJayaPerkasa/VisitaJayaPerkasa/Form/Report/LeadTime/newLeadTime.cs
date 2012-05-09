using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using VisitaJayaPerkasa.Entities;
using VisitaJayaPerkasa.SqlRepository;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Form.Report.LeadTime
{
    public partial class newLeadTime : Telerik.WinControls.UI.RadForm
    {
        Surat surat;
        SqlSuratRepository sqlSuratRepository;
        SqlCustomerRepository sqlCustomerRepository;
        

        public newLeadTime()
        {
            InitializeComponent();

            surat = new Surat();
            sqlSuratRepository = new SqlSuratRepository();
            sqlCustomerRepository = new SqlCustomerRepository();

            Surat tempSurat = sqlSuratRepository.GetlastNoSurat(EnumSurat.LeadTime);
            String strTempNoSurat = (tempSurat == null) ? "0" : tempSurat.NoSurat.Substring(0, 4);
            etNoSurat.Text = surat.GenerateNoSurat(Int32.Parse(strTempNoSurat), EnumSurat.LeadTime);


            List<Customer> listCustomer = sqlCustomerRepository.listCustomerForPriceList();
            cboCustomer.DataSource = listCustomer;
            cboCustomer.DisplayMember = "CustomerName";
            cboCustomer.ValueMember = "ID";

            listCustomer = null;
            sqlCustomerRepository = null;
            tempSurat = null;
            strTempNoSurat = null;
            sqlSuratRepository = null;
        }

        private void cboCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            if (cboCustomer.Text.Equals(String.Empty))
                MessageBox.Show(this, "No customer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else 
            {
                sqlSuratRepository = new SqlSuratRepository();
                SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "No_Surat", "Tgl", "Customer_Id", "Supplier_Id" }, 
                    new object[] { etNoSurat.Text.Trim(), radDateTimePicker1.Value.Date, cboCustomer.SelectedValue, DBNull.Value});

                if (sqlSuratRepository.CreateSurat(sqlParam))
                {
                    MessageBox.Show(this, "Success Create Surat", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel.PerformClick();
                }
                else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                {
                    MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }
}
