using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisitaJayaPerkasa.Control.TypeCont
{
    public partial class TypeContView : UserControl
    {
        public TypeContView(VisitaJayaPerkasa.Entities.TypeCont typeCont)
        {
            InitializeComponent();

            lblTypeCode.Text = Utility.Utility.DisplayNullValues(typeCont.TypeCode);
            lblTypeName.Text = Utility.Utility.DisplayNullValues(typeCont.TypeName);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new TypeContList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
