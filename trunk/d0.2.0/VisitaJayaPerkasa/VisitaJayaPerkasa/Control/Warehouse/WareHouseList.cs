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

namespace VisitaJayaPerkasa.Control.Warehouse
{
    public partial class WareHouseList : UserControl
    {
        private SqlWareHouseRepository sqlWareHouseRepository;
        private List<Entities.WareHouse> wareHouses;
        private List<Entities.WareHouse> ShowWareHouse;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        public WareHouseList()
        {
            InitializeComponent();
            pageSize = 15;
            LoadData();
        }

        private void radButtonElementCreate_Click(object sender, EventArgs e)
        {
            Entities.WareHouse warehouse = null;
            UserControl controllers = new WareHouseEdit(warehouse);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
        }

        public void LoadData()
        {
            sqlWareHouseRepository = new SqlWareHouseRepository();
            wareHouses = null;

            string searchValue = radTextBoxElementSearchWord.Text;
            string searchKey = radComboBoxElement.Text;

            wareHouses = sqlWareHouseRepository.GetWareHouse();
            if (wareHouses != null)
            {
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchKey))
                {
                    switch (searchKey)
                    {
                        case "Address":
                            ShowWareHouse = wareHouses.Where(c => c.Address.Contains(searchValue)).ToList<Entities.WareHouse>();
                            break;
                        case "Phone":
                            ShowWareHouse = wareHouses.Where(c => c.Phone.Contains(searchValue)).ToList<Entities.WareHouse>();
                            break;
                    }

                }
                else
                    ShowWareHouse = wareHouses;
            }
            else
                ShowWareHouse = null;

            if (ShowWareHouse != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ShowWareHouse.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;

            RefreshGrid();
        }

        public void RefreshGrid()
        {
            if (totalPage != 0)
                ShowWareHouse = ShowWareHouse.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<Entities.WareHouse>();
            else
                ShowWareHouse = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            WareHouseGridView.DataSource = ShowWareHouse;
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

        private void radButtonElementRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void radComboBoxElement_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void WareHouseGridView_DoubleClick(object sender, EventArgs e)
        {
            if (ShowWareHouse != null)
            {
                GridViewRowInfo gridInfo = WareHouseGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                Entities.WareHouse warehouse = wareHouses.Where(c => c.Id.ToString().Equals(id)).FirstOrDefault();


                UserControl controllers = new WareHouseView(warehouse);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }

        private void radButtonElementRemove_Click(object sender, EventArgs e)
        {
            if (ShowWareHouse != null)
            {
                sqlWareHouseRepository = new SqlWareHouseRepository();
                DialogResult dResult = MessageBox.Show(this, "Are you sure want delete this data ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    GridViewRowInfo gridInfo = WareHouseGridView.SelectedRows.First();
                    string id = gridInfo.Cells[0].Value.ToString();
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "stuffing_place_id" }, new object[] { id });

                    if (sqlWareHouseRepository.DeleteWareHouse(sqlParam))
                    {
                        MessageBox.Show("Data Deleted !");
                        LoadData();
                    }
                    else
                        MessageBox.Show("Cannot Delete Data !");

                    sqlParam = null;
                }
            }
        }

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {
            if (ShowWareHouse != null)
            {
                GridViewRowInfo gridInfo = WareHouseGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                Entities.WareHouse warehouse = ShowWareHouse.Where(c => c.Id.ToString() == id).SingleOrDefault();

                UserControl controllers = new WareHouseEdit(warehouse);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }
    }
}
