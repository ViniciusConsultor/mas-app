using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Control.Schedule
{
    public partial class ScheduleEdit : UserControl
    {
        private SqlCityRepository sqlCityRepository;
        private SqlScheduleRepository sqlScheduleRepository;
        private SqlPelayaranRepository sqlPelayaranRepository;

        private VisitaJayaPerkasa.Entities.Schedule schedule;
        private bool wantToCreateVessel;

        public ScheduleEdit(VisitaJayaPerkasa.Entities.Schedule schedule)
        {
            InitializeComponent();
            sqlCityRepository = new SqlCityRepository();
            sqlPelayaranRepository = new SqlPelayaranRepository();
            this.schedule = schedule;

            List<VisitaJayaPerkasa.Entities.City> listCity = sqlCityRepository.GetCity();
            List<VisitaJayaPerkasa.Entities.PelayaranDetail> listPelayaran = sqlPelayaranRepository.GetVessels();

            cboTujuan.DataSource = listCity;
            cboTujuan.DisplayMember = "CityName";
            cboTujuan.ValueMember = "ID";

            cboKapal.DataSource = listPelayaran;
            cboKapal.DisplayMember = "VesselName";
            cboKapal.ValueMember = "ID";

            if (schedule == null)
            {
                wantToCreateVessel = true;

                cboTujuan.SelectedIndex = -1;
                cboTujuan.Text = Constant.VisitaJayaPerkasaApplication.cboDefaultText;
                cboKapal.SelectedIndex = -1;
                cboKapal.Text = Constant.VisitaJayaPerkasaApplication.cboDefaultText;
            }
            else {
                wantToCreateVessel = false;
                cboTujuan.SelectedItem = schedule.berangkatTujuan;
                cboKapal.SelectedItem = schedule.namaKapal;

                pickerETD.Value = schedule.etd;
                pickerTglClosing.Value = schedule.tglclosing;

                etVOY.Text = schedule.voy;
                etRObegin.Text = schedule.ro;
                etKet.Text = schedule.keterangan;
            }

            listCity = null;
            sqlCityRepository = null;
         }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new ScheduleList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void cboBerangkat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboTujuan_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboPelayaran_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboKapal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if(cboTujuan.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please select tujuan", "Information");
            else if (cboKapal.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please select kapal", "Information");
            else if(etVOY.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill voy", "Information");
            else if(pickerETD.Value.Date <= DateTime.Today.Date)
                MessageBox.Show(this, "Please correct etd", "Information");
            else if(pickerTglClosing.Value.Date < pickerETD.Value.Date)
                MessageBox.Show(this, "date of tgl closing must be greather than etd", "Information");
            else
            {
                try
                {
                    Int32.Parse(etRObegin.Text.Trim());
                    Int32.Parse(etROend.Text.Trim());
                }
                catch (Exception ex) {
                    Utility.Log.Logging.Information("try parse int for validate");
                    MessageBox.Show(this, "Please correct Ro value", "Information");
                    return;
                }

                sqlScheduleRepository = new SqlScheduleRepository();

                if (wantToCreateVessel) {
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(
                            new string[]{"schedule_id", "tujuan",
                                "vessel_id", "etd", "tgl_closing",
                                "voy", "ro", "deleted", "keterangan"},
                            new object[]{Guid.NewGuid(), 
                                Utility.Utility.ConvertToUUID(cboTujuan.SelectedValue.ToString()),
                                Utility.Utility.ConvertToUUID(cboKapal.SelectedValue.ToString()),
                                pickerETD.Value.Date,
                                pickerTglClosing.Value.Date,
                                etVOY.Text.Trim(),
                                etRObegin.Text.Trim(),
                                0,
                                etKet.Text.Trim()}
                        );

                    if (sqlScheduleRepository.CreateNewSchedule(sqlParam))
                    {
                        MessageBox.Show(this, "Success Create schedule", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else {
                        MessageBox.Show(this, "Cannot Create schedule", "Information");
                    }
                }
                else {
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(
                                new string[]{"tujuan",
                                "vessel_id", "etd", "tgl_closing",
                                "voy", "ro", "deleted", "keterangan", "schedule_id"},
                                new object[]{
                                Utility.Utility.ConvertToUUID(cboTujuan.SelectedValue.ToString()),
                                Utility.Utility.ConvertToUUID(cboKapal.SelectedValue.ToString()),
                                pickerETD.Value.Date,
                                pickerTglClosing.Value.Date,
                                etVOY.Text.Trim(),
                                etRObegin.Text.Trim(),
                                0,
                                etKet.Text.Trim(),
                                schedule.ID}
                            );

                    if (sqlScheduleRepository.EditSchedule(sqlParam))
                    {
                        MessageBox.Show(this, "Success edit schedule", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot edit schedule", "Information");
                    }
                }


                sqlScheduleRepository = null;
            }
        }

    }
}
