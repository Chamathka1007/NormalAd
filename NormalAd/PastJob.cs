using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NormalAd
{
    public partial class PastJob : Form
    {
        private readonly int _customerId;

        public PastJob(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;
        }

        private void PastJob_Load(object sender, EventArgs e)
        {
            LoadPastJobs();
        }

        private void LoadPastJobs()
        {
            using (var conn = new MySqlConnection("server=localhost;database=ad;uid=root;pwd=2002;"))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT RID, PickupLocation, DeliveryLocation, Date, SpecialNote, Item_Type, Quantity, Status
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

                            // Update and Delete processes
                            if (!dataGridView1.Columns.Contains("Update"))
                            {
                                DataGridViewButtonColumn btnUpdate = new DataGridViewButtonColumn
                                {
                                    HeaderText = "Update",
                                    Name = "Update",
                                    Text = "Update",
                                    UseColumnTextForButtonValue = true
                                };
                                dataGridView1.Columns.Add(btnUpdate);
                            }

                            if (!dataGridView1.Columns.Contains("Delete"))
                            {
                                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn
                                {
                                    HeaderText = "Delete",
                                    Name = "Delete",
                                    Text = "Delete",
                                    UseColumnTextForButtonValue = true
                                };
                                dataGridView1.Columns.Add(btnDelete);
                            }

                            dataGridView1.CellClick -= dataGridView1_CellClick;
                            dataGridView1.CellClick += dataGridView1_CellClick;

                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading past jobs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            int rid = Convert.ToInt32(row.Cells["RID"].Value);
            string status = row.Cells["Status"].Value.ToString();

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Update")
            {
                if (status.Equals("Confirmed", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        "This request is already confirmed. Please contact E-Shift support: +94 77 123 4567",
                        "Not Editable",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Inline editing: just get the current cell values
                string pickup = row.Cells["PickupLocation"].Value?.ToString();
                string delivery = row.Cells["DeliveryLocation"].Value?.ToString();
                string specialNote = row.Cells["SpecialNote"].Value?.ToString();
                string itemType = row.Cells["Item_Type"].Value?.ToString();
                int quantity = int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int q) ? q : 0;

                using (var conn = new MySqlConnection("server=localhost;database=ad;uid=root;pwd=2002;"))
                {
                    try
                    {
                        conn.Open();
                        string updateQuery = @"
                            UPDATE service_request
                            SET PickupLocation = @PickupLocation,
                                DeliveryLocation = @DeliveryLocation,
                                SpecialNote = @SpecialNote,
                                Item_Type = @Item_Type,
                                Quantity = @Quantity
                            WHERE RID = @RID";

                        using (var cmd = new MySqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@PickupLocation", pickup);
                            cmd.Parameters.AddWithValue("@DeliveryLocation", delivery);
                            cmd.Parameters.AddWithValue("@SpecialNote", specialNote);
                            cmd.Parameters.AddWithValue("@Item_Type", itemType);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@RID", rid);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Request updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (status.Equals("Confirmed", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        "This request is already confirmed. Please contact E-Shift support: +94 77 123 4567",
                        "Not Deletable",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show("Are you sure you want to delete this request?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    using (var conn = new MySqlConnection("server=localhost;database=ad;uid=root;pwd=2002;"))
                    {
                        try
                        {
                            conn.Open();
                            string deleteQuery = "DELETE FROM service_request WHERE RID = @RID";

                            using (var cmd = new MySqlCommand(deleteQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@RID", rid);
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Request deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadPastJobs(); // refresh
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error deleting request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        
    }
}
