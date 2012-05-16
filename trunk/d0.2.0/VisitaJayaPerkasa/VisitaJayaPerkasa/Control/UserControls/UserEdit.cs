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
using System.Text.RegularExpressions;

namespace VisitaJayaPerkasa.Control.UserControls
{
    public partial class UserEdit : UserControl
    {
        private bool wantToCreateUser;
        private SqlRoleRepository sqlRoleRepository;
        private SqlUserRepository sqlUserRepository;
        private User user;

        public UserEdit(User user)
        {
            InitializeComponent();
            sqlRoleRepository = new SqlRoleRepository();
            List<Role> listRole = sqlRoleRepository.GetRoles();
            cboUserRole.DataSource = listRole;
            cboUserRole.DisplayMember = "RoleName";
            cboUserRole.ValueMember = "ID";

            //radioButtonMale.IsChecked = true;

            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (listRole != null)
            {
                if (user != null)
                {
                    wantToCreateUser = false;
                    this.user = user;
                    etUserName.Text = user.UserName;
                    etFirstName.Text = user.FirstName;
                    etLastName.Text = user.LastName;
                    etEmail.Text = user.email;
                    etAddress.Text = user.Address;
                    etPassword.Text = user.Password;
                    etPasswordHint.Text = user.PasswordHint;
                    etMobilePhone.Text = user.MobilePhoneNumber;
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
            if (etUserName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill username", "Information");
            else if (etPassword.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill password", "Information");
            else if (cboUserRole.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please select user role", "Information");
            else if (!Regex.Match(etEmail.Text.Trim(), @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$").Success)
            {
                MessageBox.Show(this, "invalid email", "Information");
            }
            else {
                sqlUserRepository = new SqlUserRepository();
                //Check username has already exists?
                SqlParameter[] param = SqlUtility.SetSqlParameter(new string[]{"username"}, new object[]{etUserName.Text.Trim()});
                

                if (wantToCreateUser)
                {
                    User user = new User();
                    user.Address = etAddress.Text.Trim();
                    user.DateOfBirth = DOB.Value;
                    user.Deleted = 0;
                    user.email = etEmail.Text.Trim();
                    user.FirstName = etFirstName.Text.Trim();
                    user.LastName = etLastName.Text.Trim();
                    user.MaritalStatus = (cbMarital.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText)) ? null : cbMarital.Text;
                    user.MobilePhoneNumber = etMobilePhone.Text.Trim();
                    user.Password = Utility.Utility.MD5(etPassword.Text);
                    user.PasswordHint = etPasswordHint.Text;
                    user.PersonID = Guid.NewGuid();
                    user.UserName = etUserName.Text.Trim();

                    user.RoleObj = new Role();
                    user.RoleObj.ID = Utility.Utility.ConvertToUUID(cboUserRole.SelectedValue.ToString());

                    if (sqlUserRepository.CheckUserName(param, Guid.Empty, true))
                    {
                        DialogResult dResult = MessageBox.Show(this, "Username has already deleted. Do you want to activate ?", "Confirmation", MessageBoxButtons.YesNo);
                        if (dResult == DialogResult.Yes)
                        {
                            SqlParameter[] parameters = SqlUtility.SetSqlParameter(new string[] { "person_id", "username", "password", "password_hint", "email", "first_name", "last_name", "address", "date_of_birth", "marital_status", "gender", "mobile_phone_number", "deleted", "user_role_id", "user_id", "role_id", "deleted" }
                            , new object[] { user.PersonID, user.UserName, user.Password, user.PasswordHint, user.email, user.FirstName, user.LastName, user.Address, user.DateOfBirth, user.MaritalStatus, user.Gender, user.MobilePhoneNumber, user.Deleted, Guid.NewGuid(), user.PersonID, user.RoleObj.ID, user.Deleted });

                            if (sqlUserRepository.ActivateUser(parameters))
                            {
                                MessageBox.Show(this, "Success Activate User", "Information");
                                radButtonElement2.PerformClick();
                            }
                            else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                            {
                                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show(this, "Cannot Activate User", "Information");
                            
                            parameters = null;
                        }
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (sqlUserRepository.CheckUserName(param, Guid.Empty))
                    {
                        MessageBox.Show(this, "Username has already exists", "Information");
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //Create user 
                    SqlParameter []sqlParam = SqlUtility.SetSqlParameter(new string[] { "person_id", "username", "password", "password_hint", "email", "first_name", "last_name", "address", "date_of_birth", "marital_status", "gender", "mobile_phone_number", "deleted", "user_role_id", "user_id", "role_id", "deleted" }
                        , new object[] { user.PersonID, user.UserName, user.Password, user.PasswordHint, user.email, user.FirstName, user.LastName, user.Address, user.DateOfBirth, user.MaritalStatus, user.Gender, user.MobilePhoneNumber, user.Deleted, Guid.NewGuid(), user.PersonID, user.RoleObj.ID, user.Deleted });

                    if (sqlUserRepository.CreateUser(sqlParam))
                    {
                        MessageBox.Show(this, "Success create user", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else {
                        MessageBox.Show(this, "Cannot Create User", "Information");
                    }
                }
                else{ 
                    User user = new User();
                    user.Address = etAddress.Text.Trim();
                    user.DateOfBirth = DOB.Value;
                    user.Deleted = 0;
                    user.email = etEmail.Text.Trim();
                    user.FirstName = etFirstName.Text.Trim();
                    user.LastName = etLastName.Text.Trim();
                    user.MaritalStatus = (cbMarital.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText)) ? null : cbMarital.Text;
                    user.MobilePhoneNumber = etMobilePhone.Text.Trim();
                    user.Password = Utility.Utility.MD5(etPassword.Text);
                    user.PasswordHint = etPasswordHint.Text;
                    user.PersonID = this.user.PersonID;
                    user.UserName = etUserName.Text.Trim();

                    user.RoleObj = new Role();
                    user.RoleObj.ID = Utility.Utility.ConvertToUUID(cboUserRole.SelectedValue.ToString());
                    
                    
                    if (sqlUserRepository.CheckUserName(param, user.PersonID))
                    {
                        MessageBox.Show(this, "Username has already exist. if it has already deleted. you must activate it with create new data", "Information");
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    SqlParameter []sqlParam = SqlUtility.SetSqlParameter(new string[] { "person_id", "username", "password", "password_hint", "email", "first_name", "last_name", "address", "date_of_birth", "marital_status", "gender", "mobile_phone_number", "deleted", "user_id", "role_id", "deleted" }
                        , new object[] { user.PersonID, user.UserName, user.Password, user.PasswordHint, user.email, user.FirstName, user.LastName, user.Address, user.DateOfBirth, user.MaritalStatus, user.Gender, user.MobilePhoneNumber, user.Deleted,  user.PersonID, user.RoleObj.ID, user.Deleted });

                    if (sqlUserRepository.EditUser(sqlParam))
                    {
                        MessageBox.Show(this, "Success edit user", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else {
                        MessageBox.Show(this, "Cannot edit User", "Information");
                    }
                }
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
