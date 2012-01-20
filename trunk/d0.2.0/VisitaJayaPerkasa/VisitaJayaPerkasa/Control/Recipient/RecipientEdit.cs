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

namespace VisitaJayaPerkasa.Control.Recipient
{
    public partial class RecipientEdit : UserControl
    {
        private bool wantToCreateRecipient;
        private VisitaJayaPerkasa.Entities.Recipient recipient;
        private List<VisitaJayaPerkasa.Entities.Supplier> listSupplier;
        private SqlRecipientRepository sqlRecipientRepository;

        public RecipientEdit(VisitaJayaPerkasa.Entities.Recipient recipient)
        {
            InitializeComponent();
            this.recipient = recipient;
            sqlRecipientRepository = new SqlRecipientRepository();
            SqlSupplierRepository sqlSupplierRepository = new SqlSupplierRepository();
            listSupplier = sqlSupplierRepository.GetListSupplierForRecipient();


            cbSupplier.DataSource = listSupplier;
            cbSupplier.ValueMember = "Id";
            cbSupplier.DisplayMember = "SupplierName";
            cbSupplier.SelectedIndex = -1;
            cbSupplier.Text = VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.cboDefaultText;

            if (recipient == null)
            {
                wantToCreateRecipient = true;
            }
            else
            {
                wantToCreateRecipient = false;
                etRecipientName.Text = recipient.Name;
                cbSupplier.SelectedItem = recipient.SupplierName;
            }
            sqlRecipientRepository = null;
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new RecipientList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if(cbSupplier.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose supplier", "Information");
            else if (etRecipientName.Text.Trim().Equals(""))
                MessageBox.Show(this, "Please fill recipient name", "Information");
            else { 
                SqlParameter[] param;
                sqlRecipientRepository = new SqlRecipientRepository();

                if (wantToCreateRecipient)
                {
                    //Check recipient name has already exists ?
                    param = SqlUtility.SetSqlParameter(new string[] { "recipient_name" }, new object[] { etRecipientName.Text.Trim() });
                    if (sqlRecipientRepository.CheckRecipient(param))
                    {
                        MessageBox.Show(this, "Recipient has already exists !", "Information");
                        return;
                    }

                    param = null;
                    param = SqlUtility.SetSqlParameter(new string[] { "recipient_id", "recipient_name", "supplier_id", "deleted" }, new object[] { Guid.NewGuid(), etRecipientName.Text.Trim(), cbSupplier.SelectedValue, 0 });

                    if (sqlRecipientRepository.CreateRecipient(param))
                    {
                        MessageBox.Show(this, "Success create recipient", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Create recipient", "Information");
                    }
                }
                else
                {
                    param = SqlUtility.SetSqlParameter(new string[] { "recipient_name", "supplier_id", "recipient_id" }, new object[] { etRecipientName.Text.Trim(), cbSupplier.SelectedValue, recipient.ID });

                    if (sqlRecipientRepository.EditRecipient(param))
                    {
                        MessageBox.Show(this, "Success Edit recipient", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Edit recipient", "Information");
                    }
                }

                param = null;
                sqlRecipientRepository = null;
            }
        }

        private void cbSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }
         
    }
}
