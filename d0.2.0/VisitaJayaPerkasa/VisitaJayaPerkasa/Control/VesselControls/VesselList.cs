using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using Telerik.WinControls.UI;

namespace VisitaJayaPerkasa.Control.VesselControls
{
    public partial class VesselList : UserControl
    {
        private SqlVesselRepository sqlVesselRepository;
        private List<Vessel> vessels;
        private List<Vessel> ShowVessel;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        public VesselList()
        {
            InitializeComponent();
            pageSize = 15;
            LoadData();
        }

        private void radButtonElementBtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            sqlVesselRepository = new SqlVesselRepository();
            vessels = null;

            string searchValue = radTextBoxElementSearchWord.Text;
            string searchKey = radComboBoxElement.Text;

            vessels = sqlVesselRepository.GetVessels();
            if (vessels != null)
            {
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchKey))
                {
                    switch (searchKey)
                    {
                        case "Code":
                            ShowVessel = vessels.Where(c => c.VesselCode.Contains(searchValue)).ToList<Vessel>();
                            break;
                        case "Name":
                            ShowVessel = vessels.Where(c => c.VesselName.Contains(searchValue)).ToList<Vessel>();
                            break;
                    }

                }
                else
                    ShowVessel = vessels;
            }
            else
                ShowVessel = null;

            if (ShowVessel != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ShowVessel.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;

            RefreshGrid();
        }


        public void RefreshGrid()
        {
            if (totalPage != 0)
                ShowVessel = ShowVessel.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<Vessel>();
            else
                ShowVessel = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            VesselGridView.DataSource = ShowVessel;
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

        private void VesselGridView_DoubleClick(object sender, EventArgs e)
        {
            if (ShowVessel != null)
            {
                GridViewRowInfo gridInfo = VesselGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                Vessel vessel = vessels.Where(c => c.ID.ToString().Equals(id)).FirstOrDefault();
                

                UserControl controllers = new VesselView(vessel);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }

        private void radButtonElementRemove_Click(object sender, EventArgs e)
        {
            
            if (ShowVessel != null)
            {
                sqlVesselRepository = new SqlVesselRepository();
                DialogResult dResult = MessageBox.Show(this, "Are you sure want delete this data ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    GridViewRowInfo gridInfo = VesselGridView.SelectedRows.First();
                    string id = gridInfo.Cells[0].Value.ToString();
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "vessel_id" }, new object[] { id });

                    if (sqlVesselRepository.DeleteVessel(sqlParam))
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

        private void radButtonElementCreate_Click(object sender, EventArgs e)
        {
            Vessel vessel = null;
            UserControl controllers = new VesselEdit(vessel);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
        }

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {
            if (ShowVessel != null)
            {
                GridViewRowInfo gridInfo = VesselGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                Vessel vessel = ShowVessel.Where(c => c.ID.ToString() == id).SingleOrDefault();

                UserControl controllers = new VesselEdit(vessel);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }
    }
}
