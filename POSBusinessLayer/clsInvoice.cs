
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSDataAccessLayer;

namespace POSBusinessLayer
{
    public class clsInvoice
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public static string Message;

        public int? InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int? CustomerID { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalDiscount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public int? CreatedByUserID { get; set; }
        public DataTable InvoiceItems { get; set; }
        public clsInvoice()
        {
            this.InvoiceID = null;
            this.InvoiceDate = DateTime.Now;
            this.TotalAmount = 0;
            this.CustomerID = -1;
            this.SubTotal = 0;
            this.TotalTax = 0;
            this.TotalDiscount = 0;
            this.PaymentMethod = "";
            this.Status = "";
            this.CreatedByUserID = -1;
            this.InvoiceItems = new DataTable();

            Mode = enMode.AddNew;
        }

        public clsInvoice(int? InvoiceID, DateTime InvoiceDate, decimal TotalAmount, int? CustomerID,
                          decimal SubTotal, decimal TotalTax, decimal TotalDiscount,
                          string PaymentMethod, int? CreatedByUserID)
        {
            this.InvoiceID = InvoiceID;
            this.InvoiceDate = InvoiceDate;
            this.TotalAmount = TotalAmount;
            this.CustomerID = CustomerID;
            this.SubTotal = SubTotal;
            this.TotalTax = TotalTax;
            this.TotalDiscount = TotalDiscount;
            this.PaymentMethod = PaymentMethod;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewInvoice()
        {
            this.InvoiceID = clsInvoiceData.AddNewInvoice(this.InvoiceDate, this.TotalAmount,
                this.CustomerID, this.PaymentMethod, this.CreatedByUserID,
                 this.InvoiceItems,this.SubTotal, this.TotalTax, this.TotalDiscount,this.Status);

            return (this.InvoiceID.HasValue);
        }

        private bool _UpdateInvoice()
        {
            if (!this.InvoiceID.HasValue)
            {
                Message = "InvoiceID Is Null Value";
                return false;
            }

            return clsInvoiceData.UpdateInvoice(this.InvoiceID.Value, this.InvoiceDate, this.TotalAmount,
                this.CustomerID, this.SubTotal, this.TotalTax, this.TotalDiscount,
                this.PaymentMethod, this.CreatedByUserID,this.InvoiceItems, this.Status);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInvoice())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateInvoice();
            }
            return false;
        }

        public static bool DeleteInvoice(int? InvoiceID)
        {
            if (!InvoiceID.HasValue)
            {
                Message = "InvoiceID Is Null Value";
                return false;
            }            
            return clsInvoiceData.DeleteInvoice(InvoiceID.Value);
        }

        public static bool CanceledInvoice(int? InvoiceID)
        {
            if (!InvoiceID.HasValue)
            {
                Message = "InvoiceID Is Null Value";
                return false;
            }
            return clsInvoiceData.CanceledInvoice(InvoiceID.Value);
        }

        public static DataTable GetAllInvoices(int currentPage, int PAGE_SIZE)
        {
            return clsInvoiceData.GetAllInvoices(currentPage,PAGE_SIZE);
        }

        public static clsInvoice Find(int? InvoiceID)
        {
            if (!InvoiceID.HasValue)
                return null;

            DateTime InvoiceDate = DateTime.Now;
            decimal TotalAmount = 0, SubTotal = 0, TotalTax = 0, TotalDiscount = 0;
            int? CustomerID = -1;
            int?  CreatedByUserID = -1;
            string PaymentMethod = "";

            if (clsInvoiceData.GetInvoiceByInvoiceID(InvoiceID.Value, ref InvoiceDate, ref TotalAmount,
                ref CustomerID, ref SubTotal, ref TotalTax, ref TotalDiscount, ref PaymentMethod, ref CreatedByUserID))
            {
                return new clsInvoice(InvoiceID, InvoiceDate, TotalAmount, CustomerID, SubTotal,
                                      TotalTax, TotalDiscount, PaymentMethod, CreatedByUserID);
            }
            else
                return null;
        }

        public static DataTable GetInvoicesByCustomerID(int? CustomerID)
        {
            return clsInvoiceData.GetInvoicesByCustomerID(CustomerID);
        }

        public static DataTable GetInvoicesByDateRange(DateTime startDate, DateTime endDate)
        {
            return clsInvoiceData.GetInvoicesByDateRange(startDate, endDate);
        }
    }
}
