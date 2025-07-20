using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NormalAd
{
    public partial class vehicleRegistrationForm : Form
    {
        private readonly string connectionString = "server=localhost;database=ad;uid=root;pwd=2002;";

        public vehicleRegistrationForm()
        {
            InitializeComponent();
            cmbType.SelectedIndex = 0;  // Default selection
        }

        /// <summary>
        /// Clears all form fields to default.
        /// </summary>
        private void ClearForm()
        {
            txtNum.Clear();
            txtContact.Clear();
            txtNotes.Clear();
            cmbType.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles vehicle registration click.
        /// </summary>
        private void btnReg_Click(object sender, EventArgs e)
        {
            string vehicleNumber = txtNum.Text.Trim();
            string ownerContact = txtContact.Text.Trim();
            string vehicleType = cmbType.SelectedItem?.ToString();
            string note = txtNotes.Text.Trim();

            // Validation
            if (string.IsNullOrEmpty(vehicleNumber) || string.IsNullOrEmpty(vehicleType) ||
                string.IsNullOrEmpty(ownerContact))
            {
                MessageBox.Show("Please fill all mandatory fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"
                                 INSERT INTO vehicle_registration
                                     (Vehicle_Number, Vehicle_Type, OwnerContact, Note, Status) 
                                     VALUES (@VehicleNumber, @VehicleType, @OwnerContact, @Note, @Status)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VehicleNumber", vehicleNumber);
                        cmd.Parameters.AddWithValue("@VehicleType", vehicleType);
                        cmd.Parameters.AddWithValue("@OwnerContact", ownerContact);
                        cmd.Parameters.AddWithValue("@Note", note);
                        cmd.Parameters.AddWithValue("@Status", "Pending");


                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Vehicle registered successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Failed to register vehicle.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Clears the form when Clear button is clicked.
        /// </summary>
        private void btnCl_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void vehicleRegistrationForm_Load(object sender, EventArgs e)
        {
            // Optional: Load dropdowns dynamically if needed.
        }
    }
}
