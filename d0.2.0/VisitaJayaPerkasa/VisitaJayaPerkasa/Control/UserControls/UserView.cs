using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.Entities;

namespace VisitaJayaPerkasa.Control.UserControls
{
    public partial class UserView : UserControl
    {
        private User user;

        public UserView(User user)
        {
            InitializeComponent();
            this.user = user;

            radLabelAddress.Text = Utility.Utility.DisplayNullValues(user.Address);
            radLabelUserName.Text = Utility.Utility.DisplayNullValues(user.UserName);
            radLabelPhoneNumber.Text = Utility.Utility.DisplayNullValues(user.MobilePhoneNumber);
            radLabelMarital.Text = Utility.Utility.DisplayNullValues(user.MaritalStatus);
            radLabelFirstName.Text = Utility.Utility.DisplayNullValues(user.FirstName);
            radLabelLastName.Text = Utility.Utility.DisplayNullValues(user.LastName);
            radLabelEmail.Text = Utility.Utility.DisplayNullValues(user.email);
            radLabelDOB.Text = Utility.Utility.DisplayNullValues(Utility.Utility.ConvertDateToString(user.DateOfBirth));
            radLabelGender.Text = Utility.Utility.DisplayNullValues(user.Gender);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new UserList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
