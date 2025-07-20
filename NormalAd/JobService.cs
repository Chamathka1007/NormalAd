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
    public partial class JobService : Form
    {
        private readonly int _customerId;
        public JobService(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;

            lblID.Text = _customerId.ToString();
            dtpDate.Value = DateTime.Today;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            //  Pickup Location
            if (string.IsNullOrWhiteSpace(txtPL.Text))
            {
                MessageBox.Show("Please enter the Pickup Location.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Delivery Location
            if (string.IsNullOrWhiteSpace(txtDL.Text))
            {
                MessageBox.Show("Please enter the Delivery Location.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Item Type
            if (string.IsNullOrWhiteSpace(txtItemType.Text))
            {
                MessageBox.Show("Please enter the Item Type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Quantity (must be > 0)
            int quantity = (int)numericUpDownQuantity.Value;
            if (quantity <= 0)
            {
                MessageBox.Show("Please enter a valid Quantity greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Special Note (can be blank)
            string specialNote = txtNote.Text.Trim();

            using (var conn = new MySqlConnection("server=localhost;database=ad;uid=root;pwd=2002;"))
            {
                try
                {
                    conn.Open();

                    string query = @"
                INSERT INTO service_request
                (CustomerID, PickupLocation, DeliveryLocation, Date, SpecialNote, Item_Type, Quantity)
                VALUES
                (@CustomerID, @PickupLocation, @DeliveryLocation, @Date, @SpecialNote, @Item_Type, @Quantity)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", _customerId);
                        cmd.Parameters.AddWithValue("@PickupLocation", txtPL.Text.Trim());
                        cmd.Parameters.AddWithValue("@DeliveryLocation", txtDL.Text.Trim());
                        cmd.Parameters.AddWithValue("@Date", dtpDate.Value.Date);
                        cmd.Parameters.AddWithValue("@SpecialNote", string.IsNullOrEmpty(specialNote) ? DBNull.Value : (object)specialNote);
                        cmd.Parameters.AddWithValue("@Item_Type", txtItemType.Text.Trim());
                        cmd.Parameters.AddWithValue("@Quantity", quantity);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Service request submitted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadServiceRequests();   // Refresh the grid to show the new request
                            btnCl_Click(sender, e);  // Clear input fields
                        }
                        else
                        {
                            MessageBox.Show("Failed to submit service request.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving service request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }






        private void btnCl_Click(object sender, EventArgs e)
        {
            txtPL.Clear();
            txtDL.Clear();
            txtNote.Clear();
            txtItemType.Clear();
            numericUpDownQuantity.Value = numericUpDownQuantity.Minimum;
            dtpDate.Value = DateTime.Today;

        }

        private void JobService_Load(object sender, EventArgs e)
        {
            lblID.Text = _customerId.ToString();
            dtpDate.Value = DateTime.Today;
            dtpDate.MinDate = DateTime.Today; // Ensure the date is not in the past
            LoadServiceRequests();
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadServiceRequests()
        {
            using (var conn = new MySqlConnection("server=localhost;database=ad;uid=root;pwd=2002;"))
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT RID, PickupLocation, DeliveryLocation, Date, SpecialNote, Item_Type, Quantity
                        FROM service_request
                        WHERE CustomerID = @CustomerID";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", _customerId);

                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading service requests: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}


       
        
    