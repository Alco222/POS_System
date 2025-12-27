using DVLD.Classes;
using Guna.UI2.WinForms;
using HandlerErrors;
using POS_System.Class_Globale;
using POS_System.Properties;
using POSBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System
{
    public partial class FrmAddUpdatePerson : Form
    {
        public delegate void DelegateDataBackEventHandler(object sender, int Person);
        public event DelegateDataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 }
        public enum enGender { Male = 0, Female = 1 }

        private enMode _Mode;

        private int? _PersonID ;
        clsPerson _Person;


        public FrmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public FrmAddUpdatePerson(int? PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void _AssginementPropertyToTextBox()
        {
            txtFirstName.Tag = nameof(clsPerson.FirstName);
            txtLastName.Tag = nameof(clsPerson.LastName);
            txtAddress.Tag = nameof(clsPerson.Address);
            txtPhone.Tag = nameof(clsPerson.Phone);
        }

        private bool _HandlePersonImage()
        {
            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.

            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != PbImagePerson.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (PbImagePerson.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = PbImagePerson.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        PbImagePerson.ImageLocation = SourceImageFile;
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

        private void _ResetDefaultValue()
        {
            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblMode.Text = "Update Person";
            }

            if (rbMale.Checked)
                PbImagePerson.Image = Resources.person_boy;
            else
                PbImagePerson.Image = Resources.person_girl;

            llRemoveImage.Visible = (PbImagePerson.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            lblPersonID.Text = "[???]";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
        }

        private void _LoadPersonData()
        {
            if (!_PersonID.HasValue)
            {
                MessageBox.Show($"Error: Person ID = Null", "Nullable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Person = clsPerson.Find(_PersonID.Value);

            if (_Person == null)
            {
                MessageBox.Show($"No Person with ID= {_PersonID}!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogException.logException($"No Person with ID= {_PersonID}!", EventLogEntryType.Error);

                return;
            }
            lblPersonID.Text = _PersonID?.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            if (_Person.Gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.ImagePath != null)
                PbImagePerson.ImageLocation = _Person.ImagePath;
            else
                PbImagePerson.Image = Resources.person_boy;

        }

        private void FrmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _AssginementPropertyToTextBox();
            _ResetDefaultValue();

            if (_Mode == enMode.Update)
            {
                _LoadPersonData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandlePersonImage())
                return;

            _Person.FirstName = txtFirstName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.NationalNo = txtNationalNo.Text;
            if (rbMale.Checked)
                _Person.Gender = (byte)enGender.Male;
            else
                _Person.Gender = (byte)enGender.Female;

            _Person.Phone = txtPhone.Text;
            _Person.Email = txtEmail.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Address = txtAddress.Text;

            if (PbImagePerson.ImageLocation != null)
                _Person.ImagePath = PbImagePerson.ImageLocation;
            else
                _Person.ImagePath = "";
            if (_Person.Save())
            {
                if (_Person.PersonID.HasValue)
                {
                    lblMode.Text = "Update Person";
                    lblPersonID.Text = _Person.PersonID?.ToString();
                    MessageBox.Show("Data Saved Successfuly.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogException.logException("Data Saved Successfuly.", EventLogEntryType.Information);
                    DataBack?.Invoke(this, _Person.PersonID.Value);
                }
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException.logException(" Data Is not Saved Successfully.", EventLogEntryType.Warning);
            }
        }
       
        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                PbImagePerson.ImageLocation = selectedFilePath;
                llRemoveImage.Visible = true;
                // ...
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PbImagePerson.ImageLocation = null;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if(PbImagePerson.ImageLocation == null)
                PbImagePerson.Image = Resources.person_girl;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if(PbImagePerson.ImageLocation == null)
                PbImagePerson.Image = Resources.person_boy;
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            Guna2TextBox tb = sender as Guna2TextBox;
            if (tb == null || tb.Tag == null) return;

            string propertyName = tb.Tag.ToString();

            // تحديث الخاصية في الـ Model مباشرة
            typeof(clsPerson).GetProperty(propertyName).SetValue(_Person, tb.Text);

            // استدعاء clsValidation
            if (!clsValidatoin.ValidateProperty(_Person, propertyName, out string errorMessage))
            {
                e.Cancel = true;
                errorProvider1.SetError(tb, errorMessage);
            }
            else
            {
                errorProvider1.SetError(tb, string.Empty);
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            _Person.NationalNo = txtNationalNo.Text;
            if (!clsValidatoin.ValidateProperty(_Person, nameof(_Person.NationalNo), out string errorMessage))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, errorMessage);
            }
            else
            {
                if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.IsPersonExists(txtNationalNo.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");
                }
                else
                {
                    errorProvider1.SetError(txtNationalNo, "");
                }
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase it's empty.
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidatoin.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
