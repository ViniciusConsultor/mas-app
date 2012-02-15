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
    public partial class SearchCustomerTrans : Telerik.WinControls.UI.RadForm
    {
        private List<CustomerTrans> listCustomerTrans;

        public SearchCustomerTrans()
        {
            InitializeComponent();
            SqlCustomerTransRepository sqlCustomerTransRepository = new SqlCustomerTransRepository();
            listCustomerTrans = sqlCustomerTransRepository.ListCustomerTrans();

            CustomerTransGridView.DataSource = listCustomerTrans;

            sqlCustomerTransRepository = null;
        }

        private void CustomerTransGridView_DoubleClick(object sender, EventArgs e)
        {
            if (CustomerTransGridView.SelectedRows.Count > 0)
            {
                GridViewRowInfo gridInfo = CustomerTransGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.CustomerTrans customer = listCustomerTrans.Where(c => c.CustomerTransID.ToString().Equals(id)).FirstOrDefault();

                Constant.VisitaJayaPerkasaApplication.objGetOtherView = customer;
                listCustomerTrans = null;

                this.Dispose();
                this.Close();
            }
        }
    }
}
