using Guna.UI2.WinForms;
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

namespace POS_System.Product
{
    public partial class FrmListProduct : Form
    {
        static DataTable _dtAllProduct;
        private static int _currentPage = 1;
        private const int _PAGE_SIZE = 60;
        private int _totalPages = 0;
        private int _totalRows = 0;

        public FrmListProduct()
        {
            InitializeComponent();
        }

        private void LoadCurrentPage()
        {
            _dtAllProduct = clsProduct.GetAllProducts2(_currentPage, _PAGE_SIZE);

            clsUtil.LoadDataPage2(_dtAllProduct, dgvAllProduct, _currentPage, _PAGE_SIZE,
               lblPageInfo, btnPrevious, btnNext, ref _totalRows, ref _totalPages);

            dgvAllProduct.DataSource = _dtAllProduct;
            _dtAllProduct.Columns.Remove("TotalRows"); // Remove the 9th column (index 8)
            lblRecords.Text = _totalRows.ToString();

            if (_totalPages <= 1)
            {
                btnPrevious.Visible = false;
                lblPageInfo.Visible = false;
                btnNext.Visible = false;
            }
        }

        private void _RefreshListProduct()
        {
            LoadCurrentPage();
            cbFilter.SelectedIndex = 0;

            if (dgvAllProduct.Rows.Count > 0)
            {
                dgvAllProduct.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                dgvAllProduct.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
                dgvAllProduct.Columns[0].HeaderText = "Prod.ID";
                dgvAllProduct.Columns[0].Width = 90;
                dgvAllProduct.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllProduct.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllProduct.Columns[1].HeaderText = "P.Name";
                dgvAllProduct.Columns[1].Width = 190;
                dgvAllProduct.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllProduct.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                dgvAllProduct.Columns[2].HeaderText = "Description";
                dgvAllProduct.Columns[2].Width = 250;
                dgvAllProduct.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllProduct.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dgvAllProduct.ShowCellToolTips = true;


                dgvAllProduct.Columns[3].HeaderText = "Price";
                dgvAllProduct.Columns[3].Width = 100;
                dgvAllProduct.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllProduct.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                dgvAllProduct.Columns[4].HeaderText = "QuanStock";
                dgvAllProduct.Columns[4].Width = 95;
                dgvAllProduct.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllProduct.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllProduct.Columns[5].HeaderText = "StockStatus";
                dgvAllProduct.Columns[5].Width = 120;
                dgvAllProduct.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllProduct.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                dgvAllProduct.Columns[6].HeaderText = "Tax";
                dgvAllProduct.Columns[6].Width = 80;
                dgvAllProduct.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllProduct.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                dgvAllProduct.Columns[7].HeaderText = "T.Price";
                dgvAllProduct.Columns[7].Width = 130;
                dgvAllProduct.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllProduct.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void FrmListProduct_Load(object sender, EventArgs e)
        {
            _RefreshListProduct();
        } 
        
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FrmAddUpdateProduct frmAddUpdateProduct = new FrmAddUpdateProduct();
            frmAddUpdateProduct.ShowDialog();
            FrmListProduct_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvAllProduct_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
            if ((dgvAllProduct.Columns[e.ColumnIndex].Name == "Price" || dgvAllProduct.Columns[e.ColumnIndex].Name == "PriceWithTax") && e.Value != null)
            {
                decimal value = Convert.ToDecimal(e.Value);
                e.Value = value.ToString("N2") + " DH"; // مثل: 1,200.00 DH
                e.FormattingApplied = true;
            }

            if (dgvAllProduct.Columns[e.ColumnIndex].Name == "Description" && e.Value != null)
            {
                string text = e.Value.ToString();
                int maxLength = 30; // الطول الأقصى الذي تريده

                if (text.Length > maxLength)
                {
                    // قص النص وإضافة "..."
                    e.Value = text.Substring(0, maxLength) + "...";

                    // إضافة ToolTip للنص الكامل
                    dgvAllProduct.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = text;

                    e.FormattingApplied = true;
                }
            }


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
                case "Product ID":
                    FilterCotumn = "ProductID";
                    break;
                case "Product Name":
                    FilterCotumn = "ProductName";
                    break;
                default:
                    FilterCotumn = "None";
                    break;
            }

            if (txtfilter.Text.Trim() == "" || FilterCotumn == "None")
            {
                _dtAllProduct.DefaultView.RowFilter = "";

                LoadCurrentPage();

                if (_totalPages > 1)
                {
                    btnPrevious.Visible = true;
                    lblPageInfo.Visible = true;
                    btnNext.Visible = true;
                }

                lblRecords.Text = _dtAllProduct.DefaultView.Count.ToString();
                return;
            }

            if (FilterCotumn == "ProductID")
            {
                _dtAllProduct.DefaultView.RowFilter = string.Format($"[{FilterCotumn}] = {TextValue}");
                lblRecords.Text = _dtAllProduct.DefaultView.Count.ToString();
            }
            else
            {
                _dtAllProduct.DefaultView.RowFilter = string.Format($"[{FilterCotumn}] LIKE'{TextValue}%'");
                lblRecords.Text = _dtAllProduct.DefaultView.Count.ToString();
            }

            dgvAllProduct.DataSource = _dtAllProduct.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";

        }

        private void txtfilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "Product ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void editProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddUpdateProduct frmAddUpdateProduct = new FrmAddUpdateProduct((int)dgvAllProduct.CurrentRow.Cells[0].Value);
            frmAddUpdateProduct.ShowDialog();
            FrmListProduct_Load(null, null);
        }

        private void showProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? ProductID = (int)dgvAllProduct.CurrentRow.Cells [0].Value; 
            FrmShowProduct frmShowProduct = new FrmShowProduct(ProductID);
            frmShowProduct.ShowDialog();
            FrmListProduct_Load(null, null);
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
