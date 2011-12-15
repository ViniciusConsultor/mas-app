using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;
using System.Data.SqlClient;
using Telerik.WinControls.UI;

namespace VisitaJayaPerkasa.Control.Pelayaran
{
    public partial class PelayaranList : UserControl
    {
        private SqlPelayaranRepository sqlPelataranRepository;
        private List<VisitaJayaPerkasa.Entities.Pelayaran> pelayaran;
        private List<VisitaJayaPerkasa.Entities.Pelayaran> showPelayaran;

        private int currentPage;
        private int pageSize;
        private int totalPage;

        public PelayaranList()
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
            sqlPelataranRepository = new SqlPelayaranRepository();
            pelayaran = null;

            string searchValue = radTextBoxElementSearchWord.Text;
            string searchKey = radComboBoxElement.Text;

            pelayaran = sqlPelataranRepository.GetPelayaran();
            if (pelayaran != null)
            {
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchKey))
                {
                    switch (searchKey)
                    {
                        case "Name":
                            showPelayaran = pelayaran.Where(c => c.Name.Contains(searchValue)).ToList<VisitaJayaPerkasa.Entities.Pelayaran>();
                            break;
                    }
                }
                else
                    showPelayaran = pelayaran;
            }
            else
                showPelayaran = null;

            if (showPelayaran != null)
            {
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(showPelayaran.Count() / Convert.ToDecimal(pageSize))));
                currentPage = 1;
            }
            else
                totalPage = 0;

            RefreshGrid();
        }


        public void RefreshGrid()
        {
            if (totalPage != 0)
                showPelayaran = showPelayaran.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<VisitaJayaPerkasa.Entities.Pelayaran>();
            else
                showPelayaran = null;

            radToolStripLabelIndexing.Text = currentPage + " / " + totalPage;
            PelayaranGridView.DataSource = showPelayaran;
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

        private void PelayaranGridView_DoubleClick(object sender, EventArgs e)
        {
            if (showPelayaran != null)
            {
                GridViewRowInfo gridInfo = PelayaranGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.Pelayaran pelayaran = showPelayaran.Where(c => c.ID.ToString().Equals(id)).FirstOrDefault();


                UserControl controllers = new PelayaranView(pelayaran);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }

        private void radButtonElementRemove_Click(object sender, EventArgs e)
        {
            if (showPelayaran != null)
            {
                sqlPelataranRepository = new SqlPelayaranRepository();
                DialogResult dResult = MessageBox.Show(this, "Are you sure want delete this data ? ", "Confirmation", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    GridViewRowInfo gridInfo = PelayaranGridView.SelectedRows.First();
                    string id = gridInfo.Cells[0].Value.ToString();
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "pelayaran_id" }, new object[] { id });

                    if (sqlPelataranRepository.DeletePelayaran(sqlParam))
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
            VisitaJayaPerkasa.Entities.Pelayaran pelayaran = null;
            UserControl controllers = new PelayaranEdit(pelayaran);
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
        }

        private void radButtonElementEdit_Click(object sender, EventArgs e)
        {
            if (showPelayaran != null)
            {
                GridViewRowInfo gridInfo = PelayaranGridView.SelectedRows.First();
                string id = gridInfo.Cells[0].Value.ToString();
                VisitaJayaPerkasa.Entities.Pelayaran pelayaran = showPelayaran.Where(c => c.ID.ToString() == id).SingleOrDefault();

                UserControl controllers = new PelayaranEdit(pelayaran);
                Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(controllers);
            }
        }
    }
}
