using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;

namespace VisitaJayaPerkasa.Control.Schedule
{
    public partial class ScheduleList : UserControl
    {
        private List<VisitaJayaPerkasa.Entities.Schedule> schedules;
        private List<VisitaJayaPerkasa.Entities.Schedule> showShedule;
        private SqlScheduleRepository sqlScheduleRepository;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        public ScheduleList()
        {
            InitializeComponent();
            sqlScheduleRepository = new SqlScheduleRepository();

        }

        private void radCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            MessageBox.Show(radCalendar1.SelectedDate + "");
            radCalendar1.Visible = false;
        }

        private void radImageButtonElement1_Click(object sender, EventArgs e)
        {
            radCalendar1.Visible = true;
        }
    }
}
