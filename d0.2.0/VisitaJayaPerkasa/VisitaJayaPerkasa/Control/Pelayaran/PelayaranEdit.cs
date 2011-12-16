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

namespace VisitaJayaPerkasa.Control.Pelayaran
{
    public partial class PelayaranEdit : UserControl
    {
        private bool wantToCreateVessel;
        private VisitaJayaPerkasa.Entities.Pelayaran pelayaran;

        public PelayaranEdit(VisitaJayaPerkasa.Entities.Pelayaran pelayaran)
        {
            InitializeComponent();
            this.pelayaran = pelayaran;

            if (pelayaran == null)
            {
                wantToCreateVessel = true;
            }
            else {
                wantToCreateVessel = false;
                etPelayaranName.Text = pelayaran.Name;
            }
        }

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
    }
}
