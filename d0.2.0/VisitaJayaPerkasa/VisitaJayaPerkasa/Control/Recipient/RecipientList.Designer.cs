﻿namespace VisitaJayaPerkasa.Control.Recipient
{
    partial class RecipientList
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
            this.radToolStripLabelIndexing = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.RecipientGridView = new Telerik.WinControls.UI.RadGridView();
            this.radComboBoxElement = new Telerik.WinControls.UI.RadComboBoxElement();
            this.radComboBoxItem1 = new Telerik.WinControls.UI.RadComboBoxItem();
            this.radComboBoxItem3 = new Telerik.WinControls.UI.RadComboBoxItem();
            this.radToolStripItem4 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radTextBoxElementSearchWord = new Telerik.WinControls.UI.RadTextBoxElement();
            this.radButtonElementBtnSearch = new Telerik.WinControls.UI.RadButtonElement();
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
            ((System.ComponentModel.ISupportInitialize)(this.RecipientGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radComboBoxElement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).BeginInit();
            this.SuspendLayout();
            // 
            // radToolStripLabelIndexing
            // 
            this.radToolStripLabelIndexing.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radToolStripLabelIndexing.Name = "radToolStripLabelIndexing";
            this.radToolStripLabelIndexing.Text = "-";
            // 
            // RecipientGridView
            // 
            this.RecipientGridView.BackColor = System.Drawing.SystemColors.Control;
            this.RecipientGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.RecipientGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecipientGridView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.RecipientGridView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RecipientGridView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RecipientGridView.Location = new System.Drawing.Point(0, 76);
            // 
            // 
            // 
            this.RecipientGridView.MasterGridViewTemplate.AllowAddNewRow = false;
            gridViewTextBoxColumn1.FieldAlias = "ID";
            gridViewTextBoxColumn1.FieldName = "ID";
            gridViewTextBoxColumn1.HeaderText = "ID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.UniqueName = "ID";
            gridViewTextBoxColumn2.FieldAlias = "Name";
            gridViewTextBoxColumn2.FieldName = "Name";
            gridViewTextBoxColumn2.HeaderText = "Name";
            gridViewTextBoxColumn2.UniqueName = "Name";
            gridViewTextBoxColumn2.Width = 181;
            gridViewTextBoxColumn3.FieldAlias = "Address";
            gridViewTextBoxColumn3.FieldName = "Address";
            gridViewTextBoxColumn3.HeaderText = "Address";
            gridViewTextBoxColumn3.UniqueName = "Address";
            gridViewTextBoxColumn3.Width = 200;
            this.RecipientGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn1);
            this.RecipientGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn2);
            this.RecipientGridView.MasterGridViewTemplate.Columns.Add(gridViewTextBoxColumn3);
            gridSortField1.FieldAlias = "SupplierName";
            gridSortField1.FieldName = "SupplierName";
            gridSortField1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.RecipientGridView.MasterGridViewTemplate.SortExpressions.Add(gridSortField1);
            this.RecipientGridView.Name = "RecipientGridView";
            this.RecipientGridView.ReadOnly = true;
            this.RecipientGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RecipientGridView.Size = new System.Drawing.Size(837, 300);
            this.RecipientGridView.TabIndex = 5;
            this.RecipientGridView.ThemeName = "ControlDefault";
            this.RecipientGridView.DoubleClick += new System.EventHandler(this.RecipientGridView_DoubleClick);
            // 
            // radComboBoxElement
            // 
            this.radComboBoxElement.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radComboBoxElement.ArrowButtonMinWidth = 16;
            this.radComboBoxElement.DefaultValue = null;
            this.radComboBoxElement.EditorElement = this.radComboBoxElement;
            this.radComboBoxElement.EditorManager = null;
            this.radComboBoxElement.FlipText = false;
            this.radComboBoxElement.Focusable = true;
            this.radComboBoxElement.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radComboBoxItem1,
            this.radComboBoxItem3});
            this.radComboBoxElement.MaxSize = new System.Drawing.Size(118, 20);
            this.radComboBoxElement.MaxValue = null;
            this.radComboBoxElement.MinSize = new System.Drawing.Size(118, 17);
            this.radComboBoxElement.MinValue = null;
            this.radComboBoxElement.Name = "radComboBoxElement";
            this.radComboBoxElement.NullText = "-- Choose --";
            this.radComboBoxElement.NullTextColor = System.Drawing.SystemColors.GrayText;
            this.radComboBoxElement.NullValue = null;
            this.radComboBoxElement.OwnerOffset = 0;
            this.radComboBoxElement.Sorted = Telerik.WinControls.Enumerations.SortStyle.Ascending;
            this.radComboBoxElement.Text = "";
            this.radComboBoxElement.Value = null;
            this.radComboBoxElement.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.radComboBoxElement_KeyPress);
            // 
            // radComboBoxItem1
            // 
            this.radComboBoxItem1.Name = "radComboBoxItem1";
            this.radComboBoxItem1.Text = "Address";
            // 
            // radComboBoxItem3
            // 
            this.radComboBoxItem3.Name = "radComboBoxItem3";
            this.radComboBoxItem3.Text = "Name";
            // 
            // radToolStripItem4
            // 
            this.radToolStripItem4.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radComboBoxElement,
            this.radTextBoxElementSearchWord,
            this.radButtonElementBtnSearch});
            this.radToolStripItem4.Key = "0";
            this.radToolStripItem4.Name = "radToolStripItem4";
            this.radToolStripItem4.Text = "radToolStripItem4";
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
            this.radToolStrip1.Size = new System.Drawing.Size(837, 76);
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
            // radButtonElementNext
            // 
            this.radButtonElementNext.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElementNext.Image = global::VisitaJayaPerkasa.Properties.Resources.next_16;
            this.radButtonElementNext.Name = "radButtonElementNext";
            this.radButtonElementNext.ShowBorder = false;
            this.radButtonElementNext.Text = "";
            this.radButtonElementNext.Click += new System.EventHandler(this.radButtonElementNext_Click);
            // 
            // radToolStripElement2
            // 
            this.radToolStripElement2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripItem4});
            this.radToolStripElement2.Name = "radToolStripElement2";
            this.radToolStripElement2.Text = "radToolStripElement2";
            // 
            // RecipientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RecipientGridView);
            this.Controls.Add(this.radToolStrip1);
            this.Name = "RecipientList";
            this.Size = new System.Drawing.Size(837, 376);
            ((System.ComponentModel.ISupportInitialize)(this.RecipientGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radComboBoxElement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadToolStripLabelElement radToolStripLabelIndexing;
        private Telerik.WinControls.UI.RadGridView RecipientGridView;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementNext;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementPrev;
        private Telerik.WinControls.UI.RadComboBoxElement radComboBoxElement;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem4;
        private Telerik.WinControls.UI.RadTextBoxElement radTextBoxElementSearchWord;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementBtnSearch;
        private Telerik.WinControls.UI.RadToolStrip radToolStrip1;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement1;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem1;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementCreate;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementEdit;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementRemove;
        private Telerik.WinControls.UI.RadButtonElement radButtonElementRefresh;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem2;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement2;
        private Telerik.WinControls.UI.RadComboBoxItem radComboBoxItem1;
        private Telerik.WinControls.UI.RadComboBoxItem radComboBoxItem3;
    }
}
