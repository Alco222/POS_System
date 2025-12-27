using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSDataAccessLayer;

namespace POSBusinessLayer
{
    public class clsProduct
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        
        public static string Message;
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public string Descriptions { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public decimal TaxPercent { get; set; }
        public string ImageProduct { get; set; }
        public int? CreatedByUserID { get; set; }

        public string StockStatus
        {
            get
            {
                if (QuantityInStock > 10)
                    return "In Stock";
                else if (QuantityInStock > 0)
                    return "Low Stock";
                else
                    return "Out of Stock";
            }
        }
      
        public clsProduct()
        {
            this.ProductID = null;
            this.ProductName = "";
            this.Descriptions = "";
            this.Price = 0;
            this.QuantityInStock = 0;
            this.TaxPercent = 0;
            this.ImageProduct = "";
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        public clsProduct(int? ProductID, string ProductName, string Descriptions, decimal Price,
            int QuantityInStock,decimal TaxPercent, string ImageProduct, int? CreatedByUserID)
        {
            this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.Descriptions = Descriptions;
            this.Price = Price;
            this.QuantityInStock = QuantityInStock;
            this.TaxPercent = TaxPercent;
            this.ImageProduct = ImageProduct;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewProduct()
        {
            this.ProductID = clsProductData.AddNewProduct(this.ProductName, this.Descriptions,
                                                         this.Price, this.QuantityInStock,this.TaxPercent,
                                                         this.ImageProduct, this.CreatedByUserID);
            return (this.ProductID.HasValue);
        }

        private bool _UpdateProduct()
        {
            if (!this.ProductID.HasValue)
            {
                Message = "ProductID Is Null Value";
                return false;
            }

            return clsProductData.UpdateProduct(this.ProductID.Value, this.ProductName, this.Descriptions,
                                               this.Price, this.QuantityInStock,this.TaxPercent, this.ImageProduct,
                                               this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewProduct())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateProduct();
            }

            return false;
        }

        public static bool DeleteProduct(int? ProductID)
        {
            if (!ProductID.HasValue)
            {
                Message = "ProductID Is Null Value";
                return false;
            }
            return clsProductData.DeleteProduct(ProductID.Value);
        }

        public static DataTable GetAllProducts()
        {
            return clsProductData.GetAllProducts();
        }

        public static DataTable GetAllProducts2(int currentPage, int PAGE_SIZE)
        {
            return clsProductData.GetAllProducts2(currentPage, PAGE_SIZE);
        }


        public static clsProduct Find(int? ProductID)
        {
            if (!ProductID.HasValue)
                return null;

            string ProductName = "", Descriptions = "", ImageProduct = "";
            decimal Price = 0, TaxPercent =0;
            int QuantityInStock = 0, CreatedByUserID = -1;

            if (clsProductData.GetProductByProductID(ProductID.Value, ref ProductName, ref Descriptions,
                                                    ref Price, ref QuantityInStock,ref TaxPercent, ref ImageProduct,
                                                    ref CreatedByUserID))
                return new clsProduct(ProductID, ProductName, Descriptions, Price, QuantityInStock, TaxPercent,
                                     ImageProduct, CreatedByUserID);
            else
                return null;
        }

        public static clsProduct Find(string ProductName)
        {
            int ProductID = -1;
            string Descriptions = "", ImageProduct = "";
            decimal Price = 0, TaxPercent = 0;
            int QuantityInStock = 0, CreatedByUserID = -1;

            if (clsProductData.GetProductByProductName(ProductName, ref ProductID, ref Descriptions,
                                                      ref Price, ref QuantityInStock,ref TaxPercent, ref ImageProduct,
                                                      ref CreatedByUserID))
                return new clsProduct(ProductID, ProductName, Descriptions, Price, QuantityInStock, TaxPercent,
                                     ImageProduct, CreatedByUserID);
            else
                return null;
        }

        public static bool IsProductExists(string ProductName)
        {
            return clsProductData.IsProductExists(ProductName);
        }

        public  bool UpdateQuantity(int? ProductID,int NewQuantity)
        {
            return clsProductData.UpdateProductQuantity(ProductID, NewQuantity);
        }
    }
}
