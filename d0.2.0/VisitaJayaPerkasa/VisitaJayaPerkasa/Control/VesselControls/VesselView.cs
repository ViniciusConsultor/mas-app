using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.Entities;

namespace VisitaJayaPerkasa.Control.VesselControls
{
    public partial class VesselView : UserControl
    {
        public VesselView(Vessel vessel)
        {
            InitializeComponent();

            lblVesselCode.Text = Utility.Utility.DisplayNullValues(vessel.VesselCode);
            lblVesselName.Text = Utility.Utility.DisplayNullValues(vessel.VesselName);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new VesselList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
