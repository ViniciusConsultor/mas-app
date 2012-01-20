using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisitaJayaPerkasa.Control.Recipient
{
    public partial class RecipientView : UserControl
    {
        public RecipientView(VisitaJayaPerkasa.Entities.Recipient recipient)
        {
            InitializeComponent();

            lblRecipientName.Text = recipient.Name;
            lblSupplierName.Text = recipient.SupplierName;
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new RecipientList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
