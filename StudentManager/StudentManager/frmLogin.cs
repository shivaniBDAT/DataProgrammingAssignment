using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace StudentManager
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            

        }

        
        public  void btnLogin_Click(object sender, EventArgs e)
        {
            
            string user;
            string pass;
            user = txtUserName.Text;
            pass = txtPwd.Text;
            
                if (user == "admin" && pass == "admin")
                {
                    MessageBox.Show("Successful");
                    this.Hide();
                    FormEntry formEntry = new FormEntry();
                    formEntry.Show();


                }
                else
                {
                    MessageBox.Show("Invalid credentials");
                txtUserName.Clear();
                txtPwd.Clear();

                }
           
                
            
            
        }

        
    }
}
