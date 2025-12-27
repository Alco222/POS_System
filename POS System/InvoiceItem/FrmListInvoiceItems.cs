using POSBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System.InvoiceItem
{
    public partial class FrmListInvoiceItems : Form
    {
        int _InvoiceItemID ;
        static DataTable _dtAllInvoiceItems;

        public FrmListInvoiceItems(int InvoiceItemID)
        {
            InitializeComponent();
            _InvoiceItemID = InvoiceItemID;

        }

        private void _RefreshListInvoiceItems()
        {
            _dtAllInvoiceItems = clsInvoiceItems.GetInvoiceItemsByInvoiceID(_InvoiceItemID);

            dgvAllInvoiceItems.DataSource = _dtAllInvoiceItems;
            lblRecords.Text = dgvAllInvoiceItems.Rows.Count.ToString();
            cbFilter.SelectedIndex = 0;

            if (dgvAllInvoiceItems.Rows.Count > 0)
            {
                dgvAllInvoiceItems.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                dgvAllInvoiceItems.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
              
                dgvAllInvoiceItems.Columns[0].HeaderText = "InvIte.ID";
                dgvAllInvoiceItems.Columns[0].Width = 80;
                dgvAllInvoiceItems.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoiceItems.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoiceItems.Columns[1].HeaderText = "P.Name";
                dgvAllInvoiceItems.Columns[1].Width = 190;
                dgvAllInvoiceItems.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoiceItems.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoiceItems.Columns[2].HeaderText = "P.Desc";
                dgvAllInvoiceItems.Columns[2].Width = 200;
                dgvAllInvoiceItems.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoiceItems.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoiceItems.Columns[3].HeaderText = "P.Qnty";
                dgvAllInvoiceItems.Columns[3].Width = 100;
                dgvAllInvoiceItems.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoiceItems.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoiceItems.Columns[4].HeaderText = "P.UnitPrice";
                dgvAllInvoiceItems.Columns[4].Width = 100;
                dgvAllInvoiceItems.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoiceItems.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoiceItems.Columns[5].HeaderText = "P.Tax(%)";
                dgvAllInvoiceItems.Columns[5].Width = 90;
                dgvAllInvoiceItems.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoiceItems.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoiceItems.Columns[6].Visible = false ;

                dgvAllInvoiceItems.Columns[7].HeaderText = "DiscAmount";
                dgvAllInvoiceItems.Columns[7].Width = 90;
                dgvAllInvoiceItems.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoiceItems.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoiceItems.Columns[8].HeaderText = "TotalAmount";
                dgvAllInvoiceItems.Columns[8].Width = 140;
                dgvAllInvoiceItems.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoiceItems.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoiceItems.Columns[9].HeaderText = "Inv.ID";
                dgvAllInvoiceItems.Columns[9].Width = 70;
                dgvAllInvoiceItems.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoiceItems.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }

        private void FrmListInvoiceItems_Load(object sender, EventArgs e)
        {
            _RefreshListInvoiceItems();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "None")
            {
                txtfilter.Visible = false;
            }
            else
            {
                txtfilter.Visible = true;
                txtfilter.Text = "";
                txtfilter.Focus();

            }
        }

        private void txtfilter_TextChanged(object sender, EventArgs e)
        {
            string CBFilter = cbFilter.Text;
            string FilterCotumn = "";
            string TextValue = txtfilter.Text.Trim();

            switch (CBFilter)
            {
                case "InvoiceItems ID":
                    FilterCotumn = "InvoiceItemID";
                    break;
                case "Product Name":
                    FilterCotumn = "ProductName";
                    break;
                case "InvoiceID":
                    FilterCotumn = "InvoiceID";
                    break;
                case "Quantity":
                    FilterCotumn = "Quantity";
                    break;
                default:
                    FilterCotumn = "None";
                    break;
            }

            if (txtfilter.Text.Trim() == "" || FilterCotumn == "None")
            {
                _dtAllInvoiceItems.DefaultView.RowFilter = "";
                lblRecords.Text = _dtAllInvoiceItems.DefaultView.Count.ToString();
                return;
            }

            if (FilterCotumn == "InvoiceID" || FilterCotumn == "InvoiceItemID" || FilterCotumn == "Quantity")
                _dtAllInvoiceItems.DefaultView.RowFilter = string.Format($"[{FilterCotumn}] = {TextValue}");
            else
                _dtAllInvoiceItems.DefaultView.RowFilter = string.Format($"[{FilterCotumn}] LIKE'{TextValue}%'");

            lblRecords.Text = _dtAllInvoiceItems.DefaultView.Count.ToString();

        }

        private void txtfilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "Invoice ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
