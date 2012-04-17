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
        private SqlRecipientRepository sqlRecipientRepository;

        public RecipientEdit(VisitaJayaPerkasa.Entities.Recipient recipient)
        {
            InitializeComponent();
            this.recipient = recipient;
            sqlRecipientRepository = new SqlRecipientRepository();

            if (recipient == null)
            {
                wantToCreateRecipient = true;
            }
            else
            {
                wantToCreateRecipient = false;
                etRecipientName.Text = recipient.Name;
                etAddress.Text = recipient.Address;
                etPhone1.Text = recipient.Phone1;
                etPhone2.Text = recipient.Phone2;
                etPhone3.Text = recipient.Phone3;
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
            if (etRecipientName.Text.Trim().Equals(""))
                MessageBox.Show(this, "Please fill recipient name", "Information");
            else if (etAddress.Text.Trim().Equals(""))
                MessageBox.Show(this, "Please fill address", "Information");
            else
            {
                sqlRecipientRepository = new SqlRecipientRepository();

                SqlParameter[] param = SqlUtility.SetSqlParameter(new string[] { "recipient_name", "address" }, new object[] { etRecipientName.Text.Trim(), etAddress.Text.Trim() });

                if (wantToCreateRecipient)
                {
                    VisitaJayaPerkasa.Entities.Recipient recipient = new VisitaJayaPerkasa.Entities.Recipient();
                    recipient.ID = Guid.NewGuid();
                    recipient.Name = etRecipientName.Text.Trim();
                    recipient.Address = etAddress.Text.Trim();
                    recipient.Phone1 = etPhone1.Text.Trim();
                    recipient.Phone2 = etPhone2.Text.Trim();
                    recipient.Phone3 = etPhone3.Text.Trim();
                    recipient.Deleted = 0;

                    if (sqlRecipientRepository.CheckRecipientName(param, Guid.Empty, true))
                    {
                        DialogResult dResult = MessageBox.Show(this, "Recipient name has already deleted. Do you want to activate ?", "Confirmation", MessageBoxButtons.YesNo);
                        if (dResult == DialogResult.Yes)
                        {
                            SqlParameter[] parameters = SqlUtility.SetSqlParameter(new string[] { "recipient_id", "recipient_name", "address", "phone1", "phone2", "phone3", "deleted" }
                            , new object[] { recipient.ID, recipient.Name, recipient.Address, recipient.Phone1, recipient.Phone2, recipient.Phone3, recipient.Deleted });

                            if (sqlRecipientRepository.ActivateRecipient(parameters))
                            {
                                MessageBox.Show(this, "Success Activate Recipient", "Information");
                                radButtonElement2.PerformClick();
                            }
                            else
                                MessageBox.Show(this, "Cannot Activate Recipient", "Information");

                            parameters = null;
                        }
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (sqlRecipientRepository.CheckRecipientName(param, Guid.Empty))
                    {
                        MessageBox.Show(this, "Recipient has already exists", "Information");
                        return;
                    }

                    //Create user 
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "recipient_id", "recipient_name", "address", "phone1", "phone2", "phone3", "deleted" }
                    , new object[] { recipient.ID, recipient.Name, recipient.Address, recipient.Phone1, recipient.Phone2, recipient.Phone3, recipient.Deleted });

                    if (sqlRecipientRepository.CreateRecipient(sqlParam))
                    {
                        MessageBox.Show(this, "Success create recipient", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Create recipient", "Information");
                    }
                }
                else
                {
                    VisitaJayaPerkasa.Entities.Recipient recipient = new VisitaJayaPerkasa.Entities.Recipient();
                    recipient.ID = this.recipient.ID;
                    recipient.Name = etRecipientName.Text.Trim();
                    recipient.Address = etAddress.Text.Trim();
                    recipient.Phone1 = etPhone1.Text.Trim();
                    recipient.Phone2 = etPhone2.Text.Trim();
                    recipient.Phone3 = etPhone3.Text.Trim();
                    recipient.Deleted = 0;


                    if (sqlRecipientRepository.CheckRecipientName(param, recipient.ID))
                    {
                        MessageBox.Show(this, "Recipient has already exist. if it has already deleted. you must activate it with create new data", "Information");
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "recipient_id", "recipient_name", "address", "phone1", "phone2", "phone3", "deleted" }
                    , new object[] { recipient.ID, recipient.Name, recipient.Address, recipient.Phone1, recipient.Phone2, recipient.Phone3, recipient.Deleted });

                    if (sqlRecipientRepository.EditRecipient(sqlParam))
                    {
                        MessageBox.Show(this, "Success edit recipient", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show(this, "Cannot edit recipient", "Information");
                    }
                }
            }
        }

        private void cbSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }
         
    }
}
