using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisitaJayaPerkasa.Control.LeadTime
{
    public partial class LeadTimeView : UserControl
    {
        public LeadTimeView(VisitaJayaPerkasa.Entities.LeadTime leadTime)
        {
            InitializeComponent();

            lblCityName.Text = leadTime.CityName;
            lblDays.Text = leadTime.Days.ToString();
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new LeadTimeList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
