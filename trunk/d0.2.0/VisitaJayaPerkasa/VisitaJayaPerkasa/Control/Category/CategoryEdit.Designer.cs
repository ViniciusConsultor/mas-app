namespace VisitaJayaPerkasa.Control.CategoryControl
{
    partial class CategoryEdit
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.etCategoryCode = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.etCategoryName = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radButtonClose = new Telerik.WinControls.UI.RadButtonElement();
            this.radButtonSave = new Telerik.WinControls.UI.RadButtonElement();
            this.radToolStripItem1 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radToolStripElement1 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radToolStrip1 = new Telerik.WinControls.UI.RadToolStrip();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etCategoryCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etCategoryName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.etCategoryCode);
            this.groupBox2.Controls.Add(this.radLabel2);
            this.groupBox2.Controls.Add(this.etCategoryName);
            this.groupBox2.Controls.Add(this.radLabel1);
            this.groupBox2.Location = new System.Drawing.Point(42, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 94);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Condition";
            // 
            // etCategoryCode
            // 
            this.etCategoryCode.Location = new System.Drawing.Point(129, 19);
            this.etCategoryCode.Name = "etCategoryCode";
            this.etCategoryCode.Size = new System.Drawing.Size(210, 20);
            this.etCategoryCode.TabIndex = 1;
            this.etCategoryCode.TabStop = false;
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(21, 19);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(56, 16);
            this.radLabel2.TabIndex = 12;
            this.radLabel2.Text = "City Code";
            // 
            // etCategoryName
            // 
            this.etCategoryName.Location = new System.Drawing.Point(129, 55);
            this.etCategoryName.Name = "etCategoryName";
            this.etCategoryName.Size = new System.Drawing.Size(210, 20);
            this.etCategoryName.TabIndex = 2;
            this.etCategoryName.TabStop = false;
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(21, 55);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(59, 16);
            this.radLabel1.TabIndex = 7;
            this.radLabel1.Text = "City Name";
            // 
            // radButtonClose
            // 
            this.radButtonClose.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonClose.Image = global::VisitaJayaPerkasa.Properties.Resources.close_16;
            this.radButtonClose.Name = "radButtonClose";
            this.radButtonClose.ShowBorder = false;
            this.radButtonClose.Text = "Close";
            this.radButtonClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radButtonClose.Click += new System.EventHandler(this.radButtonClose_Click);
            // 
            // radButtonSave
            // 
            this.radButtonSave.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonSave.Image = global::VisitaJayaPerkasa.Properties.Resources.save_16;
            this.radButtonSave.Name = "radButtonSave";
            this.radButtonSave.ShowBorder = false;
            this.radButtonSave.Text = "Save";
            this.radButtonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radButtonSave.Click += new System.EventHandler(this.radButtonSave_Click);
            // 
            // radToolStripItem1
            // 
            this.radToolStripItem1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radButtonSave,
            this.radButtonClose});
            this.radToolStripItem1.Key = "0";
            this.radToolStripItem1.Name = "radToolStripItem1";
            this.radToolStripItem1.Text = "radToolStripItem1";
            // 
            // radToolStripElement1
            // 
            this.radToolStripElement1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripItem1});
            this.radToolStripElement1.Name = "radToolStripElement1";
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
            this.radToolStrip1.Size = new System.Drawing.Size(627, 48);
            this.radToolStrip1.TabIndex = 28;
            this.radToolStrip1.Text = "radToolStrip1";
            // 
            // CategoryEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.radToolStrip1);
            this.Name = "CategoryEdit";
            this.Size = new System.Drawing.Size(627, 468);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etCategoryCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etCategoryName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private Telerik.WinControls.UI.RadTextBox etCategoryCode;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox etCategoryName;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButtonElement radButtonClose;
        private Telerik.WinControls.UI.RadButtonElement radButtonSave;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem1;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement1;
        private Telerik.WinControls.UI.RadToolStrip radToolStrip1;
    }
}
