﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository;

namespace VisitaJayaPerkasa.Control.Supplier
{
    public partial class SupplierView : UserControl
    {
        private SqlSupplierRepository sqlSupplierRepository;
        private List<VisitaJayaPerkasa.Entities.SupplierDetail> listSupplierDetail;

        public SupplierView(Entities.Supplier supplier)
        {
            InitializeComponent();
            sqlSupplierRepository = new SqlSupplierRepository();
            listSupplierDetail = sqlSupplierRepository.ListSupplierDetail(supplier.Id);

            if (!Constant.VisitaJayaPerkasaApplication.anyConnection)
                MessageBox.Show(this, "Please check your connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (listSupplierDetail != null)
                SupplierDetailGridView.DataSource = listSupplierDetail;


            lblSupplierName.Text = supplier.SupplierName;
            lblCategoryName.Text = supplier.CategoryName;
            lblAddres.Text = supplier.Address;
            lblEmail.Text = supplier.Email;
            lblPhone.Text = supplier.Phone;
            lblFax.Text = supplier.Fax;
            lblContactPerson.Text = supplier.ContactPerson;

            if (supplier.CategoryName.Equals("Trucking")) {
                gbTrucking.Visible = true;
                List<string> listTemp = sqlSupplierRepository.ListTruckingNumber(supplier.Id);

                if (listTemp != null)
                    for (int i = 0; i < listTemp.Count; i++)
                        gvTrucking.Rows.Add(listTemp.ElementAt(i));

                listTemp = null;
            }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new SupplierList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
