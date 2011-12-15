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

namespace VisitaJayaPerkasa.Control.LeadTime
{
    public partial class LeadTimeEdit : UserControl
    {
        private bool wantToCreateVessel;
        private VisitaJayaPerkasa.Entities.LeadTime leadTime;

        public LeadTimeEdit(VisitaJayaPerkasa.Entities.LeadTime leadTime)
        {
            InitializeComponent();
            this.leadTime = leadTime;
            SqlCityRepository sqlCityRepository = new SqlCityRepository();
            List<VisitaJayaPerkasa.Entities.City> City = sqlCityRepository.GetCity();

            cbCity.DataSource = City;
            cbCity.DisplayMember = "CityName";
            cbCity.ValueMember = "ID";

            if (leadTime == null)
            {
                wantToCreateVessel = true;
            }
            else
            {
                wantToCreateVessel = false;
                cbCity.SelectedItem = leadTime.CityName;
                etLeadTimeDays.Text = leadTime.Days + "";
            }
        }

        private void radComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new LeadTimeList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (cbCity.SelectedItem.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose city", "Information");
            else if (etLeadTimeDays.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill days", "Information");
            else
            {
                SqlLeadTimeRepository sqlLeadTimeRepository = new SqlLeadTimeRepository();
                SqlParameter[] param;

                if (wantToCreateVessel)
                {
                    //Check vessel code has already exists ?
                    param = SqlUtility.SetSqlParameter(new string[] { "city_id" }, new object[] { cbCity.SelectedValue });
                    if (sqlLeadTimeRepository.CheckLeadTime(param))
                    {
                        MessageBox.Show(this, "lead time city has already exists !", "Information");
                        return;
                    }

                    param = null;
                    param = SqlUtility.SetSqlParameter(new string[] { "lead_time_id", "city_id", "days", "deleted" }, new object[] { Guid.NewGuid(), cbCity.SelectedValue, etLeadTimeDays.Text.Trim(), 0 });

                    if (sqlLeadTimeRepository.CreateLeadTime(param))
                    {
                        MessageBox.Show(this, "Success create lead time", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Create lead time", "Information");
                    }
                }
                else
                {
                    param = SqlUtility.SetSqlParameter(new string[] { "days", "lead_time_id" }, new object[] { etLeadTimeDays.Text.Trim(), leadTime.ID });

                    if (sqlLeadTimeRepository.EditLeadTime(param))
                    {
                        MessageBox.Show(this, "Success Edit lead time", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Edit lead time", "Information");
                    }
                }

                param = null;
                sqlLeadTimeRepository = null;
            }
        }
    }
}
