using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using VisitaJayaPerkasa.Entities;
using VisitaJayaPerkasa.SqlRepository;
using System.Data.SqlClient;
using System.IO;

namespace VisitaJayaPerkasa.Form
{
    public partial class LoginForm : Telerik.WinControls.UI.RadForm
    {
        public LoginForm()
        {
            InitializeComponent();
            VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.loginForm = this;

            Guid id = Guid.NewGuid();

            if (File.Exists(Constant.VisitaJayaPerkasaApplication.nameFile))
            {
                TextReader tr = new StreamReader(Constant.VisitaJayaPerkasaApplication.nameFile);
                String[] temp = Utility.Utility.DecryptString(tr.ReadLine().Trim()).Split('#');
                Constant.VisitaJayaPerkasaApplication.connectionString = temp[0] + Constant.VisitaJayaPerkasaApplication.connectionString + temp[1];
                tr.Close();
                temp = null;
            }
            else {
                string str = "9vywex8RZH8TL8c5df9a3wAjz98t0qaTxQ4VjK4DCmHiMRprRMeszINYlyqxfA66eWkDCZ6cwKG6ZpREcBsqvA==";
                TextWriter tw = new StreamWriter(Constant.VisitaJayaPerkasaApplication.nameFile);
                tw.WriteLine(str);
                tw.Close();

                string[] temp = Utility.Utility.DecryptString(str).Split('#');
                Constant.VisitaJayaPerkasaApplication.connectionString = temp[0] + Constant.VisitaJayaPerkasaApplication.connectionString + temp[1];
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Utility.Utility.IsStringNullorEmpty(etUserName.Text)) 
                MessageBox.Show(this, "Please fill username", "Caution", MessageBoxButtons.OK);
            else if (Utility.Utility.IsStringNullorEmpty(etPassword.Text))
                MessageBox.Show(this, "Please fill password", "Caution", MessageBoxButtons.OK);
            else {
                SqlUserRepository sqlUser = new SqlUserRepository();
                SqlParameter []sqlparam = SqlUtility.SetSqlParameter(new string[]{"UserName", "Password"}, new object[]{etUserName.Text, Utility.Utility.MD5(etPassword.Text)});
                
                sqlUser.ValidateLogin(sqlparam);
                sqlparam = null;
                sqlUser = null;

                if (!Constant.VisitaJayaPerkasaApplication.anyConnection) {
                    MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (UserProfile.user == null)
                {
                    MessageBox.Show(this, "Please correct username and password", "Caution", MessageBoxButtons.OK);
                }
                else
                {
                    VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.loginForm.Hide();
                    etPassword.Text = "";
                    new MainForm().Show();
                }
              
            }
        }

        private void etPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                btnLogin.PerformClick();
            }
        }

    }
}
