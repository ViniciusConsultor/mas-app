using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using VisitaJayaPerkasa.SqlRepository;
using VisitaJayaPerkasa.Entities;
using Telerik.WinControls.UI;
using System.Linq;
using System.Data.SqlClient;


namespace VisitaJayaPerkasa.Form
{
    public partial class SearchCustomer : Telerik.WinControls.UI.RadForm
    {
        private List<Customer> listCustomer;

        public SearchCustomer()
        {
            InitializeComponent();
            SqlCustomerRepository sqlCustomerRepository = new SqlCustomerRepository();
            listCustomer = sqlCustomerRepository.listCustomerForPriceList();

            CustomerGridView.DataSource = listCustomer;

            sqlCustomerRepository = null;
        }

        private void CustomerGridView_DoubleClick(object sender, EventArgs e)
        {
            if (CustomerGridView.SelectedRows.Count > 0)
            {
                GridViewRowInfo gridInfo = CustomerGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.Customer customer = listCustomer.Where(c => c.ID.ToString().Equals(id)).FirstOrDefault();

                Constant.VisitaJayaPerkasaApplication.objGetOtherView = customer;
                listCustomer = null;

                this.Dispose();
                this.Close();
            }
        }
    }
}
