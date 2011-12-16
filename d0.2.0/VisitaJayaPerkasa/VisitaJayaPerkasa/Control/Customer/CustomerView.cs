using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;

namespace VisitaJayaPerkasa.Control.Customer
{
    public partial class CustomerView : UserControl
    {
        private SqlCustomerRepository sqlCustomerRepository;
        private List<VisitaJayaPerkasa.Entities.CustomerDetail> listCustomerDetail;

        public CustomerView(VisitaJayaPerkasa.Entities.Customer customer)
        {
            InitializeComponent();
            sqlCustomerRepository = new SqlCustomerRepository();
            listCustomerDetail = sqlCustomerRepository.ListCustomerDetail(customer.ID);

            if (listCustomerDetail != null)
                CustomerDetailGridView.DataSource = listCustomerDetail;


            lblCustomerName.Text = customer.CustomerName;
            lblOffice.Text = customer.Office;
            lblAddres.Text = customer.Address;
            lblEmail.Text = customer.Email;
            lblPhone.Text = customer.Phone;
            lblFax.Text = customer.Fax;
            lblContactPerson.Text = customer.ContactPerson;
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new CustomerList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
