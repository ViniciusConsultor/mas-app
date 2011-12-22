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
    public partial class CustomerTransEdit : UserControl
    {
        private VisitaJayaPerkasa.Entities.CustomerTrans customerTrans;
        private bool wantToCreateVessel;
        private List<VisitaJayaPerkasa.Entities.CustomerTransDetail> listCustomerTransDetail;

        public CustomerTransEdit(VisitaJayaPerkasa.Entities.CustomerTrans customerTrans)
        {
            InitializeComponent();
            this.customerTrans = customerTrans;

            if (customerTrans == null)
            {
                wantToCreateVessel = true;
                listCustomerTransDetail = new List<Entities.CustomerTransDetail>();
            }
            else
            {
                wantToCreateVessel = false;
                cboCustomer.SelectedValue = customerTrans.CustomerID;
                pickerFrom.Value = customerTrans.TransDate.Value;

                SqlCustomerTransRepository sqlCustomerTransRepository = new SqlCustomerTransRepository();
                listCustomerTransDetail = sqlCustomerTransRepository.ListCustomerTransDetail(customerTrans.CustomerTransID);

                if (listCustomerTransDetail != null)
                    CustomerTransDetailGridView.DataSource = listCustomerTransDetail;
                else
                    listCustomerTransDetail = new List<VisitaJayaPerkasa.Entities.CustomerTransDetail>();

                sqlCustomerTransRepository = null;
            }
        }
    }
}
