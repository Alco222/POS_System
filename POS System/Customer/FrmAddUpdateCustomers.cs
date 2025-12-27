using Guna.UI2.WinForms;
using HandlerErrors;
using POSBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System
{
    public partial class FrmAddUpdateCustomers : Form
    {
        //public delegate void DelegateDataBackEventHandler(object sender, int CustomerID);
        //public DelegateDataBackEventHandler DataBack;

        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;

        int? _CustomerID =null;
        clsCustomer _Customer;

        public FrmAddUpdateCustomers()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public FrmAddUpdateCustomers(int? CustomerID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _CustomerID = CustomerID;
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Customer";
                _Customer = new clsCustomer();
                tpCustomerInfo.Enabled = false;
                ctrCardPersonWithFilter1.FelterFoucs();
            }
            else
            {
                lblMode.Text = "Update Customer";
                tpCustomerInfo.Enabled = true;
                GbtnSave.Enabled = true;
            }

            lblRegesterDate.Text = DateTime.Now.ToString();
            txtTaxNumber.Text = "";
            txtTaxNumber.Text = "";
            lblCreatedbyUserName.Text = "[???]";
            tsIsActive.Checked = true;
        }

        private void _LoadCustomerData()
        {
            if (!_CustomerID.HasValue)
            {
                MessageBox.Show($"Error: Customer ID = Null", "Nullable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Customer = clsCustomer.Find(_CustomerID.Value);
            ctrCardPersonWithFilter1.FelterEnbled = false;
            if (_Customer == null)
            {
                MessageBox.Show($"Not Found Customer With ID = {_CustomerID}","Not Alowed",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblMode.Text = "Update Customer";
            ctrCardPersonWithFilter1.LoadPersonData(_Customer.PersonID);
            _CustomerID = _Customer.CustomerID;
            lblCustomerID.Text = _CustomerID?.ToString();
            lblRegesterDate.Text = _Customer.RegisterDate.ToString();
            txtTaxNumber.Text = _Customer.TaxNumber;
            //ndLoyaltyPoints.Text = _Customer.LoyaltyPoints.ToString();
            lblCreatedbyUserName.Text = "";

        }

        private void FrmAddUpdateCustomers_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if( _Mode == enMode.Update)
              _LoadCustomerData();
        }

        private void GbtnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                GbtnSave.Enabled = true;
                tpCustomerInfo.Enabled = true;
                tcCustomerInfo.SelectedTab = tcCustomerInfo.TabPages["tpCustomerInfo"];
                return;
            }

            //incase of add new mode.
            if (ctrCardPersonWithFilter1.PersonID != null)
            {
                if (clsCustomer.IsCustomerExist(ctrCardPersonWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a Customer, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrCardPersonWithFilter1.FelterFoucs();
                }
                else
                {
                    GbtnSave.Enabled = true;
                    tpCustomerInfo.Enabled = true;
                    tcCustomerInfo.SelectedTab = tcCustomerInfo.TabPages["tpCustomerInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrCardPersonWithFilter1.FelterFoucs();
            }
        }

        private void GbtnSave_Click(object sender, EventArgs e)
        {
            Exception ex = new Exception();
            if (ctrCardPersonWithFilter1.PersonID != null)
            {
                _Customer.CustomerName = ctrCardPersonWithFilter1.SelectPersonInfo.FullName; 
                _Customer.RegisterDate = Convert.ToDateTime(lblRegesterDate.Text.Trim());
                _Customer.TaxNumber = txtTaxNumber.Text.Trim();
                _Customer.LoyaltyPoints = 0;
                _Customer.IsActive = tsIsActive.Checked;
                _Customer.PersonID = ctrCardPersonWithFilter1.PersonID;
                _Customer.CreatedByUserID = null;
            }

            if (_Customer.Save())
            {
                lblCustomerID.Text = _Customer.CustomerID.ToString();

                _Mode = enMode.Update;

                lblMode.Text = "Update Customer";

                MessageBox.Show("Data Saved Successfuly!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                LogException.logException(ex.Message, EventLogEntryType.Error);

                if (!_Customer.PersonID.HasValue)
                    MessageBox.Show($"Error: Data Is not Saved Successfully,because {clsCustomer.Message}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show($"Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void GbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GbtnClose2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ndLoyaltyPoints_Validating(object sender, CancelEventArgs e)
        {
            //if(string.IsNullOrEmpty(ndLoyaltyPoints.Text.Trim()))
            //{
            //    e.Cancel = true;
            //    errorProvider1.SetError(ndLoyaltyPoints,"Please this Textbox cann't be blank!");
            //    return;
            //}
            //else
            //{
            //    errorProvider1.SetError(ndLoyaltyPoints, "");
            //}
        }

        private void txtTaxNumber_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtTaxNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTaxNumber, "Please this Textbox cann't be blank!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTaxNumber, "");
            }

            if(txtTaxNumber.Text.Trim() != _Customer.TaxNumber && clsCustomer.IsCustomerExist(txtTaxNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTaxNumber,"This Tax Number is already exist, please enter another one!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTaxNumber, "");
            }
        }

    }
}
