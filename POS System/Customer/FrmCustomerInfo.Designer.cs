namespace POS_System.Customer
{
    partial class FrmCustomerInfo
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
            this.ctrCustomerInfo1 = new POS_System.Customer.Controls.ctrCustomerInfo();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // ctrCustomerInfo1
            // 
            this.ctrCustomerInfo1.BackColor = System.Drawing.Color.White;
            this.ctrCustomerInfo1.Location = new System.Drawing.Point(19, 6);
            this.ctrCustomerInfo1.Name = "ctrCustomerInfo1";
            this.ctrCustomerInfo1.Size = new System.Drawing.Size(805, 392);
            this.ctrCustomerInfo1.TabIndex = 0;
            // 
            // guna2Button1
            // 
            this.guna2Button1.BackColor = System.Drawing.Color.Red;
            this.guna2Button1.BorderColor = System.Drawing.Color.Red;
            this.guna2Button1.BorderThickness = 2;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.White;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.Red;
            this.guna2Button1.Location = new System.Drawing.Point(741, 401);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(84, 28);
            this.guna2Button1.TabIndex = 3;
            this.guna2Button1.Text = "Close";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // FrmCustomerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(836, 434);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.ctrCustomerInfo1);
            this.Name = "FrmCustomerInfo";
            this.Text = "FrmCustomerInfo";
            this.Load += new System.EventHandler(this.FrmCustomerInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrCustomerInfo ctrCustomerInfo1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}