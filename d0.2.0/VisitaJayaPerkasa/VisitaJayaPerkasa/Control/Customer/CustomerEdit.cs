using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisitaJayaPerkasa.Control.Customer
{
    public partial class CustomerEdit : UserControl
    {
        private VisitaJayaPerkasa.Entities.Customer customer;

        public CustomerEdit(VisitaJayaPerkasa.Entities.Customer customer)
        {
            InitializeComponent();
            this.customer = customer;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetDetailData();
        }

        private void ResetDetailData() {
            etFirstName.Text = "";
            etLastName.Text = "";
            etDetailAddress.Text = "";
            etDetailMobile.Text = "";
            etDetailPhone.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (etFirstName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill first name", "Information");
            else { 
                
            }
        }
    }
}
