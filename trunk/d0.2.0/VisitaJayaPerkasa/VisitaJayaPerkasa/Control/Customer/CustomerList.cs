using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using VisitaJayaPerkasa.SqlRepository;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Control.Customer
{
    public partial class CustomerList : UserControl
    {
        private SqlCustomerRepository sqlCustomerRepository;
        private List<VisitaJayaPerkasa.Entities.Customer> listCustomer;
        private List<VisitaJayaPerkasa.Entities.Customer> showListCustomer;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        public CustomerList()
        {
            InitializeComponent();
            pageSize = 15;
            LoadData();
        }

        public void LoadData()
        {
            sqlCustomerRepository = new SqlCustomerRepository();
            listCustomer = null;

            string searchValue = radTextBoxElementSearchWord.Text;
            string searchKey = radComboBoxElement.Text;

            listCustomer = sqlCustomerRepository.ListCustomers();
            if (listCustomer != null)
            {
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchKey))
                {
                    /*SqlParameter[] sqlParam;

                    switch (searchKey)
                    {
                        case "First Name":
                            sqlParam = SqlUtility.SetSqlParameter(new string[] { "first_name" }, new object[] { searchValue });
                            showListCustomer = listCustomer.Where(c => c.FirstName.Contains(searchValue)).ToList<VisitaJayaPerkasa.Entities.Customer>();
                            break;
                        case "Last Name":
                            sqlParam = SqlUtility.SetSqlParameter(new string[] { "last_name" }, new object[] { searchValue });
                            showListCustomer = listCustomer.Where(c => c.LastName.Contains(searchValue)).ToList<VisitaJayaPerkasa.Entities.Customer>();
                            break;
                    }

                    sqlParam = null;*/
                }
            }

            if (showListCustomer != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(showListCustomer.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;

            RefreshGrid();
        }

        public void RefreshGrid()
        {
            if (totalPage != 0)
                showListCustomer = listCustomer.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<VisitaJayaPerkasa.Entities.Customer>();
            else
                showListCustomer = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            CustomerGridView.DataSource = showListCustomer;
        }

        private void radButtonElementCreate_Click(object sender, EventArgs e)
        {
            /*VisitaJayaPerkasa.Entities.Customer customer = null;
            UserControl controllers = new CustomerEdit(customer);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);*/
        }

        private void radButtonElementBtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void radButtonElementNext_Click(object sender, EventArgs e)
        {
            if (totalPage > currentPage)
            {
                currentPage++;
                RefreshGrid();
            }
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

        private void CustomerGridView_DoubleClick(object sender, EventArgs e)
        {/*
            if (listCustomer != null)
            {
                GridViewRowInfo gridInfo = CustomerGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.Customer customer = listCustomer.Where(c => c.PersonID.ToString().Equals(id)).FirstOrDefault();


                UserControl controllers = new CustomerView(customer);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }*/
        }

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {
           /* if (showListCustomer != null)
            {
                GridViewRowInfo gridInfo = CustomerGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "person_id" }, new object[] { id });

                if (sqlCustomerRepository.DeleteUser(sqlParam))
                {
                    MessageBox.Show("Data Deleted !");
                    LoadData();
                }
                else
                    MessageBox.Show("Cannot Delete Data !");

                sqlParam = null;

                VisitaJayaPerkasa.Entities.Customer customer = null;
                UserControl controllers = new CustomerEdit(customer);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }*/
        }

    }
}
