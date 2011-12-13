using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.Entities;
using VisitaJayaPerkasa.SqlRepository;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Control.UserControls
{
    public partial class UserEdit : UserControl
    {
        private bool wantToCreateUser;
        private SqlRoleRepository sqlRoleRepository;
        private SqlUserRepository sqlUserRepository;

        public UserEdit(User user)
        {
            InitializeComponent();
            sqlRoleRepository = new SqlRoleRepository();
            List<Role> listRole = sqlRoleRepository.GetRoles();
            cboUserRole.DataSource = listRole;
            cboUserRole.DisplayMember = "RoleName";
            cboUserRole.ValueMember = "ID";

            radioButtonMale.IsChecked = true;

            if (listRole != null)
            {
                if (user != null)
                {
                    wantToCreateUser = false;
                    etUserName.Text = user.UserName;
                    etFirstName.Text = user.FirstName;
                    etLastName.Text = user.LastName;
                    etEmail.Text = user.email;
                    etAddress.Text = user.Address;
                    etPassword.Text = user.Password;
                    etPasswordHint.Text = user.PasswordHint;
                    etPhone.Text = user.MobilePhoneNumber;
                    DOB.Value = user.DateOfBirth;
                    cbMarital.SelectedItem = user.MaritalStatus;
                }
                else
                {
                    wantToCreateUser = true;
                    DOB.Value = DateTime.Today;
                }
            }
            else {
                MessageBox.Show(this, "Cannot create/edit user, try again please", "Warning");
                radButtonElement2.PerformClick();
            }

            sqlRoleRepository = null;
        }

        private void cboMarital_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(8);
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new UserList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (wantToCreateUser)
            {
                if (etUserName.Text.Trim().Length == 0)
                    MessageBox.Show(this, "Please fill username", "Information");
                else if (etPassword.Text.Trim().Length == 0)
                    MessageBox.Show(this, "Please fill password", "Information");
                else if (cboUserRole.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                    MessageBox.Show(this, "Please select user role", "Information");
                else {
                    sqlUserRepository = new SqlUserRepository();
                    //Check username has already exists?
                    SqlParameter[] param = SqlUtility.SetSqlParameter(new string[]{"username"}, new object[]{etUserName.Text.Trim()});
                    if (sqlUserRepository.CheckUserName(param)) {
                        MessageBox.Show(this, "Username has already exists !", "Information");
                        return;
                    }

                    //Create user
                    User user = new User();
                    user.Address = etAddress.Text.Trim();
                    user.DateOfBirth = DOB.Value;
                    user.Deleted = 0;
                    user.email = etEmail.Text.Trim();
                    user.FirstName = etFirstName.Text.Trim();
                    user.LastName = etLastName.Text.Trim();
                    user.MaritalStatus = (cbMarital.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText)) ? null : cbMarital.Text;
                    user.MobilePhoneNumber = etPhone.Text.Trim();
                    user.Password = Utility.Utility.MD5(etPassword.Text);
                    user.PasswordHint = etPasswordHint.Text;
                    user.PersonID = Guid.NewGuid();
                    user.UserName = etUserName.Text.Trim();

                    user.RoleObj = new Role();
                    user.RoleObj.ID = Utility.Utility.ConvertToUUID(cboUserRole.SelectedValue.ToString());
                    SqlParameter []sqlParam = SqlUtility.SetSqlParameter(new string[] { "person_id", "username", "password", "password_hint", "email", "first_name", "last_name", "address", "date_of_birth", "marital_status", "gender", "mobile_phone_number", "deleted", "user_role_id", "user_id", "role_id", "deleted" }
                        , new object[] { user.PersonID, user.UserName, user.Password, user.PasswordHint, user.email, user.FirstName, user.LastName, user.Address, user.DateOfBirth, user.MaritalStatus, user.Gender, user.MobilePhoneNumber, user.Deleted, Guid.NewGuid(), user.PersonID, user.RoleObj.ID, user.Deleted });

                    if (sqlUserRepository.CreateUser(sqlParam))
                    {
                        MessageBox.Show(this, "Success create user", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else {
                        MessageBox.Show(this, "Cannot Create User", "Information");
                    }
                }
            }
            else { 
                
            }
        }

        private void cboUserRole_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cbMarital_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }
    }
}
