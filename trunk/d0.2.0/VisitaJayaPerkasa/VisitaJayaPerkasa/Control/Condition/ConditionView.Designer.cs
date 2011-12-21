namespace VisitaJayaPerkasa.Control.ConditionControl
{
    partial class ConditionView
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
            this.lblConditionName = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radToolStripItem1 = new Telerik.WinControls.UI.RadToolStripItem();
            this.lblConditionCode = new Telerik.WinControls.UI.RadLabel();
            this.radToolStripElement1 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radToolStrip1 = new Telerik.WinControls.UI.RadToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.lblConditionName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblConditionCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
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
            // lblConditionName
            // 
            this.lblConditionName.Location = new System.Drawing.Point(125, 91);
            this.lblConditionName.Name = "lblConditionName";
            this.lblConditionName.Size = new System.Drawing.Size(36, 16);
            this.lblConditionName.TabIndex = 28;
            this.lblConditionName.Text = "Name";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(47, 91);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(59, 16);
            this.radLabel2.TabIndex = 26;
            this.radLabel2.Text = "City Name";
            // 
            // radToolStripItem1
            // 
            this.radToolStripItem1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radButtonClose});
            this.radToolStripItem1.Key = "0";
            this.radToolStripItem1.Name = "radToolStripItem1";
            this.radToolStripItem1.Text = "radToolStripItem1";
            // 
            // lblConditionCode
            // 
            this.lblConditionCode.Location = new System.Drawing.Point(125, 69);
            this.lblConditionCode.Name = "lblConditionCode";
            this.lblConditionCode.Size = new System.Drawing.Size(33, 16);
            this.lblConditionCode.TabIndex = 27;
            this.lblConditionCode.Text = "Code";
            // 
            // radToolStripElement1
            // 
            this.radToolStripElement1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radToolStripItem1});
            this.radToolStripElement1.Name = "radToolStripElement1";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(47, 69);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(56, 16);
            this.radLabel1.TabIndex = 25;
            this.radLabel1.Text = "City Code";
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
            this.radToolStrip1.Size = new System.Drawing.Size(498, 49);
            this.radToolStrip1.TabIndex = 24;
            this.radToolStrip1.Text = "radToolStrip1";
            // 
            // ConditionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblConditionName);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.lblConditionCode);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radToolStrip1);
            this.Name = "ConditionView";
            this.Size = new System.Drawing.Size(498, 333);
            ((System.ComponentModel.ISupportInitialize)(this.lblConditionName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblConditionCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButtonElement radButtonClose;
        private Telerik.WinControls.UI.RadLabel lblConditionName;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem1;
        private Telerik.WinControls.UI.RadLabel lblConditionCode;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadToolStrip radToolStrip1;
    }
}
