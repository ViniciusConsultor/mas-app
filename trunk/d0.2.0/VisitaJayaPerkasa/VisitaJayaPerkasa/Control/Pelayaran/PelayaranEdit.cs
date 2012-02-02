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
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.Control.Pelayaran
{
    public partial class PelayaranEdit : UserControl
    {
        private bool wantToCreateVessel;
        private VisitaJayaPerkasa.Entities.Pelayaran pelayaran;
        private List<VisitaJayaPerkasa.Entities.PelayaranDetail> listPelayaranDetail;

        public PelayaranEdit(VisitaJayaPerkasa.Entities.Pelayaran pelayaran)
        {
            InitializeComponent();
            this.pelayaran = pelayaran;
            List<VisitaJayaPerkasa.Entities.Supplier> listSupplier;
            SqlSupplierRepository sqlSupplierRepository = new SqlSupplierRepository();
            listSupplier = sqlSupplierRepository.ListSuppliers();

            cbSupplier.DataSource = listSupplier;
            cbSupplier.DisplayMember = "SupplierName";
            cbSupplier.ValueMember = "Id";

            if (pelayaran == null)
            {
                wantToCreateVessel = true;
                listPelayaranDetail = new List<Entities.PelayaranDetail>();

                cbSupplier.SelectedIndex = -1;
                cbSupplier.Text = VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText;
            }
            else
            {
                wantToCreateVessel = false;
                cbSupplier.SelectedValue = pelayaran.supplierID;

                SqlPelayaranRepository sqlPelayaranRepository = new SqlPelayaranRepository();
                listPelayaranDetail = sqlPelayaranRepository.ListPelayaranDetail(pelayaran.ID);

                if (listPelayaranDetail != null)
                    PelayaranDetailGridView.DataSource = listPelayaranDetail;
                else
                    listPelayaranDetail = new List<VisitaJayaPerkasa.Entities.PelayaranDetail>();

                sqlPelayaranRepository = null;
            }
        }

        private void ResetDetailData()
        {
            etVesselCode.Text = "";
            etVesselName.Text = "";
            chkStatus.Checked = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbSupplier.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose pelayaran name", "Information");
            else if(etVesselCode.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill vessel code", "Information");
            else if (etVesselName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill vessel name", "Information");
            else
            {
                VisitaJayaPerkasa.Entities.PelayaranDetail pelDetail = new Entities.PelayaranDetail();

                pelDetail.VesselCode = etVesselCode.Text.Trim();
                pelDetail.VesselName = etVesselName.Text.Trim();
                pelDetail.StatusPinjaman = Convert.ToInt32(chkStatus.Checked);
                for (int i = 0; i < listPelayaranDetail.Count; i++)
                {
                    if (listPelayaranDetail.ElementAt(i).VesselCode == pelDetail.VesselCode &&
                        listPelayaranDetail.ElementAt(i).VesselName == pelDetail.VesselName)
                    {
                        MessageBox.Show(this, "Data has already exist", "Information");
                        return;
                    }
                }


                listPelayaranDetail.Add(pelDetail);
                PelayaranDetailGridView.DataSource = typeof(List<VisitaJayaPerkasa.Entities.PelayaranDetail>);
                PelayaranDetailGridView.DataSource = listPelayaranDetail;

                pelDetail = null;
                ResetDetailData();
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new PelayaranList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            listPelayaranDetail.Clear();
            PelayaranDetailGridView.DataSource = null;
        }

        private void btnRemoveGrid_Click(object sender, EventArgs e)
        {
            List<VisitaJayaPerkasa.Entities.PelayaranDetail> listTemp = new List<VisitaJayaPerkasa.Entities.PelayaranDetail>();

            if (PelayaranDetailGridView.SelectedRows.Count == 1)
            {
                int n = 0;
                for (int i = 0; i < listPelayaranDetail.Count; i++)
                {
                    GridViewRowInfo gridInfo = PelayaranDetailGridView.SelectedRows.First();
                    string vesselCode = gridInfo.Cells[0].Value.ToString();
                    string vesselName = (gridInfo.Cells[1].Value != null) ? gridInfo.Cells[1].Value.ToString() : null;

                    if (listPelayaranDetail.ElementAt(i).VesselCode == vesselCode &&
                        listPelayaranDetail.ElementAt(i).VesselName == vesselName)
                        n = i;
                    else
                        listTemp.Add(listPelayaranDetail.ElementAt(i));
                }

                listPelayaranDetail.RemoveAt(n);
                listPelayaranDetail = null;
                listPelayaranDetail = listTemp;

                PelayaranDetailGridView.DataSource = typeof(List<VisitaJayaPerkasa.Entities.PelayaranDetail>);
                PelayaranDetailGridView.DataSource = listPelayaranDetail;
            }
            else
                MessageBox.Show(this, "Please select row who will be removed", "Information");
        }

        private string[] getStringSqlParameter()
        {
            //rowcount * 8 is number of field of customer detail
            // + 9 is number of field of customer
            string[] strSqlParameter = new string[(PelayaranDetailGridView.RowCount * 6) + 3];
            strSqlParameter[0] = "pelayaran_id";
            strSqlParameter[1] = "supplier_id";
            strSqlParameter[2] = "deleted";

            int j = 3;
            for (int i = 0; i < PelayaranDetailGridView.RowCount; i++)
            {
                strSqlParameter[j++] = "pelayaran_detail_id";
                strSqlParameter[j++] = "pelayaran_id";
                strSqlParameter[j++] = "vessel_code";
                strSqlParameter[j++] = "vessel_name";
                strSqlParameter[j++] = "status_pinjaman";
                strSqlParameter[j++] = "deleted";
            }

            return strSqlParameter;
        }

        private object[] GetObjSqlParameter(Guid id)
        {
            //rowcount * 8 is number of field of customer detail
            // + 9 is number of field of customer
            object[] obj = new object[(PelayaranDetailGridView.RowCount * 6) + 3];
            obj[0] = id;
            obj[1] = cbSupplier.SelectedValue;
            obj[2] = 0;

            int i = 3;
            for (int j = 0; j < PelayaranDetailGridView.RowCount; j++)
            {
                obj[i++] = Guid.NewGuid();
                obj[i++] = id;
                obj[i++] = SqlUtility.isDBNULL(PelayaranDetailGridView.Rows[j].Cells[0].Value + "");
                obj[i++] = SqlUtility.isDBNULL(PelayaranDetailGridView.Rows[j].Cells[1].Value + "");
                obj[i++] = SqlUtility.isDBNULL(PelayaranDetailGridView.Rows[j].Cells[2].Value + "");
                obj[i++] = 0;
            }

            return obj;
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (cbSupplier.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
            {
                MessageBox.Show(this, "Please choose pelayaran name", "Information");
            }
            else
            {
                if (PelayaranDetailGridView.RowCount == 0)
                {
                    DialogResult dResult = MessageBox.Show(this, "Are you sure want save this data without pelayaran detail ? ", "Confirmation", MessageBoxButtons.YesNo);
                    if (dResult == DialogResult.Yes)
                    {
                        SaveData();
                    }
                }
                else
                    SaveData();
            }
        }


        private void SaveData()
        {
            SqlPelayaranRepository sqlPelayaranRepository = null;

            if (wantToCreateVessel)
            {
                sqlPelayaranRepository = new SqlPelayaranRepository();
                Guid newGuid = Guid.NewGuid();

                string[] strSqlParam = getStringSqlParameter();
                object[] objSqlParam = GetObjSqlParameter(newGuid);
                SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(strSqlParam, objSqlParam);

                if (sqlPelayaranRepository.CheckPelayaran(sqlParam, Guid.Empty, true))
                {
                    DialogResult dResult = MessageBox.Show(this, "Pelayaran has already deleted. Do you want to activate ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dResult == DialogResult.Yes)
                    {
                        if (sqlPelayaranRepository.ActivatePelayaran(sqlParam))
                        {
                            MessageBox.Show(this, "Success Activate Pelayaran", "Information");
                            radButtonElement2.PerformClick();
                        }
                        else
                            MessageBox.Show(this, "Cannot Activate Pelayaran", "Information");

                        sqlParam = null;
                    }
                    return;
                }
                else if (sqlPelayaranRepository.CheckPelayaran(sqlParam, Guid.Empty))
                {
                    MessageBox.Show(this, "Pelayaran has already exists", "Information");
                    return;
                }


                if (sqlPelayaranRepository.CreatePelayaran(sqlParam))
                {
                    MessageBox.Show(this, "Success insert pelayaran data", "Information");
                    radButtonElement2.PerformClick();
                }
                else
                {
                    MessageBox.Show(this, "Cannot insert pelayaran data", "Information");
                }
                sqlPelayaranRepository = null;
                strSqlParam = null;
                objSqlParam = null;
                sqlParam = null;
            }
            else
            {
                sqlPelayaranRepository = new SqlPelayaranRepository();
                string[] strSqlParam = getStringSqlParameter();
                object[] objSqlParam = GetObjSqlParameter(pelayaran.ID);
                SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(strSqlParam, objSqlParam);

                if (sqlPelayaranRepository.CheckPelayaran(sqlParam, this.pelayaran.ID))
                {
                    MessageBox.Show(this, "Pelayaran has already exist. if it has already deleted. you must activate it with create new data", "Information");
                    return;
                }

                if (sqlPelayaranRepository.EditPelayaran(sqlParam))
                {
                    MessageBox.Show(this, "Success edit pelayaran data", "Information");
                    radButtonElement2.PerformClick();
                }
                else
                {
                    MessageBox.Show(this, "Cannot edit pelayaran data", "Information");
                }

                sqlPelayaranRepository = null;
                strSqlParam = null;
                objSqlParam = null;
                sqlParam = null;
            }

        }

        private void radComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }


    }
}
