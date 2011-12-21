using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisitaJayaPerkasa.Entities;

namespace VisitaJayaPerkasa.Control.CategoryControl
{
    public partial class CategoryView : UserControl
    {
        public CategoryView(Category category)
        {
            InitializeComponent();

            lblCategoryCode.Text = Utility.Utility.DisplayNullValues(category.CategoryCode);
            lblCategoryName.Text = Utility.Utility.DisplayNullValues(category.CategoryName);
        }

        private void radButtonClose_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new CategoryList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
