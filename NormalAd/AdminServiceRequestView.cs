using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
    public partial class AdminServiceRequestView : Form
    {
        private int selectedRequestId = -1;
        private DataTable serviceRequestsTable;
        string connStr = "server=localhost;database=ad;uid=root;pwd=2002;";

        public AdminServiceRequestView()
        {
            InitializeComponent();
            
        }

        private void AdminServiceRequestView_Load(object sender, EventArgs e)
        {
            LoadPendingRequests();
        }

        private void LoadPendingRequests()
        {
            using (var conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT 
                                        RID, CustomerID, Item_Type, Date, PickupLocation, 
                                        DeliveryLocation, Quantity, SpecialNote, Status
                                     FROM service_request
                                     WHERE Status = 'Pending'";

                    using (var adapter = new MySqlDataAdapter(query, conn))
                    {
                        serviceRequestsTable = new DataTable();
                        adapter.Fill(serviceRequestsTable);

                        dataGridView1.DataSource = serviceRequestsTable;

                        dataGridView1.Columns["RID"].HeaderText = "Request ID";
                        dataGridView1.Columns["RID"].ReadOnly = true;
                        dataGridView1.Columns["Status"].ReadOnly = true;

                        selectedRequestId = -1; // reset selection
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading service requests: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (row.Cells["RID"].Value != null)
                {
                    selectedRequestId = Convert.ToInt32(row.Cells["RID"].Value);
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (selectedRequestId == -1)
            {
                MessageBox.Show("Please select a service request.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hide AdminDashboard
            this.Hide();

            // Open AssigningForm
            AssigningForm assigningForm = new AssigningForm(selectedRequestId);
            assigningForm.StartPosition = FormStartPosition.CenterScreen;
            assigningForm.Show();

            // When AssigningForm closes, show AdminDashboard again
            assigningForm.FormClosed += (s, args) =>
            {
                this.Show();
                LoadPendingRequests(); // reload pending jobs
            };


        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            if (selectedRequestId == -1)
            {
                MessageBox.Show("Please select a service request.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UpdateRequestStatus("Declined");
        }

        private void UpdateRequestStatus(string newStatus)
        {
            using (var conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    string query = @"UPDATE service_request
                             SET Status = @Status
                             WHERE RID = @RID";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@RID", selectedRequestId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Request marked as {newStatus}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the request. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    selectedRequestId = -1;
                    LoadPendingRequests(); // refresh view
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating request: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}