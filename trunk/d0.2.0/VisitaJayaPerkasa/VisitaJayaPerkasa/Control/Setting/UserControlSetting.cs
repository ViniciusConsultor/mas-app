using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisitaJayaPerkasa.Control.Setting
{
    public partial class UserControlSetting : UserControl
    {
        public UserControlSetting()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtIPAddress.Text.Trim().Equals(""))
                MessageBox.Show(this, "Please fill IP address", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (txtPass.Text.Trim().Equals(""))
                MessageBox.Show(this, "Please fill password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                txtResult.Text = Utility.Utility.EncryptString("Data Source = " + txtIPAddress.Text.Trim() + ";#User ID=sa;password=" + txtPass.Text.Trim() + ";");
        }

    }
}
