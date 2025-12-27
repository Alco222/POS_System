using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POSBusinessLayer;

namespace POS_System.Customer.Controls
{
    public partial class ctrCustomerInfo : UserControl
    {
        int? _CustomerID = null;
        clsCustomer _Customer;

        public ctrCustomerInfo()
        {
            InitializeComponent();
        }

        public void LoadCustomerInfo(int? CustomerID)
        {
            _CustomerID = CustomerID;
            _Customer = clsCustomer.Find(_CustomerID.Value);

            if (!_CustomerID.HasValue || _Customer == null)
            {
                //ResetPersonInfo();
                MessageBox.Show("No Person with National No. = " + _CustomerID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //_FillPersonInfo();
            ctrPersonDetails1.LoadDataByPersonID(_Customer.PersonID);
            lblCustomerID.Text = _CustomerID?.ToString();
            lblRegesterDate.Text = _Customer.RegisterDate.ToString("dd/MM/yyyy");
            lblLogaltyPoint.Text = _Customer.LoyaltyPoints.ToString();
            lblTaxNumber.Text = _Customer.TaxNumber.ToString();
        }

    }
}
