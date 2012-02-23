using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisitaJayaPerkasa.Form.Report.Schedule
{
    public partial class RptSchedule : UserControl
    {
        public RptSchedule()
        {
            InitializeComponent();
            
            rbBeginDate.Text = Utility.Utility.ConvertDateToString(DateTime.Now.Date);
            rbEndDate.Text = Utility.Utility.ConvertDateToString(DateTime.Now.Date);
        }

        private void rbBeginDate_Click(object sender, EventArgs e)
        {
            radCalendarBegin.Visible = true;
        }

        private void radCalendarEnd_SelectionChanged(object sender, EventArgs e)
        {
            rbEndDate.Text = Utility.Utility.ConvertDateToString(radCalendarEnd.SelectedDate);
            radCalendarEnd.Visible = false;
        }

        private void radCalendarBegin_SelectionChanged(object sender, EventArgs e)
        {
            rbBeginDate.Text = Utility.Utility.ConvertDateToString(radCalendarBegin.SelectedDate);
            radCalendarBegin.Visible = false;
        }

        private void rbEndDate_Click(object sender, EventArgs e)
        {
            radCalendarEnd.Visible = true;
        }
    }
}








/*

select 'Jakarta - ' + c.city_name as tujuan,
(
	select TOP 1 supplier_name FROM pelayaran_detail pd JOIN pelayaran p 
	ON 	p.pelayaran_id = pd.pelayaran_id
	AND pd.pelayaran_detail_id = s.pelayaran_detail_id 
	JOIN supplier s
	ON s.supplier_id = p.supplier_id	
) as pelayaran,
(select TOP 1 vessel_name FROM pelayaran_detail pd WHERE pd.pelayaran_detail_id = s.pelayaran_detail_id) as vessel,
s.voy, s.tgl_closing, s.etd
FROM schedule s, city c
WHERE ta is null AND s.tujuan = c.city_id
*/