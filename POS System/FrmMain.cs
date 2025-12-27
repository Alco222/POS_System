using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_System.Customer;
using POS_System.Invoice;
using POS_System.Person;
using POS_System.Product;
using POS_System.Users;

namespace POS_System
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void mangmentPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListPerson frmListPerson = new FrmListPerson();
            frmListPerson.Show();
        }

        private void mangmentCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListCustomers frmListCustomers = new FrmListCustomers();
            frmListCustomers.Show();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListProduct frmListProduct = new FrmListProduct();
            frmListProduct.Show();
        }

        private void InvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListInvoice frmListInvoice = new FrmListInvoice();
            frmListInvoice.Show();

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListMangerUsers frmListMangerUsers = new FrmListMangerUsers();
            frmListMangerUsers.ShowDialog();
        }
    }
}
