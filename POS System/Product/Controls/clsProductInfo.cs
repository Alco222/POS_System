using POS_System.Properties;
using POSBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static POS_System.FrmAddUpdatePerson;

namespace POS_System.Product
{
    public partial class clsProductInfo : UserControl
    {
        int? _ProductID = null;
        clsProduct _Product;

        public int? ProductID
        {
            get { return _ProductID; }
        }

        public clsProduct SelectProductInfo
        {
            get { return _Product; }
        }

        public clsProductInfo()
        {
            InitializeComponent();
        }

        private void _LoadImageProduct()
        {
            string ImagePath = _Product.ImageProduct;
            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbImageProduct.ImageLocation = _Product.ImageProduct;
                else
                    MessageBox.Show($"Couldn't find this imagePath {ImagePath}", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void _FillProductInfo()
        {
            lblProductID.Text = _Product.ProductID.ToString();
            lblProductName.Text = _Product.ProductName;
            lblQuantityStock.Text = _Product.QuantityInStock.ToString();
            lblPrice.Text = _Product.Price.ToString();
            txtDescription.Text = _Product.Descriptions;
            _LoadImageProduct();
        }

        public void ResetProductInfo()
        {
            lblProductID.Text = "[???]";
            lblProductName.Text = "[???]";
            lblQuantityStock.Text = "[???]";
            lblPrice.Text = "[???]";
            txtDescription.Text = "";
        }

        public void LoadDataByProductID(int? ProductID)
        {
            _ProductID = ProductID;
            if (! _ProductID.HasValue)
            {
                MessageBox.Show($"Error: Product ID = Null", "Nullable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ProductID = ProductID;
            _Product = clsProduct.Find(_ProductID.Value);

            if (_Product == null)
            {
                ResetProductInfo();
                MessageBox.Show("No Product with Product ID. = " + _ProductID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillProductInfo();
        }

        public void LoadDateByProductName(string ProductName)
        {
            _Product = clsProduct.Find(ProductName);

            if (_Product != null)
            {
                ResetProductInfo();
                MessageBox.Show("No Product with  Product Name. = " + ProductName.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillProductInfo();
        }

    }
}
