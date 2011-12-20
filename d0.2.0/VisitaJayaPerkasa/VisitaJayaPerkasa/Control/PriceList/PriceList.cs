using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;
using VisitaJayaPerkasa.Entities;

namespace VisitaJayaPerkasa.Control.PriceList
{
    public partial class PriceList : UserControl
    {
        private SqlPriceListRepository sqlPriceListRepository;
 
        public PriceList()
        {
            InitializeComponent();
            sqlPriceListRepository = new SqlPriceListRepository();

            List<VisitaJayaPerkasa.Entities.Category> listTypeOfSupplier = sqlPriceListRepository.GetTypeOfSupplier();

            cboTypeSupplier.DataSource = listTypeOfSupplier;
            cboTypeSupplier.DisplayMember = "CategoryName";
            cboTypeSupplier.ValueMember = "ID";
            cboTypeSupplier.SelectedIndex = -1;
            cboTypeSupplier.SelectedText = "-- Choose --";

            cbSupplier.Enabled = false;
            cboRecipient.Enabled = false;
            cboStuffingPlace.Enabled = false;
            cbDestination.Enabled = false;

            sqlPriceListRepository = null;
            listTypeOfSupplier = null;
        }

        private void cboTypeSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cbSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cbDestination_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboRecipient_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboStuffingPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboTypeSupplier_SelectedValueChanged(object sender, EventArgs e)
        { 
            if ((!cboTypeSupplier.SelectedText.Equals("-- Choose --")) && (!cboTypeSupplier.SelectedText.Equals("")))
            {
                sqlPriceListRepository = new SqlPriceListRepository();
                List<VisitaJayaPerkasa.Entities.Supplier> listSupplier = sqlPriceListRepository.GetSupplier(Utility.Utility.ConvertToUUID(cboTypeSupplier.SelectedValue.ToString()));

                cbSupplier.Enabled = true;
                cbSupplier.DataSource = listSupplier;
                cbSupplier.DisplayMember = "SupplierName";
                cbSupplier.ValueMember = "ID";
                cbSupplier.SelectedIndex = -1;
                cbSupplier.Text = "-- Choose --";

                listSupplier = null;
                sqlPriceListRepository = null;
            }
        }
    }
}
