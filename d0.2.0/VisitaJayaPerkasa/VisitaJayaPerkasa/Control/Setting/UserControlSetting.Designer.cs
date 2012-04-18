namespace VisitaJayaPerkasa.Control.Setting
{
    partial class UserControlSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlSetting));
            this.radTextBox1 = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.txtIPAddress = new Telerik.WinControls.UI.RadTextBox();
            this.txtPass = new Telerik.WinControls.UI.RadTextBox();
            this.txtResult = new Telerik.WinControls.UI.RadTextBox();
            this.btnGenerate = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerate)).BeginInit();
            this.SuspendLayout();
            // 
            // radTextBox1
            // 
            this.radTextBox1.Location = new System.Drawing.Point(32, 61);
            this.radTextBox1.Multiline = true;
            this.radTextBox1.Name = "radTextBox1";
            this.radTextBox1.ReadOnly = true;
            // 
            // 
            // 
            this.radTextBox1.RootElement.StretchVertically = true;
            this.radTextBox1.Size = new System.Drawing.Size(475, 69);
            this.radTextBox1.TabIndex = 0;
            this.radTextBox1.TabStop = false;
            this.radTextBox1.Text = resources.GetString("radTextBox1.Text");
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(32, 149);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(61, 16);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "IP Address";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(32, 186);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(56, 16);
            this.radLabel2.TabIndex = 2;
            this.radLabel2.Text = "Password";
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(32, 39);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(30, 16);
            this.radLabel3.TabIndex = 3;
            this.radLabel3.Text = "Note";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(124, 149);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(224, 20);
            this.txtIPAddress.TabIndex = 4;
            this.txtIPAddress.TabStop = false;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(124, 182);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(224, 20);
            this.txtPass.TabIndex = 5;
            this.txtPass.TabStop = false;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(188, 218);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(319, 20);
            this.txtResult.TabIndex = 6;
            this.txtResult.TabStop = false;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(54, 218);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(128, 23);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate result :";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // UserControlSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radTextBox1);
            this.Name = "UserControlSetting";
            this.Size = new System.Drawing.Size(532, 359);
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox radTextBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadTextBox txtIPAddress;
        private Telerik.WinControls.UI.RadTextBox txtPass;
        private Telerik.WinControls.UI.RadTextBox txtResult;
        private Telerik.WinControls.UI.RadButton btnGenerate;

    }
}
