namespace VisitaJayaPerkasa.Control.ATK
{
    partial class AtkList
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
            this.AtkGridView = new Telerik.WinControls.UI.RadGridView();
            this.radToolStripItem2 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radButtonElementPrev = new Telerik.WinControls.UI.RadButtonElement();
            this.radToolStripLabelIndexing = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.radButtonElementNext = new Telerik.WinControls.UI.RadButtonElement();
            this.radToolStripElement1 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radToolStripItem1 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radButtonElementCreate = new Telerik.WinControls.UI.RadButtonElement();
            this.radButtonElementEdit = new Telerik.WinControls.UI.RadButtonElement();
            this.radButtonElementRemove = new Telerik.WinControls.UI.RadButtonElement();
            this.radButtonElementRefresh = new Telerik.WinControls.UI.RadButtonElement();
            this.radToolStripElement2 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radToolStripItem4 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radToolStripLabelElement1 = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.radTextBoxElementSearchWord = new Telerik.WinControls.UI.RadTextBoxElement();
            this.radButtonElementBtnSearch = new Telerik.WinControls.UI.RadButtonElement();
            this.radToolStrip1 = new Telerik.WinControls.UI.RadToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.AtkGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).BeginInit();
            this.SuspendLayout();
            // 
            // AtkGridView
            // 
            this.AtkGridView.BackColor = System.Drawing.SystemColors.Control;
            this.AtkGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.AtkGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AtkGridView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.AtkGridView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AtkGridView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AtkGridView.Location = new System.Drawing.Point(0, 54);
            // 
            // 
            // 
            this.AtkGridView.MasterGridViewTemplate.AllowAddNewRow = false;
            gridViewTextBoxColumn1.FieldAlias = "ID";
            gridViewTextBoxColumn1.FieldName = "ID";
            gridViewTextBoxColumn1.HeaderText = "ID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.UniqueName = "ID";
            gridViewTextBoxColumn2.FieldAlias = "DateFrom";
            gridViewTextBoxColumn2.FieldName = "DateFrom";
            gridViewTextBoxColumn2.FormatString = "{0:MM/dd/yyyy}";
            gridViewTextBoxColumn2.HeaderText = "Date";
            gridViewTextBoxColumn2.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewTextBoxColumn2.UniqueName = "DateFrom";
            gridViewTextBoxColumn2.Width = 120;
            gridViewTextBoxColumn3.FieldAlias = "SupplierName";
            gridViewTextBoxColumn3.FieldName = "SupplierName";
            gridViewTextBoxColumn3.HeaderText = "Supplier";
            gridViewTextBoxColumn3.UniqueName = "SupplierName";
            gridViewTextBoxColumn3.Width = 120;
            gridViewTextBoxColumn4.FieldAlias = "Item";
            gridViewTextBoxColumn4.FieldName = "Item";
            gridViewTextBoxColumn4.HeaderText = "Item";
            gridViewTextBoxColumn4.UniqueName = "Item";
            gridViewTextBoxColumn4.Width = 200;
            gridViewTextBoxColumn5.FieldAlias = "PriceSupplier";
            gridViewTextBoxColumn5.FieldName = "PriceSupplier";
            gridViewTextBoxColumn5.FormatString = "{0:c}";
            gridViewTextBoxColumn5.HeaderText = "Price";
            gridViewTextBoxColumn5.UniqueName = "PriceSupplier";
            gridViewTextBoxColumn5.Width = 120;
            this.AtkGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn1);
            this.AtkGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn2);
            this.AtkGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn3);
            this.AtkGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn4);
            this.AtkGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn5);
            gridSortField1.FieldAlias = "DateFrom";
            gridSortField1.FieldName = "DateFrom";
            gridSortField1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.AtkGridView.MasterGridViewTemplate.SortExpressions.Add(gridSortField1);
            this.AtkGridView.Name = "AtkGridView";
            this.AtkGridView.ReadOnly = true;
            this.AtkGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AtkGridView.Size = new System.Drawing.Size(694, 331);
            this.AtkGridView.TabIndex = 7;
            this.AtkGridView.ThemeName = "ControlDefault";
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
            this.radButtonElementPrev.Click += new System.EventHandler(this.radButtonElementPrev_Click);
            // 
            // radToolStripLabelIndexing
            // 
            this.radToolStripLabelIndexing.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radToolStripLabelIndexing.Name = "radToolStripLabelIndexing";
            this.radToolStripLabelIndexing.Text = "-";
            // 
            // radButtonElementNext
            // 
            this.radButtonElementNext.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElementNext.Image = global::VisitaJayaPerkasa.Properties.Resources.next_16;
            this.radButtonElementNext.Name = "radButtonElementNext";
            this.radButtonElementNext.ShowBorder = false;
            this.radButtonElementNext.Text = "";
            this.radButtonElementNext.Click += new System.EventHandler(this.radButtonElementNext_Click);
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
            this.radButtonElementCreate.Click += new System.EventHandler(this.radButtonElementCreate_Click);
            // 
            // radButtonElementEdit
            // 
            this.radButtonElementEdit.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElementEdit.Image = global::VisitaJayaPerkasa.Properties.Resources.edit_16;
            this.radButtonElementEdit.Name = "radButtonElementEdit";
            this.radButtonElementEdit.ShowBorder = false;
            this.radButtonElementEdit.Text = "Edit";
            this.radButtonElementEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radButtonElementEdit.Click += new System.EventHandler(this.radButtonElementEdit_Click);
            // 
            // radButtonElementRemove
            // 
            this.radButtonElementRemove.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElementRemove.Image = global::VisitaJayaPerkasa.Properties.Resources.delete_16;
            this.radButtonElementRemove.Name = "radButtonElementRemove";
            this.radButtonElementRemove.ShowBorder = false;
            this.radButtonElementRemove.Text = "Remove";
            this.radButtonElementRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radButtonElementRemove.Click += new System.EventHandler(this.radButtonElementRemove_Click);
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
            this.radButtonElementRefresh.Click += new System.EventHandler(this.radButtonElementRefresh_Click);
            // 
            // radToolStripElement2
            // 
            this.radToolStripElement2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripItem4});
            this.radToolStripElement2.Name = "radToolStripElement2";
            this.radToolStripElement2.Text = "radToolStripElement2";
            // 
            // radToolStripItem4
            // 
            this.radToolStripItem4.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripLabelElement1,
            this.radTextBoxElementSearchWord,
            this.radButtonElementBtnSearch});
            this.radToolStripItem4.Key = "0";
            this.radToolStripItem4.Name = "radToolStripItem4";
            this.radToolStripItem4.Text = "radToolStripItem4";
            // 
            // radToolStripLabelElement1
            // 
            this.radToolStripLabelElement1.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radToolStripLabelElement1.Name = "radToolStripLabelElement1";
            this.radToolStripLabelElement1.Text = "Item : ";
            // 
            // radTextBoxElementSearchWord
            // 
            this.radTextBoxElementSearchWord.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radTextBoxElementSearchWord.MaxSize = new System.Drawing.Size(118, 19);
            this.radTextBoxElementSearchWord.MinSize = new System.Drawing.Size(118, 19);
            this.radTextBoxElementSearchWord.Name = "radTextBoxElementSearchWord";
            this.radTextBoxElementSearchWord.ShowBorder = true;
            this.radTextBoxElementSearchWord.StretchVertically = false;
            this.radTextBoxElementSearchWord.Text = "";
            // 
            // radButtonElementBtnSearch
            // 
            this.radButtonElementBtnSearch.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElementBtnSearch.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.radButtonElementBtnSearch.Image = global::VisitaJayaPerkasa.Properties.Resources.search_16;
            this.radButtonElementBtnSearch.Name = "radButtonElementBtnSearch";
            this.radButtonElementBtnSearch.ShowBorder = false;
            this.radButtonElementBtnSearch.Text = "";
            this.radButtonElementBtnSearch.Click += new System.EventHandler(this.radButtonElementBtnSearch_Click);
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
            this.radToolStrip1.Size = new System.Drawing.Size(694, 54);
            this.radToolStrip1.TabIndex = 6;
            this.radToolStrip1.Text = "radToolStrip1";
            // 
            // AtkList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AtkGridView);
            this.Controls.Add(this.radToolStrip1);
            this.Name = "AtkList";
            this.Size = new System.Drawing.Size(694, 385);
            ((System.ComponentModel.ISupportInitialize)(this.AtkGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButtonElement radButtonElementNext;
        private Telerik.WinControls.UI.RadGridView AtkGridView;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem2;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementPrev;
        private Telerik.WinControls.UI.RadToolStripLabelElement radToolStripLabelIndexing;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementRefresh;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement1;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem1;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementCreate;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementEdit;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementRemove;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement2;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem4;
        private Telerik.WinControls.UI.RadTextBoxElement radTextBoxElementSearchWord;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementBtnSearch;
        private Telerik.WinControls.UI.RadToolStrip radToolStrip1;
        private Telerik.WinControls.UI.RadToolStripLabelElement radToolStripLabelElement1;
    }
}
