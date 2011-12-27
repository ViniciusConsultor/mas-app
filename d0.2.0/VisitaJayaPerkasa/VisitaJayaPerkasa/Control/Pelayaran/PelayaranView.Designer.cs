namespace VisitaJayaPerkasa.Control.Pelayaran
{
    partial class PelayaranView
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
            Telerik.WinControls.UI.GridSortField gridSortField1 = new Telerik.WinControls.UI.GridSortField();
            this.lblPelayaranName = new Telerik.WinControls.UI.RadLabel();
            this.radToolStripElement1 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radToolStripItem1 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radButtonElement1 = new Telerik.WinControls.UI.RadButtonElement();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radToolStrip1 = new Telerik.WinControls.UI.RadToolStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PelayaranDetailGridView = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.lblPelayaranName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PelayaranDetailGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPelayaranName
            // 
            this.lblPelayaranName.Location = new System.Drawing.Point(160, 28);
            this.lblPelayaranName.Name = "lblPelayaranName";
            this.lblPelayaranName.Size = new System.Drawing.Size(63, 16);
            this.lblPelayaranName.TabIndex = 26;
            this.lblPelayaranName.Text = "User Name";
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
            this.radButtonElement1.Click += new System.EventHandler(this.radButtonElement1_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(20, 28);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(90, 16);
            this.radLabel1.TabIndex = 17;
            this.radLabel1.Text = "Pelayaran Name";
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
            this.groupBox1.Controls.Add(this.lblPelayaranName);
            this.groupBox1.Location = new System.Drawing.Point(14, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 213);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Master Data";
            // 
            // PelayaranDetailGridView
            // 
            this.PelayaranDetailGridView.BackColor = System.Drawing.SystemColors.Control;
            this.PelayaranDetailGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.PelayaranDetailGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PelayaranDetailGridView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.PelayaranDetailGridView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PelayaranDetailGridView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PelayaranDetailGridView.Location = new System.Drawing.Point(0, 355);
            // 
            // 
            // 
            this.PelayaranDetailGridView.MasterGridViewTemplate.AllowAddNewRow = false;
            gridViewTextBoxColumn1.FieldName = "VesselCode";
            gridViewTextBoxColumn1.HeaderText = "Vessel Code";
            gridViewTextBoxColumn1.UniqueName = "VesselCode";
            gridViewTextBoxColumn1.Width = 130;
            gridViewTextBoxColumn2.FieldName = "VesselName";
            gridViewTextBoxColumn2.HeaderText = "Vessel Name";
            gridViewTextBoxColumn2.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewTextBoxColumn2.UniqueName = "VesselName";
            gridViewTextBoxColumn2.Width = 130;
            this.PelayaranDetailGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn1);
            this.PelayaranDetailGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn2);
            gridSortField1.FieldAlias = "VesselName";
            gridSortField1.FieldName = "VesselName";
            gridSortField1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.PelayaranDetailGridView.MasterGridViewTemplate.SortExpressions.Add(gridSortField1);
            this.PelayaranDetailGridView.Name = "PelayaranDetailGridView";
            this.PelayaranDetailGridView.ReadOnly = true;
            this.PelayaranDetailGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PelayaranDetailGridView.Size = new System.Drawing.Size(695, 284);
            this.PelayaranDetailGridView.TabIndex = 36;
            // 
            // PelayaranView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PelayaranDetailGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.radToolStrip1);
            this.Name = "PelayaranView";
            this.Size = new System.Drawing.Size(695, 639);
            ((System.ComponentModel.ISupportInitialize)(this.lblPelayaranName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PelayaranDetailGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lblPelayaranName;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement1;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem1;
        private Telerik.WinControls.UI.RadButtonElement radButtonElement1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadToolStrip radToolStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadGridView PelayaranDetailGridView;
    }
}
