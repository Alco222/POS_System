using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System.Customer
{
    public partial class FrmCustomerInfo : Form
    {
        int? _CustomerID;

        public FrmCustomerInfo(int? CustomerID)
        {
            InitializeComponent();
            _CustomerID = CustomerID;
        }

        private void FrmCustomerInfo_Load(object sender, EventArgs e)
        {
            if (_CustomerID.HasValue)
                ctrCustomerInfo1.LoadCustomerInfo(_CustomerID.Value);
            else
                MessageBox.Show("the CustomerID = null!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
