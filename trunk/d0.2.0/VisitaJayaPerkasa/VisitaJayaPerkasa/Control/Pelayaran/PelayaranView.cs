using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.SqlRepository; 

namespace VisitaJayaPerkasa.Control.Pelayaran
{
    public partial class PelayaranView : UserControl
    {
        private SqlPelayaranRepository sqlPelayaranRepository;
        private List<VisitaJayaPerkasa.Entities.PelayaranDetail> listPelayaranDetail;

        public PelayaranView(VisitaJayaPerkasa.Entities.Pelayaran pelayaran)
        {
            InitializeComponent();
            sqlPelayaranRepository = new SqlPelayaranRepository();
            listPelayaranDetail = sqlPelayaranRepository.ListPelayaranDetail(pelayaran.ID);

            if (listPelayaranDetail != null)
                PelayaranDetailGridView.DataSource = listPelayaranDetail;


            lblPelayaranName.Text = pelayaran.supplierName;
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new PelayaranList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }

    }
}
