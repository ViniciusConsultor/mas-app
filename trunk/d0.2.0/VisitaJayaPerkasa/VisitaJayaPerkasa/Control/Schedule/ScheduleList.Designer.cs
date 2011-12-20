namespace VisitaJayaPerkasa.Control.Schedule
{
    partial class ScheduleList
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridSortField gridSortField1 = new Telerik.WinControls.UI.GridSortField();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleList));
            this.radToolStripLabelIndexing = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.PelayaranGridView = new Telerik.WinControls.UI.RadGridView();
            this.radCalendar1 = new Telerik.WinControls.UI.RadCalendar();
            this.radToolStripItem4 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radToolStripLabelElement1 = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.radImageButtonElement1 = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radToolStrip1 = new Telerik.WinControls.UI.RadToolStrip();
            this.radToolStripElement1 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radToolStripItem1 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radButtonElementCreate = new Telerik.WinControls.UI.RadButtonElement();
            this.radButtonElementEdit = new Telerik.WinControls.UI.RadButtonElement();
            this.radButtonElementRemove = new Telerik.WinControls.UI.RadButtonElement();
            this.radButtonElementRefresh = new Telerik.WinControls.UI.RadButtonElement();
            this.radToolStripItem2 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radButtonElementPrev = new Telerik.WinControls.UI.RadButtonElement();
            this.radButtonElementNext = new Telerik.WinControls.UI.RadButtonElement();
            this.radToolStripElement2 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radToolStripItem3 = new Telerik.WinControls.UI.RadToolStripItem();
            ((System.ComponentModel.ISupportInitialize)(this.PelayaranGridView)).BeginInit();
            this.PelayaranGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radCalendar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).BeginInit();
            this.SuspendLayout();
            // 
            // radToolStripLabelIndexing
            // 
            this.radToolStripLabelIndexing.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radToolStripLabelIndexing.Name = "radToolStripLabelIndexing";
            this.radToolStripLabelIndexing.Text = "-";
            // 
            // PelayaranGridView
            // 
            this.PelayaranGridView.BackColor = System.Drawing.SystemColors.Control;
            this.PelayaranGridView.Controls.Add(this.radCalendar1);
            this.PelayaranGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.PelayaranGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PelayaranGridView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.PelayaranGridView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PelayaranGridView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PelayaranGridView.Location = new System.Drawing.Point(0, 53);
            // 
            // 
            // 
            this.PelayaranGridView.MasterGridViewTemplate.AllowAddNewRow = false;
            gridViewTextBoxColumn1.FieldAlias = "ID";
            gridViewTextBoxColumn1.FieldName = "ID";
            gridViewTextBoxColumn1.HeaderText = "ID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.UniqueName = "ID";
            gridViewTextBoxColumn2.FieldAlias = "Tujuan";
            gridViewTextBoxColumn2.FieldName = "Tujuan";
            gridViewTextBoxColumn2.HeaderText = "Tujuan";
            gridViewTextBoxColumn2.UniqueName = "Tujuan";
            gridViewTextBoxColumn2.Width = 181;
            gridViewTextBoxColumn3.FieldAlias = "PelayaranName";
            gridViewTextBoxColumn3.FieldName = "PelayaranName";
            gridViewTextBoxColumn3.HeaderText = "Pelayaran";
            gridViewTextBoxColumn3.UniqueName = "PelayaranName";
            gridViewTextBoxColumn3.Width = 100;
            gridViewTextBoxColumn4.FieldAlias = "VesselName";
            gridViewTextBoxColumn4.FieldName = "VesselName";
            gridViewTextBoxColumn4.HeaderText = "Kapal";
            gridViewTextBoxColumn4.UniqueName = "VesselName";
            gridViewTextBoxColumn4.Width = 100;
            gridViewTextBoxColumn5.FieldAlias = "voy";
            gridViewTextBoxColumn5.FieldName = "voy";
            gridViewTextBoxColumn5.HeaderText = "VOY";
            gridViewTextBoxColumn5.UniqueName = "voy";
            gridViewTextBoxColumn6.FieldAlias = "tglclosing";
            gridViewTextBoxColumn6.FieldName = "tglclosing";
            gridViewTextBoxColumn6.HeaderText = "Tgl Closing";
            gridViewTextBoxColumn6.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewTextBoxColumn6.UniqueName = "tglclosing";
            gridViewTextBoxColumn6.Width = 120;
            gridViewTextBoxColumn7.FieldAlias = "etd";
            gridViewTextBoxColumn7.FieldName = "etd";
            gridViewTextBoxColumn7.HeaderText = "ETD";
            gridViewTextBoxColumn7.UniqueName = "etd";
            gridViewTextBoxColumn8.FieldAlias = "Keterangan";
            gridViewTextBoxColumn8.FieldName = "Keterangan";
            gridViewTextBoxColumn8.HeaderText = "Keterangan";
            gridViewTextBoxColumn8.UniqueName = "Keterangan";
            gridViewTextBoxColumn9.FieldAlias = "ro";
            gridViewTextBoxColumn9.FieldName = "ro";
            gridViewTextBoxColumn9.HeaderText = "RO";
            gridViewTextBoxColumn9.UniqueName = "ro";
            this.PelayaranGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn1);
            this.PelayaranGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn2);
            this.PelayaranGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn3);
            this.PelayaranGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn4);
            this.PelayaranGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn5);
            this.PelayaranGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn6);
            this.PelayaranGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn7);
            this.PelayaranGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn8);
            this.PelayaranGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn9);
            gridSortField1.FieldAlias = "tglclosing";
            gridSortField1.FieldName = "tglclosing";
            gridSortField1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.PelayaranGridView.MasterGridViewTemplate.SortExpressions.Add(gridSortField1);
            this.PelayaranGridView.Name = "PelayaranGridView";
            this.PelayaranGridView.ReadOnly = true;
            this.PelayaranGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PelayaranGridView.Size = new System.Drawing.Size(666, 365);
            this.PelayaranGridView.TabIndex = 5;
            this.PelayaranGridView.ThemeName = "ControlDefault";
            // 
            // radCalendar1
            // 
            this.radCalendar1.CellAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radCalendar1.FastNavigationNextImage = ((System.Drawing.Image)(resources.GetObject("radCalendar1.FastNavigationNextImage")));
            this.radCalendar1.FastNavigationPrevImage = ((System.Drawing.Image)(resources.GetObject("radCalendar1.FastNavigationPrevImage")));
            this.radCalendar1.HeaderHeight = 17;
            this.radCalendar1.HeaderWidth = 17;
            this.radCalendar1.Location = new System.Drawing.Point(75, 0);
            this.radCalendar1.Name = "radCalendar1";
            this.radCalendar1.NavigationNextImage = ((System.Drawing.Image)(resources.GetObject("radCalendar1.NavigationNextImage")));
            this.radCalendar1.NavigationPrevImage = ((System.Drawing.Image)(resources.GetObject("radCalendar1.NavigationPrevImage")));
            this.radCalendar1.RangeMaxDate = new System.DateTime(2099, 12, 30, 0, 0, 0, 0);
            this.radCalendar1.ShowOtherMonthsDays = false;
            this.radCalendar1.Size = new System.Drawing.Size(324, 139);
            this.radCalendar1.TabIndex = 0;
            this.radCalendar1.Text = "radCalendar1";
            this.radCalendar1.TitleAlign = System.Windows.Forms.VisualStyles.ContentAlignment.Center;
            this.radCalendar1.Visible = false;
            this.radCalendar1.ZoomFactor = 1.2F;
            this.radCalendar1.SelectionChanged += new System.EventHandler(this.radCalendar1_SelectionChanged);
            // 
            // radToolStripItem4
            // 
            this.radToolStripItem4.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripLabelElement1,
            this.radImageButtonElement1});
            this.radToolStripItem4.Key = "0";
            this.radToolStripItem4.Name = "radToolStripItem4";
            this.radToolStripItem4.Text = "radToolStripItem4";
            // 
            // radToolStripLabelElement1
            // 
            this.radToolStripLabelElement1.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radToolStripLabelElement1.Name = "radToolStripLabelElement1";
            this.radToolStripLabelElement1.Text = "Date";
            // 
            // radImageButtonElement1
            // 
            this.radImageButtonElement1.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radImageButtonElement1.Image = global::VisitaJayaPerkasa.Properties.Resources.calendar2;
            this.radImageButtonElement1.ImageIndexClicked = 0;
            this.radImageButtonElement1.ImageIndexHovered = 0;
            this.radImageButtonElement1.Name = "radImageButtonElement1";
            this.radImageButtonElement1.ShowBorder = false;
            this.radImageButtonElement1.Text = "radImageButtonElement1";
            this.radImageButtonElement1.Click += new System.EventHandler(this.radImageButtonElement1_Click);
            // 
            // radToolStrip1
            // 
            this.radToolStrip1.AllowDragging = false;
            this.radToolStrip1.AllowFloating = false;
            this.radToolStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radToolStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripElement1,
            this.radToolStripElement2});
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
            this.radToolStrip1.Size = new System.Drawing.Size(666, 53);
            this.radToolStrip1.TabIndex = 4;
            this.radToolStrip1.Text = "radToolStrip1";
            // 
            // radToolStripElement1
            // 
            this.radToolStripElement1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripItem1,
            this.radToolStripItem2});
            this.radToolStripElement1.Name = "radToolStripElement1";
            // 
            // radToolStripItem1
            // 
            this.radToolStripItem1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radButtonElementCreate,
            this.radButtonElementEdit,
            this.radButtonElementRemove,
            this.radButtonElementRefresh});
            this.radToolStripItem1.Key = "0";
            this.radToolStripItem1.Name = "radToolStripItem1";
            this.radToolStripItem1.Text = "";
            // 
            // radButtonElementCreate
            // 
            this.radButtonElementCreate.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElementCreate.DisplayStyle = Telerik.WinControls.DisplayStyle.ImageAndText;
            this.radButtonElementCreate.Image = global::VisitaJayaPerkasa.Properties.Resources.add_16;
            this.radButtonElementCreate.Name = "radButtonElementCreate";
            this.radButtonElementCreate.ShowBorder = false;
            this.radButtonElementCreate.Text = "Create New";
            this.radButtonElementCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // radButtonElementEdit
            // 
            this.radButtonElementEdit.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElementEdit.Image = global::VisitaJayaPerkasa.Properties.Resources.edit_16;
            this.radButtonElementEdit.Name = "radButtonElementEdit";
            this.radButtonElementEdit.ShowBorder = false;
            this.radButtonElementEdit.Text = "Edit";
            this.radButtonElementEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // radButtonElementRemove
            // 
            this.radButtonElementRemove.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElementRemove.Image = global::VisitaJayaPerkasa.Properties.Resources.delete_16;
            this.radButtonElementRemove.Name = "radButtonElementRemove";
            this.radButtonElementRemove.ShowBorder = false;
            this.radButtonElementRemove.Text = "Remove";
            this.radButtonElementRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // radButtonElementRefresh
            // 
            this.radButtonElementRefresh.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElementRefresh.DisplayStyle = Telerik.WinControls.DisplayStyle.ImageAndText;
            this.radButtonElementRefresh.Image = global::VisitaJayaPerkasa.Properties.Resources.refresh_16;
            this.radButtonElementRefresh.Name = "radButtonElementRefresh";
            this.radButtonElementRefresh.ShowBorder = false;
            this.radButtonElementRefresh.Text = "Refresh";
            this.radButtonElementRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // radToolStripItem2
            // 
            this.radToolStripItem2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radButtonElementPrev,
            this.radToolStripLabelIndexing,
            this.radButtonElementNext});
            this.radToolStripItem2.Key = "1";
            this.radToolStripItem2.Name = "radToolStripItem2";
            this.radToolStripItem2.Text = "radToolStripItem2";
            // 
            // radButtonElementPrev
            // 
            this.radButtonElementPrev.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElementPrev.Image = global::VisitaJayaPerkasa.Properties.Resources.prev_16;
            this.radButtonElementPrev.Name = "radButtonElementPrev";
            this.radButtonElementPrev.ShowBorder = false;
            this.radButtonElementPrev.Text = "";
            // 
            // radButtonElementNext
            // 
            this.radButtonElementNext.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElementNext.Image = global::VisitaJayaPerkasa.Properties.Resources.next_16;
            this.radButtonElementNext.Name = "radButtonElementNext";
            this.radButtonElementNext.ShowBorder = false;
            this.radButtonElementNext.Text = "";
            // 
            // radToolStripElement2
            // 
            this.radToolStripElement2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripItem4});
            this.radToolStripElement2.Name = "radToolStripElement2";
            this.radToolStripElement2.Text = "radToolStripElement2";
            // 
            // radToolStripItem3
            // 
            this.radToolStripItem3.Name = "radToolStripItem3";
            this.radToolStripItem3.Text = "radToolStripItem3";
            // 
            // ScheduleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PelayaranGridView);
            this.Controls.Add(this.radToolStrip1);
            this.Name = "ScheduleList";
            this.Size = new System.Drawing.Size(666, 418);
            ((System.ComponentModel.ISupportInitialize)(this.PelayaranGridView)).EndInit();
            this.PelayaranGridView.ResumeLayout(false);
            this.PelayaranGridView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radCalendar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadToolStripLabelElement radToolStripLabelIndexing;
        private Telerik.WinControls.UI.RadGridView PelayaranGridView;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementNext;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementPrev;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem4;
        private Telerik.WinControls.UI.RadToolStrip radToolStrip1;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement1;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem1;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementCreate;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementEdit;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementRemove;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementRefresh;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem2;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement2;
        private Telerik.WinControls.UI.RadToolStripLabelElement radToolStripLabelElement1;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonElement1;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem3;
        private Telerik.WinControls.UI.RadCalendar radCalendar1;

    }
}
