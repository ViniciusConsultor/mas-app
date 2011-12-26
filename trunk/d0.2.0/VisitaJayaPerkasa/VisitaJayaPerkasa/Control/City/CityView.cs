using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.Entities;

namespace VisitaJayaPerkasa.Control.City
{
    public partial class CityView : UserControl
    {
        public CityView(VisitaJayaPerkasa.Entities.City city)
        {
            InitializeComponent();

            lblCityCode.Text = Utility.Utility.DisplayNullValues(city.CityCode);
            lblCityName.Text = Utility.Utility.DisplayNullValues(city.CityName);
            lblDays.Text = Utility.Utility.DisplayNullValues(city.Days.ToString());
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new CityList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
