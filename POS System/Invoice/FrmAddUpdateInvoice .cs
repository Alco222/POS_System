using Guna.UI2.WinForms;
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


    public partial class FrmAddUpdateInvoice : Form
    {
        // 🔹 Define form modes
        enum enMode { AddNew = 0, Update = 1 }

        // 🔹 Private fields
        enMode _Mode;
        private clsCustomer _Customer;
        private clsProduct _Product;
        private clsInvoice _Invoice;
        private clsInvoiceItems _InvoiceItems;

        private int? _InvoiceID;
        private int? _InvoiceItemsID;

        private int? _ProductID;
        private int? _CustomerID;

        private decimal _currentPrice = 0;
        private decimal _currentTaxPercent = 0;
        private decimal _currentDiscountPercent = 0;
        private DataGridViewRow _CurrentEditingRow = null;
        private bool _IsEditingLine = false;

        // ============================================================
        // 🔹 Constructors
        // ============================================================
        public FrmAddUpdateInvoice()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public FrmAddUpdateInvoice(int InvoiceID)
        {
            InitializeComponent();
            _InvoiceID = InvoiceID;
            _Mode = enMode.Update;
        }

        // ============================================================
        // 🔹 Reset form default values
        // ============================================================
        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                _Invoice = new clsInvoice();
                _InvoiceItems = new clsInvoiceItems();
                lblMode.Text = "Add New Invoice and Items";
            }
        }

        // ============================================================
        // 🔹 Load Invoice and its Items
        // ============================================================
        public void LoadInvoiceData(int? InvoiceID)
        {
            if (InvoiceID == null) return;

            _Invoice = clsInvoice.Find(InvoiceID);

            if (_Invoice == null)
            {
                MessageBox.Show("Invoice not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 1️⃣ Load General Invoice Information
            _InvoiceID = _Invoice.InvoiceID;
            lblNumInvoice.Text = _InvoiceID.ToString();
            lblDateRegister.Text = _Invoice.InvoiceDate.ToString("dd/MM/yyyy");
            _CustomerID = _Invoice.CustomerID;

            // 🔸 Load Customer Info
            if (_CustomerID != null)
            {
                _Customer = clsCustomer.Find(_CustomerID);
                if (_Customer != null)
                {
                    cbSelectCustomer.Text = _Customer.CustomerName;
                    lblPhNumber.Text = _Customer.PersonInfo.Phone;
                    lblTaxNumber.Text = _Customer.TaxNumber;
                    lblAddress.Text = _Customer.PersonInfo.Address;
                }
            }

            // 🔸 Fill invoice general fields
            lblDateRegister.Text = _Invoice.InvoiceDate.ToString("dd/MM/yyyy HH:mm:ss");
            cbPaymentMethod.Text = _Invoice.PaymentMethod ?? "Cash";
            txtSubTotal.Text = _Invoice.SubTotal.ToString("N2");
            txtTotalTax.Text = _Invoice.TotalTax.ToString("N2");
            txtTotalDiscount.Text = _Invoice.TotalDiscount.ToString("N2");
            txtGrandTotal.Text = _Invoice.TotalAmount.ToString("N2");

            // 2️⃣ Load Invoice Items
            DataTable dtInvoiceItems = clsInvoiceItems.GetInvoiceItemsByInvoiceID(InvoiceID);
            dgvInvoiceItems.Rows.Clear();

            foreach (DataRow row in dtInvoiceItems.Rows)
            {
                int rowIndex = dgvInvoiceItems.Rows.Add();
                DataGridViewRow dgvRow = dgvInvoiceItems.Rows[rowIndex];

                //dgvRow.Cells["ProductID"].Value = row["ProductID"];
                dgvRow.Cells["ProductName"].Value = row["ProductName"];
                dgvRow.Cells["Description"].Value = row["Descriptions"];
                dgvRow.Cells["UnitPrice"].Value = Convert.ToDecimal(row["UnitPrice"]).ToString("N2");
                dgvRow.Cells["Quantity"].Value = Convert.ToInt32(row["Quantity"]);
                dgvRow.Cells["TaxPercent"].Value = Convert.ToDecimal(row["TaxPercent"]).ToString("0");
                dgvRow.Cells["DiscountPercent"].Value = Convert.ToDecimal(row["DiscountPercent"]).ToString("0");
                dgvRow.Cells["DiscountAmount"].Value = Convert.ToDecimal(row["DiscountAmount"]).ToString("N2");
                dgvRow.Cells["LineTotal"].Value = Convert.ToDecimal(row["LineTotal"]).ToString("N2");
            }

            // 3️⃣ Update Totals
            RecalculateInvoiceTotals();

            // 4️⃣ Lock fields when editing
            if (_Mode == enMode.Update)
            {
                cbSelectCustomer.Enabled = false;
                lblDateRegister.Enabled = false;
            }
        }

        // ============================================================
        // 🔹 Form Load Event
        // ============================================================
        private void FrmAddUpdateInvoice_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            FillComboBox(cbSelectCustomer, clsCustomer.GetAllCustomers(), "CustomerName");
            FillComboBox(cbProductName, clsProduct.GetAllProducts(), "ProductName");
            InitializeDgvInvoiceItems();

            cbDiscount.Items.Clear();
            cbDiscount.Items.AddRange(new object[] { "0%", "10%", "25%", "50%", "75%" });
            cbDiscount.SelectedIndex = 0;

            lblDateRegister.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            if (_Mode == enMode.Update && _InvoiceID != null)
                LoadInvoiceData(_InvoiceID);
        }

        // ============================================================
        // 🔹 Fill ComboBoxes
        // ============================================================
        private void FillComboBox(ComboBox cb, DataTable dt, string displayMember)
        {
            cb.Items.Clear();
            foreach (DataRow row in dt.Rows)
                cb.Items.Add(row[displayMember]);
        }

        // ============================================================
        // 🔹 Initialize DataGridView structure
        // ============================================================
        private void InitializeDgvInvoiceItems()
        {
            dgvInvoiceItems.Columns.Clear();

            dgvInvoiceItems.Columns.Add("ProductID", "ProductID");
            dgvInvoiceItems.Columns["ProductID"].Visible = true;

            dgvInvoiceItems.Columns.Add("ProductName", "Product Name");
            dgvInvoiceItems.Columns.Add("Description", "Description");
            dgvInvoiceItems.Columns.Add("UnitPrice", "Price(DH)");
            dgvInvoiceItems.Columns.Add("Quantity", "Quantity");
            dgvInvoiceItems.Columns.Add("TaxPercent", "Tax (%)");
            dgvInvoiceItems.Columns.Add("DiscountPercent", "Discount (%)");
            dgvInvoiceItems.Columns.Add("DiscountAmount", "Discount Amount(DH)");
            dgvInvoiceItems.Columns.Add("LineTotal", "Line Total(DH)");

            // 🔸 Add "Remove" button column
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            {
                Name = "DeleteColumn",
                HeaderText = "Remove",
                Text = "Remove",
                UseColumnTextForButtonValue = true
            };
            dgvInvoiceItems.Columns.Add(deleteButtonColumn);

            // 🔸 Format columns
            dgvInvoiceItems.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
            dgvInvoiceItems.Columns["DiscountAmount"].DefaultCellStyle.Format = "N2";
            dgvInvoiceItems.Columns["LineTotal"].DefaultCellStyle.Format = "N2";
            dgvInvoiceItems.Columns["TaxPercent"].DefaultCellStyle.Format = "0";
            dgvInvoiceItems.Columns["DiscountPercent"].DefaultCellStyle.Format = "0";

            dgvInvoiceItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInvoiceItems.AllowUserToAddRows = false;
        }

        // ============================================================
        // 🔹 Delete Product from DataGridView
        // ============================================================
        private void dgvInvoiceItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvInvoiceItems.Columns[e.ColumnIndex].Name == "DeleteColumn")
            {
                DialogResult result = MessageBox.Show("Do you want to delete this row?",
                    "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    dgvInvoiceItems.Rows.RemoveAt(e.RowIndex);
            }
        }

        // ============================================================
        // 🔹 Customer Selection
        // ============================================================
        private void cbSelectCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSelectCustomer.SelectedItem == null) return;
            _CustomerID = clsCustomer.Find(cbSelectCustomer.Text).CustomerID;
            LoadCustomerData(_CustomerID);
        }

        private void LoadCustomerData(int? customerID)
        {
            _Customer = clsCustomer.Find(customerID);
            if (_Customer != null)
            {
                lblPhNumber.Text = _Customer.PersonInfo.Phone;
                lblTaxNumber.Text = _Customer.TaxNumber;
                lblAddress.Text = _Customer.PersonInfo.Address;
            }
            else
            {
                lblPhNumber.Text = "[????]";
                lblTaxNumber.Text = "[????]";
                lblAddress.Text = "[????]";
            }
        }

        // ============================================================
        // 🔹 Product Selection
        // ============================================================
        private void cbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProductName.SelectedItem == null) return;
            _ProductID = clsProduct.Find(cbProductName.Text).ProductID;
            LoadProductData(_ProductID);
        }

        private void LoadProductData(int? productID)
        {
            _Product = clsProduct.Find(productID);
            if (_Product == null) return;

            txtDescription.Text = _Product.Descriptions ?? "";
            _currentPrice = _Product.Price;
            _currentTaxPercent = NormalizeTaxPercent(_Product.TaxPercent);

            txtPrice.Text = _currentPrice.ToString("N2");
            txtTax.Text = (_currentTaxPercent * 100).ToString("0") + " %";

            ndQuantity.Value = 1;
            ndQuantity.Maximum = _Product.QuantityInStock;
            cbDiscount.SelectedIndex = 0;

            RecalculateLineTotal();
        }

        private decimal NormalizeTaxPercent(decimal rawTax)
        {
            return rawTax > 1 ? rawTax / 100m : rawTax;
        }

        // ============================================================
        // 🔹 Line Total Calculation
        // ============================================================
        private void ndQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (ndQuantity.Value <= _Product.QuantityInStock)
                RecalculateLineTotal();
            else
                MessageBox.Show("The selected quantity exceeds available stock.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtPrice_TextChanged(object sender, EventArgs e) => RecalculateLineTotal();

        private void cbDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDiscount.SelectedItem == null) return;
            _currentDiscountPercent = decimal.Parse(cbDiscount.SelectedItem.ToString().Replace("%", "")) / 100m;
            RecalculateLineTotal();
        }

        private void RecalculateLineTotal()
        {
            int qty = (int)ndQuantity.Value;
            decimal subTotal = _currentPrice * qty;
            decimal taxAmount = subTotal * _currentTaxPercent;
            decimal discountAmount = subTotal * _currentDiscountPercent;
            decimal total = subTotal + taxAmount - discountAmount;

            txtTotal.Text = total.ToString("N2");
        }

        // ============================================================
        // 🔹 Add Product to DataGridView
        // ============================================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            /* if (cbProductName.SelectedItem == null)
             {
                 MessageBox.Show("Please select a product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

             int quantity = (int)ndQuantity.Value;

             // 🔹 Check for duplicate product before adding or updating
             foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
             {
                 if (row.IsNewRow) continue;

                 object cellValue = row.Cells["ProductID"].Value;
                 if (cellValue == null) continue; // <== تجنب NullReference

                 int existingProductID = Convert.ToInt32(cellValue);


                 // When adding a new row
                 if (!_IsEditingLine && (int)row.Cells["ProductID"].Value == _ProductID)
                 {
                     MessageBox.Show("This product already exists in the invoice.", "Warning",
                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return;
                 }

                 // When updating an existing row, ignore the row being edited
                 if (_IsEditingLine && (int)row.Cells["ProductID"].Value == _ProductID && row.Index != _CurrentEditingRow?.Index)
                 {
                     MessageBox.Show("This product already exists in the invoice.", "Warning",
                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return;
                 }
             }

             decimal subTotal = _currentPrice * quantity;
             decimal discountAmount = subTotal * _currentDiscountPercent;
             decimal taxAmount = subTotal * _currentTaxPercent;
             decimal lineTotal = subTotal + taxAmount - discountAmount;

             if (_IsEditingLine && _CurrentEditingRow != null )
             {
                 // Update the row after confirming no duplicates
                 _CurrentEditingRow.Cells["ProductID"].Value = _ProductID;
                 _CurrentEditingRow.Cells["ProductName"].Value = cbProductName.Text;
                 _CurrentEditingRow.Cells["Description"].Value = txtDescription.Text;
                 _CurrentEditingRow.Cells["UnitPrice"].Value = _currentPrice;
                 _CurrentEditingRow.Cells["Quantity"].Value = quantity;
                 _CurrentEditingRow.Cells["TaxPercent"].Value = (_currentTaxPercent * 100).ToString("0");
                 _CurrentEditingRow.Cells["DiscountPercent"].Value = (_currentDiscountPercent * 100).ToString("0");
                 _CurrentEditingRow.Cells["DiscountAmount"].Value = discountAmount.ToString("N2");
                 _CurrentEditingRow.Cells["LineTotal"].Value = lineTotal.ToString("N2");
             }
             else
             {
                 // Add a new row
                 dgvInvoiceItems.Rows.Add(
                     _ProductID,
                     cbProductName.Text,
                     txtDescription.Text,
                     _currentPrice,
                     quantity,
                     (_currentTaxPercent * 100).ToString("0"),
                     (_currentDiscountPercent * 100).ToString("0"),
                     discountAmount.ToString("N2"),
                     lineTotal.ToString("N2")
                 );
             }

             // Recalculate invoice totals
             RecalculateInvoiceTotals();

             // Reset product input fields
             ndQuantity.Value = 1;
             cbDiscount.SelectedIndex = 0;
             cbProductName.Text = null;
             txtDescription.Text = null;
             txtPrice.Text = null;
             ndQuantity.Text = null;
             txtTax.Text = null;
             txtTotal.Clear();*/

            if (cbProductName.SelectedItem == null)
            {
                MessageBox.Show("Please select a product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int quantity = (int)ndQuantity.Value;

            // 🔹 Check for duplicate product before adding or updating
            foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
            {
                if (row.IsNewRow) continue;

                // تأكد من أن الخلية ليست null
                object cellValue = row.Cells["ProductID"].Value;
                if (cellValue == null) continue;

                int existingProductID = Convert.ToInt32(cellValue);

                // When adding a new row
                if (!_IsEditingLine && existingProductID == _ProductID)
                {
                    MessageBox.Show("This product already exists in the invoice.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // When updating an existing row, ignore the row being edited
                if (_IsEditingLine && existingProductID == _ProductID && row.Index != _CurrentEditingRow.Index)
                {
                    MessageBox.Show("This product already exists in the invoice.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            decimal subTotal = _currentPrice * quantity;
            decimal discountAmount = subTotal * _currentDiscountPercent;
            decimal taxAmount = subTotal * _currentTaxPercent;
            decimal lineTotal = subTotal + taxAmount - discountAmount;

            if (_IsEditingLine && _CurrentEditingRow != null)
            {
                // Update the row after confirming no duplicates
                _CurrentEditingRow.Cells["ProductID"].Value = _ProductID;
                _CurrentEditingRow.Cells["ProductName"].Value = cbProductName.Text;
                _CurrentEditingRow.Cells["Description"].Value = txtDescription.Text;
                _CurrentEditingRow.Cells["UnitPrice"].Value = _currentPrice;
                _CurrentEditingRow.Cells["Quantity"].Value = quantity;
                _CurrentEditingRow.Cells["TaxPercent"].Value = (_currentTaxPercent * 100).ToString("0");
                _CurrentEditingRow.Cells["DiscountPercent"].Value = (_currentDiscountPercent * 100).ToString("0");
                _CurrentEditingRow.Cells["DiscountAmount"].Value = discountAmount.ToString("N2");
                _CurrentEditingRow.Cells["LineTotal"].Value = lineTotal.ToString("N2");
            }
            else
            {
                // Add a new row
                dgvInvoiceItems.Rows.Add(
                    _ProductID,
                    cbProductName.Text,
                    txtDescription.Text,
                    _currentPrice,
                    quantity,
                    (_currentTaxPercent * 100).ToString("0"),
                    (_currentDiscountPercent * 100).ToString("0"),
                    discountAmount.ToString("N2"),
                    lineTotal.ToString("N2")
                );
            }

            // Recalculate invoice totals
            RecalculateInvoiceTotals();

            // Reset product input fields
            ndQuantity.Value = 0;
            cbDiscount.SelectedIndex = 0;
            cbProductName.SelectedIndex = -1; // تفريغ الاختيار
            txtDescription.Clear();
            txtPrice.Clear();
            ndQuantity.Text = "1";
            txtTax.Clear();
            txtTotal.Clear();

            _IsEditingLine = false;
            _CurrentEditingRow = null;

        }


        // ============================================================
        // 🔹 Recalculate Full Invoice Totals
        // ============================================================
        private void RecalculateInvoiceTotals()
        {
            decimal subTotal = 0, totalTax = 0, totalDiscount = 0, grandTotal = 0;

            foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
            {
                if (row.IsNewRow) continue;

                decimal unitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                decimal taxPercent = Convert.ToDecimal(row.Cells["TaxPercent"].Value) / 100;
                decimal discountAmount = Convert.ToDecimal(row.Cells["DiscountAmount"].Value);
                decimal lineTotal = Convert.ToDecimal(row.Cells["LineTotal"].Value);

                decimal lineSubTotal = unitPrice * quantity;
                decimal lineTax = lineSubTotal * taxPercent;

                subTotal += lineSubTotal;
                totalTax += lineTax;
                totalDiscount += discountAmount;
                grandTotal += lineTotal;
            }

            txtSubTotal.Text = subTotal.ToString("N2");
            txtTotalTax.Text = totalTax.ToString("N2");
            txtTotalDiscount.Text = totalDiscount.ToString("N2");
            txtGrandTotal.Text = grandTotal.ToString("N2");
        }

        // ============================================================
        // 🔹 Save Button
        // ============================================================
        /*  // 🔸 Save Each Item
foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
{
    if (row.IsNewRow) continue;

    _InvoiceItems.Quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
    _InvoiceItems.UnitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
    _InvoiceItems.LineTotal = Convert.ToDecimal(row.Cells["LineTotal"].Value);
    _InvoiceItems.ProductID = Convert.ToInt32(row.Cells["ProductID"].Value);
    _InvoiceItems.InvoiceID = _InvoiceID;
    _InvoiceItems.DiscountPercent = Convert.ToDecimal(row.Cells["DiscountPercent"].Value) / 100m;
    _InvoiceItems.DiscountAmount = Convert.ToDecimal(row.Cells["DiscountAmount"].Value);
    _InvoiceItems.CreatedByUserID = null;

    if (!_InvoiceItems.Save())
    {
        MessageBox.Show("Failed to save invoice items!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    // 🔸 Update stock quantity
    clsProduct Product = new clsProduct();
    Product.UpdateQuantity(_InvoiceItems.ProductID, _InvoiceItems.Quantity);
}*/
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_CustomerID == null)
                {
                    MessageBox.Show("Please select a customer first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dgvInvoiceItems.Rows.Count == 0)
                {
                    MessageBox.Show("Please add at least one product.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dtItems = new DataTable();
                dtItems.Columns.Add("ProductID", typeof(int));
                dtItems.Columns.Add("Quantity", typeof(int));
                dtItems.Columns.Add("UnitPrice", typeof(decimal));
                dtItems.Columns.Add("DiscountPercent", typeof(decimal));
                dtItems.Columns.Add("DiscountAmount", typeof(decimal));
                dtItems.Columns.Add("LineTotal", typeof(decimal));
                foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
                {
                    if (row.IsNewRow) continue;

                    dtItems.Rows.Add(
                        row.Cells["ProductID"].Value,
                        row.Cells["Quantity"].Value,
                        row.Cells["UnitPrice"].Value,
                        row.Cells["DiscountPercent"].Value,
                        row.Cells["DiscountAmount"].Value,
                        row.Cells["LineTotal"].Value
                    );
                }

                if (dtItems == null || dtItems.Columns.Count == 0)
                    MessageBox.Show("InvoiceItem DataTable must contain columns and rows.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 🔸 Fill Invoice Info
                _Invoice.InvoiceDate = Convert.ToDateTime(lblDateRegister.Text);
                _Invoice.TotalAmount = Convert.ToDecimal(txtGrandTotal.Text);
                _Invoice.CustomerID = _CustomerID;
                _Invoice.SubTotal = Convert.ToDecimal(txtSubTotal.Text);
                _Invoice.TotalTax = Convert.ToDecimal(txtTotalTax.Text);
                _Invoice.TotalDiscount = Convert.ToDecimal(txtTotalDiscount.Text);
                _Invoice.PaymentMethod = cbPaymentMethod.Text;
                _Invoice.CreatedByUserID = null;
                _Invoice.InvoiceItems = dtItems;


                if (!_Invoice.Save()) return;


                lblNumInvoice.Text = _Invoice.InvoiceID.ToString();
                _Mode = enMode.Update;
              

                MessageBox.Show("Invoice saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Saving Invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // 🔹 Close Button
        // ============================================================
        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        // ============================================================
        // 🔹 Edit Product Line on Double Click
        // ============================================================
        private void dgvInvoiceItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >=0)
            {
                DataGridViewRow SelectRow = dgvInvoiceItems.Rows[e.RowIndex];

                _IsEditingLine = true;
                _CurrentEditingRow = SelectRow;
                btnAdd.Text = "Update";
                // Load selected row data into input fields
                cbProductName.Text = _CurrentEditingRow.Cells["ProductName"].Value.ToString();
                txtDescription.Text = _CurrentEditingRow.Cells["Description"].Value.ToString();
                txtPrice.Text = Convert.ToDecimal(_CurrentEditingRow.Cells["UnitPrice"].Value).ToString("N2");
                ndQuantity.Value = Convert.ToInt32(_CurrentEditingRow.Cells["Quantity"].Value);
                cbDiscount.SelectedItem = _CurrentEditingRow.Cells["DiscountPercent"].Value.ToString() + "%";
                txtTotal.Text = Convert.ToDecimal(_CurrentEditingRow.Cells["LineTotal"].Value).ToString("N2");
            }
        }
    }

}
