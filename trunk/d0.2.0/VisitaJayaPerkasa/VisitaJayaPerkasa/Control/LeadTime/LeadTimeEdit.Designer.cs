namespace VisitaJayaPerkasa.Control.LeadTime
{
    partial class LeadTimeEdit
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
            this.cbCity = new Telerik.WinControls.UI.RadComboBox();
            this.etLeadTimeDays = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radToolStripItem1 = new Telerik.WinControls.UI.RadToolStripItem();
            this.radButtonElement1 = new Telerik.WinControls.UI.RadButtonElement();
            this.radButtonElement2 = new Telerik.WinControls.UI.RadButtonElement();
            this.radToolStripElement1 = new Telerik.WinControls.UI.RadToolStripElement();
            this.radToolStrip1 = new Telerik.WinControls.UI.RadToolStrip();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbCity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etLeadTimeDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbCity);
            this.groupBox2.Controls.Add(this.etLeadTimeDays);
            this.groupBox2.Controls.Add(this.radLabel2);
            this.groupBox2.Controls.Add(this.radLabel1);
            this.groupBox2.Location = new System.Drawing.Point(34, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(317, 99);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lead Time";
            // 
            // cbCity
            // 
            this.cbCity.Location = new System.Drawing.Point(93, 19);
            this.cbCity.Name = "cbCity";
            // 
            // 
            // 
            this.cbCity.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.cbCity.Size = new System.Drawing.Size(210, 20);
            this.cbCity.TabIndex = 13;
            this.cbCity.TabStop = false;
            this.cbCity.Text = "-- Choose --";
            this.cbCity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.radComboBox1_KeyPress);
            // 
            // etLeadTimeDays
            // 
            this.etLeadTimeDays.Location = new System.Drawing.Point(93, 51);
            this.etLeadTimeDays.Name = "etLeadTimeDays";
            this.etLeadTimeDays.Size = new System.Drawing.Size(210, 20);
            this.etLeadTimeDays.TabIndex = 1;
            this.etLeadTimeDays.TabStop = false;
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(21, 19);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(26, 16);
            this.radLabel2.TabIndex = 12;
            this.radLabel2.Text = "City";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(21, 55);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(32, 16);
            this.radLabel1.TabIndex = 7;
            this.radLabel1.Text = "Days";
            // 
            // radToolStripItem1
            // 
            this.radToolStripItem1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radButtonElement1,
            this.radButtonElement2});
            this.radToolStripItem1.Key = "0";
            this.radToolStripItem1.Name = "radToolStripItem1";
            this.radToolStripItem1.Text = "radToolStripItem1";
            // 
            // radButtonElement1
            // 
            this.radButtonElement1.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElement1.Image = global::VisitaJayaPerkasa.Properties.Resources.save_16;
            this.radButtonElement1.Name = "radButtonElement1";
            this.radButtonElement1.ShowBorder = false;
            this.radButtonElement1.Text = "Save";
            this.radButtonElement1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radButtonElement1.Click += new System.EventHandler(this.radButtonElement1_Click);
            // 
            // radButtonElement2
            // 
            this.radButtonElement2.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radButtonElement2.Image = global::VisitaJayaPerkasa.Properties.Resources.close_16;
            this.radButtonElement2.Name = "radButtonElement2";
            this.radButtonElement2.ShowBorder = false;
            this.radButtonElement2.Text = "Close";
            this.radButtonElement2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radButtonElement2.Click += new System.EventHandler(this.radButtonElement2_Click);
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
            this.radToolStrip1.Size = new System.Drawing.Size(646, 49);
            this.radToolStrip1.TabIndex = 24;
            this.radToolStrip1.Text = "radToolStrip1";
            // 
            // LeadTimeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.radToolStrip1);
            this.Name = "LeadTimeEdit";
            this.Size = new System.Drawing.Size(646, 429);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbCity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etLeadTimeDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radToolStrip1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private Telerik.WinControls.UI.RadTextBox etLeadTimeDays;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButtonElement radButtonElement2;
        private Telerik.WinControls.UI.RadButtonElement radButtonElement1;
        private Telerik.WinControls.UI.RadToolStripItem radToolStripItem1;
        private Telerik.WinControls.UI.RadToolStripElement radToolStripElement1;
        private Telerik.WinControls.UI.RadToolStrip radToolStrip1;
        private Telerik.WinControls.UI.RadComboBox cbCity;
    }
}
