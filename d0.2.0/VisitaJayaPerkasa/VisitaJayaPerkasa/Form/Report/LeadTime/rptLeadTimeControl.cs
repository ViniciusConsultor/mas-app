using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace VisitaJayaPerkasa.Form.Report.LeadTime
{
    public partial class rptLeadTimeControl : UserControl
    {
        public rptLeadTimeControl()
        {
            InitializeComponent();

            rtsiHal.Visibility = ElementVisibility.Collapsed;
            rtsiCustomer.Visibility = ElementVisibility.Collapsed;
            radCalendarBegin.Visible = false;
            radCalendarEnd.Visible = false;


            lblDateBegin.Text = Utility.Utility.ConvertDateToString(DateTime.Now);
            lblDateEnd.Text = Utility.Utility.ConvertDateToString(DateTime.Now);
        }

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearch.Text.ToLower().Equals("all")) 
            {
                rtsiHal.Visibility = ElementVisibility.Collapsed;
                rtsiCustomer.Visibility = ElementVisibility.Collapsed;
            }
            else if (cbSearch.Text.ToLower().Equals("hal"))
            {
                rtsiHal.Visibility = ElementVisibility.Visible;
                rtsiCustomer.Visibility = ElementVisibility.Collapsed;
            }
            else if (cbSearch.Text.ToLower().Equals("customer"))
            {
                rtsiHal.Visibility = ElementVisibility.Collapsed;
                rtsiCustomer.Visibility = ElementVisibility.Visible;
            }

        }


        private void Search() { }


        private void radImageButtonSearch_Click(object sender, EventArgs e)
        {
            if (cbSearch.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText)) 
                MessageBox.Show(this, "Please choose search by", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (Utility.Utility.ConvertStringToDate(Utility.Utility.ChangeDateMMDD(lblDateBegin.Text)) > Utility.Utility.ConvertStringToDate(Utility.Utility.ChangeDateMMDD(lblDateEnd.Text)))
                MessageBox.Show(this, "Date begin greather than date end", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (rtsiHal.Visibility == ElementVisibility.Visible)
                {
                    if (cbHal.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                        MessageBox.Show(this, "Please choose hal", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        Search();
                }
                else if (rtsiCustomer.Visibility == ElementVisibility.Visible)
                {
                    if (cboCustomer.Text.Equals(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                        MessageBox.Show(this, "Please choose Customer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        Search();
                }
                else
                    Search();
            }
        }

        private void radCalendarBegin_SelectionChanged(object sender, EventArgs e)
        {
            lblDateBegin.Text = Utility.Utility.ConvertDateToString(radCalendarBegin.SelectedDate);
            radCalendarBegin.Visible = false;
        }

        private void radCalendarEnd_SelectionChanged(object sender, EventArgs e)
        {
            lblDateEnd.Text = Utility.Utility.ConvertDateToString(radCalendarEnd.SelectedDate);
            radCalendarEnd.Visible = false;
        }

        private void imageButtonDateEnd_Click(object sender, EventArgs e)
        {
            radCalendarEnd.Visible = true;
        }

        private void imageButtonDateStart_Click(object sender, EventArgs e)
        {
            radCalendarBegin.Visible = true;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new PrintLeadTime().ShowDialog();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            new newLeadTime().ShowDialog();
        }


    }
}
