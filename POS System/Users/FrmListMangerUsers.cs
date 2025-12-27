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

namespace POS_System.Users
{
    public partial class FrmListMangerUsers : Form
    {
        static DataTable _dtAllUser;
        private static int _currentPage = 1;
        private const int _PAGE_SIZE = 60;
        private int _totalPages = 0;
        private int _totalRows = 0;

        public FrmListMangerUsers()
        {
            InitializeComponent();
        }

        private void LoadCurrentPage()
        {

            _dtAllUser = clsUsers.GetAllUsers2(_currentPage, _PAGE_SIZE);
          
            clsUtil.LoadDataPage2(_dtAllUser, dgvAllUsers, _currentPage, _PAGE_SIZE,
               lblPageInfo, btnPrevious, btnNext, ref _totalRows, ref _totalPages);

            dgvAllUsers.DataSource = _dtAllUser;
            lblRecords.Text = _totalRows.ToString();

            if (_totalPages <= 1)
            {
                btnPrevious.Visible = false;
                lblPageInfo.Visible = false;
                btnNext.Visible = false;
            }
        }

        private void _RefreshListUser()
        {
            LoadCurrentPage();

            cbFilter.SelectedIndex = 0;

            if (dgvAllUsers.Rows.Count > 0)
            {
                dgvAllUsers.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                dgvAllUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
                
                dgvAllUsers.Columns[0].HeaderText = "UserID";
                dgvAllUsers.Columns[0].Width = 60;
                dgvAllUsers.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllUsers.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllUsers.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllUsers.Columns[1].HeaderText = "User Name";
                dgvAllUsers.Columns[1].Width =120;
                dgvAllUsers.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllUsers.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllUsers.Columns[2].HeaderText = "Full Name";
                dgvAllUsers.Columns[2].Width = 150;
                dgvAllUsers.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllUsers.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllUsers.Columns[3].HeaderText = "RoleID";
                dgvAllUsers.Columns[3].Width = 60;
                dgvAllUsers.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllUsers.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllUsers.Columns[4].HeaderText = "IsActive";
                dgvAllUsers.Columns[4].Width = 60;
                dgvAllUsers.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllUsers.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

               
                dgvAllUsers.Columns[5].HeaderText = "CreatedAt";
                dgvAllUsers.Columns[5].Width = 130;
                dgvAllUsers.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllUsers.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                dgvAllUsers.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllUsers.Columns[6].HeaderText = "UpdateAt";
                dgvAllUsers.Columns[6].Width = 130;
                dgvAllUsers.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllUsers.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                dgvAllUsers.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllUsers.Columns.RemoveAt(7);
            }
        }

        private void FrmListMangerUsers_Load(object sender, EventArgs e)
        {
            _RefreshListUser();
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

        private void txtfilter_TextChanged(object sender, EventArgs e)
        {
            string CBFilter = cbFilter.Text;
            string FilterCotumn = "";
            string TextValue = txtfilter.Text.Trim();

            switch (CBFilter)
            {
                case "User ID":
                    FilterCotumn = "UserID";
                    break;   
                case "Person ID":
                    FilterCotumn = "PersonID";
                    break;
                case "Full Name":
                    FilterCotumn = "FullName";
                    break;
                case "User Name":
                    FilterCotumn = "UserName";
                    break;
             
                case "Is Active":
                    FilterCotumn = "IsActive";
                    break;
                case "Role ID":
                    FilterCotumn = "RoleID";
                    break;
                default:
                    FilterCotumn = "None"; // Default to UserName if no match
                    break;
            }

            if (TextValue == "" || FilterCotumn == "None")
            {
                _dtAllUser.DefaultView.RowFilter = "";

                LoadCurrentPage();
                if (_totalPages > 1)
                {
                    btnPrevious.Visible = true;
                    lblPageInfo.Visible = true;
                    btnNext.Visible = true;
                }
                lblRecords.Text = _dtAllUser.Rows.Count.ToString();

                return;
            }

            if (FilterCotumn == "PersonID"|| FilterCotumn == "UserID" || FilterCotumn == "RoleID")
            {
                _dtAllUser.DefaultView.RowFilter = string.Format($"[{FilterCotumn}] = {TextValue}");
                lblRecords.Text = dgvAllUsers.Rows.Count.ToString();
            }
            else
            {
                _dtAllUser.DefaultView.RowFilter = string.Format($"[{FilterCotumn}] LIKE'{TextValue}%'");
                lblRecords.Text = dgvAllUsers.Rows.Count.ToString();
            }

            dgvAllUsers.DataSource = _dtAllUser.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";

        }

        private void txtfilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.Text=="User ID" || cbFilter.Text == "Person ID" || cbFilter.Text == "Role ID")
            {
                // Allow only digits and control characters (like backspace)
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; // Ignore the input
                }
            }
        }
      
        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
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


            if (FilterValue == "All")
                _dtAllUser.DefaultView.RowFilter = "";
            else//in this case we deal with numbers not string.
                _dtAllUser.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            dgvAllUsers.DataSource = _dtAllUser.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";
            lblRecords.Text = dgvAllUsers.Rows.Count.ToString();
        }
      
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Is Active")
            {
                txtfilter.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtfilter.Visible = (cbFilter.Text != "None");
                cbIsActive.Visible = false;

                txtfilter.Text = "";
                txtfilter.Focus();
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not available now.", "Not Available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddUpdateUser frmAddUpdateUser = new FrmAddUpdateUser((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frmAddUpdateUser.ShowDialog();
            _RefreshListUser();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddUpdateUser frmAddUpdateUser = new FrmAddUpdateUser(-1);
            frmAddUpdateUser.ShowDialog();
            _RefreshListUser();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("are you sure want to deleted User [" + dgvAllUsers.CurrentRow.Cells[0].Value + "]", "Confirm Deleted", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsUsers.DeleteUser((int)dgvAllUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfuly.", "Deleted Seccessed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmListMangerUsers_Load(null, null);
                }
                else
                    MessageBox.Show("User was not deleted, because it has data linked to it.", "Deleted Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not available now.", "Not Available", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void callPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not available now.", "Not Available", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FrmAddUpdateUser frmAddUpdateUser = new FrmAddUpdateUser(-1);
            frmAddUpdateUser.ShowDialog();
            _RefreshListUser();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
