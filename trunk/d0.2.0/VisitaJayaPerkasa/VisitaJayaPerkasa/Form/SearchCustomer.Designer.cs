namespace VisitaJayaPerkasa.Form
{
    partial class SearchCustomer
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridSortField gridSortField1 = new Telerik.WinControls.UI.GridSortField();
            this.CustomerGridView = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGridView.MasterGridViewTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // CustomerGridView
            // 
            this.CustomerGridView.BackColor = System.Drawing.SystemColors.Control;
            this.CustomerGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.CustomerGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomerGridView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.CustomerGridView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CustomerGridView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CustomerGridView.Location = new System.Drawing.Point(0, 0);
            // 
            // gridViewTemplate1
            // 
            this.CustomerGridView.MasterGridViewTemplate.AllowAddNewRow = false;
            gridViewTextBoxColumn1.FieldAlias = "ID";
            gridViewTextBoxColumn1.FieldName = "ID";
            gridViewTextBoxColumn1.HeaderText = "ID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.UniqueName = "ID";
            gridViewTextBoxColumn2.FieldAlias = "CustomerName";
            gridViewTextBoxColumn2.FieldName = "CustomerName";
            gridViewTextBoxColumn2.HeaderText = "Customer Name";
            gridViewTextBoxColumn2.UniqueName = "CustomerName";
            gridViewTextBoxColumn2.Width = 128;
            gridViewTextBoxColumn3.FieldAlias = "Office";
            gridViewTextBoxColumn3.FieldName = "Office";
            gridViewTextBoxColumn3.HeaderText = "Office";
            gridViewTextBoxColumn3.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewTextBoxColumn3.UniqueName = "Office";
            gridViewTextBoxColumn3.Width = 100;
            gridViewTextBoxColumn4.FieldAlias = "Address";
            gridViewTextBoxColumn4.FieldName = "Address";
            gridViewTextBoxColumn4.HeaderText = "Address";
            gridViewTextBoxColumn4.UniqueName = "Address";
            gridViewTextBoxColumn4.Width = 181;
            gridViewTextBoxColumn5.FieldAlias = "Phone";
            gridViewTextBoxColumn5.FieldName = "Phone";
            gridViewTextBoxColumn5.HeaderText = "Phone";
            gridViewTextBoxColumn5.UniqueName = "Phone";
            gridViewTextBoxColumn5.Width = 90;
            gridViewTextBoxColumn6.FieldAlias = "Fax";
            gridViewTextBoxColumn6.FieldName = "Fax";
            gridViewTextBoxColumn6.HeaderText = "Fax";
            gridViewTextBoxColumn6.UniqueName = "Fax";
            gridViewTextBoxColumn6.Width = 90;
            gridViewTextBoxColumn7.FieldAlias = "Email";
            gridViewTextBoxColumn7.FieldName = "Email";
            gridViewTextBoxColumn7.HeaderText = "Email";
            gridViewTextBoxColumn7.UniqueName = "Email";
            gridViewTextBoxColumn7.Width = 110;
            gridViewTextBoxColumn8.FieldAlias = "ContactPerson";
            gridViewTextBoxColumn8.FieldName = "ContactPerson";
            gridViewTextBoxColumn8.HeaderText = "Contact Person";
            gridViewTextBoxColumn8.UniqueName = "ContactPerson";
            gridViewTextBoxColumn8.Width = 110;
            gridViewTextBoxColumn9.FieldAlias = "StatusPPN";
            gridViewTextBoxColumn9.FieldName = "StatusPPN";
            gridViewTextBoxColumn9.HeaderText = "Status PPN";
            gridViewTextBoxColumn9.IsVisible = false;
            gridViewTextBoxColumn9.UniqueName = "StatusPPN";
            this.CustomerGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn1);
            this.CustomerGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn2);
            this.CustomerGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn3);
            this.CustomerGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn4);
            this.CustomerGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn5);
            this.CustomerGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn6);
            this.CustomerGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn7);
            this.CustomerGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn8);
            this.CustomerGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn9);
            this.CustomerGridView.MasterGridViewTemplate.EnableFiltering = true;
            gridSortField1.FieldAlias = "Office";
            gridSortField1.FieldName = "Office";
            gridSortField1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.CustomerGridView.MasterGridViewTemplate.SortExpressions.Add(gridSortField1);
            this.CustomerGridView.Name = "CustomerGridView";
            this.CustomerGridView.ReadOnly = true;
            this.CustomerGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CustomerGridView.Size = new System.Drawing.Size(663, 279);
            this.CustomerGridView.TabIndex = 4;
            this.CustomerGridView.ThemeName = "ControlDefault";
            this.CustomerGridView.DoubleClick += new System.EventHandler(this.CustomerGridView_DoubleClick);
            // 
            // SearchCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 279);
            this.Controls.Add(this.CustomerGridView);
            this.Name = "SearchCustomer";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "SearchCustomer";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGridView.MasterGridViewTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView CustomerGridView;

    }
}

