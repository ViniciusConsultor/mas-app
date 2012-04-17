using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.Utility.Log;
using VisitaJayaPerkasa.SqlRepository;
using System.Data.SqlClient;

namespace VisitaJayaPerkasa.Control.ATK
{
    public partial class AtkEdit : UserControl
    {
        private Entities.PriceList price;
        private SqlPriceListRepository sqlPriceListRepository;
        private bool wantToCreateNew;
        private Entities.Category categorySupplier;
        private List<Entities.Supplier> listSupplier;

        public AtkEdit(Entities.PriceList price)
        {
            InitializeComponent();
            this.price = price;

            sqlPriceListRepository = new SqlPriceListRepository();
            List<Entities.Category> listCategory = sqlPriceListRepository.GetTypeOfSupplier(1);
            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
            {
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            categorySupplier = listCategory != null ? listCategory.FirstOrDefault() : null;

            if (categorySupplier != null)
            {
                listSupplier = sqlPriceListRepository.GetSupplier(categorySupplier.ID);
                if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                {
                    MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cbSupplier.Enabled = true;
                cbSupplier.DataSource = listSupplier;
                cbSupplier.DisplayMember = "SupplierName";
                cbSupplier.ValueMember = "Id";
                cbSupplier.SelectedIndex = -1;
                cbSupplier.Text = "-- Choose --";


                if (this.price == null)
                {
                    pickerDate.Value = DateTime.Now;
                    wantToCreateNew = true;
                }
                else
                {
                    cbSupplier.SelectedValue = price.SupplierID;
                    if (cbSupplier.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText) || cbSupplier.Text.Equals(""))
                        radButtonElement1.Enabled = false;

                    pickerDate.Value = price.DateFrom;
                    etItem.Text = price.Item;
                    etPrice.Text = price.PriceSupplier.ToString();
                    wantToCreateNew = false;
                }
            }

            listCategory = null;
            sqlPriceListRepository = null;
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new ATK.AtkList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            //save
            if (cbSupplier.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
                MessageBox.Show(this, "Please choose supplier name", "Information");
            else if (etItem.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill item", "Information");
            else if (etPrice.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill price", "Information");
            else
            {
                try
                {
                    Convert.ToInt32(etPrice.Text.Trim());
                }
                catch (Exception err)
                {
                    MessageBox.Show(this, "price must be numeric", "Information");
                    Logging.Error("AtkEdit.cs - " + err.Message);
                    return;
                }


                sqlPriceListRepository = new SqlPriceListRepository();


                if (wantToCreateNew)
                {
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "dateFrom", "item", "suplier_id" }, new object[] { Utility.Utility.ChangeDateMMDD(pickerDate.Value.Date.ToString()), etItem.Text.Trim(), cbSupplier.SelectedValue });

                    if (sqlPriceListRepository.GetExistsDatePriceATK(sqlParam, wantToCreateNew))
                    {
                        MessageBox.Show(this, "Your data has already exist. \nDelete it first", "Information");
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    sqlParam = null;
                    SqlParameter[] sqlParamInsert = null;

                    //14 is field who any in below for
                    string[] key = new string[14];
                    object[] value = new object[14];

                    int nn = 0;

                    key[nn] = "price_id";
                    value[nn++] = Guid.NewGuid();

                    key[nn] = "dateFrom";
                    value[nn++] = pickerDate.Value;

                    key[nn] = "dateTo";
                    value[nn++] = DBNull.Value;

                    key[nn] = "supplier_id";
                    value[nn++] = Utility.Utility.ConvertToUUID(cbSupplier.SelectedValue.ToString());

                    key[nn] = "destination";
                    value[nn++] = Guid.Empty;

                    key[nn] = "type_cont_id";
                    value[nn++] = Guid.Empty;

                    key[nn] = "condition_id";
                    value[nn++] = Guid.Empty;

                    key[nn] = "price_supplier";
                    value[nn++] = Convert.ToInt32(etPrice.Text.Trim());

                    key[nn] = "customer_id";
                    value[nn++] = Guid.Empty;

                    key[nn] = "price_customer";
                    value[nn++] = DBNull.Value;

                    key[nn] = "stuffing_id";
                    value[nn++] = Guid.Empty;

                    key[nn] = "recipient_id";
                    value[nn++] = Guid.Empty;

                    key[nn] = "price_courier";
                    value[nn++] = DBNull.Value;

                    key[nn] = "item";
                    value[nn++] = etItem.Text.Trim();

                    sqlParamInsert = SqlUtility.SetSqlParameter(key, value);
                    if (sqlPriceListRepository.SavePriceList(null, sqlParamInsert))
                    {
                        MessageBox.Show(this, "Success saving !", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show(this, "Failed save data !", "Information");

                    sqlParamInsert = null;
                }
                else {
                    SqlParameter[] sqlParam = SqlUtility.SetSqlParameter(new string[] { "dateFrom", "item", "supplier_id", "price_id" }, new object[] { Utility.Utility.ChangeDateMMDD(pickerDate.Value.Date.ToString()), etItem.Text.Trim(), cbSupplier.SelectedValue, this.price.ID });

                    if (sqlPriceListRepository.GetExistsDatePriceATK(sqlParam, wantToCreateNew))
                    {
                        MessageBox.Show(this, "Your data has already exist. \nDelete it first", "Information");
                        return;
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                    {
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    sqlParam = null;



                    SqlParameter[] sqlParamEdit = SqlUtility.SetSqlParameter(new string[] { "supplier_id", "dateFrom", "item", "price_supplier", "price_id" }, new object[] { Utility.Utility.ConvertToUUID(cbSupplier.SelectedValue.ToString()), pickerDate.Value.Date, etItem.Text.Trim(), etPrice.Text.Trim(), this.price.ID });
                    if (sqlPriceListRepository.EditPriceATK(sqlParamEdit))
                    {
                        MessageBox.Show(this, "Success Edit Price ATK", "Information");
                        radButtonElement2.PerformClick();
                    }
                    else if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                        MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show(this, "Cannot Edit Price ATK", "Information");
                    }

                    sqlParamEdit = null;
                }
            }

            sqlPriceListRepository = null;
        }

        private void cbSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }
    }
}
