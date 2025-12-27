using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System.Product
{
    public partial class FrmShowProduct : Form
    {
        public FrmShowProduct(int ? ProductID)
        {
            InitializeComponent();
            clsProductInfo1.LoadDataByProductID(ProductID);
        }

        public FrmShowProduct(string ProductName)
        {
            InitializeComponent();
            clsProductInfo1.LoadDateByProductName(ProductName);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
