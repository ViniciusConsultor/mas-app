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

namespace VisitaJayaPerkasa.Control.CategoryControl
{
    public partial class CategoryEdit : UserControl
    {
        private bool wantToCreateCategory;
        private Category category;

        public CategoryEdit(Category category)
        {
            InitializeComponent();

            this.category = category;

            if (category == null)
            {
                wantToCreateCategory = true;
            }
            else
            {
                wantToCreateCategory = false;
                etCategoryCode.Text = category.CategoryCode;
                etCategoryName.Text = category.CategoryName;
            }
        }

        private void radButtonSave_Click(object sender, EventArgs e)
        {
            if (etCategoryCode.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill category code", "Information");
            else if (etCategoryName.Text.Trim().Length == 0)
                MessageBox.Show(this, "Please fill category name", "Information");
            else
            {
                SqlCategoryRepository sqlCategoryRepository = new SqlCategoryRepository();
                SqlParameter[] param;

                if (wantToCreateCategory)
                {
                    //Check city code has already exists ?
                    param = SqlUtility.SetSqlParameter(new string[] { "category_code" }, new object[] { etCategoryCode.Text.Trim() });
                    if (sqlCategoryRepository.CheckCategoryCode(param))
                    {
                        MessageBox.Show(this, "Category has already exists !", "Information");
                        return;
                    }

                    param = null;
                    param = SqlUtility.SetSqlParameter(new string[] { "category_id", "category_code", "category_name", "deleted" }, new object[] { Guid.NewGuid(), etCategoryCode.Text.Trim(), etCategoryName.Text.Trim(), 0 });

                    if (sqlCategoryRepository.CreateCategory(param))
                    {
                        MessageBox.Show(this, "Success create category", "Information");
                        radButtonClose.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Create category", "Information");
                    }
                }
                else
                {
                    param = SqlUtility.SetSqlParameter(new string[] { "category_code", "category_name", "category_id" }, new object[] { etCategoryCode.Text.Trim(), etCategoryName.Text.Trim(), category.ID });

                    if (sqlCategoryRepository.EditCategory(param))
                    {
                        MessageBox.Show(this, "Success Edit category", "Information");
                        radButtonClose.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(this, "Cannot Edit category", "Information");
                    }
                }

                param = null;
                sqlCategoryRepository = null;
            }
        }

        private void radButtonClose_Click(object sender, EventArgs e)
        {
            UserControl Controllers = new CategoryList();
            Constant.VisitaJayaPerkasaApplication.mainForm.ShowUserControl(Controllers);
        }
    }
}
