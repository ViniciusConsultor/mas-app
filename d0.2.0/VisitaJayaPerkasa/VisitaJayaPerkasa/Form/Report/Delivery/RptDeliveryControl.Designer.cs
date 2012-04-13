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
            this.reportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.radTabStrip1 = new Telerik.WinControls.UI.RadTabStrip();
            this.tabItem1 = new Telerik.WinControls.UI.TabItem();
            this.btnViewReport = new Telerik.WinControls.UI.RadButton();
            this.spnYear = new Telerik.WinControls.UI.RadSpinEditor();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.cboCustomer = new Telerik.WinControls.UI.RadComboBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.tabItem2 = new Telerik.WinControls.UI.TabItem();
            this.cboTransDetail = new Telerik.WinControls.UI.RadComboBox();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.cboTransDate = new Telerik.WinControls.UI.RadComboBox();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.btnViewBeritaAcara = new Telerik.WinControls.UI.RadButton();
            this.cboCustomer2 = new Telerik.WinControls.UI.RadComboBox();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radTabStrip1)).BeginInit();
            this.radTabStrip1.SuspendLayout();
            this.tabItem1.ContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.tabItem2.ContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewBeritaAcara)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            this.SuspendLayout();
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
            // radTabStrip1
            // 
            this.radTabStrip1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.radTabStrip1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.radTabStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.tabItem1,
            this.tabItem2});
            this.radTabStrip1.Location = new System.Drawing.Point(7, 3);
            this.radTabStrip1.Name = "radTabStrip1";
            this.radTabStrip1.ScrollOffsetStep = 5;
            this.radTabStrip1.Size = new System.Drawing.Size(226, 407);
            this.radTabStrip1.TabIndex = 6;
            this.radTabStrip1.TabScrollButtonsPosition = Telerik.WinControls.UI.TabScrollButtonsPosition.RightBottom;
            this.radTabStrip1.Text = "radTabStrip1";
            // 
            // tabItem1
            // 
            this.tabItem1.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tabItem1.ContentPanel
            // 
            this.tabItem1.ContentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.tabItem1.ContentPanel.CausesValidation = true;
            this.tabItem1.ContentPanel.Controls.Add(this.btnViewReport);
            this.tabItem1.ContentPanel.Controls.Add(this.spnYear);
            this.tabItem1.ContentPanel.Controls.Add(this.cboMonth);
            this.tabItem1.ContentPanel.Controls.Add(this.cboCustomer);
            this.tabItem1.ContentPanel.Controls.Add(this.radLabel2);
            this.tabItem1.ContentPanel.Controls.Add(this.radLabel1);
            this.tabItem1.ContentPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabItem1.ContentPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabItem1.ContentPanel.Location = new System.Drawing.Point(1, 29);
            this.tabItem1.ContentPanel.Size = new System.Drawing.Size(224, 377);
            this.tabItem1.IsSelected = true;
            this.tabItem1.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.StretchHorizontally = false;
            this.tabItem1.Text = "Delivery";
            // 
            // btnViewReport
            // 
            this.btnViewReport.Location = new System.Drawing.Point(6, 78);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(213, 24);
            this.btnViewReport.TabIndex = 9;
            this.btnViewReport.Text = "View Report";
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // spnYear
            // 
            this.spnYear.Location = new System.Drawing.Point(169, 39);
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
            this.spnYear.TabIndex = 8;
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
            this.cboMonth.Location = new System.Drawing.Point(61, 39);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(102, 21);
            this.cboMonth.TabIndex = 7;
            // 
            // cboCustomer
            // 
            this.cboCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCustomer.Location = new System.Drawing.Point(61, 13);
            this.cboCustomer.Name = "cboCustomer";
            // 
            // 
            // 
            this.cboCustomer.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.cboCustomer.Size = new System.Drawing.Size(158, 20);
            this.cboCustomer.TabIndex = 4;
            this.cboCustomer.TabStop = false;
            this.cboCustomer.Text = "radComboBox1";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(6, 42);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(39, 16);
            this.radLabel2.TabIndex = 6;
            this.radLabel2.Text = "Period";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(6, 15);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(55, 16);
            this.radLabel1.TabIndex = 5;
            this.radLabel1.Text = "Customer";
            // 
            // tabItem2
            // 
            this.tabItem2.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tabItem2.ContentPanel
            // 
            this.tabItem2.ContentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.tabItem2.ContentPanel.CausesValidation = true;
            this.tabItem2.ContentPanel.Controls.Add(this.cboTransDetail);
            this.tabItem2.ContentPanel.Controls.Add(this.radLabel5);
            this.tabItem2.ContentPanel.Controls.Add(this.cboTransDate);
            this.tabItem2.ContentPanel.Controls.Add(this.radLabel4);
            this.tabItem2.ContentPanel.Controls.Add(this.btnViewBeritaAcara);
            this.tabItem2.ContentPanel.Controls.Add(this.cboCustomer2);
            this.tabItem2.ContentPanel.Controls.Add(this.radLabel3);
            this.tabItem2.ContentPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabItem2.ContentPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabItem2.ContentPanel.Location = new System.Drawing.Point(1, 29);
            this.tabItem2.ContentPanel.Size = new System.Drawing.Size(224, 377);
            this.tabItem2.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.StretchHorizontally = false;
            this.tabItem2.Text = "Berita Acara";
            // 
            // cboTransDetail
            // 
            this.cboTransDetail.Location = new System.Drawing.Point(41, 65);
            this.cboTransDetail.Name = "cboTransDetail";
            // 
            // 
            // 
            this.cboTransDetail.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.cboTransDetail.Size = new System.Drawing.Size(178, 20);
            this.cboTransDetail.TabIndex = 14;
            this.cboTransDetail.TabStop = false;
            this.cboTransDetail.Text = "radComboBox1";
            // 
            // radLabel5
            // 
            this.radLabel5.Location = new System.Drawing.Point(6, 69);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(29, 16);
            this.radLabel5.TabIndex = 13;
            this.radLabel5.Text = "Detil";
            // 
            // cboTransDate
            // 
            this.cboTransDate.Location = new System.Drawing.Point(86, 39);
            this.cboTransDate.Name = "cboTransDate";
            // 
            // 
            // 
            this.cboTransDate.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.cboTransDate.Size = new System.Drawing.Size(133, 20);
            this.cboTransDate.TabIndex = 12;
            this.cboTransDate.TabStop = false;
            this.cboTransDate.Text = "radComboBox1";
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(6, 43);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(74, 16);
            this.radLabel4.TabIndex = 11;
            this.radLabel4.Text = "Tgl Transaksi";
            // 
            // btnViewBeritaAcara
            // 
            this.btnViewBeritaAcara.Location = new System.Drawing.Point(6, 96);
            this.btnViewBeritaAcara.Name = "btnViewBeritaAcara";
            this.btnViewBeritaAcara.Size = new System.Drawing.Size(213, 24);
            this.btnViewBeritaAcara.TabIndex = 10;
            this.btnViewBeritaAcara.Text = "View Berita Acara";
            this.btnViewBeritaAcara.Click += new System.EventHandler(this.btnViewBeritaAcara_Click);
            // 
            // cboCustomer2
            // 
            this.cboCustomer2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCustomer2.Location = new System.Drawing.Point(61, 13);
            this.cboCustomer2.Name = "cboCustomer2";
            // 
            // 
            // 
            this.cboCustomer2.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.cboCustomer2.Size = new System.Drawing.Size(158, 20);
            this.cboCustomer2.TabIndex = 2;
            this.cboCustomer2.TabStop = false;
            this.cboCustomer2.Text = "radComboBox1";
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(6, 17);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(55, 16);
            this.radLabel3.TabIndex = 1;
            this.radLabel3.Text = "Customer";
            // 
            // RptDeliveryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radTabStrip1);
            this.Controls.Add(this.reportViewer);
            this.Name = "RptDeliveryControl";
            this.Size = new System.Drawing.Size(1000, 425);
            ((System.ComponentModel.ISupportInitialize)(this.radTabStrip1)).EndInit();
            this.radTabStrip1.ResumeLayout(false);
            this.tabItem1.ContentPanel.ResumeLayout(false);
            this.tabItem1.ContentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.tabItem2.ContentPanel.ResumeLayout(false);
            this.tabItem2.ContentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewBeritaAcara)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer reportViewer;
        private Telerik.WinControls.UI.RadTabStrip radTabStrip1;
        private Telerik.WinControls.UI.TabItem tabItem1;
        private Telerik.WinControls.UI.RadButton btnViewReport;
        private Telerik.WinControls.UI.RadSpinEditor spnYear;
        private System.Windows.Forms.ComboBox cboMonth;
        private Telerik.WinControls.UI.RadComboBox cboCustomer;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.TabItem tabItem2;
        private Telerik.WinControls.UI.RadButton btnViewBeritaAcara;
        private Telerik.WinControls.UI.RadComboBox cboCustomer2;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadComboBox cboTransDate;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadComboBox cboTransDetail;
        private Telerik.WinControls.UI.RadLabel radLabel5;

    }
}
