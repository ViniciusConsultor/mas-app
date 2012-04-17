using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;
using Telerik.WinControls.UI;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Control.Transaction
{
    public partial class CustomerTransList : UserControl
    {
        private SqlCustomerTransRepository sqlCustomerTransRepository;
        private List<VisitaJayaPerkasa.Entities.CustomerTrans> listCustomerTrans;
        private List<VisitaJayaPerkasa.Entities.CustomerTrans> showListCustomerTrans;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        public CustomerTransList()
        {
            InitializeComponent();
            pageSize = 15;
            LoadData();
        }

        public void LoadData()
        {
            sqlCustomerTransRepository = new SqlCustomerTransRepository();
            listCustomerTrans = null;

            string searchValue = radTextBoxElementSearchWord.Text;
            string searchKey = radComboBoxElement.Text;

            listCustomerTrans = sqlCustomerTransRepository.ListCustomerTrans();

            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (listCustomerTrans != null)
            {
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchKey))
                {
                    switch (searchKey)
                    {
                        case "Customer Name":
                            showListCustomerTrans = listCustomerTrans.Where(c => c.CustomerName.Contains(searchValue)).ToList<VisitaJayaPerkasa.Entities.CustomerTrans>();
                            break;
                    }

                }
                else
                    showListCustomerTrans = listCustomerTrans;
            }
            else
                showListCustomerTrans = null;

            if (showListCustomerTrans != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(showListCustomerTrans.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;

            RefreshGrid();
        }

        public void RefreshGrid()
        {
            if (totalPage != 0)
                showListCustomerTrans = showListCustomerTrans.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<VisitaJayaPerkasa.Entities.CustomerTrans>();
            else
                showListCustomerTrans = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            CustomerTransGridView.DataSource = showListCustomerTrans;
        }

        private void radButtonElementCreate_Click(object sender, EventArgs e)
        {
            VisitaJayaPerkasa.Entities.CustomerTrans customerTrans = null;
            UserControl controllers = new CustomerTransEdit(customerTrans);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
        }

        private void radButtonElementRefresh_Click(object sender, EventArgs e)
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

        private void radButtonElementBtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void radComboBoxElement_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void CustomerTransGridView_DoubleClick(object sender, EventArgs e)
        {
            if (showListCustomerTrans != null)
            {
                GridViewRowInfo gridInfo = CustomerTransGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.CustomerTrans customerTrans = showListCustomerTrans.Where(c => c.CustomerTransID.ToString().Equals(id)).FirstOrDefault();


                UserControl controllers = new CustomerTransView(customerTrans);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {
            if (showListCustomerTrans != null)
            {
                GridViewRowInfo gridInfo = CustomerTransGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.CustomerTrans customerTrans = showListCustomerTrans.Where(c => c.CustomerTransID.ToString() == id).SingleOrDefault();

                UserControl controllers = new CustomerTransEdit(customerTrans);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }

        private void radButtonElementRemove_Click(object sender, EventArgs e)
        {
            if (showListCustomerTrans != null)
            {
                sqlCustomerTransRepository = new SqlCustomerTransRepository();
                DialogResult dResult = MessageBox.Show(this, "Are you sure want delete master and detail this customer transaction ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    GridViewRowInfo gridInfo = CustomerTransGridView.SelectedRows.First();
                    string id = gridInfo.Cells[0].Value.ToString();
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "id" }, new object[] { id });

                    if (sqlCustomerTransRepository.DeleteCustomerTrans(sqlParam))
                    {
                        MessageBox.Show("Data Deleted !");
                        LoadData();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Cannot Delete Data !");

                    sqlParam = null;
                }
            }
        }


    }
}
