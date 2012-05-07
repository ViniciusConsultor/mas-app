using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisitaJayaPerkasa.Control.Schedule
{
    public partial class ScheduleView : UserControl
    {
        public ScheduleView(VisitaJayaPerkasa.Entities.Schedule tempSchedule)
        {
            InitializeComponent();
            
            lblTujuan.Text = tempSchedule.berangkatTujuan;
            lblKapal.Text = tempSchedule.namaKapal;
            lblVOY.Text = tempSchedule.voy;
            lblKeterangan.Text = tempSchedule.keterangan;

            lblETD.Text =  Utility.Utility.ConvertDateToString(tempSchedule.etd);
            if (tempSchedule.unLoading != null)
                lblUnLoading.Text = Utility.Utility.ConvertDateToString(tempSchedule.unLoading.Value);

            lblRB20.Text = tempSchedule.ro_begin_20.ToString();
            lblRB40.Text = tempSchedule.ro_begin_40.ToString();
            lblRE20.Text = tempSchedule.ro_end_20.ToString();
            lblRE40.Text = tempSchedule.ro_end_40.ToString();
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new ScheduleList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
