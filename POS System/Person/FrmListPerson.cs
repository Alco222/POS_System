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

namespace POS_System.Person
{
    public partial class FrmListPerson : Form
    {
        static DataTable _dtPeople;
        DataTable _dtAllPeople;
        private static int _currentPage = 1;
        private const int _PAGE_SIZE = 60;
        private int _totalPages = 0;
        private int _totalRows = 0;

        public FrmListPerson()
        {
            InitializeComponent();
        }

        private void LoadCurrentPage()
        {

            _dtPeople = clsPerson.GetAllPeople(_currentPage, _PAGE_SIZE);
            _dtAllPeople = _dtPeople.DefaultView.ToTable(false, "PersonID", "Full_Name", "NationalNo", "Gender_Caption",
                                                               "DateOfBirth", "Phone", "Email");

            clsUtil.LoadDataPage2(_dtPeople, dgvAllPerson, _currentPage, _PAGE_SIZE,
               lblPageInfo, btnPrevious, btnNext, ref _totalRows, ref _totalPages);

            dgvAllPerson.DataSource = _dtAllPeople;
            lblRecords.Text = _totalRows.ToString();

            if (_totalPages <= 1)
            {
                btnPrevious.Visible = false;
                lblPageInfo.Visible = false;
                btnNext.Visible = false;
            }
        }

        private void _RefreshListPerson()
        {
             LoadCurrentPage();

            cbFilter.SelectedIndex = 0;

            if (dgvAllPerson.Rows.Count > 0)
            {
                dgvAllPerson.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                dgvAllPerson.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);

                dgvAllPerson.Columns[0].HeaderText = "PersonID";
                dgvAllPerson.Columns[0].Width = 60;
                dgvAllPerson.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllPerson.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[1].HeaderText = "Full Name";
                dgvAllPerson.Columns[1].Width = 80;
                dgvAllPerson.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[2].HeaderText = "National No";
                dgvAllPerson.Columns[2].Width = 70;
                dgvAllPerson.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[3].HeaderText = "Gender";
                dgvAllPerson.Columns[3].Width = 60;
                dgvAllPerson.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


                dgvAllPerson.Columns[4].HeaderText = "Bith Date";
                dgvAllPerson.Columns[4].Width = 90;
                dgvAllPerson.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllPerson.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvAllPerson.Columns[5].HeaderText = "Phone";
                dgvAllPerson.Columns[5].Width = 90;
                dgvAllPerson.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[6].HeaderText = "Email";
                dgvAllPerson.Columns[6].Width = 120;
                dgvAllPerson.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void FrmListPerson_Load(object sender, EventArgs e)
        {
            _RefreshListPerson();
        }
       
        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? PersonID = (int)dgvAllPerson.CurrentRow.Cells[0].Value;
            FrmShowDetailsPerson frmShowDetails = new FrmShowDetailsPerson(PersonID.Value);
            frmShowDetails.ShowDialog();
            _RefreshListPerson();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddUpdatePerson frmAddUpdatePerson = new FrmAddUpdatePerson();
            frmAddUpdatePerson.Show();
            _RefreshListPerson();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
              this.Close();
        }

        private void txtfilter_TextChanged(object sender, EventArgs e)
        {
            string CBFilter = cbFilter.Text;
            string FilterCotumn = "";
            string TextValue = txtfilter.Text.Trim();
       
            switch (CBFilter)
            {
                case "Person ID":
                    FilterCotumn = "PersonID";
                    break;
                case "Full Name":
                    FilterCotumn = "Full_Name";
                    break;
                case "National No":
                    FilterCotumn = "NationalNo";
                    break;
                case "Gender":
                    FilterCotumn = "Gender_Caption";
                    break;
                case "Email":
                    FilterCotumn = "Email";
                    break;
                case "Phone":
                    FilterCotumn = "Phone";
                    break;
                default:
                    FilterCotumn = "None";
                    break;
            }

            if (TextValue == "" || FilterCotumn == "None")
            {
                _dtAllPeople.DefaultView.RowFilter = "";

                LoadCurrentPage();
                if (_totalPages > 1)
                {
                    btnPrevious.Visible = true;
                    lblPageInfo.Visible = true;
                    btnNext.Visible = true;
                }
                lblRecords.Text = _dtAllPeople.Rows.Count.ToString();

                return;
            }

            if (FilterCotumn == "PersonID")
            {
                _dtAllPeople.DefaultView.RowFilter = string.Format($"[{FilterCotumn}] = {TextValue}");
                lblRecords.Text = dgvAllPerson.Rows.Count.ToString();
            }
            else
            {
                _dtAllPeople.DefaultView.RowFilter = string.Format($"[{FilterCotumn}] LIKE'{TextValue}%'");
                lblRecords.Text = dgvAllPerson.Rows.Count.ToString();
            }

           dgvAllPerson.DataSource = _dtAllPeople.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";
           

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

        private void txtfilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "Person ID" || cbFilter.SelectedItem.ToString() == "Phone")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("are you sure want to deleted Person [" + dgvAllPerson.CurrentRow.Cells[0].Value + "]", "Confirm Deleted", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson((int)dgvAllPerson.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfuly.", "Deleted Seccessed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshListPerson();
                }
                else
                    MessageBox.Show("Person was not deleted, because it has data linked to it.", "Deleted Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddUpdatePerson frmAddUpdatePerson = new FrmAddUpdatePerson((int)dgvAllPerson.CurrentRow.Cells[0].Value);
            frmAddUpdatePerson.ShowDialog();
            _RefreshListPerson();
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
            FrmAddUpdatePerson frmAddUpdatePerson = new FrmAddUpdatePerson();
            frmAddUpdatePerson.ShowDialog();
            _RefreshListPerson();
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
