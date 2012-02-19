using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;

namespace VisitaJayaPerkasa.Control.Transaction
{
    public partial class CustomerTransView : UserControl
    {
        private SqlCustomerTransRepository sqlCustomerTransRepository;
        private List<VisitaJayaPerkasa.Entities.CustomerTransDetail> listCustomerTransDetail;

        public CustomerTransView(VisitaJayaPerkasa.Entities.CustomerTrans customerTrans)
        {
            InitializeComponent();
            sqlCustomerTransRepository = new SqlCustomerTransRepository();
            listCustomerTransDetail = sqlCustomerTransRepository.ListCustomerTransDetail(customerTrans.CustomerTransID);

            if (listCustomerTransDetail != null)
                CustomerTransDetailGridView.DataSource = listCustomerTransDetail;


            lblCustomerName.Text = customerTrans.CustomerName;
            lblDate.Text = Utility.Utility.GetDateOnly(Utility.Utility.ChangeDateMMDD(customerTrans.TransDate.ToString()));
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new CustomerTransList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);

        }
    }
}
