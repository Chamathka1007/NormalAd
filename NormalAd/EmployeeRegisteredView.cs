using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormalAd
{
    public partial class EmployeeRegisteredView : Form
    {
        private int SelectedEmployeeId = -1;
        private MySqlDataAdapter adapter;
        private DataTable employeeTable;
        private MySqlConnection conn;

        private string connStr = "server=localhost;database=ad;uid=root;pwd=2002;";
        public EmployeeRegisteredView()
        {
            InitializeComponent();
            conn = new MySqlConnection(connStr);
        }

        private void dataGridView1_CellClick(object sender, EventArgs e)
        {


        }

        private void LoadEmployees()
        {
            try
            {
                
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = @"
                        SELECT 
                          EmpID,
                          Name,
                          Email,
                          DOB,
                          Gender,
                          Address,
                          NIC,
                          Contact,
                          MartialStatus,
                          JobRole
                        FROM emp_registration";

                    adapter = new MySqlDataAdapter(query, conn);

                    // Create commands for updating, deleting, inserting

                    // UPDATE command
                    adapter.UpdateCommand = new MySqlCommand(@"
                        UPDATE emp_registration SET
                            Name=@Name,
                            Email=@Email,
                            DOB=@DOB,
                            Gender=@Gender,
                            Address=@Address,
                            NIC=@NIC,
                            Contact=@Contact,
                            MartialStatus=@MartialStatus,
                            JobRole=@JobRole
                        WHERE EmpID=@EmpID", conn);

                    adapter.UpdateCommand.Parameters.Add("@Name", MySqlDbType.VarChar, 100, "Name");
                    adapter.UpdateCommand.Parameters.Add("@Email", MySqlDbType.VarChar, 45, "Email");
                    adapter.UpdateCommand.Parameters.Add("@DOB", MySqlDbType.Date, 0, "DOB");
                    adapter.UpdateCommand.Parameters.Add("@Gender", MySqlDbType.VarChar, 7, "Gender");
                    adapter.UpdateCommand.Parameters.Add("@Address", MySqlDbType.VarChar, 200, "Address");
                    adapter.UpdateCommand.Parameters.Add("@NIC", MySqlDbType.VarChar, 12, "NIC");
                    adapter.UpdateCommand.Parameters.Add("@Contact", MySqlDbType.Int32, 0, "Contact");
                    adapter.UpdateCommand.Parameters.Add("@MartialStatus", MySqlDbType.VarChar, 45, "MartialStatus");
                    adapter.UpdateCommand.Parameters.Add("@JobRole", MySqlDbType.VarChar, 45, "JobRole");
                    adapter.UpdateCommand.Parameters.Add("@EmpID", MySqlDbType.Int32, 0, "EmpID").SourceVersion = DataRowVersion.Original;

                    // DELETE command
                    adapter.DeleteCommand = new MySqlCommand("DELETE FROM emp_registration WHERE EmpID=@EmpID", conn);
                    adapter.DeleteCommand.Parameters.Add("@EmpID", MySqlDbType.Int32, 0, "EmpID").SourceVersion = DataRowVersion.Original;

                    // INSERT command
                    adapter.InsertCommand = new MySqlCommand(@"
                        INSERT INTO emp_registration
                        (Name, Email, DOB, Gender, Address, NIC, Contact, MartialStatus, JobRole)
                        VALUES (@Name, @Email, @DOB, @Gender, @Address, @NIC, @Contact, @MartialStatus, @JobRole)", conn);

                    adapter.InsertCommand.Parameters.Add("@Name", MySqlDbType.VarChar, 100, "Name");
                    adapter.InsertCommand.Parameters.Add("@Email", MySqlDbType.VarChar, 45, "Email");
                    adapter.InsertCommand.Parameters.Add("@DOB", MySqlDbType.Date, 0, "DOB");
                    adapter.InsertCommand.Parameters.Add("@Gender", MySqlDbType.VarChar, 7, "Gender");
                    adapter.InsertCommand.Parameters.Add("@Address", MySqlDbType.VarChar, 200, "Address");
                    adapter.InsertCommand.Parameters.Add("@NIC", MySqlDbType.VarChar, 12, "NIC");
                    adapter.InsertCommand.Parameters.Add("@Contact", MySqlDbType.Int32, 0, "Contact");
                    adapter.InsertCommand.Parameters.Add("@MartialStatus", MySqlDbType.VarChar, 45, "MartialStatus");
                    adapter.InsertCommand.Parameters.Add("@JobRole", MySqlDbType.VarChar, 45, "JobRole");

                    employeeTable = new DataTable();
                    adapter.Fill(employeeTable);

                    dataGridView1.DataSource = employeeTable;
                    dataGridView1.Columns["EmpID"].ReadOnly = true; // ID should not be edited
                }
            
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void EmployeeRegisteredView_Load(object sender, EventArgs e)
        {
            LoadEmployees();
        }



        private void btnDl_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an employee row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure to delete the selected employee?",
                                                "Confirm Delete",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            dataGridView1.Rows.Remove(row);
                        }
                    }

                    // Update the database after removing rows from DataTable
                    adapter.Update(employeeTable);

                    MessageBox.Show("Selected employee(s) deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting employee(s): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                SelectedEmployeeId = Convert.ToInt32(row.Cells["EmpID"].Value);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Commit any edits in the DataGridView
                if (dataGridView1.IsCurrentCellInEditMode)
                    dataGridView1.EndEdit();

                // Update the database with changes in DataTable
                adapter.Update(employeeTable);
                MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployees(); // Reload to reflect updated data
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Dispose();
            }


        }
    }
}
    

