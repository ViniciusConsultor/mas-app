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

            if (File.Exists(Constant.VisitaJayaPerkasaApplication.nameFile))
            {
                TextReader tr = new StreamReader(Constant.VisitaJayaPerkasaApplication.nameFile);
                Constant.VisitaJayaPerkasaApplication.connectionString = tr.ReadLine().Trim() + Constant.VisitaJayaPerkasaApplication.connectionString;
                tr.Close();
            }
            else {
                TextWriter tw = new StreamWriter(Constant.VisitaJayaPerkasaApplication.nameFile);
                tw.WriteLine("Data Source = localhost;");
                tw.Close();

                Constant.VisitaJayaPerkasaApplication.connectionString = "Data Source = localhost;" + Constant.VisitaJayaPerkasaApplication.connectionString;
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

                if (UserProfile.user == null)
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
