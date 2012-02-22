namespace VisitaJayaPerkasa.Form.Report.Delivery
{
    partial class RptDeliveryControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnViewReport = new Telerik.WinControls.UI.RadButton();
            this.spnYear = new Telerik.WinControls.UI.RadSpinEditor();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.cboCustomer = new Telerik.WinControls.UI.RadComboBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.reportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnViewReport);
            this.groupBox1.Controls.Add(this.spnYear);
            this.groupBox1.Controls.Add(this.cboMonth);
            this.groupBox1.Controls.Add(this.cboCustomer);
            this.groupBox1.Controls.Add(this.radLabel2);
            this.groupBox1.Controls.Add(this.radLabel1);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 407);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Field Filter";
            // 
            // btnViewReport
            // 
            this.btnViewReport.Location = new System.Drawing.Point(7, 85);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(213, 24);
            this.btnViewReport.TabIndex = 3;
            this.btnViewReport.Text = "View Report";
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // spnYear
            // 
            this.spnYear.Location = new System.Drawing.Point(170, 46);
            this.spnYear.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.spnYear.Minimum = new decimal(new int[] {
            1970,
            0,
            0,
            0});
            this.spnYear.Name = "spnYear";
            // 
            // 
            // 
            this.spnYear.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.spnYear.ShowBorder = true;
            this.spnYear.Size = new System.Drawing.Size(50, 20);
            this.spnYear.TabIndex = 2;
            this.spnYear.TabStop = false;
            this.spnYear.Value = new decimal(new int[] {
            2012,
            0,
            0,
            0});
            // 
            // cboMonth
            // 
            this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cboMonth.Location = new System.Drawing.Point(62, 46);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(102, 21);
            this.cboMonth.TabIndex = 1;
            // 
            // cboCustomer
            // 
            this.cboCustomer.Location = new System.Drawing.Point(62, 20);
            this.cboCustomer.Name = "cboCustomer";
            // 
            // 
            // 
            this.cboCustomer.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.cboCustomer.Size = new System.Drawing.Size(158, 20);
            this.cboCustomer.TabIndex = 0;
            this.cboCustomer.TabStop = false;
            this.cboCustomer.Text = "radComboBox1";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(7, 49);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(39, 16);
            this.radLabel2.TabIndex = 1;
            this.radLabel2.Text = "Period";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(7, 22);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(55, 16);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Customer";
            // 
            // reportViewer
            // 
            this.reportViewer.ActiveViewIndex = -1;
            this.reportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.reportViewer.Location = new System.Drawing.Point(239, 3);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(750, 407);
            this.reportViewer.TabIndex = 4;
            this.reportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // RptDeliveryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.reportViewer);
            this.Name = "RptDeliveryControl";
            this.Size = new System.Drawing.Size(1000, 425);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadButton btnViewReport;
        private Telerik.WinControls.UI.RadSpinEditor spnYear;
        private System.Windows.Forms.ComboBox cboMonth;
        private Telerik.WinControls.UI.RadComboBox cboCustomer;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer reportViewer;

    }
}
