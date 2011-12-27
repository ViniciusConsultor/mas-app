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
    public partial class PelayaranEdit : UserControl
    {
        private bool wantToCreateVessel;
        private VisitaJayaPerkasa.Entities.Pelayaran pelayaran;
        private List<VisitaJayaPerkasa.Entities.PelayaranDetail> listPelayaranDetail;

        public PelayaranEdit(VisitaJayaPerkasa.Entities.Pelayaran pelayaran)
        {
            InitializeComponent();
            this.pelayaran = pelayaran;

            if (pelayaran == null)
            {
                wantToCreateVessel = true;
                listPelayaranDetail = new List<Entities.PelayaranDetail>();
            }
            else
            {
                wantToCreateVessel = false;
                etPelayaranName.Text = pelayaran.Name;

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
            if (etPelayaranName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill pelayaran name", "Information");
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
            strSqlParameter[1] = "name";
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
            obj[1] = etPelayaranName.Text.Trim();
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
            if (etPelayaranName .Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "Please fill pelayaran name", "Information");
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


        /*
        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new PelayaranList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (etPelayaranName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill pelayaran name", "Information");
            else
            {
                SqlPelayaranRepository sqlPelayaranRepository = new SqlPelayaranRepository();
                SqlParameter[] sqlParam;


                if (wantToCreateVessel)
                {
                    sqlParam = SqlUtility.SetSqlParameter(new string[] { "name" }, new object[] { etPelayaranName.Text });
                    if (sqlPelayaranRepository.CheckPelayaran(sqlParam)) {
                        MessageBox.Show(this, "Please fill another pelayaran name (has already exists)", "Information");
                        return;
                    }

                    sqlParam = null;
                    sqlParam = SqlUtility.SetSqlParameter(new string[]{"pelayaran_id", "name", "deleted"}, new object[]{Guid.NewGuid(), etPelayaranName.Text, 0});
                    if (sqlPelayaranRepository.CreatePelayaran(sqlParam)) {
                        MessageBox.Show(this, "Success create pelayaran", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Create pelayaran", "Information");
                    }
                }
                else
                {
                    sqlParam = SqlUtility.SetSqlParameter(new string[] {"name" }, new object[] { etPelayaranName.Text });
                    VisitaJayaPerkasa.Entities.Pelayaran TempObj = sqlPelayaranRepository.CheckPelayaranForEdit(sqlParam);

                    if (TempObj == null)
                    {
                        sqlParam = SqlUtility.SetSqlParameter(new string[] { "name", "pelayaran_id" }, new object[] { etPelayaranName.Text, pelayaran.ID });
                        if (sqlPelayaranRepository.EditPelayaran(sqlParam))
                        {
                            MessageBox.Show(this, "Success Edit Pelayaran", "Information");
                            radButtonElement2.PerformClick();
                        }
                        else {
                            MessageBox.Show(this, "Cannot Edit Pelayaran", "Information");
                        }
                    }
                    else if (TempObj.ID == pelayaran.ID)
                    {
                        MessageBox.Show(this, "No any changed", "Information");
                        return;
                    }
                    else {
                        MessageBox.Show(this, "Name has already exists", "Information");
                        return;
                    }

                    sqlParam = null;
                    TempObj = null;
                    sqlPelayaranRepository = null;
                }
            }
        }
         */ 
    }
}
