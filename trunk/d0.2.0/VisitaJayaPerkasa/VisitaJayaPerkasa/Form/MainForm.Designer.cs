﻿namespace VisitaJayaPerkasa.Form
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.radStatusStrip = new Telerik.WinControls.UI.RadStatusStrip();
            this.radLabelElementWelcome = new Telerik.WinControls.UI.RadLabelElement();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.radMenuItem7 = new Telerik.WinControls.UI.RadMenuItem();
            this.radRibbonBar1 = new Telerik.WinControls.UI.RadRibbonBar();
            this.ribbonTab1 = new Telerik.WinControls.UI.RibbonTab();
            this.radRibbonBarGroupUser = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radImageButtonUser = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radRibbonBarGroupCategory = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radRibbonBarGroupVessel = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radRibbonBarGroup1 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radRibbonBarGroup2 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radRibbonBarGroup3 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radRibbonBarGroup4 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radRibbonBarGroup5 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radRibbonBarGroup6 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radRibbonBarGroup7 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radRibbonBarGroup8 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.ribbonTab2 = new Telerik.WinControls.UI.RibbonTab();
            this.ribbonTab3 = new Telerik.WinControls.UI.RibbonTab();
            this.radMenuItemMasterData = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuItemUser = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuItemRole = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuItemVessel = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuItemTransaction = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuItem9 = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuItemReporting = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuSeparatorItem1 = new Telerik.WinControls.UI.RadMenuSeparatorItem();
            this.radMenuItemLogOut = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuSeparatorItem2 = new Telerik.WinControls.UI.RadMenuSeparatorItem();
            this.radMenuItemExit = new Telerik.WinControls.UI.RadMenuItem();
            this.radImageButtonElementWareHouse = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radImageButtonElementTypeCont = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radImageButtonElementSupplier = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radImageButtonElementPelayaran = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radImageButtonElementLeadTime = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radImageButtonElementCustomer = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radImageButtonElementCondition = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radImageButtonElementCity = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radImageButtonElementVessel = new Telerik.WinControls.UI.RadImageButtonElement();
            this.radImageButtonElementCategory = new Telerik.WinControls.UI.RadImageButtonElement();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "user.jpeg");
            this.imageList1.Images.SetKeyName(1, "vessel.jpeg");
            this.imageList1.Images.SetKeyName(2, "role.jpeg");
            this.imageList1.Images.SetKeyName(3, "city.jpeg");
            this.imageList1.Images.SetKeyName(4, "customer.jpeg");
            this.imageList1.Images.SetKeyName(5, "supplier.jpeg");
            this.imageList1.Images.SetKeyName(6, "lead_time.jpeg");
            this.imageList1.Images.SetKeyName(7, "pelayaran.jpeg");
            this.imageList1.Images.SetKeyName(8, "warehouse.jpeg");
            this.imageList1.Images.SetKeyName(9, "condition.jpeg");
            this.imageList1.Images.SetKeyName(10, "typecont.jpeg");
            // 
            // radStatusStrip
            // 
            this.radStatusStrip.AutoSize = true;
            this.radStatusStrip.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radLabelElementWelcome});
            this.radStatusStrip.LayoutStyle = Telerik.WinControls.UI.RadStatusBarLayoutStyle.Stack;
            this.radStatusStrip.Location = new System.Drawing.Point(0, 401);
            this.radStatusStrip.Name = "radStatusStrip";
            this.radStatusStrip.Size = new System.Drawing.Size(641, 24);
            this.radStatusStrip.SizingGrip = false;
            this.radStatusStrip.TabIndex = 1;
            this.radStatusStrip.Text = "radStatusStrip1";
            // 
            // radLabelElementWelcome
            // 
            this.radLabelElementWelcome.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radLabelElementWelcome.Margin = new System.Windows.Forms.Padding(1);
            this.radLabelElementWelcome.Name = "radLabelElementWelcome";
            this.radStatusStrip.SetSpring(this.radLabelElementWelcome, false);
            this.radLabelElementWelcome.Text = "";
            this.radLabelElementWelcome.TextWrap = true;
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Location = new System.Drawing.Point(0, 153);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(641, 248);
            this.MainPanel.TabIndex = 2;
            // 
            // radMenuItem7
            // 
            this.radMenuItem7.Name = "radMenuItem7";
            this.radMenuItem7.Text = "radMenuItem7";
            // 
            // radRibbonBar1
            // 
            this.radRibbonBar1.AutoSize = true;
            this.radRibbonBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.radRibbonBar1.CloseButton = false;
            this.radRibbonBar1.CommandTabs.AddRange(new Telerik.WinControls.RadItem[] {
            this.ribbonTab1,
            this.ribbonTab2,
            this.ribbonTab3});
            this.radRibbonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radRibbonBar1.EnableKeyMap = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.radRibbonBar1.ExitButton.ButtonElement.Class = "RadMenuButtonElement";
            this.radRibbonBar1.ExitButton.ButtonElement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radRibbonBar1.ExitButton.Text = "Exit";
            this.radRibbonBar1.ImageList = this.imageList1;
            this.radRibbonBar1.Location = new System.Drawing.Point(0, 0);
            this.radRibbonBar1.Name = "radRibbonBar1";
            // 
            // 
            // 
            // 
            // 
            // 
            this.radRibbonBar1.OptionsButton.ButtonElement.Class = "RadMenuButtonElement";
            this.radRibbonBar1.OptionsButton.ButtonElement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radRibbonBar1.OptionsButton.Text = "Options";
            // 
            // 
            // 
            this.radRibbonBar1.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.radRibbonBar1.Size = new System.Drawing.Size(641, 153);
            this.radRibbonBar1.StartButtonImage = ((System.Drawing.Image)(resources.GetObject("radRibbonBar1.StartButtonImage")));
            this.radRibbonBar1.StartMenuItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuItemMasterData,
            this.radMenuItemTransaction,
            this.radMenuItemReporting,
            this.radMenuSeparatorItem1,
            this.radMenuItemLogOut,
            this.radMenuSeparatorItem2,
            this.radMenuItemExit});
            this.radRibbonBar1.StartMenuWidth = 250;
            this.radRibbonBar1.TabIndex = 0;
            this.radRibbonBar1.Text = "MainForm";
            this.radRibbonBar1.Click += new System.EventHandler(this.radRibbonBar1_Click);
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // ribbonTab1.ContentPanel
            // 
            this.ribbonTab1.ContentPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ribbonTab1.ContentPanel.CausesValidation = true;
            this.ribbonTab1.ContentPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonTab1.ContentPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonTab1.ContentPanel.Location = new System.Drawing.Point(0, 0);
            this.ribbonTab1.ContentPanel.Size = new System.Drawing.Size(200, 100);
            this.ribbonTab1.IsSelected = true;
            this.ribbonTab1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radRibbonBarGroupUser,
            this.radRibbonBarGroupCategory,
            this.radRibbonBarGroupVessel,
            this.radRibbonBarGroup1,
            this.radRibbonBarGroup2,
            this.radRibbonBarGroup3,
            this.radRibbonBarGroup4,
            this.radRibbonBarGroup5,
            this.radRibbonBarGroup6,
            this.radRibbonBarGroup7,
            this.radRibbonBarGroup8});
            this.ribbonTab1.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.StretchHorizontally = false;
            this.ribbonTab1.Text = "Master Data";
            // 
            // radRibbonBarGroupUser
            // 
            this.radRibbonBarGroupUser.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radImageButtonUser});
            this.radRibbonBarGroupUser.Name = "radRibbonBarGroupUser";
            this.radRibbonBarGroupUser.Text = "User";
            this.radRibbonBarGroupUser.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // radImageButtonUser
            // 
            this.radImageButtonUser.Image = ((System.Drawing.Image)(resources.GetObject("radImageButtonUser.Image")));
            this.radImageButtonUser.ImageIndex = 0;
            this.radImageButtonUser.Name = "radImageButtonUser";
            this.radImageButtonUser.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.radImageButtonUser.Text = "radImageButtonUser";
            this.radImageButtonUser.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.radImageButtonUser.Click += new System.EventHandler(this.radImageButtonUser_Click);
            // 
            // radRibbonBarGroupCategory
            // 
            this.radRibbonBarGroupCategory.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radImageButtonElementCategory});
            this.radRibbonBarGroupCategory.Name = "radRibbonBarGroupCategory";
            this.radRibbonBarGroupCategory.Text = "Category";
            // 
            // radRibbonBarGroupVessel
            // 
            this.radRibbonBarGroupVessel.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radImageButtonElementVessel});
            this.radRibbonBarGroupVessel.Name = "radRibbonBarGroupVessel";
            this.radRibbonBarGroupVessel.Text = "Vessel";
            // 
            // radRibbonBarGroup1
            // 
            this.radRibbonBarGroup1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radImageButtonElementCity});
            this.radRibbonBarGroup1.Name = "radRibbonBarGroup1";
            this.radRibbonBarGroup1.Text = "City";
            // 
            // radRibbonBarGroup2
            // 
            this.radRibbonBarGroup2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radImageButtonElementCondition});
            this.radRibbonBarGroup2.Name = "radRibbonBarGroup2";
            this.radRibbonBarGroup2.Text = "Condition";
            // 
            // radRibbonBarGroup3
            // 
            this.radRibbonBarGroup3.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radImageButtonElementCustomer});
            this.radRibbonBarGroup3.Name = "radRibbonBarGroup3";
            this.radRibbonBarGroup3.Text = "Customer";
            // 
            // radRibbonBarGroup4
            // 
            this.radRibbonBarGroup4.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radImageButtonElementLeadTime});
            this.radRibbonBarGroup4.Name = "radRibbonBarGroup4";
            this.radRibbonBarGroup4.Text = "Lead Time";
            // 
            // radRibbonBarGroup5
            // 
            this.radRibbonBarGroup5.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radImageButtonElementPelayaran});
            this.radRibbonBarGroup5.Name = "radRibbonBarGroup5";
            this.radRibbonBarGroup5.Text = "Pelayaran";
            // 
            // radRibbonBarGroup6
            // 
            this.radRibbonBarGroup6.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radImageButtonElementSupplier});
            this.radRibbonBarGroup6.Name = "radRibbonBarGroup6";
            this.radRibbonBarGroup6.Text = "Supplier";
            // 
            // radRibbonBarGroup7
            // 
            this.radRibbonBarGroup7.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radImageButtonElementTypeCont});
            this.radRibbonBarGroup7.Name = "radRibbonBarGroup7";
            this.radRibbonBarGroup7.Text = "Type Cont";
            // 
            // radRibbonBarGroup8
            // 
            this.radRibbonBarGroup8.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radImageButtonElementWareHouse});
            this.radRibbonBarGroup8.Name = "radRibbonBarGroup8";
            this.radRibbonBarGroup8.Text = "WareHouse";
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // ribbonTab2.ContentPanel
            // 
            this.ribbonTab2.ContentPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ribbonTab2.ContentPanel.CausesValidation = true;
            this.ribbonTab2.ContentPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonTab2.ContentPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonTab2.ContentPanel.Location = new System.Drawing.Point(0, 0);
            this.ribbonTab2.ContentPanel.Size = new System.Drawing.Size(200, 100);
            this.ribbonTab2.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ribbonTab2.Name = "ribbonTab2";
            this.ribbonTab2.StretchHorizontally = false;
            this.ribbonTab2.Text = "Transaction";
            // 
            // ribbonTab3
            // 
            this.ribbonTab3.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // ribbonTab3.ContentPanel
            // 
            this.ribbonTab3.ContentPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ribbonTab3.ContentPanel.CausesValidation = true;
            this.ribbonTab3.ContentPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonTab3.ContentPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonTab3.ContentPanel.Location = new System.Drawing.Point(0, 0);
            this.ribbonTab3.ContentPanel.Size = new System.Drawing.Size(200, 100);
            this.ribbonTab3.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ribbonTab3.Name = "ribbonTab3";
            this.ribbonTab3.StretchHorizontally = false;
            this.ribbonTab3.Text = "Reporting";
            // 
            // radMenuItemMasterData
            // 
            this.radMenuItemMasterData.Image = global::VisitaJayaPerkasa.Properties.Resources.ic_masterdata;
            this.radMenuItemMasterData.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuItemUser,
            this.radMenuItemRole,
            this.radMenuItemVessel});
            this.radMenuItemMasterData.Name = "radMenuItemMasterData";
            this.radMenuItemMasterData.Text = "Master Data";
            // 
            // radMenuItemUser
            // 
            this.radMenuItemUser.Name = "radMenuItemUser";
            this.radMenuItemUser.Text = "User";
            this.radMenuItemUser.Click += new System.EventHandler(this.radMenuItemUser_Click);
            // 
            // radMenuItemRole
            // 
            this.radMenuItemRole.Name = "radMenuItemRole";
            this.radMenuItemRole.Text = "Role";
            this.radMenuItemRole.Click += new System.EventHandler(this.radMenuItemRole_Click);
            // 
            // radMenuItemVessel
            // 
            this.radMenuItemVessel.Name = "radMenuItemVessel";
            this.radMenuItemVessel.Text = "Vessel";
            this.radMenuItemVessel.Click += new System.EventHandler(this.radMenuItemVessel_Click);
            // 
            // radMenuItemTransaction
            // 
            this.radMenuItemTransaction.Image = global::VisitaJayaPerkasa.Properties.Resources.ic_transaction;
            this.radMenuItemTransaction.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuItem9});
            this.radMenuItemTransaction.Name = "radMenuItemTransaction";
            this.radMenuItemTransaction.Text = "Transaction";
            // 
            // radMenuItem9
            // 
            this.radMenuItem9.Name = "radMenuItem9";
            this.radMenuItem9.Text = "Transaction Pelayaran";
            // 
            // radMenuItemReporting
            // 
            this.radMenuItemReporting.Image = global::VisitaJayaPerkasa.Properties.Resources.ic_reporting;
            this.radMenuItemReporting.Name = "radMenuItemReporting";
            this.radMenuItemReporting.Text = "Reporting";
            // 
            // radMenuSeparatorItem1
            // 
            this.radMenuSeparatorItem1.Name = "radMenuSeparatorItem1";
            this.radMenuSeparatorItem1.Text = "radMenuSeparatorItem1";
            // 
            // radMenuItemLogOut
            // 
            this.radMenuItemLogOut.Name = "radMenuItemLogOut";
            this.radMenuItemLogOut.Text = "LogOut";
            this.radMenuItemLogOut.Click += new System.EventHandler(this.radMenuItemLogOut_Click);
            // 
            // radMenuSeparatorItem2
            // 
            this.radMenuSeparatorItem2.Name = "radMenuSeparatorItem2";
            this.radMenuSeparatorItem2.Text = "radMenuSeparatorItem2";
            // 
            // radMenuItemExit
            // 
            this.radMenuItemExit.Name = "radMenuItemExit";
            this.radMenuItemExit.Text = "Exit";
            this.radMenuItemExit.Click += new System.EventHandler(this.radMenuItemExit_Click);
            // 
            // radImageButtonElementWareHouse
            // 
            this.radImageButtonElementWareHouse.Image = ((System.Drawing.Image)(resources.GetObject("radImageButtonElementWareHouse.Image")));
            this.radImageButtonElementWareHouse.ImageKey = "warehouse.jpeg";
            this.radImageButtonElementWareHouse.Name = "radImageButtonElementWareHouse";
            this.radImageButtonElementWareHouse.Text = "radImageButtonElementWareHouse";
            // 
            // radImageButtonElementTypeCont
            // 
            this.radImageButtonElementTypeCont.Image = ((System.Drawing.Image)(resources.GetObject("radImageButtonElementTypeCont.Image")));
            this.radImageButtonElementTypeCont.ImageKey = "typecont.jpeg";
            this.radImageButtonElementTypeCont.Name = "radImageButtonElementTypeCont";
            this.radImageButtonElementTypeCont.Text = "radImageButtonElementTypeCont";
            // 
            // radImageButtonElementSupplier
            // 
            this.radImageButtonElementSupplier.Image = ((System.Drawing.Image)(resources.GetObject("radImageButtonElementSupplier.Image")));
            this.radImageButtonElementSupplier.ImageKey = "supplier.jpeg";
            this.radImageButtonElementSupplier.Name = "radImageButtonElementSupplier";
            this.radImageButtonElementSupplier.Text = "radImageButtonElementSupplier";
            // 
            // radImageButtonElementPelayaran
            // 
            this.radImageButtonElementPelayaran.Image = ((System.Drawing.Image)(resources.GetObject("radImageButtonElementPelayaran.Image")));
            this.radImageButtonElementPelayaran.ImageKey = "pelayaran.jpeg";
            this.radImageButtonElementPelayaran.Name = "radImageButtonElementPelayaran";
            this.radImageButtonElementPelayaran.Text = "radImageButtonElementPelayaran";
            // 
            // radImageButtonElementLeadTime
            // 
            this.radImageButtonElementLeadTime.Image = ((System.Drawing.Image)(resources.GetObject("radImageButtonElementLeadTime.Image")));
            this.radImageButtonElementLeadTime.ImageKey = "lead_time.jpeg";
            this.radImageButtonElementLeadTime.Name = "radImageButtonElementLeadTime";
            this.radImageButtonElementLeadTime.Text = "radImageButtonElementLeadTime";
            // 
            // radImageButtonElementCustomer
            // 
            this.radImageButtonElementCustomer.Image = ((System.Drawing.Image)(resources.GetObject("radImageButtonElementCustomer.Image")));
            this.radImageButtonElementCustomer.ImageKey = "customer.jpeg";
            this.radImageButtonElementCustomer.Name = "radImageButtonElementCustomer";
            this.radImageButtonElementCustomer.Text = "radImageButtonElementCustomer";
            // 
            // radImageButtonElementCondition
            // 
            this.radImageButtonElementCondition.Image = ((System.Drawing.Image)(resources.GetObject("radImageButtonElementCondition.Image")));
            this.radImageButtonElementCondition.ImageKey = "condition.jpeg";
            this.radImageButtonElementCondition.Name = "radImageButtonElementCondition";
            this.radImageButtonElementCondition.Text = "radImageButtonElementCondition";
            // 
            // radImageButtonElementCity
            // 
            this.radImageButtonElementCity.Image = ((System.Drawing.Image)(resources.GetObject("radImageButtonElementCity.Image")));
            this.radImageButtonElementCity.ImageKey = "city.jpeg";
            this.radImageButtonElementCity.Name = "radImageButtonElementCity";
            this.radImageButtonElementCity.Text = "radImageButtonElementCity";
            // 
            // radImageButtonElementVessel
            // 
            this.radImageButtonElementVessel.Image = ((System.Drawing.Image)(resources.GetObject("radImageButtonElementVessel.Image")));
            this.radImageButtonElementVessel.ImageKey = "vessel.jpeg";
            this.radImageButtonElementVessel.Name = "radImageButtonElementVessel";
            this.radImageButtonElementVessel.Text = "radImageButtonElementVessel";
            // 
            // radImageButtonElementCategory
            // 
            this.radImageButtonElementCategory.Image = ((System.Drawing.Image)(resources.GetObject("radImageButtonElementCategory.Image")));
            this.radImageButtonElementCategory.ImageKey = "condition.jpeg";
            this.radImageButtonElementCategory.Name = "radImageButtonElementCategory";
            this.radImageButtonElementCategory.Text = "radImageButtonElementCategory";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 425);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.radStatusStrip);
            this.Controls.Add(this.radRibbonBar1);
            this.MainMenuStrip = null;
            this.Name = "MainForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadRibbonBar radRibbonBar1;
        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip;
        private System.Windows.Forms.Panel MainPanel;
        private Telerik.WinControls.UI.RadMenuItem radMenuItemMasterData;
        private Telerik.WinControls.UI.RadMenuItem radMenuItemTransaction;
        private Telerik.WinControls.UI.RadMenuItem radMenuItemReporting;
        private Telerik.WinControls.UI.RadMenuItem radMenuItemLogOut;
        private Telerik.WinControls.UI.RadMenuItem radMenuItemExit;
        private Telerik.WinControls.UI.RibbonTab ribbonTab1;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroupUser;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroupCategory;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroupVessel;
        private Telerik.WinControls.UI.RibbonTab ribbonTab2;
        private Telerik.WinControls.UI.RibbonTab ribbonTab3;
        private System.Windows.Forms.ImageList imageList1;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonUser;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem7;
        private Telerik.WinControls.UI.RadMenuItem radMenuItemUser;
        private Telerik.WinControls.UI.RadMenuItem radMenuItemRole;
        private Telerik.WinControls.UI.RadMenuItem radMenuItemVessel;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem9;
        private Telerik.WinControls.UI.RadMenuSeparatorItem radMenuSeparatorItem1;
        private Telerik.WinControls.UI.RadMenuSeparatorItem radMenuSeparatorItem2;
        private Telerik.WinControls.UI.RadLabelElement radLabelElementWelcome;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup1;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup2;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup3;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup4;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup5;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup6;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup7;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup8;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonElementCategory;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonElementVessel;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonElementCity;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonElementCondition;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonElementCustomer;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonElementLeadTime;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonElementPelayaran;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonElementSupplier;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonElementTypeCont;
        private Telerik.WinControls.UI.RadImageButtonElement radImageButtonElementWareHouse;
    }
}