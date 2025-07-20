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
    public partial class EmployeeRegistration : Form
    {
        private readonly string connectionString = "server=localhost;database=ad;uid=root;pwd=2002;";
        public EmployeeRegistration()
        {
            InitializeComponent();
            cmbRole.SelectedIndex = 0;
            cmbMartial.SelectedIndex = 0;
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPass.Text.Trim();
            string CPassword = txtCPass.Text.Trim();
            DateTime dob = dtpDOB.Value.Date;
            string gender = rbMale.Checked ? "Male" : (rbFemale.Checked ? "Female" : "");
            string address = txtAdd.Text.Trim();
            string nic = txtNIC.Text.Trim();
            string contact = txtCon.Text.Trim();
            string martialStatus = cmbMartial.SelectedItem?.ToString();
            string jobRole = cmbRole.SelectedItem?.ToString();

            // Validation
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(CPassword) || string.IsNullOrEmpty(gender) ||
                string.IsNullOrEmpty(jobRole) || string.IsNullOrEmpty(nic))
            {
                MessageBox.Show("Please fill all mandatory fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != CPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Insert into database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO emp_registration 
                        (Name, Email, DOB, Gender, Address, NIC, Contact, MartialStatus, JobRole, Password, CPassword)
                        VALUES 
                        (@Name, @Email, @DOB, @Gender, @Address, @NIC, @Contact, @MartialStatus, @JobRole, @Password, @CPassword)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@DOB", dob);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@NIC", nic);
                        cmd.Parameters.AddWithValue("@Contact", contact);
                        cmd.Parameters.AddWithValue("@MartialStatus", martialStatus);
                        cmd.Parameters.AddWithValue("@JobRole", jobRole);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@CPassword", CPassword);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employee registered successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Failed to register employee.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ClearForm()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtPass.Clear();
            txtCPass.Clear();
            txtAdd.Clear();
            txtNIC.Clear();
            txtCon.Clear();
            cmbMartial.SelectedIndex = -1;
            cmbRole.SelectedIndex = -1;
            dtpDOB.Value = DateTime.Now;
            rbMale.Checked = false;
            rbFemale.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EmployeeRegistration_Load(object sender, EventArgs e)
        {

        }
    }
}
