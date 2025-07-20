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
    public partial class ProfileUpdationForm : Form
    {
        private readonly string connectionString = "server=localhost;database=ad;uid=root;pwd=2002;";
        private readonly string _email;
        private int _customerId;
        private int _employeeId;

        // Constructor to initialize the form with the customer's email
        public ProfileUpdationForm(string email)
        {
            InitializeComponent();
            _email = email;
            LoadCustomerDetailsByEmail(_email);
        }

        // Update the customer profile with the provided details

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                        UPDATE customer_registration SET
                            Full_Name=@FullName,
                            Email=@Email,
                            Address=@Address,
                            DOB=@DOB,
                            Gender=@Gender,
                            NIC=@NIC,
                            Contact_Number=@ContactNumber,
                            Martial_Status=@MartialStatus,
                            Password=@Password,
                            Confirm_Password=@ConfirmPassword
                        WHERE ID=@ID";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", txtAdd.Text.Trim());
                        cmd.Parameters.AddWithValue("@DOB", dtpDOB.Value.Date);
                        cmd.Parameters.AddWithValue("@Gender", rbMale.Checked ? "Male" : "Female");
                        cmd.Parameters.AddWithValue("@NIC", txtNIC.Text.Trim());
                        if (!int.TryParse(txtCon.Text.Trim(), out int contactNumber))
                        {
                            MessageBox.Show("Contact Number must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                        cmd.Parameters.AddWithValue("@MartialStatus", cmbMartial.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@Password", txtPass.Text.Trim());
                        cmd.Parameters.AddWithValue("@ConfirmPassword", txtCPass.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID", _customerId);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                            MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Failed to update profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load customer details from the database using the provided email

        private void LoadCustomerDetailsByEmail(string email)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM customer_registration WHERE Email=@Email";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _customerId = Convert.ToInt32(reader["ID"]);
                                txtName.Text = reader["Full_Name"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                txtAdd.Text = reader["Address"].ToString();
                                dtpDOB.Value = Convert.ToDateTime(reader["DOB"]);
                                txtNIC.Text = reader["NIC"].ToString();
                                txtCon.Text = reader["Contact_Number"].ToString();
                                cmbMartial.SelectedItem = reader["Martial_Status"].ToString();
                                txtPass.Text = reader["Password"].ToString();
                                txtCPass.Text = reader["Confirm_Password"].ToString();

                                string gender = reader["Gender"].ToString();
                                if (gender == "Male") rbMale.Checked = true;
                                else if (gender == "Female") rbFemale.Checked = true;
                                else { rbMale.Checked = false; rbFemale.Checked = false; }
                            }
                            else
                            {
                                MessageBox.Show("Customer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB Error while loading profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        // Delete the profile of the customer
        private void btnDl_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete your profile?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM customer_registration WHERE ID=@ID";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", _customerId);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Profile deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Validate the form inputs before updating
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtAdd.Text) ||
                string.IsNullOrWhiteSpace(txtNIC.Text) ||
                string.IsNullOrWhiteSpace(txtCon.Text) ||
                cmbMartial.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtPass.Text) ||
                string.IsNullOrWhiteSpace(txtCPass.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtPass.Text != txtCPass.Text)
            {
                MessageBox.Show("Password and Confirm Password do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ProfileUpdationForm_Load(object sender, EventArgs e)
        {

        }

        // Clear all input fields and reset the form
        private void btnCl_Click(object sender, EventArgs e)
        {
            txtAdd.Clear();
            txtCon.Clear();
            txtCPass.Clear();
            txtEmail.Clear();
            txtName.Clear();
            txtNIC.Clear();
            txtPass.Clear();
            rbMale.Checked = false;
            rbFemale.Checked = false;
            dtpDOB.Value = DateTime.Today;
            cmbMartial.Text = "Select Martial Status";
        }
    }
    
}
