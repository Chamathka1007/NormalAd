using ADProject;
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
    public partial class CustomerReg : Form
    {
        private readonly string connectionString = "server=localhost;database=ad;uid=root;pwd=2002;";
        public CustomerReg()
        {
            InitializeComponent();
            cmbMartial.SelectedIndex = 0;
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            string fullName = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string address = txtAdd.Text.Trim();
            DateTime dob = dtpDOB.Value.Date;
            string gender = GetSelectedGender();
            string nic = txtNIC.Text.Trim();
            int contactNumber = int.TryParse(txtCon.Text.Trim(), out var c) ? c : 0;
            string martialStatus = cmbMartial.SelectedItem?.ToString();
            string password = txtPass.Text.Trim();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO customer_registration
                        (Full_Name, Email, Address, DOB, Gender, NIC, Contact_Number, Martial_Status, Password, Confirm_Password)
                        VALUES
                        (@FullName, @Email, @Address, @DOB, @Gender, @NIC, @ContactNumber, @MartialStatus, @Password, @ConfirmPassword)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", fullName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@DOB", dob);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@NIC", nic);
                        cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                        cmd.Parameters.AddWithValue("@MartialStatus", martialStatus);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@ConfirmPassword", password);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Customer registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Failed to register customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSelectedGender()
        {
            if (rbMale.Checked) return "Male";
            if (rbFemale.Checked) return "Female";
            return "Other";
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtAdd.Text) ||
                string.IsNullOrWhiteSpace(txtNIC.Text) ||
                string.IsNullOrWhiteSpace(txtCon.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Text) ||
                string.IsNullOrWhiteSpace(txtCPass.Text) ||
                cmbMartial.SelectedIndex == -1 ||
                (!rbMale.Checked && !rbFemale.Checked))
            {
                MessageBox.Show("Please fill all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtPass.Text != txtCPass.Text)
            {
                MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void CustomerReg_Load(object sender, EventArgs e)
        {
            dtpDOB.Value = DateTime.Today;
            cmbMartial.SelectedIndex = 0; ;
        }
        private void ClearForm()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtAdd.Clear();
            dtpDOB.Value = DateTime.Today;
            rbMale.Checked = false;
            rbFemale.Checked = false;
            txtNIC.Clear();
            txtCon.Clear();
            cmbMartial.SelectedIndex = -1;
            txtPass.Clear();
            txtCPass.Clear();
        }

        private void btnCl_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void cmbMartial_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
