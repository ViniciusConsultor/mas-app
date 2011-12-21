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
using Telerik.WinControls.UI;

namespace VisitaJayaPerkasa.Control.PriceList
{
    public partial class PriceList : UserControl
    {
        private SqlPriceListRepository sqlPriceListRepository;
        private SqlSupplierRepository sqlSupplierRepository;
        private SqlCityRepository sqlCityRepository;
        private SqlTypeContRepository sqlTypeContRepository;
        private SqlConditionRepository sqlConditionRepository;

        private List<VisitaJayaPerkasa.Entities.Category> listTypeOfSupplier;
        private List<VisitaJayaPerkasa.Entities.Supplier> listSupplier; 
        private List<VisitaJayaPerkasa.Entities.City> listCity;
        private List<VisitaJayaPerkasa.Entities.TypeCont> listType;
        private List<VisitaJayaPerkasa.Entities.Condition> listCondition;

        public PriceList()
        {
            InitializeComponent();
            sqlPriceListRepository = new SqlPriceListRepository();
            sqlCityRepository = new SqlCityRepository();

            listTypeOfSupplier = sqlPriceListRepository.GetTypeOfSupplier();
            cboTypeSupplier.DataSource = listTypeOfSupplier;
            cboTypeSupplier.DisplayMember = "CategoryName";
            cboTypeSupplier.ValueMember = "ID";
            cboTypeSupplier.SelectedIndex = -1;
            cboTypeSupplier.SelectedText = Constant.VisitaJayaPerkasaApplication.cboDefaultText;

            listCity = sqlCityRepository.GetCity();
            cbDestination.DataSource = listCity;
            cbDestination.DisplayMember = "CityName";
            cbDestination.ValueMember = "ID";
            cbDestination.SelectedIndex = -1;
            cbDestination.SelectedText = Constant.VisitaJayaPerkasaApplication.cboDefaultText;

            cbSupplier.Enabled = false;
            cboRecipient.Enabled = false;
            sqlPriceListRepository = null;
            PriceListGridView.Enabled = false;

            sqlPriceListRepository = null;
            sqlCityRepository = null;
        }

        private void LoadCboGridView() {
            sqlSupplierRepository = new SqlSupplierRepository();
            sqlCityRepository = new SqlCityRepository();
            sqlTypeContRepository = new SqlTypeContRepository();
            sqlConditionRepository = new SqlConditionRepository();

            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[1]).DataSource = listSupplier;
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[1]).DisplayMember = "SupplierName";
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[1]).ValueMember = "ID";

            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[2]).DataSource = listCity;
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[2]).DisplayMember = "CityName";
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[2]).ValueMember = "ID";

            listType = sqlTypeContRepository.GetTypeCont();
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[3]).DataSource = listType;
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[3]).DisplayMember = "TypeName";
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[3]).ValueMember = "ID";

            listCondition = sqlConditionRepository.GetConditions();
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[4]).DataSource = listCondition;
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[4]).DisplayMember = "ConditionName";
            ((GridViewComboBoxColumn)this.PriceListGridView.Columns[4]).ValueMember = "ID";

            sqlSupplierRepository = null;
            sqlCityRepository = null;
            sqlTypeContRepository = null;
            sqlConditionRepository = null;
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
                if (PriceListGridView.RowCount > 0) {
                    DialogResult dResult = MessageBox.Show(this, "Changing your data grid will not be save ? ", "Confirmation", MessageBoxButtons.YesNo);

                    if (dResult == DialogResult.Yes)
                    {
                        PriceListGridView.Rows.Clear();
                        PriceListGridView.Enabled = false;
                    }
                    else
                        return;
                }

                sqlPriceListRepository = new SqlPriceListRepository();
                listSupplier = sqlPriceListRepository.GetSupplier(Utility.Utility.ConvertToUUID(cboTypeSupplier.SelectedValue.ToString()));

                cbSupplier.Enabled = true;
                cbSupplier.DataSource = listSupplier;
                cbSupplier.DisplayMember = "SupplierName";
                cbSupplier.ValueMember = "ID";
                cbSupplier.SelectedIndex = -1;
                cbSupplier.Text = "-- Choose --";

                sqlPriceListRepository = null;
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            if (PriceListGridView.RowCount > 0)
            {
                DialogResult dResult = MessageBox.Show(this, "Changing your data grid will not be save ? ", "Confirmation", MessageBoxButtons.YesNo);

                if (dResult == DialogResult.Yes)
                {
                    PriceListGridView.Rows.Clear();
                }
                else
                    return;
            }

            PriceListGridView.Enabled = true;
            if (cboTypeSupplier.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose type of supplier", "Information");
            else if (cbSupplier.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose supplier", "Information");
            else if (cbDestination.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose destination", "Information");
            else if (pickerFrom.Value.Date < pickerTo.Value.Date)
                MessageBox.Show(this, "Date from must not be greather than date to", "Information");
            else
            {
                PriceListGridView.Enabled = true;
                LoadCboGridView();
            }
        }
    }
}
