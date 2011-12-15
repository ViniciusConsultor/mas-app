using System;
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

namespace VisitaJayaPerkasa.Control.VesselControls
{
    public partial class VesselEdit : UserControl
    {
        private bool wantToCreateVessel;
        private Vessel vessel;

        public VesselEdit(Vessel vessel)
        {
            InitializeComponent();
            this.vessel = vessel;

            if (vessel == null)
            {
                wantToCreateVessel = true;
            }
            else {
                wantToCreateVessel = false;
                etVesselCode.Text = vessel.VesselCode;
                etVesselName.Text = vessel.VesselName;
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new VesselList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (etVesselCode.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill vessel code", "Information");
            else if (etVesselName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill vessel name", "Information");
            else
            {
                SqlVesselRepository sqlVesselRepository = new SqlVesselRepository();
                SqlParameter[] param;

                if (wantToCreateVessel)
                {
                    //Check vessel code has already exists ?
                    param = SqlUtility.SetSqlParameter(new string[] { "vessel_code" }, new object[] { etVesselCode.Text.Trim() });
                    if (sqlVesselRepository.CheckVesselCode(param))
                    {
                        MessageBox.Show(this, "Vessel has already exists !", "Information");
                        return;
                    }

                    param = null;
                    param = SqlUtility.SetSqlParameter(new string[]{"vessel_id", "vessel_code", "vessel_name", "deleted"}, new object[]{ Guid.NewGuid(), etVesselCode.Text.Trim(), etVesselName.Text.Trim(), 0});

                    if (sqlVesselRepository.CreateVessel(param))
                    {
                        MessageBox.Show(this, "Success create vessel", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else {
                        MessageBox.Show(this, "Cannot Create vessel", "Information");
                    }
                }
                else {
                    param = SqlUtility.SetSqlParameter(new string[] { "vessel_code", "vessel_name", "vessel_id"}, new object[] { etVesselCode.Text.Trim(), etVesselName.Text.Trim(), vessel.ID });

                    if (sqlVesselRepository.EditVessel(param))
                    {
                        MessageBox.Show(this, "Success Edit vessel", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Edit vessel", "Information");
                    }
                }

                param = null;
                sqlVesselRepository = null;
            }
        }
    }
}
