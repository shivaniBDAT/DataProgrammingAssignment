using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace StudentManager
{
    public partial class FormEntry : Form
    {
        public FormEntry()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();

        
        private void button1_Click(object sender, EventArgs e)
        {

            string errorMsg = string.Empty;
            bool validContact;
            bool validEmail;
            bool validName;
            bool validId;
            bool validGender;
            bool validCourse;


            //validation for Gender
            if(txtGender.Text == "Female" || txtGender.Text =="female" || txtGender.Text=="Male" || txtGender.Text =="male")
            {
                validGender = true;
                
            }
            else
            {
                validGender = false;
                errorMsg = errorMsg + "\n" + "Please enter valid Gender";
            }
        
            //validation for student id
            if (txtStudentID.Text != null)
            {
                
                string rexpID = @"^[0-9]{9}$";
                validId = Regex.IsMatch(txtStudentID.Text, rexpID);
                if (validId == false)
                {
                    errorMsg = errorMsg + "\n" + "Please enter valid Student ID";
                }
            }
            else
            {
                validId = false;
                errorMsg = errorMsg + "\n" + "Please enter Student ID";

            }

            //validation for student name
            if (txtStudentName.Text != null)
            {
                string rexpName = @"^\w+( +\w+)*$";
                validName = Regex.IsMatch(txtStudentName.Text, rexpName);
                if (validName == false)
                {
                    errorMsg = errorMsg + "\n" + "Please enter valid Student Name";
                }
            }
            else
            {
                validName = false;
                errorMsg = errorMsg + "\n" + "Please enter Student Name";

            }


            //validation for contact number
            if (txtContactNumber.Text != null)
            {
                string rexpContact = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
                validContact = Regex.IsMatch(txtContactNumber.Text, rexpContact);
                if(validContact == false)
                {
                    errorMsg = errorMsg + "\n" + "Please enter valid Contact Number";
                }
            }
            else
            {
                validContact = false;
                errorMsg = errorMsg + "\n" + "Please enter Contact Number";

            }

            //validation for email
            if (txtEmail.Text != null)
            {
                var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                validEmail = Regex.IsMatch(txtEmail.Text, regex, RegexOptions.IgnoreCase);
                if (validEmail == false)
                {
                    errorMsg = errorMsg + "\n" + "Please enter Valid Email";
                }
                
            }
            else
            {
                validEmail = false;
                errorMsg = errorMsg + "\n" + "Please enter Email";
            }

            // validation for course
            if(txtCourse.Text != "BDAT" || txtCourse.Text==String.Empty )
            {
                validCourse = false;
                errorMsg = errorMsg + "\n" + "Please enter valid Course";
            }
            else
            {
                validCourse = true;
            }


            //checking if all the conditions are true
            if(validContact && validEmail && validName && validId && validCourse && validGender)
            {
                dt.Rows.Add(txtStudentName.Text, txtStudentID.Text, txtContactNumber.Text, txtGender.Text, txtEmail.Text, txtCourse.Text);
                //frmLogin form = new frmLogin();
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GeorgianBDATStudentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GeorgianStudentDB;User ID=DESKTOP-DMJ800G\Lenovo;Password=Admin";
                SqlConnection cnn = new SqlConnection(connectionString);
                cnn.Open();
                SqlCommand cmd = new SqlCommand("insert into dbo.[detailsStudent] values('" + txtStudentID.Text + "','" + txtStudentName.Text + "','" + txtContactNumber.Text + "','" + txtGender.Text + "','" + txtEmail.Text + "','" + txtCourse.Text + "')", cnn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();


                MessageBox.Show("Added Succesfully");
                cnn.Close();
            }
            else
            {
                MessageBox.Show(errorMsg);
            }
            
            
            
            //to clear values on screen
            txtStudentName.Clear();
            txtStudentID.Clear();
            txtContactNumber.Clear();
            txtGender.Clear();
            txtEmail.Clear();
            txtCourse.Clear();
           
            
        }

        private void FormEntry_Load(object sender, EventArgs e)
        {
            dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Student Name"), new DataColumn("Student ID"), new DataColumn("Contact Number"), new DataColumn("Gender"), new DataColumn("Email"), new DataColumn("Course") });
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmViewStudentDetails form = new frmViewStudentDetails();
            form.Show();
            


            string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GeorgianBDATStudentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("SELECT * from detailsStudent");
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    form.dataGridView1.DataSource = dt;
                    
                }
            }




        }

        //on clicking logout - we will exit the application
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }
    }
}