﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.Entities;
using VisitaJayaPerkasa.SqlRepository;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.Control.City
{
    public partial class CityEdit : UserControl
    {
        private bool wantToCreateCity;
        private VisitaJayaPerkasa.Entities.City city;

        public CityEdit(VisitaJayaPerkasa.Entities.City city)
        {
            InitializeComponent();
            this.city = city;

            if (city == null)
            {
                wantToCreateCity = true;
            }
            else
            {
                wantToCreateCity = false;
                etCityCode.Text = city.CityCode;
                etCityName.Text = city.CityName;
                etDays.Text = city.Days.ToString();
            }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            //save
            if (etCityCode.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill city code", "Information");
            else if (etCityName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill city name", "Information");
            else if(etDays.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill days", "Information");
            else
            {
                try{
                    Convert.ToInt32(etDays.Text.Trim());
                }
                catch(Exception err){
                    MessageBox.Show(this, "days must be numeric", "Information");
                    Logging.Error("CityEdit.cs - " + err.Message);
                    return;
                }

                SqlCityRepository sqlCityRepository = new SqlCityRepository();
                SqlParameter[] param;

                if (wantToCreateCity)
                {
                    //Check city code has already exists ?
                    param = SqlUtility.SetSqlParameter(new string[] { "city_code" }, new object[] { etCityCode.Text.Trim() });
                    if (sqlCityRepository.CheckCityCode(param, Guid.Empty, true))
                    {
                        DialogResult dResult = MessageBox.Show(this, "City has already deleted. Do you want to activate ?", "Confirmation", MessageBoxButtons.YesNo);
                        if (dResult == DialogResult.Yes)
                        {
                            param = SqlUtility.SetSqlParameter(new string[] { "city_id", "city_code", "city_name", "Days", "deleted" }, new object[] { Guid.NewGuid(), etCityCode.Text.Trim(), etCityName.Text.Trim(), Convert.ToInt32(etDays.Text.Trim()), 0 });

                            if (sqlCityRepository.ActivateCity(param))
                            {
                                MessageBox.Show(this, "Success Activate City", "Information");
                                radButtonElement2.PerformClick();
                            }
                            else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                                MessageBox.Show(this, "Cannot Activate City", "Information");

                            param = null;
                        }
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (sqlCityRepository.CheckCityCode(param, Guid.Empty))
                    {
                        MessageBox.Show(this, "City has already exists", "Information");
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    param = null;
                    param = SqlUtility.SetSqlParameter(new string[] { "city_id", "city_code", "city_name", "Days", "deleted" }, new object[] { Guid.NewGuid(), etCityCode.Text.Trim(), etCityName.Text.Trim(), Convert.ToInt32(etDays.Text.Trim()), 0 });

                    if (sqlCityRepository.CreateCity(param))
                    {
                        MessageBox.Show(this, "Success create city", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show(this, "Cannot Create city", "Information");
                    }
                }
                else
                {
                    param = SqlUtility.SetSqlParameter(new string[] { "city_code", "city_name", "Days", "city_id" }, new object[] { etCityCode.Text.Trim(), etCityName.Text.Trim(), Convert.ToInt32(etDays.Text.Trim()), city.ID });
                    if (sqlCityRepository.CheckCityCode(param, this.city.ID))
                    {
                        MessageBox.Show(this, "city has already exist. if it has already deleted. you must activate it with create new data", "Information");
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (sqlCityRepository.EditCity(param))
                    {
                        MessageBox.Show(this, "Success Edit city", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show(this, "Cannot Edit city", "Information");
                    }
                }

                param = null;
                sqlCityRepository = null;
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            //cancel
            UserControl Controllers = new CityList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
