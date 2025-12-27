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
using Guna.UI2.WinForms;
using POS_System.Class_Globale;
using POS_System.Person.Controle;
using POSBusinessLayer;

namespace POS_System.Product
{
    public partial class FrmAddUpdateProduct : Form
    {
        public static event Action<int> OneSendProduct;

        enum enMode {AddNew = 0, Update = 1 }
        enMode _Mode;
        int? _ProductID = null;
        clsProduct _Product;

        public FrmAddUpdateProduct()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public FrmAddUpdateProduct(int ProductID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _ProductID = ProductID;
        }

        private bool _ProcesingImage()
        {
            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.

            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image

            if (_Product.ImageProduct != pbImageProduct.ImageLocation)
            {
                if (_Product.ImageProduct != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Product.ImageProduct);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later
                    }

                }

                if (pbImageProduct.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbImageProduct.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbImageProduct.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void  _ResetDefaultValues()
        {
            if(_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Product";
                _Product = new clsProduct();
            }
            else
                lblMode.Text = "Update Product";
            
            lblProductID.Text = "[???]";
            txtProductName.Text = "";
            txtDescription.Text = "";
            ndPrice.Text = "0.00";
            ndQuantity.Text = "0";
            pbImageProduct.Image = Properties.Resources.new_arrival;
        }

        public void LoadDataProduct()
        {
            if (!_ProductID.HasValue)
            {
                MessageBox.Show($"Error: Product ID = Null", "Nullable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Product = clsProduct.Find(_ProductID.Value);
            if (_Product == null)
            {
                MessageBox.Show($"Not Found Product With ID = {_ProductID}", "Not Alowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblMode.Text = "Update Product";
           
            _ProductID = _Product.ProductID;
            lblProductID.Text = _Product?.ToString();
            txtProductName.Text = _Product.ProductName;
            txtDescription.Text = _Product.Descriptions;
            ndPrice.Text = _Product.Price .ToString();
            ndQuantity.Text = _Product.QuantityInStock.ToString();
            ndTax.Text = (_Product.TaxPercent*100).ToString();

            if(_Product.ImageProduct != "")
                pbImageProduct.ImageLocation = _Product.ImageProduct;
            else
                pbImageProduct.Image = Properties.Resources.new_arrival;
        }

        private void FrmAddUpdateProduct_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                LoadDataProduct();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if(!_ProcesingImage())
                return;

            _Product.ProductName = txtProductName.Text;
            _Product.Descriptions = txtDescription.Text;
            _Product.Price = ndPrice.Value;
            _Product.QuantityInStock = (int)ndQuantity.Value;
            _Product.TaxPercent = (decimal)ndTax.Value / 100;
            if(pbImageProduct.ImageLocation != null) 
                _Product.ImageProduct = pbImageProduct.ImageLocation;
            else
                _Product.ImageProduct = "";
            _Product.CreatedByUserID = null;
            //Save the product Add new or update.
            if(_Product.Save())
            {
                MessageBox.Show("Product Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);    
            }
            else
            {
                MessageBox.Show($"Error Saving Product: {clsProduct.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _Mode = enMode.Update;
            lblMode.Text = "Update Product";
            lblProductID.Text = _Product.ProductID.ToString();

            OneSendProduct?.Invoke((int)_Product.ProductID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            Guna2TextBox Temp = ((Guna2TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

        private void llSetImg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbImageProduct.ImageLocation = selectedFilePath;
                llRemoveImg.Visible = true;
                // ...
            }
        }

        private void llRemoveImg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImageProduct.ImageLocation = null;
        }
    }
}
