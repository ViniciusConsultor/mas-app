﻿namespace VisitaJayaPerkasa.Form.Report.SI
{
    partial class rptSIControl
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cboKapal = new Telerik.WinControls.UI.RadComboBox();
            this.cboCity = new Telerik.WinControls.UI.RadComboBox();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.cboSupplier = new Telerik.WinControls.UI.RadComboBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboKapal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSupplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(199, 159);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(150, 20);
            this.dateTimePicker1.TabIndex = 58;
            // 
            // cboKapal
            // 
            this.cboKapal.Location = new System.Drawing.Point(199, 127);
            this.cboKapal.Name = "cboKapal";
            // 
            // 
            // 
            this.cboKapal.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.cboKapal.RootElement.StretchVertically = true;
            this.cboKapal.Size = new System.Drawing.Size(150, 20);
            this.cboKapal.TabIndex = 57;
            this.cboKapal.TabStop = false;
            this.cboKapal.Text = "-- Choose --";
            // 
            // cboCity
            // 
            this.cboCity.Location = new System.Drawing.Point(199, 97);
            this.cboCity.Name = "cboCity";
            // 
            // 
            // 
            this.cboCity.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.cboCity.RootElement.StretchVertically = true;
            this.cboCity.Size = new System.Drawing.Size(150, 20);
            this.cboCity.TabIndex = 56;
            this.cboCity.TabStop = false;
            this.cboCity.Text = "-- Choose --";
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(58, 164);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(29, 16);
            this.radLabel4.TabIndex = 55;
            this.radLabel4.Text = "ATD";
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(58, 131);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(35, 16);
            this.radLabel3.TabIndex = 54;
            this.radLabel3.Text = "Kapal";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(58, 101);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(41, 16);
            this.radLabel1.TabIndex = 53;
            this.radLabel1.Text = "Tujuan";
            // 
            // cboSupplier
            // 
            this.cboSupplier.Location = new System.Drawing.Point(199, 68);
            this.cboSupplier.Name = "cboSupplier";
            // 
            // 
            // 
            this.cboSupplier.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.cboSupplier.RootElement.StretchVertically = true;
            this.cboSupplier.Size = new System.Drawing.Size(150, 20);
            this.cboSupplier.TabIndex = 52;
            this.cboSupplier.TabStop = false;
            this.cboSupplier.Text = "-- Choose --";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(58, 66);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(48, 16);
            this.radLabel2.TabIndex = 51;
            this.radLabel2.Text = "Supplier";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(199, 199);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(150, 22);
            this.btnPrint.TabIndex = 59;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // rptSIControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.cboKapal);
            this.Controls.Add(this.cboCity);
            this.Controls.Add(this.radLabel4);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.cboSupplier);
            this.Controls.Add(this.radLabel2);
            this.Name = "rptSIControl";
            this.Size = new System.Drawing.Size(584, 310);
            ((System.ComponentModel.ISupportInitialize)(this.cboKapal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSupplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private Telerik.WinControls.UI.RadComboBox cboKapal;
        private Telerik.WinControls.UI.RadComboBox cboCity;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadComboBox cboSupplier;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadButton btnPrint;
    }
}
