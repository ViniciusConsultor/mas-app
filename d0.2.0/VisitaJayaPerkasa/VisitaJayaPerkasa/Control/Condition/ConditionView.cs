using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.Entities;

namespace VisitaJayaPerkasa.Control.ConditionControl
{
    public partial class ConditionView : UserControl
    {
        public ConditionView(Condition condition)
        {
            InitializeComponent();

            lblConditionCode.Text = Utility.Utility.DisplayNullValues(condition.ConditionCode);
            lblConditionName.Text = Utility.Utility.DisplayNullValues(condition.ConditionName);
        }

        private void radButtonClose_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new ConditionList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
