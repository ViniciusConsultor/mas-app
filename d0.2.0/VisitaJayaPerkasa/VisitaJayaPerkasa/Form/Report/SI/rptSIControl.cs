using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;

namespace VisitaJayaPerkasa.Form.Report.SI
{
    public partial class rptSIControl : UserControl
    {
        private SqlScheduleRepository sqlScheduleRepository;
        private SqlCityRepository sqlCityRepository;

        public rptSIControl()
        {
            InitializeComponent();
            sqlScheduleRepository = new SqlScheduleRepository();
            sqlCityRepository = new SqlCityRepository();
            List<VisitaJayaPerkasa.Entities.Supplier> listSupplier;
            SqlSupplierRepository sqlSupplierRepository = new SqlSupplierRepository();
            listSupplier = sqlSupplierRepository.ListSuppliers();
            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            cboSupplier.DataSource = listSupplier;
            cboSupplier.DisplayMember = "SupplierName";
            cboSupplier.SelectedIndex = -1;
            cboSupplier.ValueMember = "Id";
            cboSupplier.Text = "-- Choose --";

            List<VisitaJayaPerkasa.Entities.Schedule> listSchedule = sqlScheduleRepository.ListSchedule();
            cboKapal.DataSource = listSchedule;
            cboKapal.DisplayMember = "namaKapal";
            cboKapal.SelectedIndex = -1;
            cboKapal.Text = "-- Choose --";

            List<VisitaJayaPerkasa.Entities.City> listDestination = sqlCityRepository.GetCity();
            cboCity.DataSource = listDestination;
            cboCity.DisplayMember = "CityName";
            cboCity.ValueMember = "ID";
            cboCity.SelectedIndex = -1;
            cboCity.Text = "-- Choose --";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            new PrintSI().ShowDialog();
        }
    }
}
