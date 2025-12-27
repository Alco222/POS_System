using POS_System.Class_Globale;
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

namespace POS_System.Customer
{
    public partial class FrmListCustomers : Form
    {
        static DataTable _dtCustomers;
        private static int _currentPage = 1;
        private const int _PAGE_SIZE = 60;
        private int _totalPages = 0;
        private int _totalRows = 0;

        public FrmListCustomers()
        {
            InitializeComponent();
        }

        private void LoadCurrentPage()
        {
            _dtCustomers = clsCustomer.GetAllCustomers2(_currentPage, _PAGE_SIZE);
           
            clsUtil.LoadDataPage2(_dtCustomers, dgvCustomers, _currentPage, _PAGE_SIZE,
               lblPageInfo, btnPrevious, btnNext, ref _totalRows, ref _totalPages);

            dgvCustomers.DataSource = _dtCustomers;
            _dtCustomers.Columns.Remove("TotalRows"); // Remove the 9th column (index 8)
            lblRecords.Text = _totalRows.ToString();

            if (_totalPages <= 1)
            {
                btnPrevious.Visible = false;
                lblPageInfo.Visible = false;
                btnNext.Visible = false;
            }
        }

        private void FrmListCustomers_Load(object sender, EventArgs e)
        {
            LoadCurrentPage();
            cbFilter.SelectedIndex = 0;

            if (dgvCustomers.Rows.Count > 0)
            {
                dgvCustomers.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);

                dgvCustomers.Columns[0].HeaderText = "CustomerID";
                dgvCustomers.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvCustomers.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvCustomers.Columns[1].HeaderText = "Customer Name";
                dgvCustomers.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvCustomers.Columns[2].HeaderText = "National NO";
                dgvCustomers.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvCustomers.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvCustomers.Columns[3].HeaderText = "Register Date";
                dgvCustomers.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvCustomers.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvCustomers.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvCustomers.Columns[4].HeaderText = "Loyalty Points";
                dgvCustomers.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvCustomers.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvCustomers.Columns[5].HeaderText = "Tax Number";
                dgvCustomers.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvCustomers.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvCustomers.Columns[6].HeaderText = "Created By";
                dgvCustomers.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvCustomers.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvCustomers.Columns[7].HeaderText = "IsActive";
                dgvCustomers.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvCustomers.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCustomerInfo frmCustomerInfo = new FrmCustomerInfo((int)dgvCustomers.CurrentRow.Cells[0].Value);
            frmCustomerInfo.ShowDialog();
            FrmListCustomers_Load(null, null);
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            FrmAddUpdateCustomers frmAddUpdateCustomers = new FrmAddUpdateCustomers();
            frmAddUpdateCustomers.ShowDialog();
            FrmListCustomers_Load(null, null);
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddUpdateCustomers frmAddUpdateCustomers = new FrmAddUpdateCustomers((int)dgvCustomers.CurrentRow.Cells[0].Value);
            frmAddUpdateCustomers.ShowDialog();
            FrmListCustomers_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("are you sure want to deleted Customer [" + dgvCustomers.CurrentRow.Cells[0].Value + "]", "Confirm Deleted", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsCustomer.DeleteCustomer((int)dgvCustomers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Customer Deleted Successfuly.", "Deleted Seccessed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmListCustomers_Load(null, null);
                }
                else
                    MessageBox.Show("Customer was not deleted, because it has data linked to it.", "Deleted Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text.ToString() == "None")
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = false;
               
            }
            else
            {
                if (cbFilter.Text == "Is Active")
                {
                    cbIsActive.Visible = true;
                    txtFilter.Visible = false;
                    cbIsActive.Focus();
                    cbIsActive.SelectedIndex = 0;
                }
                else
                {
                    cbIsActive.Visible = false;
                    txtFilter.Visible = true;
                    txtFilter.Text = "";
                    txtFilter.Focus();
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string CBFilter = cbFilter.Text;
            string FilterCotumn = "";
            string TextValue = txtFilter.Text.Trim();

            switch(CBFilter)
            {
                case "Customer ID":
                    FilterCotumn = "CustomerID";
                    break;
                case "Customer Name":
                    FilterCotumn = "CustomerName";
                    break;
                case "National No":
                    FilterCotumn = "NationalNo";
                    break;
                case "Is Active":
                    FilterCotumn = "IsActive";
                    break;
                default:
                    FilterCotumn = "None";
                    break;
            }

            if(TextValue == "" || FilterCotumn == "None")
            {
                _dtCustomers.DefaultView.RowFilter = "";

                LoadCurrentPage();

                if (_totalPages > 1)
                {
                    btnPrevious.Visible = true;
                    lblPageInfo.Visible = true;
                    btnNext.Visible = true;
                }

                lblRecords.Text = _dtCustomers.Rows.Count.ToString();
                return;
            }

            if (FilterCotumn == "CustomerID")
            {
                _dtCustomers.DefaultView.RowFilter = string.Format($"[{FilterCotumn}] = {TextValue}");
                lblRecords.Text = dgvCustomers.Rows.Count.ToString();
            }
            else
            {
                _dtCustomers.DefaultView.RowFilter = string.Format($"[{FilterCotumn}] LIKE'{TextValue}%'");
                lblRecords.Text = dgvCustomers.Rows.Count.ToString();
            }

            dgvCustomers.DataSource = _dtCustomers.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";
           
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text.Trim();

            switch(FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }
            
            if (FilterColumn == "None")
            {
                _dtCustomers.DefaultView.RowFilter = "";

                LoadCurrentPage();
                if (_totalPages > 1)
                {
                    btnPrevious.Visible = true;
                    lblPageInfo.Visible = true;
                    btnNext.Visible = true;
                }
                lblRecords.Text = _dtCustomers.Rows.Count.ToString();
                return;
            }


            if (FilterValue == "All")
            {
                _dtCustomers.DefaultView.RowFilter = "";
                lblRecords.Text = dgvCustomers.Rows.Count.ToString();

            }
            else
            {
                _dtCustomers.DefaultView.RowFilter = string.Format($"[{FilterColumn}] = {FilterValue}");
                lblRecords.Text = dgvCustomers.Rows.Count.ToString();
            }

            dgvCustomers.DataSource = _dtCustomers.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && cbFilter.Text == "Customer ID")
            {
                e.Handled = true;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _currentPage--;
            LoadCurrentPage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentPage++;
            LoadCurrentPage();
        }
    }
}
