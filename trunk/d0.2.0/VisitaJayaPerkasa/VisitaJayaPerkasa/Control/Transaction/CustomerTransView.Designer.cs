namespace VisitaJayaPerkasa.Control.Transaction
{
    partial class CustomerTransView
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

        #region Component Designer generated code

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
            Telerik.WinControls.UI.GridSortField gridSortField1 = new Telerik.WinControls.UI.GridSortField();
            this.lblDate = new Telerik.WinControls.UI.RadLabel();
            this.lblCustomerName = new Telerik.WinControls.UI.RadLabel();
            this.radToolStripElement1 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radToolStripItem1 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radButtonElement1 = new Telerik.WinControls.UI.RadButtonElement();
            this.date = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radToolStrip1 = new Telerik.WinControls.UI.RadToolStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CustomerTransDetailGridView = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerTransDetailGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(160, 50);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(61, 16);
            this.lblDate.TabIndex = 29;
            this.lblDate.Text = "First Name";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Location = new System.Drawing.Point(160, 28);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(63, 16);
            this.lblCustomerName.TabIndex = 26;
            this.lblCustomerName.Text = "User Name";
            // 
            // radToolStripElement1
            // 
            this.radToolStripElement1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripItem1});
            this.radToolStripElement1.Name = "radToolStripElement1";
            // 
            // radToolStripItem1
            // 
            this.radToolStripItem1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radButtonElement1});
            this.radToolStripItem1.Key = "0";
            this.radToolStripItem1.Name = "radToolStripItem1";
            this.radToolStripItem1.Text = "radToolStripItem1";
            // 
            // radButtonElement1
            // 
            this.radButtonElement1.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElement1.Image = global::VisitaJayaPerkasa.Properties.Resources.close_16;
            this.radButtonElement1.Name = "radButtonElement1";
            this.radButtonElement1.ShowBorder = false;
            this.radButtonElement1.Text = "Close";
            this.radButtonElement1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // date
            // 
            this.date.Location = new System.Drawing.Point(20, 50);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(30, 16);
            this.date.TabIndex = 20;
            this.date.Text = "Date";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(20, 28);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(88, 16);
            this.radLabel1.TabIndex = 17;
            this.radLabel1.Text = "Customer Name";
            // 
            // radToolStrip1
            // 
            this.radToolStrip1.AllowDragging = false;
            this.radToolStrip1.AllowFloating = false;
            this.radToolStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radToolStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripElement1});
            this.radToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.radToolStrip1.MinimumSize = new System.Drawing.Size(5, 5);
            this.radToolStrip1.Name = "radToolStrip1";
            this.radToolStrip1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radToolStrip1.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.radToolStrip1.RootElement.MinSize = new System.Drawing.Size(5, 5);
            this.radToolStrip1.ShowOverFlowButton = true;
            this.radToolStrip1.Size = new System.Drawing.Size(695, 27);
            this.radToolStrip1.TabIndex = 16;
            this.radToolStrip1.Text = "radToolStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radLabel1);
            this.groupBox1.Controls.Add(this.date);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Controls.Add(this.lblCustomerName);
            this.groupBox1.Location = new System.Drawing.Point(14, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 100);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Master Data";
            // 
            // CustomerTransDetailGridView
            // 
            this.CustomerTransDetailGridView.BackColor = System.Drawing.SystemColors.Control;
            this.CustomerTransDetailGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.CustomerTransDetailGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CustomerTransDetailGridView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.CustomerTransDetailGridView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CustomerTransDetailGridView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CustomerTransDetailGridView.Location = new System.Drawing.Point(0, 148);
            // 
            // 
            // 
            this.CustomerTransDetailGridView.MasterGridViewTemplate.AllowAddNewRow = false;
            gridViewTextBoxColumn1.FieldAlias = "FirstName";
            gridViewTextBoxColumn1.FieldName = "FirstName";
            gridViewTextBoxColumn1.HeaderText = "First Name";
            gridViewTextBoxColumn1.UniqueName = "FirstName";
            gridViewTextBoxColumn1.Width = 130;
            gridViewTextBoxColumn2.FieldAlias = "LastName";
            gridViewTextBoxColumn2.FieldName = "LastName";
            gridViewTextBoxColumn2.HeaderText = "Last Name";
            gridViewTextBoxColumn2.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewTextBoxColumn2.UniqueName = "LastName";
            gridViewTextBoxColumn2.Width = 130;
            gridViewTextBoxColumn3.FieldAlias = "CustomerDetailAddress";
            gridViewTextBoxColumn3.FieldName = "CustomerDetailAddress";
            gridViewTextBoxColumn3.HeaderText = "Address";
            gridViewTextBoxColumn3.UniqueName = "CustomerDetailAddress";
            gridViewTextBoxColumn3.Width = 200;
            gridViewTextBoxColumn4.FieldAlias = "CustomerDetailPhone";
            gridViewTextBoxColumn4.FieldName = "CustomerDetailPhone";
            gridViewTextBoxColumn4.HeaderText = "Phone";
            gridViewTextBoxColumn4.UniqueName = "CustomerDetailPhone";
            gridViewTextBoxColumn4.Width = 110;
            gridViewTextBoxColumn5.FieldAlias = "CustomerDetailMobilePhone";
            gridViewTextBoxColumn5.FieldName = "CustomerDetailMobilePhone";
            gridViewTextBoxColumn5.HeaderText = "Mobile";
            gridViewTextBoxColumn5.UniqueName = "CustomerDetailMobilePhone";
            gridViewTextBoxColumn5.Width = 110;
            this.CustomerTransDetailGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn1);
            this.CustomerTransDetailGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn2);
            this.CustomerTransDetailGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn3);
            this.CustomerTransDetailGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn4);
            this.CustomerTransDetailGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn5);
            gridSortField1.FieldAlias = "LastName";
            gridSortField1.FieldName = "LastName";
            gridSortField1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.CustomerTransDetailGridView.MasterGridViewTemplate.SortExpressions.Add(gridSortField1);
            this.CustomerTransDetailGridView.Name = "CustomerTransDetailGridView";
            this.CustomerTransDetailGridView.ReadOnly = true;
            this.CustomerTransDetailGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CustomerTransDetailGridView.Size = new System.Drawing.Size(695, 491);
            this.CustomerTransDetailGridView.TabIndex = 36;
            // 
            // CustomerTransView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CustomerTransDetailGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.radToolStrip1);
            this.Name = "CustomerTransView";
            this.Size = new System.Drawing.Size(695, 639);
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCustomerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerTransDetailGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lblDate;
        private Telerik.WinControls.UI.RadLabel lblCustomerName;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement1;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem1;
        private Telerik.WinControls.UI.RadButtonElement radButtonElement1;
        private Telerik.WinControls.UI.RadLabel date;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadToolStrip radToolStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadGridView CustomerTransDetailGridView;
    }
}
