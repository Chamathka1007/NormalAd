using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace NormalAd
{
    public partial class AdminProfile : Form
    {
        private readonly int _adminId;
        private readonly string connStr = "server=localhost;database=ad;uid=root;pwd=2002;";

        public AdminProfile(int adminId)
        {
            InitializeComponent();
            _adminId = adminId;
        }

        private void AdminProfile_Load(object sender, EventArgs e)
        {
            LoadAdminDetails();
        }

        /// <summary>
        /// Loads admin details (ID, Email, Password, CPassword) into fields
        /// </summary>
        private void LoadAdminDetails()
        {
            using (var conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT ID, Email, Password, CPassword 
                                     FROM login 
                                     WHERE ID = @ID";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", _adminId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblID.Text = reader["ID"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                txtPassword.Text = reader["Password"].ToString();
                                txtCPass.Text = reader["CPassword"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Admin details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading admin details: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Update admin details in DB
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Delete admin record from DB
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Clear all fields
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Handles the 1 event of the btnUpdate_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
         private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string cpassword = txtCPass.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(cpassword))
            {
                MessageBox.Show("Please fill all fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != cpassword)
            {
                MessageBox.Show("Password and Confirm Password do not match.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    string query = @"UPDATE login 
                                     SET Email = @Email, Password = @Password, CPassword = @CPassword
                                     WHERE ID = @ID";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@CPassword", cpassword);
                        cmd.Parameters.AddWithValue("@ID", _adminId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No changes were made.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating profile: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to delete this admin profile?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            using (var conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    string query = @"DELETE FROM login WHERE ID = @ID";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", _adminId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Profile deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); // Close form after delete
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting profile: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

      
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtCPass.Text = "";
           
        }

        private void txtCPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

