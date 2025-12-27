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

namespace POS_System.Users
{
    public partial class FrmAddUpdateUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;

        int _PersonID;
        int _UserID;
        clsUsers _User;


        private bool _ValidateUserName(string UserName)
        {

            clsUsers User = clsUsers.FindByUserName(UserName);

            if (User != null && _Mode == enMode.AddNew)
                return true;
            else
                return false;

        }

        private void _ResetDefaulValue()
        {
            timer1.Interval = 1000;//1 second
            timer1.Tick += Timer1_Tick;
            timer1.Start();

            // If we are adding a new contact
            if (_Mode == enMode.AddNew)
            {
                _User = new clsUsers(); // Create a new empty contact object
                tpUserInfo.Enabled = false; // Disable the User Info tab until a person is selected
                return; //Exit

            }
            else
            { 
                tpUserInfo.Enabled = true; // Enable the User Info tab for editing
                GbtnSave.Enabled = true; // Enable the Save button
            }

            lblUserID.Text = "[??]";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfPass.Text = "";
            tsIsActive.Checked = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblRegesterDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }


        private void _LoadData()
        {

            ctrCardPersonWithFilter1.FelterEnbled = false;

            // If you are editing, retrieve the contact by ID
            _User = clsUsers.Find(_UserID);


            // If contact not found, show a message and close the form
            if (_User == null)
            {
                MessageBox.Show($"This form will be closed because no User with ID = {_UserID}");
                this.Close();
                return;
            }

            // Update labels and form fields with contact's data
          
            ctrCardPersonWithFilter1.LoadPersonData(_User.PersonID);
            _PersonID = _User.PersonID;
            lblUserID.Text = _UserID.ToString();
            lblRegesterDate.Text = _User.CreatedAt.ToString("dd/MM/yyyy");
            txtUserName.Text = _User.UserName;
            if (_Mode == enMode.Update)
            {
                btnChangePass.Visible = true;
                txtPassword.Visible = false;
                txtConfPass.Visible = false;
            }
            else
            {
                btnChangePass.Visible = false;
                txtPassword.Visible = true;
                txtConfPass.Visible = true;
                txtPassword.Text = _User.PasswordHash;
                txtConfPass.Text = _User.PasswordHash;
            }
            tsIsActive.Checked = _User.IsActive;
        }
       
        public FrmAddUpdateUser(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;

            if (_UserID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private void FrmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefaulValue();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void GbtnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                GbtnSave.Enabled = true;
                tpUserInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpUserInfo"];
                return;
            }

            //incase of add new mode.
            if (ctrCardPersonWithFilter1.PersonID != -1)
            {

                if (clsUsers.IsUserExists(ctrCardPersonWithFilter1.PersonID))
                {

                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrCardPersonWithFilter1.FelterFoucs();
                }

                else
                {
                    GbtnSave.Enabled = true;
                    tpUserInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpUserInfo"];
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
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            // Assign the form fields to the _Contact object's properties
            _User.UserName = txtUserName.Text;
           
            if(txtPassword.Visible == true)
               _User.PasswordHash = txtPassword.Text;

            _User.IsActive = tsIsActive.Checked;
            _User.PersonID = _PersonID;
            if(_Mode == enMode.AddNew)
               _User.CreatedAt = Convert.ToDateTime(lblRegesterDate.Text);
            else
                _User.UpdateAt = Convert.ToDateTime(lblRegesterDate.Text);

            // Save the contact to the database
            if (_User.Save())
                MessageBox.Show("Data Saved Successfully.", "ADD Or Update User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error: Data Is not Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // After saving, switch to Update Mode and update labels
            _Mode = enMode.Update;
            lblUserID.Text = _User.UserID.ToString();
            ctrCardPersonWithFilter1.FelterEnbled = false; // Disable the filter after saving if (!this.ValidateChildren())
           
                //Here we dont continue becuase the form is not valid
                //MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;

            
        }

        private void txtConfPass_TextChanged(object sender, EventArgs e)
        {
            if (txtConfPass.Text != txtPassword.Text)
            {
                errorProvider1.SetError(txtConfPass, "Passwor Confirmation does not matched Password!");
                txtConfPass.Focus();
            }
            else
            {
                errorProvider1.SetError(txtConfPass, "");
            }
            if (txtConfPass.Text != txtPassword.Text)
            {
                errorProvider1.SetError(txtConfPass, "Passwor Confirmation does not matched Password!");
                txtConfPass.Focus();
            }
            else
            {
                errorProvider1.SetError(txtConfPass, "");
            }
        }

        //private void TextBox_Validating(object sender, CancelEventArgs e)
        //{
        //    TextBox textBox = sender as TextBox;


        //    if (string.IsNullOrWhiteSpace(textBox.Text))
        //    {
        //        e.Cancel = true;
        //        errorProvider1.SetError(textBox, "This field cannot be empty.");
        //    }
        //    else
        //    {
        //        errorProvider1.SetError(textBox, "");
        //    }
        //}

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "User Name cannot be blank!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, "");

            }


            if (_ValidateUserName(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "User Name is used for andther User");

            }
            else
            {
                errorProvider1.SetError(txtUserName, "");
            }
        }

        private void GbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrCardPersonWithFilter1_DelegateEnvent_1(int PersonID)
        {
            _PersonID = PersonID;
        }

        private void FrmAddUpdateUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(timer1 != null)
            {
                timer1.Stop();
                timer1.Dispose();
            }
        }
    }
}
