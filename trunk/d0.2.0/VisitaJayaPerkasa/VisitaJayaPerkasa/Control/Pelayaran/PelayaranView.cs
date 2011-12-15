using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms; 

namespace VisitaJayaPerkasa.Control.Pelayaran
{
    public partial class PelayaranView : UserControl
    {
        public PelayaranView(VisitaJayaPerkasa.Entities.Pelayaran pelayaran)
        {
            InitializeComponent();
            lblPelayaranName.Text = pelayaran.Name;
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new PelayaranList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

    }
}
