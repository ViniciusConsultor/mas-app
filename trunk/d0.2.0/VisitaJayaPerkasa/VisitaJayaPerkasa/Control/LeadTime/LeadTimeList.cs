using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisitaJayaPerkasa.Control.LeadTime
{
    public partial class LeadTimeList : UserControl
    {
        private List<VisitaJayaPerkasa.Entities.LeadTime> leadTimes;
        private List<VisitaJayaPerkasa.Entities.LeadTime> showLeadTimes;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        public LeadTimeList()
        {
            InitializeComponent();
            pageSize = 15;
            LoadData();
        }

        public void LoadData()
        {/*
            sqlUserRepository = new SqlUserRepository();
            Users = null;

            string searchValue = radTextBoxElementSearchWord.Text;
            string searchKey = radComboBoxElement.Text;

            Users = sqlUserRepository.GetUsers();
            if (Users != null)
            {
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchKey))
                {
                    SqlParameter[] sqlParam;

                    switch (searchKey)
                    {
                        case "First Name":
                            sqlParam = SqlUtility.SetSqlParameter(new string[] { "first_name" }, new object[] { searchValue });
                            Users = Users.Where(c => c.FirstName.Contains(searchValue)).ToList<User>();
                            break;
                        case "Last Name":
                            sqlParam = SqlUtility.SetSqlParameter(new string[] { "last_name" }, new object[] { searchValue });
                            Users = Users.Where(c => c.LastName.Contains(searchValue)).ToList<User>();
                            break;
                    }

                    sqlParam = null;
                }
            }

            if (Users != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Users.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;

            RefreshGrid();*/
        }

        public void RefreshGrid()
        {/*
            if (totalPage != 0)
                ShowUser = Users.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<User>();
            else
                ShowUser = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            UserGridView.DataSource = ShowUser;*/
        }

        private void radButtonElementPrev_Click(object sender, EventArgs e)
        {
            if (currentPage != 1)
            {
                currentPage--;
                RefreshGrid();
            }
        }

        private void radComboBoxElement_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void radButtonElementRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void PelayaranGridView_DoubleClick(object sender, EventArgs e)
        {/*
            if (Users != null)
            {
                GridViewRowInfo gridInfo = UserGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                User user = Users.Where(c => c.PersonID.ToString().Equals(id)).FirstOrDefault();


                UserControl controllers = new UserView(user);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }*/
        }

        private void radButtonElementRemove_Click(object sender, EventArgs e)
        {/*
            if (Users != null)
            {
                sqlUserRepository = new SqlUserRepository();
                DialogResult dResult = MessageBox.Show(this, "Are you sure want delete this data ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    GridViewRowInfo gridInfo = UserGridView.SelectedRows.First();
                    string id = gridInfo.Cells[0].Value.ToString();
                    User user = ShowUser.Where(c => c.PersonID.ToString() == id).SingleOrDefault();


                    Guid roleID = sqlUserRepository.GetUserRole(user.PersonID);
                    if (roleID != Guid.Empty)
                    {
                        user.RoleObj = new Role();
                        user.RoleObj.ID = roleID;
                    }

                    sqlUserRepository = null;
                    UserControl controllers = new UserEdit(user);
                    Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
                }
            }*/
        }

        private void radButtonElementCreate_Click(object sender, EventArgs e)
        {/*
            VisitaJayaPerkasa.Entities.LeadTime leadTime = null;
            UserControl controllers = new LeadTimeEdit(leadTime);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
        */}

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {/*
            if (ShowUser != null)
            {
                GridViewRowInfo gridInfo = UserGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "person_id" }, new object[] { id });

                if (sqlUserRepository.DeleteUser(sqlParam))
                {
                    MessageBox.Show("Data Deleted !");
                    LoadData();
                }
                else
                    MessageBox.Show("Cannot Delete Data !");

                sqlParam = null;

                User user = null;
                UserControl controllers = new UserEdit(user);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }*/
        }
    }
}
