using Dept_College;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Dept_College
{
    public partial class DepartmentForm : Form
    {
        public DepartmentForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtDepartmentId.Text) ||
                string.IsNullOrWhiteSpace(txtCollegeId.Text) ||
                string.IsNullOrWhiteSpace(txtDepartmentName.Text) ||
                string.IsNullOrWhiteSpace(txtDepartmentCode.Text))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Database connection string
            string connectionString = "server=localhost;port=3306;database=dept-college;user=root;password=root;";
            string query = "INSERT INTO Department (DepartmentID, CollegeID, DepartmentName, DepartmentCode) " +
                           "VALUES (@DepartmentID, @CollegeID, @DepartmentName, @DepartmentCode)";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Manually inputted IDs
                        cmd.Parameters.AddWithValue("@DepartmentID", txtDepartmentId.Text);
                        cmd.Parameters.AddWithValue("@CollegeID", txtCollegeId.Text);
                        cmd.Parameters.AddWithValue("@DepartmentName", txtDepartmentName.Text);
                        cmd.Parameters.AddWithValue("@DepartmentCode", txtDepartmentCode.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Department registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Redirect to Dashboard
                            Dashboard dashboard = new Dashboard();
                            dashboard.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to register department.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Redirect to Dashboard
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }
    }
}