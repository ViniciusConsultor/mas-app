namespace VisitaJayaPerkasa.Control.CategoryControl
{
    partial class CategoryView
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
            this.radButtonClose = new Telerik.WinControls.UI.RadButtonElement();
            this.lblCategoryCode = new Telerik.WinControls.UI.RadLabel();
            this.radToolStripElement1 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radToolStripItem1 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.lblCategoryName = new Telerik.WinControls.UI.RadLabel();
            this.radToolStrip1 = new Telerik.WinControls.UI.RadToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.lblCategoryCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCategoryName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).BeginInit();
            this.SuspendLayout();
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
            // lblCategoryCode
            // 
            this.lblCategoryCode.Location = new System.Drawing.Point(121, 69);
            this.lblCategoryCode.Name = "lblCategoryCode";
            this.lblCategoryCode.Size = new System.Drawing.Size(33, 16);
            this.lblCategoryCode.TabIndex = 32;
            this.lblCategoryCode.Text = "Code";
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
            this.radButtonClose});
            this.radToolStripItem1.Key = "0";
            this.radToolStripItem1.Name = "radToolStripItem1";
            this.radToolStripItem1.Text = "radToolStripItem1";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(43, 69);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(56, 16);
            this.radLabel1.TabIndex = 30;
            this.radLabel1.Text = "City Code";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(43, 91);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(59, 16);
            this.radLabel2.TabIndex = 31;
            this.radLabel2.Text = "City Name";
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.Location = new System.Drawing.Point(121, 91);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(36, 16);
            this.lblCategoryName.TabIndex = 33;
            this.lblCategoryName.Text = "Name";
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
            this.radToolStrip1.Size = new System.Drawing.Size(498, 48);
            this.radToolStrip1.TabIndex = 29;
            this.radToolStrip1.Text = "radToolStrip1";
            // 
            // CategoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCategoryCode);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.lblCategoryName);
            this.Controls.Add(this.radToolStrip1);
            this.Name = "CategoryView";
            this.Size = new System.Drawing.Size(498, 333);
            ((System.ComponentModel.ISupportInitialize)(this.lblCategoryCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCategoryName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButtonElement radButtonClose;
        private Telerik.WinControls.UI.RadLabel lblCategoryCode;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement1;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel lblCategoryName;
        private Telerik.WinControls.UI.RadToolStrip radToolStrip1;
    }
}
