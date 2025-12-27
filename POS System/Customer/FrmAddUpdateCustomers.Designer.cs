namespace POS_System
{
    partial class FrmAddUpdateCustomers
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
            this.GbtnSave = new Guna.UI2.WinForms.Guna2Button();
            this.GbtnClose = new Guna.UI2.WinForms.Guna2Button();
            this.GbtnClose2 = new Guna.UI2.WinForms.Guna2Button();
            this.tcCustomerInfo = new System.Windows.Forms.TabControl();
            this.tpPersonInfo = new System.Windows.Forms.TabPage();
            this.GbtnNext = new Guna.UI2.WinForms.Guna2Button();
            this.ctrCardPersonWithFilter1 = new POS_System.Person.Controle.ctrCardPersonWithFilter();
            this.tpCustomerInfo = new System.Windows.Forms.TabPage();
            this.gbCustomerInfo = new System.Windows.Forms.GroupBox();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblCustomerID = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtTaxNumber = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel6 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblCreatedbyUserName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.tsIsActive = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblRegesterDate = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMode = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tcCustomerInfo.SuspendLayout();
            this.tpPersonInfo.SuspendLayout();
            this.tpCustomerInfo.SuspendLayout();
            this.gbCustomerInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // GbtnSave
            // 
            this.GbtnSave.BorderColor = System.Drawing.Color.White;
            this.GbtnSave.BorderRadius = 2;
            this.GbtnSave.BorderThickness = 1;
            this.GbtnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.GbtnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.GbtnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.GbtnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.GbtnSave.Enabled = false;
            this.GbtnSave.FillColor = System.Drawing.Color.Lime;
            this.GbtnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GbtnSave.ForeColor = System.Drawing.Color.White;
            this.GbtnSave.Location = new System.Drawing.Point(764, 511);
            this.GbtnSave.Name = "GbtnSave";
            this.GbtnSave.Size = new System.Drawing.Size(82, 35);
            this.GbtnSave.TabIndex = 8;
            this.GbtnSave.Text = "Save";
            this.GbtnSave.Click += new System.EventHandler(this.GbtnSave_Click);
            // 
            // GbtnClose
            // 
            this.GbtnClose.BorderColor = System.Drawing.Color.White;
            this.GbtnClose.BorderRadius = 2;
            this.GbtnClose.BorderThickness = 1;
            this.GbtnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.GbtnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.GbtnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.GbtnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.GbtnClose.FillColor = System.Drawing.Color.Red;
            this.GbtnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GbtnClose.ForeColor = System.Drawing.Color.White;
            this.GbtnClose.Location = new System.Drawing.Point(654, 511);
            this.GbtnClose.Name = "GbtnClose";
            this.GbtnClose.Size = new System.Drawing.Size(90, 35);
            this.GbtnClose.TabIndex = 9;
            this.GbtnClose.Text = "Close";
            this.GbtnClose.Click += new System.EventHandler(this.GbtnClose_Click);
            // 
            // GbtnClose2
            // 
            this.GbtnClose2.BorderColor = System.Drawing.Color.White;
            this.GbtnClose2.BorderRadius = 8;
            this.GbtnClose2.BorderThickness = 1;
            this.GbtnClose2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.GbtnClose2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.GbtnClose2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.GbtnClose2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.GbtnClose2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.GbtnClose2.FillColor = System.Drawing.Color.Red;
            this.GbtnClose2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GbtnClose2.ForeColor = System.Drawing.Color.White;
            this.GbtnClose2.Location = new System.Drawing.Point(834, 0);
            this.GbtnClose2.Name = "GbtnClose2";
            this.GbtnClose2.Size = new System.Drawing.Size(31, 23);
            this.GbtnClose2.TabIndex = 13;
            this.GbtnClose2.Text = "X";
            this.GbtnClose2.Click += new System.EventHandler(this.GbtnClose2_Click);
            // 
            // tcCustomerInfo
            // 
            this.tcCustomerInfo.Controls.Add(this.tpPersonInfo);
            this.tcCustomerInfo.Controls.Add(this.tpCustomerInfo);
            this.tcCustomerInfo.Location = new System.Drawing.Point(4, 44);
            this.tcCustomerInfo.Margin = new System.Windows.Forms.Padding(2);
            this.tcCustomerInfo.Name = "tcCustomerInfo";
            this.tcCustomerInfo.SelectedIndex = 0;
            this.tcCustomerInfo.Size = new System.Drawing.Size(852, 458);
            this.tcCustomerInfo.TabIndex = 14;
            // 
            // tpPersonInfo
            // 
            this.tpPersonInfo.BackColor = System.Drawing.Color.White;
            this.tpPersonInfo.Controls.Add(this.GbtnNext);
            this.tpPersonInfo.Controls.Add(this.ctrCardPersonWithFilter1);
            this.tpPersonInfo.Location = new System.Drawing.Point(4, 22);
            this.tpPersonInfo.Margin = new System.Windows.Forms.Padding(2);
            this.tpPersonInfo.Name = "tpPersonInfo";
            this.tpPersonInfo.Padding = new System.Windows.Forms.Padding(2);
            this.tpPersonInfo.Size = new System.Drawing.Size(844, 432);
            this.tpPersonInfo.TabIndex = 0;
            this.tpPersonInfo.Text = "PersonInfo";
            // 
            // GbtnNext
            // 
            this.GbtnNext.BorderColor = System.Drawing.Color.MediumSpringGreen;
            this.GbtnNext.BorderRadius = 2;
            this.GbtnNext.BorderThickness = 1;
            this.GbtnNext.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.GbtnNext.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.GbtnNext.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.GbtnNext.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.GbtnNext.FillColor = System.Drawing.Color.White;
            this.GbtnNext.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GbtnNext.ForeColor = System.Drawing.Color.MediumSpringGreen;
            this.GbtnNext.Image = global::POS_System.Properties.Resources.arrow_right__1_;
            this.GbtnNext.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.GbtnNext.Location = new System.Drawing.Point(741, 397);
            this.GbtnNext.Name = "GbtnNext";
            this.GbtnNext.Size = new System.Drawing.Size(87, 30);
            this.GbtnNext.TabIndex = 16;
            this.GbtnNext.Text = "Next";
            this.GbtnNext.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.GbtnNext.Click += new System.EventHandler(this.GbtnNext_Click);
            // 
            // ctrCardPersonWithFilter1
            // 
            this.ctrCardPersonWithFilter1.BackColor = System.Drawing.Color.White;
            this.ctrCardPersonWithFilter1.FelterEnbled = true;
            this.ctrCardPersonWithFilter1.Location = new System.Drawing.Point(11, 9);
            this.ctrCardPersonWithFilter1.Name = "ctrCardPersonWithFilter1";
            this.ctrCardPersonWithFilter1.ShowAddNew = true;
            this.ctrCardPersonWithFilter1.Size = new System.Drawing.Size(827, 387);
            this.ctrCardPersonWithFilter1.TabIndex = 0;
            // 
            // tpCustomerInfo
            // 
            this.tpCustomerInfo.BackColor = System.Drawing.Color.White;
            this.tpCustomerInfo.Controls.Add(this.gbCustomerInfo);
            this.tpCustomerInfo.Location = new System.Drawing.Point(4, 22);
            this.tpCustomerInfo.Margin = new System.Windows.Forms.Padding(2);
            this.tpCustomerInfo.Name = "tpCustomerInfo";
            this.tpCustomerInfo.Padding = new System.Windows.Forms.Padding(2);
            this.tpCustomerInfo.Size = new System.Drawing.Size(844, 432);
            this.tpCustomerInfo.TabIndex = 1;
            this.tpCustomerInfo.Text = "CustomerInfo";
            // 
            // gbCustomerInfo
            // 
            this.gbCustomerInfo.Controls.Add(this.guna2HtmlLabel2);
            this.gbCustomerInfo.Controls.Add(this.lblCustomerID);
            this.gbCustomerInfo.Controls.Add(this.txtTaxNumber);
            this.gbCustomerInfo.Controls.Add(this.guna2HtmlLabel6);
            this.gbCustomerInfo.Controls.Add(this.guna2HtmlLabel5);
            this.gbCustomerInfo.Controls.Add(this.lblCreatedbyUserName);
            this.gbCustomerInfo.Controls.Add(this.tsIsActive);
            this.gbCustomerInfo.Controls.Add(this.guna2HtmlLabel1);
            this.gbCustomerInfo.Controls.Add(this.lblRegesterDate);
            this.gbCustomerInfo.Controls.Add(this.guna2HtmlLabel3);
            this.gbCustomerInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCustomerInfo.Location = new System.Drawing.Point(254, 62);
            this.gbCustomerInfo.Margin = new System.Windows.Forms.Padding(2);
            this.gbCustomerInfo.Name = "gbCustomerInfo";
            this.gbCustomerInfo.Padding = new System.Windows.Forms.Padding(2);
            this.gbCustomerInfo.Size = new System.Drawing.Size(322, 216);
            this.gbCustomerInfo.TabIndex = 5;
            this.gbCustomerInfo.TabStop = false;
            this.gbCustomerInfo.Text = "CustomerInfo";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(22, 38);
            this.guna2HtmlLabel2.Margin = new System.Windows.Forms.Padding(2);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(90, 17);
            this.guna2HtmlLabel2.TabIndex = 11;
            this.guna2HtmlLabel2.Text = "Customer ID :";
            // 
            // lblCustomerID
            // 
            this.lblCustomerID.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomerID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerID.Location = new System.Drawing.Point(159, 38);
            this.lblCustomerID.Margin = new System.Windows.Forms.Padding(2);
            this.lblCustomerID.Name = "lblCustomerID";
            this.lblCustomerID.Size = new System.Drawing.Size(27, 17);
            this.lblCustomerID.TabIndex = 12;
            this.lblCustomerID.Text = "[??]";
            // 
            // txtTaxNumber
            // 
            this.txtTaxNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTaxNumber.DefaultText = "";
            this.txtTaxNumber.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTaxNumber.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTaxNumber.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTaxNumber.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTaxNumber.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTaxNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxNumber.ForeColor = System.Drawing.Color.Black;
            this.txtTaxNumber.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTaxNumber.Location = new System.Drawing.Point(155, 97);
            this.txtTaxNumber.Name = "txtTaxNumber";
            this.txtTaxNumber.PlaceholderText = "";
            this.txtTaxNumber.SelectedText = "";
            this.txtTaxNumber.Size = new System.Drawing.Size(150, 25);
            this.txtTaxNumber.TabIndex = 8;
            this.txtTaxNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtTaxNumber_Validating);
            // 
            // guna2HtmlLabel6
            // 
            this.guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel6.Location = new System.Drawing.Point(22, 103);
            this.guna2HtmlLabel6.Margin = new System.Windows.Forms.Padding(2);
            this.guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            this.guna2HtmlLabel6.Size = new System.Drawing.Size(89, 17);
            this.guna2HtmlLabel6.TabIndex = 7;
            this.guna2HtmlLabel6.Text = "Tax Number :";
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(77, 179);
            this.guna2HtmlLabel5.Margin = new System.Windows.Forms.Padding(2);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(55, 17);
            this.guna2HtmlLabel5.TabIndex = 5;
            this.guna2HtmlLabel5.Text = "Is Active";
            // 
            // lblCreatedbyUserName
            // 
            this.lblCreatedbyUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblCreatedbyUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedbyUserName.Location = new System.Drawing.Point(159, 142);
            this.lblCreatedbyUserName.Margin = new System.Windows.Forms.Padding(2);
            this.lblCreatedbyUserName.Name = "lblCreatedbyUserName";
            this.lblCreatedbyUserName.Size = new System.Drawing.Size(35, 17);
            this.lblCreatedbyUserName.TabIndex = 3;
            this.lblCreatedbyUserName.Text = "[???]";
            // 
            // tsIsActive
            // 
            this.tsIsActive.BackColor = System.Drawing.Color.White;
            this.tsIsActive.Checked = true;
            this.tsIsActive.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tsIsActive.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tsIsActive.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.tsIsActive.CheckedState.InnerColor = System.Drawing.Color.White;
            this.tsIsActive.Location = new System.Drawing.Point(136, 180);
            this.tsIsActive.Margin = new System.Windows.Forms.Padding(2);
            this.tsIsActive.Name = "tsIsActive";
            this.tsIsActive.Size = new System.Drawing.Size(39, 17);
            this.tsIsActive.TabIndex = 4;
            this.tsIsActive.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.tsIsActive.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.tsIsActive.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.tsIsActive.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(22, 67);
            this.guna2HtmlLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(103, 17);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "Regester Date :";
            // 
            // lblRegesterDate
            // 
            this.lblRegesterDate.BackColor = System.Drawing.Color.Transparent;
            this.lblRegesterDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegesterDate.Location = new System.Drawing.Point(159, 67);
            this.lblRegesterDate.Margin = new System.Windows.Forms.Padding(2);
            this.lblRegesterDate.Name = "lblRegesterDate";
            this.lblRegesterDate.Size = new System.Drawing.Size(83, 17);
            this.lblRegesterDate.TabIndex = 1;
            this.lblRegesterDate.Text = "[??/??/????]";
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(22, 142);
            this.guna2HtmlLabel3.Margin = new System.Windows.Forms.Padding(2);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(80, 17);
            this.guna2HtmlLabel3.TabIndex = 2;
            this.guna2HtmlLabel3.Text = "Created By :";
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Red;
            this.lblMode.Location = new System.Drawing.Point(277, 0);
            this.lblMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(289, 36);
            this.lblMode.TabIndex = 15;
            this.lblMode.Text = "Add New Customer";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmAddUpdateCustomers
            // 
            this.AcceptButton = this.GbtnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.GbtnClose2;
            this.ClientSize = new System.Drawing.Size(867, 550);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.tcCustomerInfo);
            this.Controls.Add(this.GbtnClose2);
            this.Controls.Add(this.GbtnClose);
            this.Controls.Add(this.GbtnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAddUpdateCustomers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmAddUpdateCustomers_Load);
            this.tcCustomerInfo.ResumeLayout(false);
            this.tpPersonInfo.ResumeLayout(false);
            this.tpCustomerInfo.ResumeLayout(false);
            this.gbCustomerInfo.ResumeLayout(false);
            this.gbCustomerInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button GbtnSave;
        private Guna.UI2.WinForms.Guna2Button GbtnClose;
        private Guna.UI2.WinForms.Guna2Button GbtnClose2;
        private System.Windows.Forms.TabControl tcCustomerInfo;
        private System.Windows.Forms.TabPage tpPersonInfo;
        private Person.Controle.ctrCardPersonWithFilter ctrCardPersonWithFilter1;
        private System.Windows.Forms.TabPage tpCustomerInfo;
        private System.Windows.Forms.GroupBox gbCustomerInfo;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel6;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCreatedbyUserName;
        private Guna.UI2.WinForms.Guna2ToggleSwitch tsIsActive;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRegesterDate;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2TextBox txtTaxNumber;
        private System.Windows.Forms.Label lblMode;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCustomerID;
        private Guna.UI2.WinForms.Guna2Button GbtnNext;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

