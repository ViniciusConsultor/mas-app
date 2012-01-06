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
            cboKapal.ValueMember = "VesselCode";

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

                etVOY.Text = schedule.voy;

                pickerETD.Value = schedule.etd;
                pickerTD.Value = schedule.td;
                pickerETA.Value = schedule.eta;
                pickerTA.Value = schedule.ta;
                pickerUnLoading.Value = schedule.unLoading;
                pickerTglClosing.Value = schedule.tglclosing;

                etRObegin20.Text = schedule.ro_begin_20.ToString();
                etRObegin40.Text = schedule.ro_begin_40.ToString();
                etROend20.Text = schedule.ro_end_20.ToString();
                etROend40.Text = schedule.ro_end_40.ToString();
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
                    if (etRObegin20.Text.Trim().Length == 0 && etRObegin40.Text.Trim().Length == 0) {
                        MessageBox.Show(this, "Please fill ro begin", "Information");
                        return;
                    }
                    else if (etROend20.Text.Trim().Length == 0 && etROend40.Text.Trim().Length == 0) {
                        MessageBox.Show(this, "Please fill ro end", "Information");
                        return;
                    }

                    if (etRObegin20.Text.Trim().Length > 0)
                        Int32.Parse(etRObegin20.Text.Trim());
                    if (etRObegin40.Text.Trim().Length > 0)
                        Int32.Parse(etRObegin40.Text.Trim());
                    if (etROend20.Text.Trim().Length > 0)
                        Int32.Parse(etROend20.Text.Trim());
                    if (etROend40.Text.Trim().Length > 0)
                        Int32.Parse(etROend40.Text.Trim());
                }
                catch (Exception ex) {
                    Utility.Log.Logging.Information("try parse int for validate " + ex.Message);
                    MessageBox.Show(this, "Please correct Ro value", "Information");
                    return;
                }

                sqlScheduleRepository = new SqlScheduleRepository();

                if (wantToCreateVessel) {
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(
                            new string[]{"schedule_id", "tujuan", "pelayaran_id", "tgl_closing", 
                                "voy", "deleted", "keterangan", "vessel_code", "ro_begin_20",
                                "ro_begin_40", "ro_end_20", "ro_end_40", "etd", "td", "eta", "ta", 
                                "unloading"
                            },
                            new object[]{Guid.NewGuid(), 
                                Utility.Utility.ConvertToUUID(cboTujuan.SelectedValue.ToString()),
                                Utility.Utility.ConvertToUUID(cboKapal.SelectedValue.ToString().Substring(0, 36)),
                                pickerTglClosing.Value.Date,
                                etVOY.Text.Trim(),
                                0,
                                etKet.Text.Trim(),
                                cboKapal.SelectedValue.ToString().Substring(36),
                                Utility.Utility.IsStringNullorEmpty(etRObegin20.Text.Trim()) ? 0 : Int32.Parse(etRObegin20.Text.Trim()) ,
                                Utility.Utility.IsStringNullorEmpty(etRObegin40.Text.Trim()) ? 0 : Int32.Parse(etRObegin40.Text.Trim()),
                                Utility.Utility.IsStringNullorEmpty(etROend20.Text.Trim()) ? 0 : Int32.Parse(etROend20.Text.Trim()),
                                Utility.Utility.IsStringNullorEmpty(etROend40.Text.Trim()) ? 0 : Int32.Parse(etROend40.Text.Trim()),
                                pickerETD.Value.Date,
                                pickerTD.Value.Date,
                                pickerETA.Value.Date,
                                pickerTA.Value.Date,
                                pickerUnLoading.Value.Date
                            });

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
                        new string[]{"schedule_id", "tujuan", "pelayaran_id", "tgl_closing", 
                                                "voy", "deleted", "keterangan", "vessel_code", "ro_begin_20",
                                                "ro_begin_40", "ro_end_20", "ro_end_40", "etd", "td", "eta", "ta", 
                                                "unloading"
                                            },
                        new object[]{schedule.ID, 
                                                Utility.Utility.ConvertToUUID(cboTujuan.SelectedValue.ToString()),
                                                Utility.Utility.ConvertToUUID(cboKapal.SelectedValue.ToString().Substring(0, 36)),
                                                pickerTglClosing.Value.Date,
                                                etVOY.Text.Trim(),
                                                0,
                                                etKet.Text.Trim(),
                                                cboKapal.SelectedValue.ToString().Substring(36),
                                                Utility.Utility.IsStringNullorEmpty(etRObegin20.Text.Trim()) ? 0 : Int32.Parse(etRObegin20.Text.Trim()) ,
                                                Utility.Utility.IsStringNullorEmpty(etRObegin40.Text.Trim()) ? 0 : Int32.Parse(etRObegin40.Text.Trim()),
                                                Utility.Utility.IsStringNullorEmpty(etROend20.Text.Trim()) ? 0 : Int32.Parse(etROend20.Text.Trim()),
                                                Utility.Utility.IsStringNullorEmpty(etROend40.Text.Trim()) ? 0 : Int32.Parse(etROend40.Text.Trim()),
                                                pickerETD.Value.Date,
                                                pickerTD.Value.Date,
                                                pickerETA.Value.Date,
                                                pickerTA.Value.Date,
                                                pickerUnLoading.Value.Date
                            });


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
