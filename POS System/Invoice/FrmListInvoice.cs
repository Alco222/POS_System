using POS_System.Class_Globale;
using POS_System.InvoiceItem;
using POS_System.Product;
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

namespace POS_System.Invoice
{
    public partial class FrmListInvoice : Form
    {
        static DataTable _dtAllInvoice;
        private static int _currentPage = 1;
        private const int _PAGE_SIZE = 60;
        private int _totalPages = 0;
        private int _totalRows = 0;

        public FrmListInvoice()
        {
            InitializeComponent();
        }

        private void LoadCurrentPage()
        {
            _dtAllInvoice = clsInvoice.GetAllInvoices(_currentPage, _PAGE_SIZE);

            clsUtil.LoadDataPage2(_dtAllInvoice, dgvAllInvoice, _currentPage, _PAGE_SIZE,
               lblPageInfo, btnPrevious, btnNext, ref _totalRows, ref _totalPages);

            dgvAllInvoice.DataSource = _dtAllInvoice;
            _dtAllInvoice.Columns.Remove("TotalRows"); // Remove the 9th column (index 8)
            lblRecords.Text = _totalRows.ToString();

            if (_totalPages <= 1)
            {
                btnPrevious.Visible = false;
                lblPageInfo.Visible = false;
                btnNext.Visible = false;
            }
        }

        private void _RefreshListInvoice()
        {
            LoadCurrentPage();
            cbFilter.SelectedIndex = 0;

            if (dgvAllInvoice.Rows.Count > 0)
            {
                dgvAllInvoice.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                dgvAllInvoice.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
                dgvAllInvoice.Columns[0].HeaderText = "Inv.ID";
                dgvAllInvoice.Columns[0].Width = 80;
                dgvAllInvoice.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoice.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoice.Columns[1].HeaderText = "Cust.Name";
                dgvAllInvoice.Columns[1].Width = 160;
                dgvAllInvoice.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoice.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoice.Columns[2].HeaderText = "Date Rejseter";
                dgvAllInvoice.Columns[2].Width = 170;
                dgvAllInvoice.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoice.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

               
                dgvAllInvoice.Columns[3].HeaderText = "SubTotal";
                dgvAllInvoice.Columns[3].Width = 100;
                dgvAllInvoice.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoice.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoice.Columns[4].HeaderText = "TotalTax";
                dgvAllInvoice.Columns[4].Width = 80;
                dgvAllInvoice.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoice.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                dgvAllInvoice.Columns[5].HeaderText = "TotalDiscount";
                dgvAllInvoice.Columns[5].Width = 90;
                dgvAllInvoice.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoice.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoice.Columns[6].HeaderText = "PaymentStatus";
                dgvAllInvoice.Columns[6].Width = 130;
                dgvAllInvoice.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoice.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoice.Columns[7].HeaderText = "TotalAmount";
                dgvAllInvoice.Columns[7].Width = 130;
                dgvAllInvoice.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoice.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllInvoice.Columns[8].HeaderText = "Status";
                dgvAllInvoice.Columns[8].Width = 100;
                dgvAllInvoice.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllInvoice.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }

        private void FrmListInvoice_Load(object sender, EventArgs e)
        {
            _RefreshListInvoice();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FrmAddUpdateInvoice frmAddUpdateInvoice = new FrmAddUpdateInvoice();
            frmAddUpdateInvoice.ShowDialog();
            FrmListInvoice_Load(null, null);
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
                case "Invoice ID":
                    FilterCotumn = "InvoiceID";
                    break;
                case "Customer Name":
                    FilterCotumn = "CustomerName";
                    break;
                default:
                    FilterCotumn = "None";
                    break;
            }

            if (txtfilter.Text.Trim() == "" || FilterCotumn == "None")
            {
                _dtAllInvoice.DefaultView.RowFilter = "";
                LoadCurrentPage();

                if (_totalPages > 1)
                {
                    btnPrevious.Visible = true;
                    lblPageInfo.Visible = true;
                    btnNext.Visible = true;
                }

                lblRecords.Text = _dtAllInvoice.Rows.Count.ToString();
                return;
            }

            if (FilterCotumn == "InvoiceID")
            {
                _dtAllInvoice.DefaultView.RowFilter = string.Format($"[{FilterCotumn}] = {TextValue}");
                lblRecords.Text = _dtAllInvoice.DefaultView.Count.ToString();
            }

            else
            {
                _dtAllInvoice.DefaultView.RowFilter = string.Format($"[{FilterCotumn}] LIKE'{TextValue}%'");
                lblRecords.Text = _dtAllInvoice.DefaultView.Count.ToString();
            }

            dgvAllInvoice.DataSource = _dtAllInvoice.DefaultView;


            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";

            

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

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((string)dgvAllInvoice.CurrentRow.Cells[8].Value == "Canceled" || (string)dgvAllInvoice.CurrentRow.Cells[8].Value == "Deleted")
            {
                MessageBox.Show("You cannot edit a canceled or deleted invoice.", "Edit Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
             FrmAddUpdateInvoice frmAddUpdateInvoice = new FrmAddUpdateInvoice((int)dgvAllInvoice.CurrentRow.Cells[0].Value);
             frmAddUpdateInvoice.ShowDialog();
             FrmListInvoice_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InvoiceID = (int)dgvAllInvoice.CurrentRow.Cells[0].Value;
            FrmListInvoiceItems frmListInvoiceItems = new FrmListInvoiceItems(InvoiceID);
            frmListInvoiceItems.ShowDialog();
            FrmListInvoice_Load(null, null);
        }

        private void canceledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("are you sure want to Canceled Invoice [" + dgvAllInvoice.CurrentRow.Cells[0].Value + "]", "Confirm Canceled", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsInvoice.CanceledInvoice((int)dgvAllInvoice.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Invoice Canceled Successfuly.", "Canceled Seccessed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmListInvoice_Load(null, null);
                }
                else
                    MessageBox.Show("Invoice was not Canceled, because it has data linked to it.", "Canceled Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("are you sure want to deleted Invoice [" + dgvAllInvoice.CurrentRow.Cells[0].Value + "]", "Confirm Invoice", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsInvoice.DeleteInvoice((int)dgvAllInvoice.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Invoice Deleted Successfuly.", "Deleted Seccessed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmListInvoice_Load(null, null);
                }
                else
                    MessageBox.Show("Invoice was not deleted, because it has data linked to it.", "Deleted Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentPage++;
            LoadCurrentPage();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _currentPage--;
            LoadCurrentPage();
        }
    }
}
