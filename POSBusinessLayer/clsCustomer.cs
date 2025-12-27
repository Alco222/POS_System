using HandlerErrors;
using POSDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace POSBusinessLayer
{
    public class clsCustomer
    {
        public enum enMode {AddNew = 0 , Update = 1};
        public enMode Mode = enMode.AddNew;

        public static string Message;
        public int? CustomerID {  get; set; }
        public DateTime RegisterDate { get; set; }
        public int? CreatedByUserID { get; set; }
        public int LoyaltyPoints { get; set; }
        public string TaxNumber { get; set; }
        public bool IsActive { get; set; }
        public int? PersonID { get; set; }

        public string CustomerName { get; set; }
        
        public clsPerson PersonInfo;
        public clsCustomer()
        {
            this.CustomerID = null;
            this.CustomerName = "";
            this.RegisterDate = DateTime.Now;
            this.CreatedByUserID = null;
            this.TaxNumber = "";
            this.LoyaltyPoints = 0;
            this.PersonID = null;
            this.IsActive = false;
            
            Mode = enMode.AddNew;
        }

        public clsCustomer(int? CustomerID,string CustomerName, DateTime RegisterDate, int? CreatedByUserID, string TaxNumber,int LoyaltyPoints, bool IsActive, int? PersonID)
        {
            
            this.CustomerID = CustomerID;
            this.RegisterDate = RegisterDate;
            this.CreatedByUserID = CreatedByUserID;
            this.TaxNumber = TaxNumber;
            this.LoyaltyPoints = LoyaltyPoints;
            this.IsActive = IsActive;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.CustomerName = CustomerName;
            Mode = enMode.Update;
        }

        private bool _AddNewCustomer()
        {
            if (this.PersonID == null)
            {
                Message = "PersonID Is Null Value";
                LogException.logException("Validation failed: PersonID is null. This should not happen in production.", EventLogEntryType.Error);

                return false;
            }

            this.CustomerID = clsCustormerData.AddNewCustomer(this.CustomerName,this.RegisterDate, this.CreatedByUserID,this.TaxNumber,this.LoyaltyPoints,this.PersonID,this.IsActive);
            return (this.CustomerID != null);
        }

        private bool _UpdateCustomer()
        {
            if (this.CustomerID == null || this.PersonID == null)
            {
                Message = "PersonID Or CustomerID Is Null Value";
                return false;
            }
            return clsCustormerData.UpdateCustomer(this.CustomerID,this.CustomerName, this.RegisterDate, this.CreatedByUserID, this.TaxNumber, this.LoyaltyPoints, this.PersonID,this.IsActive);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCustomer())
                    { 
                       Mode = enMode.Update;
                       return true;
                    } 
                    else
                    {
                        return false;
                    }
                    
                case enMode.Update:
                    return _UpdateCustomer();   
            }

            return false;
        }

        public static bool DeleteCustomer(int? CustomerID)
        {
            if (CustomerID == null)
            {
                Message = "CustomerID Is Null Value";
                return false;
            }
              
            return clsCustormerData.DeleteCustomer(CustomerID);
        }

        public static DataTable GetAllCustomers()
        {
            return clsCustormerData.GetAllCustomer();
        }

        public static DataTable GetAllCustomers2(int currentPage, int PAGE_SIZE)
        {
            return clsCustormerData.GetAllCustomer2(currentPage,PAGE_SIZE);
        }

        public static clsCustomer Find(int? CustomerID)
        {
            if (!CustomerID.HasValue)
                return null;
            string CustomerName = "";
            DateTime RegisterDate = DateTime.Now;
            int? CreatedByUserID = null;
            string TaxNumber = "";
            int LoyaltyPoints=0;
            bool IsActive = false;
            int? PersonID= null;

            if (clsCustormerData.GetCustomerByID(CustomerID,ref CustomerName, ref RegisterDate, ref CreatedByUserID,ref TaxNumber,ref LoyaltyPoints,ref PersonID ,ref IsActive ))

                return new clsCustomer(CustomerID, CustomerName, RegisterDate, CreatedByUserID, TaxNumber,  LoyaltyPoints,IsActive,PersonID);
           else
                return null;
        }

        public static clsCustomer Find(string CustomerName)
        {
            DateTime RegisterDate = DateTime.Now;
            int? CreatedByUserID = null;
            string TaxNumber = "";
            int LoyaltyPoints = 0;
            bool IsActive = false;
            int? PersonID = null;
            int? CustomerID = null;
            if (clsCustormerData.GetCustomerByName(ref CustomerID, CustomerName, ref RegisterDate, ref CreatedByUserID, ref TaxNumber, ref LoyaltyPoints, ref PersonID, ref IsActive))

                return new clsCustomer(CustomerID,CustomerName, RegisterDate, CreatedByUserID, TaxNumber, LoyaltyPoints, IsActive, PersonID);
            else
                return null;
        }

        public static bool IsCustomerExist(int? PersonID)
        {
            if (!PersonID.HasValue)
                return false;

            return clsCustormerData.IsCustomerExist(PersonID);
        }

        public static bool IsCustomerExist(string TaxNumber)
        {
            return clsCustormerData.IsCustomerExist(TaxNumber);
        }
    }
}
