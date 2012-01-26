using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Entities;
using VisitaJayaPerkasa.SqlRepository;

namespace VisitaJayaPerkasa.Control.ConditionControl
{
    public partial class ConditionEdit : UserControl
    {
        private bool wantToCreateCondition;
        private Condition condition;

        public ConditionEdit(Condition condition)
        {
            InitializeComponent();

            this.condition = condition;

            if (condition == null)
            {
                wantToCreateCondition = true;
            }
            else
            {
                wantToCreateCondition = false;
                etConditionCode.Text = condition.ConditionCode;
                etConditionName.Text = condition.ConditionName;
            }
        }

        private void radButtonSave_Click(object sender, EventArgs e)
        {
            if (etConditionCode.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill condition code", "Information");
            else if (etConditionName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill condition name", "Information");
            else
            {
                SqlConditionRepository sqlConditionRepository = new SqlConditionRepository();
                SqlParameter[] param;

                if (wantToCreateCondition)
                {
                    //Check city code has already exists ?
                    param = SqlUtility.SetSqlParameter(new string[] { "condition_code" }, new object[] { etConditionCode.Text.Trim() });
                    if (sqlConditionRepository.CheckConditionCode(param, Guid.Empty, true))
                    {
                        DialogResult dResult = MessageBox.Show(this, "City has already deleted. Do you want to activate ?", "Confirmation", MessageBoxButtons.YesNo);
                        if (dResult == DialogResult.Yes)
                        {
                            param = SqlUtility.SetSqlParameter(new string[] { "condition_id", "condition_code", "condition_name", "deleted" }, new object[] { Guid.NewGuid(), etConditionCode.Text.Trim(), etConditionName.Text.Trim(), 0 });

                            if (sqlConditionRepository.ActivateCondition(param))
                            {
                                MessageBox.Show(this, "Success Activate Condition", "Information");
                                radButtonClose.PerformClick();
                            }
                            else
                                MessageBox.Show(this, "Cannot Activate Condition", "Information");

                            param = null;
                        }
                        return;
                    }
                    else if (sqlConditionRepository.CheckConditionCode(param, Guid.Empty))
                    {
                        MessageBox.Show(this, "Condition has already exists", "Information");
                        return;
                    }

                    param = null;
                    param = SqlUtility.SetSqlParameter(new string[] { "condition_id", "condition_code", "condition_name", "deleted" }, new object[] { Guid.NewGuid(), etConditionCode.Text.Trim(), etConditionName.Text.Trim(), 0 });

                    if (sqlConditionRepository.CreateCondition(param))
                    {
                        MessageBox.Show(this, "Success create condition", "Information");
                        radButtonClose.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Create condition", "Information");
                    }
                }
                else
                {
                    param = SqlUtility.SetSqlParameter(new string[] { "condition_code", "condition_name", "condition_id" }, new object[] { etConditionCode.Text.Trim(), etConditionName.Text.Trim(), condition.ID });

                    if (sqlConditionRepository.CheckConditionCode(param, this.condition.ID))
                    {
                        MessageBox.Show(this, "condition has already exist. if it has already deleted. you must activate it with create new data", "Information");
                        return;
                    }

                    if (sqlConditionRepository.EditCondition(param))
                    {
                        MessageBox.Show(this, "Success Edit condition", "Information");
                        radButtonClose.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Edit condition", "Information");
                    }
                }

                param = null;
                sqlConditionRepository = null;
            }
        }

        private void radButtonClose_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new ConditionList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
