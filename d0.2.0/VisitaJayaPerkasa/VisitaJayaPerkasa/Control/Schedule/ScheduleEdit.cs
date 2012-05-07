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
        private bool wantToCreateVessel, wantToEditTdETC;

        public ScheduleEdit(VisitaJayaPerkasa.Entities.Schedule schedule)
        {
            InitializeComponent();
            sqlCityRepository = new SqlCityRepository();
            sqlPelayaranRepository = new SqlPelayaranRepository();
            this.schedule = schedule;

            List<VisitaJayaPerkasa.Entities.City> listCity = sqlCityRepository.GetCity();
            List<VisitaJayaPerkasa.Entities.PelayaranDetail> listPelayaran = sqlPelayaranRepository.GetVessels();
            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            cboTujuan.DataSource = listCity;
            cboTujuan.DisplayMember = "CityName";
            cboTujuan.ValueMember = "ID";

            cboKapal.DataSource = listPelayaran;
            cboKapal.DisplayMember = "VesselName";
            cboKapal.ValueMember = "VesselCodeAndPelayaranID";

            if (schedule == null)
            {
                wantToCreateVessel = true;

                cboTujuan.SelectedIndex = -1;
                cboTujuan.Text = Constant.VisitaJayaPerkasaApplication.cboDefaultText;
                cboKapal.SelectedIndex = -1;
                cboKapal.Text = Constant.VisitaJayaPerkasaApplication.cboDefaultText;

                lblETA.Visible = false;
                lblTA.Visible = false;
                lblTD.Visible = false;
                lblUnloading.Visible = false;
                pickerETA.Visible = false;
                pickerTA.Visible = false;
                pickerTD.Visible = false;
                pickerUnLoading.Visible = false;

                pickerETD.Value = DateTime.Now;
                pickerUnLoading.Value = DateTime.Now;
                pickerTglClosing.Value = DateTime.Now;
            }
            else {
                wantToCreateVessel = false;
                 
                DialogResult dResult = MessageBox.Show(this, "Are you sure want to edit TD, TA and etc ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                    wantToEditTdETC = true;
                else
                    wantToEditTdETC = false;


                cboTujuan.SelectedItem = schedule.berangkatTujuan;
                cboKapal.SelectedItem = schedule.namaKapal;

                etVOY.Text = schedule.voy;

                pickerETD.Value = schedule.etd;
                pickerUnLoading.Value = schedule.unLoading.GetValueOrDefault(DateTime.Now);
                pickerTglClosing.Value = schedule.tglclosing;

                etRObegin20.Text = schedule.ro_begin_20.ToString();
                etRObegin40.Text = schedule.ro_begin_40.ToString();
                etKet.Text = schedule.keterangan;

                lblETA.Visible = true;
                lblTA.Visible = true;
                lblTD.Visible = true;
                pickerETA.Visible = true;
                pickerTA.Visible = true;
                pickerTD.Visible = true;
                
                pickerETA.Value = DateTime.Now;
                pickerTA.Value = DateTime.Now;
                pickerTD.Value = DateTime.Now;


                if (wantToEditTdETC)
                {
                    cboTujuan.Enabled = false;
                    cboKapal.Enabled = false;
                    etVOY.Enabled = false;
                    pickerETD.Enabled = false;
                    pickerTglClosing.Enabled = false;
                    pickerUnLoading.Enabled = false;
                    etRObegin20.Enabled = false;
                    etRObegin40.Enabled = false;
                    etKet.Enabled = false;


                    VisitaJayaPerkasa.Entities.City tempCity = sqlCityRepository.GetCityByID(cboTujuan.SelectedValue.ToString());
                    if (tempCity.Deleted == 1)
                        MessageBox.Show(this, "ETA not same like city, because city has already be deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        pickerETA.Value = DateTime.Now.AddDays(tempCity.Days);
                        pickerTA.Value = DateTime.Now.AddDays(tempCity.Days);
                    }

                    tempCity = null;
                }
                else {
                    lblETA.Visible = false;
                    lblTA.Visible = false;
                    lblTD.Visible = false;

                    pickerETA.Visible = false;
                    pickerTA.Visible = false;
                    pickerTD.Visible = false;
                }
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
            else if(pickerETD.Value.Date <= DateTime.Today.Date && wantToCreateVessel)
                MessageBox.Show(this, "Please correct etd", "Information");
            else if(pickerTglClosing.Value.Date > pickerETD.Value.Date)
                MessageBox.Show(this, "date of tgl closing must be lower than etd", "Information");
            else
            {
                try
                {
                    if (etRObegin20.Text.Trim().Length == 0 && etRObegin40.Text.Trim().Length == 0) {
                        MessageBox.Show(this, "Please fill ro begin", "Information");
                        return;
                    }

                    if (etRObegin20.Text.Trim().Length > 0)
                        Int32.Parse(etRObegin20.Text.Trim());
                    if (etRObegin40.Text.Trim().Length > 0)
                        Int32.Parse(etRObegin40.Text.Trim());
                }
                catch (Exception ex) {
                    Utility.Log.Logging.Information("try parse int for validate " + ex.Message);
                    MessageBox.Show(this, "Please correct Ro value", "Information");
                    return;
                }

                sqlScheduleRepository = new SqlScheduleRepository();

                if (wantToCreateVessel) {
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(
                            new string[]{"schedule_id", "tujuan", "pelayaran_detail_id", "tgl_closing", 
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
                                0,
                                0,
                                pickerETD.Value.Date,
                                DBNull.Value,
                                DBNull.Value,
                                DBNull.Value,
                                DBNull.Value
                            });

                    if (sqlScheduleRepository.CreateNewSchedule(sqlParam))
                    {
                        MessageBox.Show(this, "Success Create schedule", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else {
                        MessageBox.Show(this, "Cannot Create schedule", "Information");
                    }
                }
                else {
                    if (wantToEditTdETC)
                    {
                        if (pickerETA.Value.Date < pickerTD.Value.Date)
                        {
                            MessageBox.Show(this, "Eta must be greather than TD ", "Information");
                            return;
                        }
                        else if (pickerTD.Value.Date < pickerETD.Value.Date)
                        {
                            MessageBox.Show(this, "TD must be greather than ETD", "Information");
                            return;
                        }
                        else if (pickerTA.Value.Date < pickerETA.Value.Date)
                        {
                            MessageBox.Show(this, "TA must be greather than ETA", "Information");
                            return;
                        }
                        else if (schedule.ro_end_20 > Int32.Parse(etRObegin20.Text.Trim()))
                        {
                            MessageBox.Show(this, "Please correct ro begin 20. \n Ro end 20 greater than ro begin 20", "Information");
                            return;
                        }
                        else if (schedule.ro_end_40 > Int32.Parse(etRObegin40.Text.Trim()))
                        {
                            MessageBox.Show(this, "Please correct ro begin 40. \n Ro end 40 greater than ro begin 40", "Information");
                            return;
                        }
                    }
                    else { 
                        if (schedule.ro_end_20 > Int32.Parse(etRObegin20.Text.Trim()))
                        {
                            MessageBox.Show(this, "Please correct ro begin 20. \n Ro end 20 greater than ro begin 20", "Information");
                            return;
                        }
                        else if (schedule.ro_end_40 > Int32.Parse(etRObegin40.Text.Trim()))
                        {
                            MessageBox.Show(this, "Please correct ro begin 40. \n Ro end 40 greater than ro begin 40", "Information");
                            return;
                        }
                    }


                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(
                        new string[]{"schedule_id", "tujuan", "pelayaran_detail_id", "tgl_closing", 
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
                                                schedule.ro_end_20,
                                                schedule.ro_end_40,
                                                pickerETD.Value.Date,
                                                (wantToEditTdETC) ? pickerTD.Value.Date : schedule.td,
                                                (wantToEditTdETC) ? pickerETA.Value.Date : schedule.eta,
                                                (wantToEditTdETC) ? pickerTA.Value.Date : schedule.ta,
                                                pickerUnLoading.Value.Date
                            });


                    if (sqlScheduleRepository.EditSchedule(sqlParam))
                    {
                        MessageBox.Show(this, "Success edit schedule", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
