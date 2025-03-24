using Dept_College;
using System;
using System.Windows.Forms;

namespace Dept_College
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            // Confirm logout
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // to login form
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close(); // Close Dashboard
            }
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Home Page Clicked.");
        }

        private void lblAboutUs_Click(object sender, EventArgs e)
        {
            MessageBox.Show("About Us Clicked.");
        }

        private void lblContactUs_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Contact Us Clicked.");
        }

        private void lblCollegeRegistration_Click(object sender, EventArgs e)
        {
            // Open CollegeForm
            CollegeForm collegeForm = new CollegeForm();
            collegeForm.Show();
            this.Hide();
        }

        private void lblDepartmentRegistration_Click(object sender, EventArgs e)
        {
            // Open DepartmentForm
            DepartmentForm departmentForm = new DepartmentForm();
            departmentForm.Show();
            this.Hide();
        }

        private void lblViewTable_Click(object sender, EventArgs e)
        {
            ViewTableForm viewTable = new ViewTableForm();
            viewTable.Show();
        }


    }
}
