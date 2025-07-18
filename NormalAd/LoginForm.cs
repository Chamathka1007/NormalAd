using MySql.Data.MySqlClient;
using NormalAd;
using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace ADProject
{
    public partial class LoginForm : Form
    {
        private readonly string connectionString = "Server=localhost;Database=ad;Uid=root;Pwd=2002;";

        public LoginForm()
        {
            InitializeComponent();
            cmdRole.SelectedIndex = 0; // Set default role to "SELECT"
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPass.Text.Trim();
            string role = cmdRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || role == "SELECT")
            {
                MessageBox.Show("Please enter Email, Password, and select a Role.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = role.Equals("Customer", StringComparison.OrdinalIgnoreCase)
                        ? "SELECT ID, Full_Name FROM customer_registration WHERE Email=@Email AND Password=@Password"
                        : role.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                        ? "SELECT ID, Email FROM login WHERE Email=@Email AND Password=@Password AND Role='Admin'"
                        : null;

                    if (sql == null)
                    {
                        MessageBox.Show("Invalid role selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (role.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                                {
                                    int customerId = Convert.ToInt32(reader["ID"]);
                                    string name = reader["Full_Name"].ToString();
                                    MessageBox.Show($"Welcome {name}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CDashboards customerDashboard = new CDashboards(customerId, email);
                                    customerDashboard.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Welcome Admin!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    AdminDashboard adminForm = new AdminDashboard();
                                    adminForm.Show();
                                    this.Hide();
                                }
                                return;
                            }
                        }
                    }

                    MessageBox.Show("Invalid credentials.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            cmdRole.SelectedIndex = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmail.Clear();
            txtPass.Clear();
            cmdRole.SelectedIndex = 0;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Enter your email in the Email field first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string role = GetUserRole(email);
            if (role == null)
            {
                MessageBox.Show("Email not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string tempPassword = GenerateTemporaryPassword();
                UpdatePassword(email, role, tempPassword);
                SendResetEmail(email, tempPassword);
                MessageBox.Show("A temporary password has been sent to your email. Please use it to log in and change your password.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send reset email: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetUserRole(string email)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"SELECT 'Customer' AS Role FROM customer_registration WHERE Email=@Email
                                   UNION
                                   SELECT 'Admin' AS Role FROM login WHERE Email=@Email";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        var result = cmd.ExecuteScalar();
                        return result?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        private string GenerateTemporaryPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            char[] password = new char[8];
            for (int i = 0; i < 8; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            return new string(password);
        }

        private void UpdatePassword(string email, string role, string newPassword)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string table = role.Equals("Customer", StringComparison.OrdinalIgnoreCase) ? "customer_registration" : "login";
                    string sql = $"UPDATE {table} SET Password = @Password WHERE Email = @Email";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Password", newPassword);
                        cmd.Parameters.AddWithValue("@Email", email);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("Failed to update password.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating password: " + ex.Message);
                }
            }
        }

        private void SendResetEmail(string toEmail, string tempPassword)
        {
            string fromEmail = "your_email@gmail.com"; // Replace with your Gmail address
            string fromPassword = "your_app_password"; // Replace with your Gmail App Password

            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;

            string subject = "Password Reset - EShift";
            string body = $@"
                Hello,

                We received a request to reset your password. Your temporary password is:

                {tempPassword}

                Please use this password to log in and change it immediately in your account settings.

                If you did not request this, contact our support team.

                - EShift Support Team";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;

            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
            smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

        private void linkLabelForm_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomerReg userType = new CustomerReg();
            userType.Show();
            this.Hide();
        }

        private int GetCustomerIdByEmail(string email)
        {
            int customerId = -1;

            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID FROM customer_registration WHERE Email = @Email LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            customerId = Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving customer ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return customerId;
        }

       
    }
}