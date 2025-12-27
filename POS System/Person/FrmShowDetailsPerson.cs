using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System.Person
{
    public partial class FrmShowDetailsPerson : Form
    {
        public FrmShowDetailsPerson(int? PersonID)
        {
            InitializeComponent();
            ctrPersonDetails1.LoadDataByPersonID(PersonID);

        }

        public FrmShowDetailsPerson(string NationalNo)
        {
            InitializeComponent();
            ctrPersonDetails1.LoadDateByNationalNo(NationalNo);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
