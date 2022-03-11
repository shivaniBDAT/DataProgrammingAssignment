using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentManager;
namespace StudentManager
{
    public partial class frmViewStudentDetails : Form
    {
        public frmViewStudentDetails()
        {
            InitializeComponent();
        }

       

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormEntry formEntry = new FormEntry();
            formEntry.Show();
        }
    }
}
