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

namespace VisitaJayaPerkasa.Control.TypeCont
{
    public partial class TypeContEdit : UserControl
    {
        private VisitaJayaPerkasa.Entities.TypeCont typeCont;
        private bool wantToCreateVessel;

        public TypeContEdit(VisitaJayaPerkasa.Entities.TypeCont typeCont)
        {
            InitializeComponent();
            this.typeCont = typeCont;

            if (typeCont == null)
                wantToCreateVessel = true;
            else {
                wantToCreateVessel = false;
                etTypeCode.Text = typeCont.TypeCode;
                etTypeName.Text = typeCont.TypeName;
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new TypeContList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (etTypeCode.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill type code", "Information");
            else if (etTypeName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill type name", "Information");
            else
            {
                SqlTypeContRepository sqlTypeContRepository = new SqlTypeContRepository();

                SqlParameter[] param = SqlUtility.SetSqlParameter(new string[] { "type_code", "type_name" }, new object[] { etTypeCode.Text.Trim(), etTypeName.Text.Trim() });


                if (wantToCreateVessel)
                {
                    VisitaJayaPerkasa.Entities.TypeCont typeCont = new VisitaJayaPerkasa.Entities.TypeCont();
                    typeCont.ID = Guid.NewGuid();
                    typeCont.TypeCode = etTypeCode.Text.Trim();
                    typeCont.TypeName = etTypeName.Text.Trim();
                    typeCont.Deleted = 0;

                    if (sqlTypeContRepository.CheckTypeCont(param, Guid.Empty, true))
                    {
                        DialogResult dResult = MessageBox.Show(this, "Type cont has already deleted. Do you want to activate ?", "Confirmation", MessageBoxButtons.YesNo);
                        if (dResult == DialogResult.Yes)
                        {
                            SqlParameter[] parameters = SqlUtility.SetSqlParameter(new string[] { "type_id", "type_code", "type_name", "deleted" }
                            , new object[] { typeCont.ID, typeCont.TypeCode, typeCont.TypeName, typeCont.Deleted });

                            if (sqlTypeContRepository.ActivateTypeCont(parameters))
                            {
                                MessageBox.Show(this, "Success Activate Type Cont", "Information");
                                radButtonElement2.PerformClick();
                            }
                            else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                                MessageBox.Show(this, "Cannot Activate Type Cont", "Information");

                            parameters = null;
                        }
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (sqlTypeContRepository.CheckTypeCont(param, Guid.Empty))
                    {
                        MessageBox.Show(this, "Type cont has already exists", "Information");
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "type_id", "type_code", "type_name", "deleted" }
                        , new object[] { typeCont.ID, typeCont.TypeCode, typeCont.TypeName, typeCont.Deleted });
                    if (sqlTypeContRepository.CreateTypeCont(sqlParam))
                    {
                        MessageBox.Show(this, "Success create type cont", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show(this, "Cannot Create type cont", "Information");
                    }
                }
                else
                {
                    typeCont.ID = this.typeCont.ID;
                    typeCont.TypeCode = etTypeCode.Text.Trim();
                    typeCont.TypeName = etTypeName.Text.Trim();
                    typeCont.Deleted = 0;

                    if (sqlTypeContRepository.CheckTypeCont(param, typeCont.ID))
                    {
                        MessageBox.Show(this, "Type cont has already exist. if it has already deleted. you must activate it with create new data", "Information");
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "type_id", "type_code", "type_name", "deleted" }
                    , new object[] { typeCont.ID, typeCont.TypeCode, typeCont.TypeName, typeCont.Deleted });

                    if (sqlTypeContRepository.EditTypeCont(sqlParam))
                    {
                        MessageBox.Show(this, "Success edit type cont", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show(this, "Cannot edit type cont", "Information");
                    }
                }


            //    SqlParameter[] param;

            //    if (wantToCreateVessel)
            //    {
            //        //Check vessel code has already exists ?
            //        param = SqlUtility.SetSqlParameter(new string[] { "type_code" }, new object[] { etTypeCode.Text.Trim() });
            //        if (sqlTypeContRepository.CheckTypeCont(param))
            //        {
            //            MessageBox.Show(this, "Type Cont has already exists !", "Information");
            //            return;
            //        }

            //        param = null;
            //        param = SqlUtility.SetSqlParameter(new string[] { "type_id", "type_code", "type_name", "deleted" }, new object[] { Guid.NewGuid(), etTypeCode.Text.Trim(), etTypeName.Text.Trim(), 0 });

            //        if (sqlTypeContRepository.CreateTypeCont(param))
            //        {
            //            MessageBox.Show(this, "Success create type cont", "Information");
            //            radButtonElement2.PerformClick();
            //        }
            //        else
            //        {
            //            MessageBox.Show(this, "Cannot Create type cont", "Information");
            //        }
            //    }
            //    else
            //    {
            //        param = SqlUtility.SetSqlParameter(new string[] { "type_code", "type_name", "type_id" }, new object[] { etTypeCode.Text.Trim(), etTypeName.Text.Trim(), typeCont.ID });

            //        if (sqlTypeContRepository.EditTypeCont(param))
            //        {
            //            MessageBox.Show(this, "Success Edit type cont", "Information");
            //            radButtonElement2.PerformClick();
            //        }
            //        else
            //        {
            //            MessageBox.Show(this, "Cannot Edit type cont", "Information");
            //        }
            //    }

            //    param = null;
            //    sqlTypeContRepository = null;
            }
        }
    }
}
