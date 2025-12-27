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
using POS_System.Properties;
using POSBusinessLayer;

namespace POS_System.Person.Controle
{
    public partial class ctrPersonDetails : UserControl
    {
        int? _PersonID = null;
        clsPerson _Person;

        public int? PersonID
        {
            get { return _PersonID; }
        }
        public clsPerson SelectPersonInfo
        {
            get { return _Person; }
        }
        public ctrPersonDetails()
        {
            InitializeComponent();
        }

        private void _LoadImagePerson()
        {
            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbImagePerson.ImageLocation = _Person.ImagePath;
                else
                    MessageBox.Show($"Couldn't find this imagePath {ImagePath}", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (_Person.Gender == 0)
                {
                    pbImagePerson.Image = Resources.person_boy;
                    pbGender.Image = Resources.Man_32;
                }
                else
                {
                    pbImagePerson.Image = Resources.person_girl;
                    pbGender.Image = Resources.Woman_32;
                }
            } 

        }

        private void _FillPersonInfo()
        {
            lblPersonID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo;
            lblGender.Text = _Person.Gender == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblAdderss.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            _LoadImagePerson();
        }

        public void ResetPersonInfo()
        {
            lblPersonID.Text = "[????]";
            lblName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblAdderss.Text = "[????]";
            lblDateOfBirth.Text = "[??/??/????]";
            pbImagePerson.Image = Resources.person_boy;
            pbGender.Image = Resources.Man_32;
        }

        public void LoadDataByPersonID(int? PersonID)
        {
            _PersonID = PersonID;
            if (!_PersonID.HasValue)
            {
                MessageBox.Show($"Error: Person ID = Null", "Nullable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _PersonID = PersonID;
            _Person = clsPerson.Find(_PersonID.Value);

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with Person ID. = " + _PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
        }

        public void LoadDateByNationalNo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);

            if (_Person != null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with National No. = " + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
        }

        private void llEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmAddUpdatePerson frmAddUpdatePerson = new FrmAddUpdatePerson(_PersonID);
            frmAddUpdatePerson.DataBack += FrmAddUpdatePerson_DataBack;
            frmAddUpdatePerson.Show();
        }

        private void FrmAddUpdatePerson_DataBack(Object sender,int PersonID)
        {
            _PersonID =PersonID;
             if(_PersonID.HasValue)
                  LoadDataByPersonID(_PersonID);
        }
    }
}
