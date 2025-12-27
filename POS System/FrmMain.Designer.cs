namespace POS_System
{
    partial class FrmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mangmentPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mangmentCustomerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(129)))), ((int)(((byte)(220)))));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mangmentPersonToolStripMenuItem,
            this.mangmentCustomerToolStripMenuItem,
            this.productToolStripMenuItem,
            this.InvoiceToolStripMenuItem,
            this.usersToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MaximumSize = new System.Drawing.Size(900, 2954);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(185, 692);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mangmentPersonToolStripMenuItem
            // 
            this.mangmentPersonToolStripMenuItem.AutoSize = false;
            this.mangmentPersonToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mangmentPersonToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.mangmentPersonToolStripMenuItem.Image = global::POS_System.Properties.Resources.users;
            this.mangmentPersonToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mangmentPersonToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mangmentPersonToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.mangmentPersonToolStripMenuItem.MergeIndex = 0;
            this.mangmentPersonToolStripMenuItem.Name = "mangmentPersonToolStripMenuItem";
            this.mangmentPersonToolStripMenuItem.Size = new System.Drawing.Size(169, 48);
            this.mangmentPersonToolStripMenuItem.Text = "   People";
            this.mangmentPersonToolStripMenuItem.Click += new System.EventHandler(this.mangmentPersonToolStripMenuItem_Click);
            // 
            // mangmentCustomerToolStripMenuItem
            // 
            this.mangmentCustomerToolStripMenuItem.AutoSize = false;
            this.mangmentCustomerToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mangmentCustomerToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.mangmentCustomerToolStripMenuItem.Image = global::POS_System.Properties.Resources.user_accounts__1_;
            this.mangmentCustomerToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mangmentCustomerToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mangmentCustomerToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.mangmentCustomerToolStripMenuItem.MergeIndex = 0;
            this.mangmentCustomerToolStripMenuItem.Name = "mangmentCustomerToolStripMenuItem";
            this.mangmentCustomerToolStripMenuItem.RightToLeftAutoMirrorImage = true;
            this.mangmentCustomerToolStripMenuItem.Size = new System.Drawing.Size(176, 48);
            this.mangmentCustomerToolStripMenuItem.Text = "  Customer";
            this.mangmentCustomerToolStripMenuItem.Click += new System.EventHandler(this.mangmentCustomerToolStripMenuItem_Click);
            // 
            // productToolStripMenuItem
            // 
            this.productToolStripMenuItem.AutoSize = false;
            this.productToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.productToolStripMenuItem.Image = global::POS_System.Properties.Resources.box;
            this.productToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.productToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.productToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.productToolStripMenuItem.Name = "productToolStripMenuItem";
            this.productToolStripMenuItem.Size = new System.Drawing.Size(176, 48);
            this.productToolStripMenuItem.Text = "  Product";
            this.productToolStripMenuItem.Click += new System.EventHandler(this.productToolStripMenuItem_Click);
            // 
            // InvoiceToolStripMenuItem
            // 
            this.InvoiceToolStripMenuItem.AutoSize = false;
            this.InvoiceToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvoiceToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.InvoiceToolStripMenuItem.Image = global::POS_System.Properties.Resources.invoice;
            this.InvoiceToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.InvoiceToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.InvoiceToolStripMenuItem.Name = "InvoiceToolStripMenuItem";
            this.InvoiceToolStripMenuItem.Size = new System.Drawing.Size(176, 48);
            this.InvoiceToolStripMenuItem.Text = "Invoice";
            this.InvoiceToolStripMenuItem.Click += new System.EventHandler(this.InvoiceToolStripMenuItem_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usersToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.usersToolStripMenuItem.Image = global::POS_System.Properties.Resources.activate_profile;
            this.usersToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.usersToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Padding = new System.Windows.Forms.Padding(8, 0, 4, 15);
            this.usersToolStripMenuItem.ShowShortcutKeys = false;
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(166, 51);
            this.usersToolStripMenuItem.Text = "Users";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.usersToolStripMenuItem_Click);
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.BackColor = System.Drawing.Color.Azure;
            this.guna2CirclePictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2CirclePictureBox1.FillColor = System.Drawing.Color.Azure;
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(185, 0);
            this.guna2CirclePictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(1251, 692);
            this.guna2CirclePictureBox1.TabIndex = 2;
            this.guna2CirclePictureBox1.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1436, 692);
            this.Controls.Add(this.guna2CirclePictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mangmentPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mangmentCustomerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InvoiceToolStripMenuItem;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private System.Windows.Forms.ToolStripMenuItem productToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
    }
}