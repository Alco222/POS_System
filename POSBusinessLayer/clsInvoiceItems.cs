using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSDataAccessLayer;

namespace POSBusinessLayer
{
    public class clsInvoiceItems
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public static string Message;

        public int? InvoiceItemID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public int? ProductID { get; set; }
        public int? InvoiceID { get; set; }
        public decimal DiscountPercent { get; set; }     
        public decimal DiscountAmount { get; set; }
        public int? CreatedByUserID { get; set; }
   

        public clsInvoiceItems()
        {
            this.InvoiceItemID = null;
            this.Quantity = 0;
            this.UnitPrice = 0;
            this.LineTotal = 0;
            this.ProductID = -1;
            this.InvoiceID = -1;
            this.DiscountPercent = 0;
            this.DiscountAmount = 0;
            this.CreatedByUserID =null;
            

            Mode = enMode.AddNew;
        }

        public clsInvoiceItems(int? InvoiceItemID, int Quantity, decimal UnitPrice, decimal LineTotal,
                              int? ProductID, int InvoiceID, int? CreatedByUserID,decimal DiscountPercent, decimal DiscountAmount)
        {
            this.InvoiceItemID = InvoiceItemID;
            this.Quantity = Quantity;
            this.UnitPrice = UnitPrice;
            this.LineTotal = LineTotal;
            this.ProductID = ProductID;
            this.InvoiceID = InvoiceID;
            this.CreatedByUserID = CreatedByUserID;
            this.DiscountPercent = DiscountPercent;
            this.DiscountAmount = DiscountAmount;
            Mode = enMode.Update;
        }

       /* private bool _AddNewInvoiceItem()
        {
            this.InvoiceItemID = clsInvoiceItemsData.AddInvoiceItem(this.Quantity, this.UnitPrice,
                this.LineTotal, this.ProductID, this.InvoiceID, this.CreatedByUserID, this.DiscountPercent, this.DiscountAmount);

            return (this.InvoiceItemID.HasValue);
        }

        private bool _UpdateInvoiceItem()
        {
            if (!this.InvoiceItemID.HasValue)
            {
                Message = "InvoiceItemID Is Null Value";
                return false;
            }

            return clsInvoiceItemsData.UpdateInvoiceItem(this.InvoiceItemID.Value, this.Quantity, this.UnitPrice,
                this.LineTotal, this.ProductID, this.InvoiceID, this.CreatedByUserID, this.DiscountPercent, this.DiscountAmount);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInvoiceItem())
                    {
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateInvoiceItem();
            }
            return false;
        }

        public static bool DeleteInvoiceItem(int? InvoiceItemID)
        {
            if (!InvoiceItemID.HasValue)
            {
                Message = "InvoiceItemID Is Null Value";
                return false;
            }

            return clsInvoiceItemsData.DeleteInvoiceItem(InvoiceItemID.Value);
        }

        public static bool DeleteInvoiceItemsByInvoiceID(int InvoiceID)
        {
            return clsInvoiceItemsData.DeleteInvoiceItemsByInvoiceID(InvoiceID);
        }*/

        public static DataTable GetInvoiceItemsByInvoiceID(int? InvoiceID)
        {
            return clsInvoiceItemsData.GetInvoiceItemsByInvoiceID(InvoiceID);
        }

        public static clsInvoiceItems Find(int? InvoiceItemID)
        {
            if (!InvoiceItemID.HasValue)
                return null;

            int Quantity = 0, InvoiceID = -1;
            int? ProductID = -1, CreatedByUserID = -1;
            decimal UnitPrice = 0, LineTotal = 0,DiscountPercent = 0 ,DiscountAmount = 0;

            if (clsInvoiceItemsData.GetInvoiceItemByID(InvoiceItemID.Value, ref Quantity, ref UnitPrice,
                ref LineTotal, ref ProductID, ref InvoiceID, ref CreatedByUserID,ref DiscountPercent, ref DiscountAmount))
            {
                return new clsInvoiceItems(InvoiceItemID, Quantity, UnitPrice, LineTotal,
                    ProductID, InvoiceID, CreatedByUserID,DiscountPercent,DiscountAmount);
            }
            else
                return null;
        }

     /*   public static clsInvoiceItems SaveInvoiceItems(DateTime InvoiceDate, decimal TotalAmount, int? CustomerID, decimal SubTotale, decimal TotalTax, decimal TotalDiscount, string PaymantMethod, int Quantity, decimal UnitPrice, decimal LineTotal, int? ProductID, decimal DiscountAmount, int? CreatedByUserID)
        {
            clsInvoice Invoice = new clsInvoice();

            Invoice.InvoiceDate = InvoiceDate;
            Invoice.TotalAmount = TotalAmount;
            Invoice.CustomerID = CustomerID;
            Invoice.SubTotal = SubTotale;
            Invoice.TotalTax = TotalTax;
            Invoice.TotalDiscount = TotalDiscount;
            Invoice.PaymentMethod = PaymantMethod;
            Invoice.CreatedByUserID = CreatedByUserID;

            if (!Invoice.Save())
            {
                return null;
            }

            clsProduct Product = new clsProduct();

            Product.UpdateQuantity(ProductID, Quantity);


            clsInvoiceItems InvoiceItems = new clsInvoiceItems();
            InvoiceItems.InvoiceID = Invoice.InvoiceID;
            InvoiceItems.Quantity = Quantity;
            InvoiceItems.UnitPrice = UnitPrice;
            InvoiceItems.LineTotal = LineTotal;
            InvoiceItems.ProductID = ProductID;
            InvoiceItems.DiscountAmount = DiscountAmount;
            InvoiceItems.CreatedByUserID = CreatedByUserID;

            if (!InvoiceItems.Save())
            {
                return null;
            }
            return InvoiceItems;
        }
*/
    }
}
