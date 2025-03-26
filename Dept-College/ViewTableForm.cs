using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Dept_College
{
    public partial class ViewTableForm : Form
    {
        private string connectionString = "server=localhost;port=3306;database=dept-college;user=root;password=root;";

        public ViewTableForm()
        {
            InitializeComponent();
            cmbSelectTable.Items.Add("College");
            cmbSelectTable.Items.Add("Department");
            cmbSelectTable.SelectedIndex = 0;
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            string selectedTable = cmbSelectTable.SelectedItem.ToString();
            LoadTableData(selectedTable);
        }

        private void LoadTableData(string tableName)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = tableName == "College" ?
                        "SELECT CollegeID AS 'ID', CollegeName AS 'Name', CollegeCode AS 'College Code', IsActive FROM College" :
                        "SELECT DepartmentID AS 'ID', DepartmentName AS 'Name', CollegeID AS 'College ID', DepartmentCode AS 'Department Code', IsActive FROM Department";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvTable.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}");
                }
            }
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedTable = cmbSelectTable.SelectedItem.ToString();
            string searchValue = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = selectedTable == "College" ?
                        "SELECT * FROM College WHERE CollegeName LIKE @Search OR CollegeCode LIKE @Search" :
                        "SELECT * FROM Department WHERE DepartmentName LIKE @Search OR DepartmentCode LIKE @Search";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Search", $"%{searchValue}%");

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvTable.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error searching data: {ex.Message}");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to edit.");
                return;
            }

            string selectedTable = cmbSelectTable.SelectedItem.ToString();
            int id = Convert.ToInt32(dgvTable.SelectedRows[0].Cells["ID"].Value);
            string newName = dgvTable.SelectedRows[0].Cells["Name"].Value.ToString();
            string newCode = dgvTable.SelectedRows[0].Cells[selectedTable == "College" ? "College Code" : "Department Code"].Value.ToString();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = selectedTable == "College" ?
                        "UPDATE College SET CollegeName = @Name, CollegeCode = @Code WHERE CollegeID = @Id" :
                        "UPDATE Department SET DepartmentName = @Name, DepartmentCode = @Code WHERE DepartmentID = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Name", newName);
                        cmd.Parameters.AddWithValue("@Code", newCode);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Record updated successfully!");
                    LoadTableData(selectedTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating record: {ex.Message}");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            string selectedTable = cmbSelectTable.SelectedItem.ToString();
            int id = Convert.ToInt32(dgvTable.SelectedRows[0].Cells["ID"].Value);

            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) return;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = selectedTable == "College" ?
                        "DELETE FROM College WHERE CollegeID = @Id" :
                        "DELETE FROM Department WHERE DepartmentID = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Record deleted successfully!");
                    LoadTableData(selectedTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting record: {ex.Message}");
                }
            }
        }
    }
}