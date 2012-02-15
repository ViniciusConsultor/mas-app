namespace VisitaJayaPerkasa.Form
{
    partial class SearchCustomerTrans
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.CustomerTransGridView = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerTransGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // CustomerTransGridView
            // 
            this.CustomerTransGridView.BackColor = System.Drawing.SystemColors.Control;
            this.CustomerTransGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.CustomerTransGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomerTransGridView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.CustomerTransGridView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CustomerTransGridView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CustomerTransGridView.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.CustomerTransGridView.MasterGridViewTemplate.AllowAddNewRow = false;
            gridViewTextBoxColumn1.FieldAlias = "CustomerTransID";
            gridViewTextBoxColumn1.FieldName = "CustomerTransID";
            gridViewTextBoxColumn1.HeaderText = "ID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.UniqueName = "CustomerTransID";
            gridViewTextBoxColumn2.FieldAlias = "CustomerID";
            gridViewTextBoxColumn2.FieldName = "CustomerID";
            gridViewTextBoxColumn2.HeaderText = "Customer ID";
            gridViewTextBoxColumn2.IsVisible = false;
            gridViewTextBoxColumn2.UniqueName = "CustomerID";
            gridViewTextBoxColumn2.Width = 110;
            gridViewTextBoxColumn3.FieldAlias = "CustomerName";
            gridViewTextBoxColumn3.FieldName = "CustomerName";
            gridViewTextBoxColumn3.HeaderText = "Customer Name";
            gridViewTextBoxColumn3.UniqueName = "CustomerName";
            gridViewTextBoxColumn3.Width = 128;
            gridViewTextBoxColumn4.FieldAlias = "TransDate";
            gridViewTextBoxColumn4.FieldName = "TransDate";
            gridViewTextBoxColumn4.HeaderText = "Date";
            gridViewTextBoxColumn4.UniqueName = "TransDate";
            gridViewTextBoxColumn4.Width = 100;
            this.CustomerTransGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn1);
            this.CustomerTransGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn2);
            this.CustomerTransGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn3);
            this.CustomerTransGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn4);
            this.CustomerTransGridView.Name = "CustomerTransGridView";
            this.CustomerTransGridView.ReadOnly = true;
            this.CustomerTransGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CustomerTransGridView.Size = new System.Drawing.Size(663, 279);
            this.CustomerTransGridView.TabIndex = 4;
            this.CustomerTransGridView.ThemeName = "ControlDefault";
            this.CustomerTransGridView.DoubleClick += new System.EventHandler(this.CustomerTransGridView_DoubleClick);
            // 
            // SearchCustomerTrans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 279);
            this.Controls.Add(this.CustomerTransGridView);
            this.Name = "SearchCustomerTrans";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "SearchCustomerTrans";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.CustomerTransGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView CustomerTransGridView;
    }
}

