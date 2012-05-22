namespace VisitaJayaPerkasa.Form.Report.ChangeDestination
{
    partial class RptChangeDestinationControl
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
            Telerik.WinControls.UI.GridSortField gridSortField1 = new Telerik.WinControls.UI.GridSortField();
            Telerik.WinControls.UI.GridSortField gridSortField2 = new Telerik.WinControls.UI.GridSortField();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptChangeDestinationControl));
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.radCalendarBegin = new Telerik.WinControls.UI.RadCalendar();
            this.radCalendarEnd = new Telerik.WinControls.UI.RadCalendar();
            this.imageButtonDateStart = new Telerik.WinControls.UI.RadImageButtonElement();
            this.lblDateBegin = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.radToolStripLabelElement4 = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.radToolStripItem1 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radToolStripLabelElement5 = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.lblDateEnd = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.imageButtonDateEnd = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radToolStripElement2 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radToolStripItem2 = new Telerik.WinControls.UI.RadToolStripItem();
            this.buttonNew = new Telerik.WinControls.UI.RadButtonElement();
            this.buttonPrint = new Telerik.WinControls.UI.RadButtonElement();
            this.buttonEdit = new Telerik.WinControls.UI.RadButtonElement();
            this.cboCustomer = new Telerik.WinControls.UI.RadComboBoxElement();
            this.radToolStripLabelElement3 = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.cbSearch = new Telerik.WinControls.UI.RadComboBoxElement();
            this.radComboBoxItem1 = new Telerik.WinControls.UI.RadComboBoxItem();
            this.radComboBoxItem2 = new Telerik.WinControls.UI.RadComboBoxItem();
            this.radComboBoxItem3 = new Telerik.WinControls.UI.RadComboBoxItem();
            this.radToolStripLabelElement2 = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.radToolStripItem3 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radImageButtonSearch = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radToolStripElement1 = new Telerik.WinControls.UI.RadToolStripElement();
            this.rtsiHal = new Telerik.WinControls.UI.RadToolStripItem();
            this.radToolStripLabelElement1 = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.cbHal = new Telerik.WinControls.UI.RadComboBoxElement();
            this.rtsiCustomer = new Telerik.WinControls.UI.RadToolStripItem();
            this.radToolStrip1 = new Telerik.WinControls.UI.RadToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            this.radGridView1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radCalendarBegin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCalendarEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbHal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).BeginInit();
            this.SuspendLayout();
            // 
            // radGridView1
            // 
            this.radGridView1.BackColor = System.Drawing.SystemColors.Control;
            this.radGridView1.Controls.Add(this.radCalendarBegin);
            this.radGridView1.Controls.Add(this.radCalendarEnd);
            this.radGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.radGridView1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radGridView1.Location = new System.Drawing.Point(0, 74);
            // 
            // 
            // 
            this.radGridView1.MasterGridViewTemplate.AllowAddNewRow = false;
            gridViewTextBoxColumn1.FieldAlias = "column1";
            gridViewTextBoxColumn1.FieldName = "Tgl";
            gridViewTextBoxColumn1.FormatString = "{0:MM/dd/yyyy}";
            gridViewTextBoxColumn1.HeaderText = "Date";
            gridViewTextBoxColumn1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewTextBoxColumn1.UniqueName = "Tgl";
            gridViewTextBoxColumn1.Width = 120;
            gridViewTextBoxColumn2.FieldAlias = "column2";
            gridViewTextBoxColumn2.FieldName = "NoSurat";
            gridViewTextBoxColumn2.HeaderText = "No Surat";
            gridViewTextBoxColumn2.UniqueName = "NoSurat";
            gridViewTextBoxColumn2.Width = 150;
            gridViewTextBoxColumn3.FieldAlias = "column3";
            gridViewTextBoxColumn3.FieldName = "CustomerName";
            gridViewTextBoxColumn3.HeaderText = "Nama Customer";
            gridViewTextBoxColumn3.UniqueName = "NamaCustomer";
            gridViewTextBoxColumn3.Width = 160;
            this.radGridView1.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn1);
            this.radGridView1.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn2);
            this.radGridView1.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn3);
            gridSortField1.FieldAlias = "Tgl";
            gridSortField1.FieldName = "Tgl";
            gridSortField1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridSortField2.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.radGridView1.MasterGridViewTemplate.SortExpressions.Add(gridSortField1);
            this.radGridView1.MasterGridViewTemplate.SortExpressions.Add(gridSortField2);
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.ReadOnly = true;
            this.radGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radGridView1.Size = new System.Drawing.Size(725, 294);
            this.radGridView1.TabIndex = 9;
            // 
            // radCalendarBegin
            // 
            this.radCalendarBegin.CellAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radCalendarBegin.Culture = new System.Globalization.CultureInfo("id-ID");
            this.radCalendarBegin.FastNavigationNextImage = ((System.Drawing.Image)(resources.GetObject("radCalendarBegin.FastNavigationNextImage")));
            this.radCalendarBegin.FastNavigationPrevImage = ((System.Drawing.Image)(resources.GetObject("radCalendarBegin.FastNavigationPrevImage")));
            this.radCalendarBegin.HeaderHeight = 17;
            this.radCalendarBegin.HeaderWidth = 17;
            this.radCalendarBegin.Location = new System.Drawing.Point(3, 6);
            this.radCalendarBegin.Name = "radCalendarBegin";
            this.radCalendarBegin.NavigationNextImage = ((System.Drawing.Image)(resources.GetObject("radCalendarBegin.NavigationNextImage")));
            this.radCalendarBegin.NavigationPrevImage = ((System.Drawing.Image)(resources.GetObject("radCalendarBegin.NavigationPrevImage")));
            this.radCalendarBegin.RangeMaxDate = new System.DateTime(2099, 12, 30, 0, 0, 0, 0);
            this.radCalendarBegin.ShowOtherMonthsDays = false;
            this.radCalendarBegin.Size = new System.Drawing.Size(257, 147);
            this.radCalendarBegin.TabIndex = 7;
            this.radCalendarBegin.Text = "radCalendar2";
            this.radCalendarBegin.TitleAlign = System.Windows.Forms.VisualStyles.ContentAlignment.Center;
            this.radCalendarBegin.ZoomFactor = 1.2F;
            // 
            // radCalendarEnd
            // 
            this.radCalendarEnd.CellAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radCalendarEnd.Culture = new System.Globalization.CultureInfo("id-ID");
            this.radCalendarEnd.FastNavigationNextImage = ((System.Drawing.Image)(resources.GetObject("radCalendarEnd.FastNavigationNextImage")));
            this.radCalendarEnd.FastNavigationPrevImage = ((System.Drawing.Image)(resources.GetObject("radCalendarEnd.FastNavigationPrevImage")));
            this.radCalendarEnd.HeaderHeight = 17;
            this.radCalendarEnd.HeaderWidth = 17;
            this.radCalendarEnd.Location = new System.Drawing.Point(266, 6);
            this.radCalendarEnd.Name = "radCalendarEnd";
            this.radCalendarEnd.NavigationNextImage = ((System.Drawing.Image)(resources.GetObject("radCalendarEnd.NavigationNextImage")));
            this.radCalendarEnd.NavigationPrevImage = ((System.Drawing.Image)(resources.GetObject("radCalendarEnd.NavigationPrevImage")));
            this.radCalendarEnd.RangeMaxDate = new System.DateTime(2099, 12, 30, 0, 0, 0, 0);
            this.radCalendarEnd.ShowOtherMonthsDays = false;
            this.radCalendarEnd.Size = new System.Drawing.Size(257, 147);
            this.radCalendarEnd.TabIndex = 6;
            this.radCalendarEnd.Text = "radCalendar1";
            this.radCalendarEnd.TitleAlign = System.Windows.Forms.VisualStyles.ContentAlignment.Center;
            this.radCalendarEnd.ZoomFactor = 1.2F;
            // 
            // imageButtonDateStart
            // 
            this.imageButtonDateStart.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.imageButtonDateStart.Image = global::VisitaJayaPerkasa.Properties.Resources.calendar;
            this.imageButtonDateStart.ImageIndexClicked = 0;
            this.imageButtonDateStart.ImageIndexHovered = 0;
            this.imageButtonDateStart.Name = "imageButtonDateStart";
            this.imageButtonDateStart.ShowBorder = false;
            this.imageButtonDateStart.Text = "radImageButtonElement1";
            // 
            // lblDateBegin
            // 
            this.lblDateBegin.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDateBegin.Name = "lblDateBegin";
            this.lblDateBegin.Text = "dd/mm/yyyy";
            // 
            // radToolStripLabelElement4
            // 
            this.radToolStripLabelElement4.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radToolStripLabelElement4.Name = "radToolStripLabelElement4";
            this.radToolStripLabelElement4.Text = "Periode";
            // 
            // radToolStripItem1
            // 
            this.radToolStripItem1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripLabelElement4,
            this.lblDateBegin,
            this.imageButtonDateStart,
            this.radToolStripLabelElement5,
            this.lblDateEnd,
            this.imageButtonDateEnd});
            this.radToolStripItem1.Key = "0";
            this.radToolStripItem1.Name = "radToolStripItem1";
            this.radToolStripItem1.Text = "radToolStripItem1";
            // 
            // radToolStripLabelElement5
            // 
            this.radToolStripLabelElement5.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radToolStripLabelElement5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radToolStripLabelElement5.Name = "radToolStripLabelElement5";
            this.radToolStripLabelElement5.Text = "s/d";
            // 
            // lblDateEnd
            // 
            this.lblDateEnd.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDateEnd.Name = "lblDateEnd";
            this.lblDateEnd.Text = "dd/mm/yyyy";
            // 
            // imageButtonDateEnd
            // 
            this.imageButtonDateEnd.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.imageButtonDateEnd.Image = global::VisitaJayaPerkasa.Properties.Resources.calendar;
            this.imageButtonDateEnd.ImageIndexClicked = 0;
            this.imageButtonDateEnd.ImageIndexHovered = 0;
            this.imageButtonDateEnd.Name = "imageButtonDateEnd";
            this.imageButtonDateEnd.ShowBorder = false;
            this.imageButtonDateEnd.Text = "radImageButtonElement2";
            // 
            // radToolStripElement2
            // 
            this.radToolStripElement2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripItem1,
            this.radToolStripItem2});
            this.radToolStripElement2.Name = "radToolStripElement2";
            this.radToolStripElement2.Text = "Periode";
            // 
            // radToolStripItem2
            // 
            this.radToolStripItem2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.buttonNew,
            this.buttonPrint,
            this.buttonEdit});
            this.radToolStripItem2.Key = "1";
            this.radToolStripItem2.Name = "radToolStripItem2";
            this.radToolStripItem2.Text = "radToolStripItem2";
            // 
            // buttonNew
            // 
            this.buttonNew.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.ShowBorder = false;
            this.buttonNew.Text = "New";
            // 
            // buttonPrint
            // 
            this.buttonPrint.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.ShowBorder = false;
            this.buttonPrint.Text = "Print";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.ShowBorder = false;
            this.buttonEdit.Text = "Edit";
            // 
            // cboCustomer
            // 
            this.cboCustomer.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboCustomer.ArrowButtonMinWidth = 16;
            this.cboCustomer.DefaultValue = null;
            this.cboCustomer.EditorElement = this.cboCustomer;
            this.cboCustomer.EditorManager = null;
            this.cboCustomer.Focusable = true;
            this.cboCustomer.MaxSize = new System.Drawing.Size(118, 20);
            this.cboCustomer.MaxValue = null;
            this.cboCustomer.MinSize = new System.Drawing.Size(118, 17);
            this.cboCustomer.MinValue = null;
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.NullTextColor = System.Drawing.SystemColors.GrayText;
            this.cboCustomer.NullValue = null;
            this.cboCustomer.OwnerOffset = 0;
            this.cboCustomer.Text = "-- Choose --";
            this.cboCustomer.Value = null;
            // 
            // radToolStripLabelElement3
            // 
            this.radToolStripLabelElement3.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radToolStripLabelElement3.Name = "radToolStripLabelElement3";
            this.radToolStripLabelElement3.Text = "Customer";
            // 
            // cbSearch
            // 
            this.cbSearch.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbSearch.ArrowButtonMinWidth = 16;
            this.cbSearch.DefaultValue = null;
            this.cbSearch.EditorElement = this.cbSearch;
            this.cbSearch.EditorManager = null;
            this.cbSearch.Focusable = true;
            this.cbSearch.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radComboBoxItem1,
            this.radComboBoxItem2,
            this.radComboBoxItem3});
            this.cbSearch.MaxSize = new System.Drawing.Size(118, 20);
            this.cbSearch.MaxValue = null;
            this.cbSearch.MinSize = new System.Drawing.Size(118, 17);
            this.cbSearch.MinValue = null;
            this.cbSearch.Name = "cbSearch";
            this.cbSearch.NullTextColor = System.Drawing.SystemColors.GrayText;
            this.cbSearch.NullValue = null;
            this.cbSearch.OwnerOffset = 0;
            this.cbSearch.Text = "-- Choose --";
            this.cbSearch.Value = null;
            // 
            // radComboBoxItem1
            // 
            this.radComboBoxItem1.Name = "radComboBoxItem1";
            this.radComboBoxItem1.Text = "All";
            // 
            // radComboBoxItem2
            // 
            this.radComboBoxItem2.Name = "radComboBoxItem2";
            this.radComboBoxItem2.Text = "Hal";
            // 
            // radComboBoxItem3
            // 
            this.radComboBoxItem3.Name = "radComboBoxItem3";
            this.radComboBoxItem3.Text = "Customer";
            // 
            // radToolStripLabelElement2
            // 
            this.radToolStripLabelElement2.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radToolStripLabelElement2.Name = "radToolStripLabelElement2";
            this.radToolStripLabelElement2.Text = "Search By";
            // 
            // radToolStripItem3
            // 
            this.radToolStripItem3.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripLabelElement2,
            this.cbSearch,
            this.radImageButtonSearch});
            this.radToolStripItem3.Key = "2";
            this.radToolStripItem3.Name = "radToolStripItem3";
            this.radToolStripItem3.Text = "radToolStripItem3";
            // 
            // radImageButtonSearch
            // 
            this.radImageButtonSearch.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radImageButtonSearch.Image = global::VisitaJayaPerkasa.Properties.Resources.search_16;
            this.radImageButtonSearch.ImageIndexClicked = 0;
            this.radImageButtonSearch.ImageIndexHovered = 0;
            this.radImageButtonSearch.Name = "radImageButtonSearch";
            this.radImageButtonSearch.ShowBorder = false;
            this.radImageButtonSearch.Text = "";
            // 
            // radToolStripElement1
            // 
            this.radToolStripElement1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripItem3,
            this.rtsiHal,
            this.rtsiCustomer});
            this.radToolStripElement1.Name = "radToolStripElement1";
            // 
            // rtsiHal
            // 
            this.rtsiHal.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripLabelElement1,
            this.cbHal});
            this.rtsiHal.Key = "0";
            this.rtsiHal.Name = "rtsiHal";
            this.rtsiHal.Text = "radToolStripItem1";
            this.rtsiHal.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // radToolStripLabelElement1
            // 
            this.radToolStripLabelElement1.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radToolStripLabelElement1.Name = "radToolStripLabelElement1";
            this.radToolStripLabelElement1.Text = "Hal";
            // 
            // cbHal
            // 
            this.cbHal.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbHal.ArrowButtonMinWidth = 16;
            this.cbHal.DefaultValue = null;
            this.cbHal.EditorElement = this.cbHal;
            this.cbHal.EditorManager = null;
            this.cbHal.Focusable = true;
            this.cbHal.MaxSize = new System.Drawing.Size(118, 20);
            this.cbHal.MaxValue = null;
            this.cbHal.MinSize = new System.Drawing.Size(118, 17);
            this.cbHal.MinValue = null;
            this.cbHal.Name = "cbHal";
            this.cbHal.NullTextColor = System.Drawing.SystemColors.GrayText;
            this.cbHal.NullValue = null;
            this.cbHal.OwnerOffset = 0;
            this.cbHal.Text = "-- Choose --";
            this.cbHal.Value = null;
            // 
            // rtsiCustomer
            // 
            this.rtsiCustomer.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripLabelElement3,
            this.cboCustomer});
            this.rtsiCustomer.Key = "1";
            this.rtsiCustomer.Name = "rtsiCustomer";
            this.rtsiCustomer.Text = "radToolStripItem2";
            this.rtsiCustomer.Visibility = Telerik.WinControls.ElementVisibility.Visible;
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
            this.radToolStrip1.Size = new System.Drawing.Size(725, 74);
            this.radToolStrip1.TabIndex = 8;
            this.radToolStrip1.Text = "radToolStrip1";
            // 
            // RptChangeDestinationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.radToolStrip1);
            this.Name = "RptChangeDestinationControl";
            this.Size = new System.Drawing.Size(725, 368);
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            this.radGridView1.ResumeLayout(false);
            this.radGridView1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radCalendarBegin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCalendarEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbHal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView radGridView1;
        private Telerik.WinControls.UI.RadCalendar radCalendarBegin;
        private Telerik.WinControls.UI.RadCalendar radCalendarEnd;
        private Telerik.WinControls.UI.RadImageButtonElement imageButtonDateStart;
        private Telerik.WinControls.UI.RadToolStripLabelElement lblDateBegin;
        private Telerik.WinControls.UI.RadToolStripLabelElement radToolStripLabelElement4;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem1;
        private Telerik.WinControls.UI.RadToolStripLabelElement radToolStripLabelElement5;
        private Telerik.WinControls.UI.RadToolStripLabelElement lblDateEnd;
        private Telerik.WinControls.UI.RadImageButtonElement imageButtonDateEnd;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement2;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem2;
        private Telerik.WinControls.UI.RadButtonElement buttonNew;
        private Telerik.WinControls.UI.RadButtonElement buttonPrint;
        private Telerik.WinControls.UI.RadButtonElement buttonEdit;
        private Telerik.WinControls.UI.RadComboBoxElement cboCustomer;
        private Telerik.WinControls.UI.RadToolStripLabelElement radToolStripLabelElement3;
        private Telerik.WinControls.UI.RadComboBoxElement cbSearch;
        private Telerik.WinControls.UI.RadComboBoxItem radComboBoxItem1;
        private Telerik.WinControls.UI.RadComboBoxItem radComboBoxItem2;
        private Telerik.WinControls.UI.RadComboBoxItem radComboBoxItem3;
        private Telerik.WinControls.UI.RadToolStripLabelElement radToolStripLabelElement2;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem3;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonSearch;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement1;
        private Telerik.WinControls.UI.RadToolStripItem rtsiHal;
        private Telerik.WinControls.UI.RadToolStripLabelElement radToolStripLabelElement1;
        private Telerik.WinControls.UI.RadComboBoxElement cbHal;
        private Telerik.WinControls.UI.RadToolStripItem rtsiCustomer;
        private Telerik.WinControls.UI.RadToolStrip radToolStrip1;
    }
}
