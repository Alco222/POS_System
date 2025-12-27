using System;
using System.Windows.Forms;
using POSBusinessLayer;

namespace POS_System.Person.Controle
{
    public partial class ctrCardPersonWithFilter : UserControl
    {
        public delegate void DelegateEnventHandler(int PersonID);

        public event DelegateEnventHandler DelegateEnvent;

        private bool _ShowAddNew = true;

        public bool ShowAddNew
        {
            get { return _ShowAddNew; }
            set
            {
                _ShowAddNew = value;
                btnAddNew.Visible = _ShowAddNew;
            }
        }

        public int PersonID
        {
            get { return ctrPersonDetails1.PersonID.Value; }
        }

        private bool _FelterEnbled = true;

        public bool FelterEnbled
        {
            get { return _FelterEnbled; }
            set
            {
                _FelterEnbled = value;
                gbFilter.Enabled = _FelterEnbled;
            }
        }

        public clsPerson SelectPersonInfo
        {
            get { return ctrPersonDetails1.SelectPersonInfo; }
        }

        public ctrCardPersonWithFilter()
        {
            InitializeComponent();
        }

        public void LoadPersonData(int? PersonID)
        {
            cbFilter.SelectedIndex = 1;
            txtFilter.Text = PersonID?.ToString();
            _FindNow();
        }

        private void _FindNow()
        {
            switch (cbFilter.Text)
            {
                case "Person ID":
                    ctrPersonDetails1.LoadDataByPersonID(int.Parse(txtFilter.Text));
                    break;

                case "National No":
                    ctrPersonDetails1.LoadDateByNationalNo(txtFilter.Text);
                    break;
            }

            if (DelegateEnvent != null && FelterEnbled)
                // Raise the event with a parameter
                DelegateEnvent(ctrPersonDetails1.PersonID.Value);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            txtFilter.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _FindNow();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FrmAddUpdatePerson frmAddUpdatePerson = new FrmAddUpdatePerson();
            frmAddUpdatePerson.DataBack += DataBack;
            frmAddUpdatePerson.ShowDialog();
        }

        private void DataBack(object sender, int PersonID)
        {
            cbFilter.SelectedIndex = 1;
            txtFilter.Text = PersonID.ToString();
            ctrPersonDetails1.LoadDataByPersonID(PersonID);
        }

        private void ctrCardPersonWithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            txtFilter.Text = "";
        }

        public void FelterFoucs()
        {
            txtFilter.Focus();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)Keys.Enter)
               btnSearch.PerformClick();

            //this will allow only digits if person id is selected
            if (cbFilter.Text == "Person ID")
               e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFilter_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "Please this Textbox cann't be blank!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFilter, "");
            }
        }
    }
}
