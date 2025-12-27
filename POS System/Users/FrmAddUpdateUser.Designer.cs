namespace POS_System.Users
{
    partial class FrmAddUpdateUser
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
            this.tcUserInfo = new System.Windows.Forms.TabControl();
            this.tpPersonInfo = new System.Windows.Forms.TabPage();
            this.ctrCardPersonWithFilter1 = new POS_System.Person.Controle.ctrCardPersonWithFilter();
            this.GbtnNext = new Guna.UI2.WinForms.Guna2Button();
            this.tpUserInfo = new System.Windows.Forms.TabPage();
            this.gbCustomerInfo = new System.Windows.Forms.GroupBox();
            this.txtConfPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblUserID = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtUserName = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel6 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.tsIsActive = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblRegesterDate = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.GbtnClose = new Guna.UI2.WinForms.Guna2Button();
            this.GbtnSave = new Guna.UI2.WinForms.Guna2Button();
            this.lblMode = new System.Windows.Forms.Label();
            this.GbtnClose2 = new Guna.UI2.WinForms.Guna2Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.btnChangePass = new Guna.UI2.WinForms.Guna2Button();
            this.tcUserInfo.SuspendLayout();
            this.tpPersonInfo.SuspendLayout();
            this.tpUserInfo.SuspendLayout();
            this.gbCustomerInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tcUserInfo
            // 
            this.tcUserInfo.Controls.Add(this.tpPersonInfo);
            this.tcUserInfo.Controls.Add(this.tpUserInfo);
            this.tcUserInfo.Location = new System.Drawing.Point(16, 78);
            this.tcUserInfo.Name = "tcUserInfo";
            this.tcUserInfo.SelectedIndex = 0;
            this.tcUserInfo.Size = new System.Drawing.Size(1269, 718);
            this.tcUserInfo.TabIndex = 15;
            // 
            // tpPersonInfo
            // 
            this.tpPersonInfo.BackColor = System.Drawing.Color.White;
            this.tpPersonInfo.Controls.Add(this.ctrCardPersonWithFilter1);
            this.tpPersonInfo.Controls.Add(this.GbtnNext);
            this.tpPersonInfo.Location = new System.Drawing.Point(4, 29);
            this.tpPersonInfo.Name = "tpPersonInfo";
            this.tpPersonInfo.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpPersonInfo.Size = new System.Drawing.Size(1261, 685);
            this.tpPersonInfo.TabIndex = 0;
            this.tpPersonInfo.Text = "PersonInfo";
            // 
            // ctrCardPersonWithFilter1
            // 
            this.ctrCardPersonWithFilter1.BackColor = System.Drawing.Color.White;
            this.ctrCardPersonWithFilter1.FelterEnbled = true;
            this.ctrCardPersonWithFilter1.Location = new System.Drawing.Point(10, 17);
            this.ctrCardPersonWithFilter1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.ctrCardPersonWithFilter1.Name = "ctrCardPersonWithFilter1";
            this.ctrCardPersonWithFilter1.ShowAddNew = true;
            this.ctrCardPersonWithFilter1.Size = new System.Drawing.Size(1234, 592);
            this.ctrCardPersonWithFilter1.TabIndex = 17;
            this.ctrCardPersonWithFilter1.DelegateEnvent += new POS_System.Person.Controle.ctrCardPersonWithFilter.DelegateEnventHandler(this.ctrCardPersonWithFilter1_DelegateEnvent_1);
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
            this.GbtnNext.Location = new System.Drawing.Point(1107, 622);
            this.GbtnNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GbtnNext.Name = "GbtnNext";
            this.GbtnNext.Size = new System.Drawing.Size(130, 46);
            this.GbtnNext.TabIndex = 16;
            this.GbtnNext.Text = "Next";
            this.GbtnNext.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.GbtnNext.Click += new System.EventHandler(this.GbtnNext_Click);
            // 
            // tpUserInfo
            // 
            this.tpUserInfo.Controls.Add(this.gbCustomerInfo);
            this.tpUserInfo.Location = new System.Drawing.Point(4, 29);
            this.tpUserInfo.Name = "tpUserInfo";
            this.tpUserInfo.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpUserInfo.Size = new System.Drawing.Size(1261, 685);
            this.tpUserInfo.TabIndex = 1;
            this.tpUserInfo.Text = "UserInfo";
            this.tpUserInfo.UseVisualStyleBackColor = true;
            // 
            // gbCustomerInfo
            // 
            this.gbCustomerInfo.Controls.Add(this.btnChangePass);
            this.gbCustomerInfo.Controls.Add(this.guna2Button1);
            this.gbCustomerInfo.Controls.Add(this.txtConfPass);
            this.gbCustomerInfo.Controls.Add(this.guna2HtmlLabel4);
            this.gbCustomerInfo.Controls.Add(this.txtPassword);
            this.gbCustomerInfo.Controls.Add(this.guna2HtmlLabel3);
            this.gbCustomerInfo.Controls.Add(this.guna2HtmlLabel2);
            this.gbCustomerInfo.Controls.Add(this.lblUserID);
            this.gbCustomerInfo.Controls.Add(this.txtUserName);
            this.gbCustomerInfo.Controls.Add(this.guna2HtmlLabel6);
            this.gbCustomerInfo.Controls.Add(this.guna2HtmlLabel5);
            this.gbCustomerInfo.Controls.Add(this.tsIsActive);
            this.gbCustomerInfo.Controls.Add(this.guna2HtmlLabel1);
            this.gbCustomerInfo.Controls.Add(this.lblRegesterDate);
            this.gbCustomerInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCustomerInfo.Location = new System.Drawing.Point(308, 108);
            this.gbCustomerInfo.Name = "gbCustomerInfo";
            this.gbCustomerInfo.Size = new System.Drawing.Size(610, 458);
            this.gbCustomerInfo.TabIndex = 5;
            this.gbCustomerInfo.TabStop = false;
            this.gbCustomerInfo.Text = "UserInfo";
            // 
            // txtConfPass
            // 
            this.txtConfPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtConfPass.DefaultText = "";
            this.txtConfPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtConfPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtConfPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtConfPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtConfPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtConfPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfPass.ForeColor = System.Drawing.Color.Black;
            this.txtConfPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtConfPass.Location = new System.Drawing.Point(272, 282);
            this.txtConfPass.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtConfPass.Name = "txtConfPass";
            this.txtConfPass.PlaceholderText = "";
            this.txtConfPass.SelectedText = "";
            this.txtConfPass.Size = new System.Drawing.Size(312, 38);
            this.txtConfPass.TabIndex = 16;
            this.txtConfPass.TextChanged += new System.EventHandler(this.txtConfPass_TextChanged);
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(33, 289);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(177, 24);
            this.guna2HtmlLabel4.TabIndex = 15;
            this.guna2HtmlLabel4.Text = "Confirm Password :";
            // 
            // txtPassword
            // 
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.DefaultText = "";
            this.txtPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPassword.Location = new System.Drawing.Point(272, 217);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PlaceholderText = "";
            this.txtPassword.SelectedText = "";
            this.txtPassword.Size = new System.Drawing.Size(312, 38);
            this.txtPassword.TabIndex = 14;
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(33, 222);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(102, 24);
            this.guna2HtmlLabel3.TabIndex = 13;
            this.guna2HtmlLabel3.Text = "Password :";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(33, 58);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(82, 24);
            this.guna2HtmlLabel2.TabIndex = 11;
            this.guna2HtmlLabel2.Text = "User ID :";
            // 
            // lblUserID
            // 
            this.lblUserID.BackColor = System.Drawing.Color.Transparent;
            this.lblUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserID.Location = new System.Drawing.Point(272, 58);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(37, 24);
            this.lblUserID.TabIndex = 12;
            this.lblUserID.Text = "[??]";
            // 
            // txtUserName
            // 
            this.txtUserName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUserName.DefaultText = "";
            this.txtUserName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtUserName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtUserName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUserName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUserName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.ForeColor = System.Drawing.Color.Black;
            this.txtUserName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUserName.Location = new System.Drawing.Point(272, 157);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.PlaceholderText = "";
            this.txtUserName.SelectedText = "";
            this.txtUserName.Size = new System.Drawing.Size(312, 38);
            this.txtUserName.TabIndex = 8;
            this.txtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserName_Validating);
            // 
            // guna2HtmlLabel6
            // 
            this.guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel6.Location = new System.Drawing.Point(33, 162);
            this.guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            this.guna2HtmlLabel6.Size = new System.Drawing.Size(108, 24);
            this.guna2HtmlLabel6.TabIndex = 7;
            this.guna2HtmlLabel6.Text = "User Name:";
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(218, 414);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(79, 24);
            this.guna2HtmlLabel5.TabIndex = 5;
            this.guna2HtmlLabel5.Text = "Is Active";
            // 
            // tsIsActive
            // 
            this.tsIsActive.BackColor = System.Drawing.Color.White;
            this.tsIsActive.Checked = true;
            this.tsIsActive.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tsIsActive.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tsIsActive.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.tsIsActive.CheckedState.InnerColor = System.Drawing.Color.White;
            this.tsIsActive.Location = new System.Drawing.Point(306, 415);
            this.tsIsActive.Name = "tsIsActive";
            this.tsIsActive.Size = new System.Drawing.Size(58, 26);
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
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(33, 103);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(144, 24);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "Regester Date :";
            // 
            // lblRegesterDate
            // 
            this.lblRegesterDate.BackColor = System.Drawing.Color.Transparent;
            this.lblRegesterDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegesterDate.Location = new System.Drawing.Point(272, 103);
            this.lblRegesterDate.Name = "lblRegesterDate";
            this.lblRegesterDate.Size = new System.Drawing.Size(115, 24);
            this.lblRegesterDate.TabIndex = 1;
            this.lblRegesterDate.Text = "[??/??/????]";
            // 
            // GbtnClose
            // 
            this.GbtnClose.BorderColor = System.Drawing.Color.White;
            this.GbtnClose.BorderRadius = 2;
            this.GbtnClose.BorderThickness = 1;
            this.GbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.GbtnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.GbtnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.GbtnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.GbtnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.GbtnClose.FillColor = System.Drawing.Color.Red;
            this.GbtnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GbtnClose.ForeColor = System.Drawing.Color.White;
            this.GbtnClose.Location = new System.Drawing.Point(986, 808);
            this.GbtnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GbtnClose.Name = "GbtnClose";
            this.GbtnClose.Size = new System.Drawing.Size(135, 54);
            this.GbtnClose.TabIndex = 17;
            this.GbtnClose.Text = "Close";
            this.GbtnClose.Click += new System.EventHandler(this.GbtnClose_Click);
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
            this.GbtnSave.Location = new System.Drawing.Point(1150, 808);
            this.GbtnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GbtnSave.Name = "GbtnSave";
            this.GbtnSave.Size = new System.Drawing.Size(123, 54);
            this.GbtnSave.TabIndex = 16;
            this.GbtnSave.Text = "Save";
            this.GbtnSave.Click += new System.EventHandler(this.GbtnSave_Click);
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Red;
            this.lblMode.Location = new System.Drawing.Point(465, 2);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(316, 52);
            this.lblMode.TabIndex = 19;
            this.lblMode.Text = "Add New User";
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
            this.GbtnClose2.Location = new System.Drawing.Point(1239, 2);
            this.GbtnClose2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GbtnClose2.Name = "GbtnClose2";
            this.GbtnClose2.Size = new System.Drawing.Size(46, 35);
            this.GbtnClose2.TabIndex = 18;
            this.GbtnClose2.Text = "X";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // guna2Button1
            // 
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(324, 497);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(186, 56);
            this.guna2Button1.TabIndex = 17;
            this.guna2Button1.Text = "guna2Button1";
            // 
            // btnChangePass
            // 
            this.btnChangePass.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChangePass.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChangePass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChangePass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChangePass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnChangePass.ForeColor = System.Drawing.Color.White;
            this.btnChangePass.Location = new System.Drawing.Point(218, 339);
            this.btnChangePass.Name = "btnChangePass";
            this.btnChangePass.Size = new System.Drawing.Size(188, 59);
            this.btnChangePass.TabIndex = 18;
            this.btnChangePass.Text = "Change Pass";
            // 
            // FrmAddUpdateUser
            // 
            this.AcceptButton = this.GbtnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.GbtnClose2;
            this.ClientSize = new System.Drawing.Size(1290, 869);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.GbtnClose2);
            this.Controls.Add(this.GbtnClose);
            this.Controls.Add(this.GbtnSave);
            this.Controls.Add(this.tcUserInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmAddUpdateUser";
            this.Text = "FrmAddUpdateUser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAddUpdateUser_FormClosing);
            this.Load += new System.EventHandler(this.FrmAddUpdateUser_Load);
            this.tcUserInfo.ResumeLayout(false);
            this.tpPersonInfo.ResumeLayout(false);
            this.tpUserInfo.ResumeLayout(false);
            this.gbCustomerInfo.ResumeLayout(false);
            this.gbCustomerInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcUserInfo;
        private System.Windows.Forms.TabPage tpPersonInfo;
        private Guna.UI2.WinForms.Guna2Button GbtnNext;
        private System.Windows.Forms.TabPage tpUserInfo;
        private System.Windows.Forms.GroupBox gbCustomerInfo;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblUserID;
        private Guna.UI2.WinForms.Guna2TextBox txtUserName;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel6;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2ToggleSwitch tsIsActive;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRegesterDate;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Button GbtnClose;
        private Guna.UI2.WinForms.Guna2Button GbtnSave;
        private System.Windows.Forms.Label lblMode;
        private Guna.UI2.WinForms.Guna2Button GbtnClose2;
        private Guna.UI2.WinForms.Guna2TextBox txtConfPass;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Person.Controle.ctrCardPersonWithFilter ctrCardPersonWithFilter1;
        private System.Windows.Forms.Timer timer1;
        private Guna.UI2.WinForms.Guna2Button btnChangePass;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}