namespace VisitaJayaPerkasa.Form.Report.LeadTime
{
    partial class newLeadTime
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.etNoSurat = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radDateTimePicker1 = new Telerik.WinControls.UI.RadDateTimePicker();
            this.cboCustomer = new Telerik.WinControls.UI.RadComboBox();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.etNoSurat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDateTimePicker1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // etNoSurat
            // 
            this.etNoSurat.Location = new System.Drawing.Point(153, 27);
            this.etNoSurat.Name = "etNoSurat";
            this.etNoSurat.ReadOnly = true;
            this.etNoSurat.Size = new System.Drawing.Size(142, 20);
            this.etNoSurat.TabIndex = 24;
            this.etNoSurat.TabStop = false;
            // 
            // radLabel5
            // 
            this.radLabel5.Location = new System.Drawing.Point(12, 31);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(23, 16);
            this.radLabel5.TabIndex = 23;
            this.radLabel5.Text = "Hal";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(12, 53);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(112, 16);
            this.radLabel1.TabIndex = 25;
            this.radLabel1.Text = "Tgl Terima Container";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(12, 75);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(55, 16);
            this.radLabel2.TabIndex = 27;
            this.radLabel2.Text = "Customer";
            // 
            // radDateTimePicker1
            // 
            this.radDateTimePicker1.AutoSize = true;
            this.radDateTimePicker1.Culture = new System.Globalization.CultureInfo("id-ID");
            this.radDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.radDateTimePicker1.Location = new System.Drawing.Point(153, 49);
            this.radDateTimePicker1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.radDateTimePicker1.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.radDateTimePicker1.Name = "radDateTimePicker1";
            this.radDateTimePicker1.NullDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.radDateTimePicker1.Size = new System.Drawing.Size(142, 22);
            this.radDateTimePicker1.TabIndex = 28;
            this.radDateTimePicker1.TabStop = false;
            this.radDateTimePicker1.Text = "radDateTimePicker1";
            this.radDateTimePicker1.Value = new System.DateTime(2012, 5, 9, 10, 13, 22, 606);
            // 
            // cboCustomer
            // 
            this.cboCustomer.Location = new System.Drawing.Point(153, 77);
            this.cboCustomer.Name = "cboCustomer";
            // 
            // 
            // 
            this.cboCustomer.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.cboCustomer.RootElement.StretchVertically = true;
            this.cboCustomer.Size = new System.Drawing.Size(150, 20);
            this.cboCustomer.TabIndex = 42;
            this.cboCustomer.TabStop = false;
            this.cboCustomer.Text = "-- Choose --";
            this.cboCustomer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboCustomer_KeyPress);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 135);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 22);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(53, 135);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 22);
            this.btnSave.TabIndex = 44;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // newLeadTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 219);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboCustomer);
            this.Controls.Add(this.radDateTimePicker1);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.etNoSurat);
            this.Controls.Add(this.radLabel5);
            this.Name = "newLeadTime";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Create Lead Time";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.etNoSurat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDateTimePicker1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox etNoSurat;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadDateTimePicker radDateTimePicker1;
        private Telerik.WinControls.UI.RadComboBox cboCustomer;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadButton btnSave;
    }
}

