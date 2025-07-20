using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NormalAd
{
    public partial class ServiceStatus : Form
    {
        private readonly int _customerId;
        private readonly string connStr = "server=localhost;database=ad;uid=root;pwd=2002;";

        public ServiceStatus(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;
        }

        private void ServiceStatus_Load(object sender, EventArgs e)
        {
            LoadServiceStatuses();
        }

        /// <summary>
        /// Loads service requests for this customer and renders them as dynamic panels.
        /// </summary>
        private void LoadServiceStatuses()
        {
            this.Controls.Clear();  // Clear any old controls

            using (var conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT RID, Item_Type, Date, PickupLocation, DeliveryLocation, Quantity, Status
                        FROM service_request
                        WHERE CustomerID = @CustomerID";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", _customerId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            int yPos = 10;

                            while (reader.Read())
                            {
                                int rid = reader.GetInt32("RID");
                                string itemType = reader.GetString("Item_Type");
                                DateTime date = reader.GetDateTime("Date");
                                string pickup = reader.GetString("PickupLocation");
                                string delivery = reader.GetString("DeliveryLocation");
                                int quantity = reader.GetInt32("Quantity");
                                string status = reader.GetString("Status");

                                // 🌟 Panel for each request
                                Panel panel = new Panel
                                {
                                    BorderStyle = BorderStyle.FixedSingle,
                                    Size = new Size(this.ClientSize.Width - 40, 40),
                                    Location = new Point(10, yPos),
                                    BackColor = Color.WhiteSmoke
                                };

                                // Summary label
                                Label lblSummary = new Label
                                {
                                    Text = $"Item: {itemType}   Date: {date.ToShortDateString()}",
                                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                                    ForeColor = Color.Black,
                                    AutoSize = false,
                                    Size = new Size(panel.Width - 200, 30),
                                    Location = new Point(5, 5)
                                };

                                // Detailed view label (initially hidden)
                                Label lblDetails = new Label
                                {
                                    Text =
                                        $"Pickup Location: {pickup}\n" +
                                        $"Delivery Location: {delivery}\n" +
                                        $"Quantity: {quantity}\n" +
                                        $"Status: {status}",
                                    Font = new Font("Segoe UI", 9),
                                    ForeColor = Color.Gray,
                                    AutoSize = false,
                                    Size = new Size(panel.Width - 20, 70),
                                    Location = new Point(5, 40),
                                    Visible = false
                                };

                                // Status button (color-coded)
                                Button btnStatus = new Button
                                {
                                    Size = new Size(90, 30),
                                    Location = new Point(panel.Width - 195, 5),
                                    Text = status,
                                    Enabled = false,
                                    FlatStyle = FlatStyle.Flat,
                                    ForeColor = Color.Black,
                                    BackColor =
                                    status.Equals("Confirmed", StringComparison.OrdinalIgnoreCase) ? Color.LightGreen :
                                     status.Equals("Pending", StringComparison.OrdinalIgnoreCase) ? Color.LightSalmon :
                                     status.Equals("Completed", StringComparison.OrdinalIgnoreCase) ? Color.Gray :
                                     Color.LightGray
                                };

                                // View Progress button (only enabled if NOT completed)
                                Button btnViewProgress = new Button
                                {
                                    Size = new Size(90, 30),
                                    Location = new Point(panel.Width - 100, 5),
                                    Text = "View Progress",
                                    FlatStyle = FlatStyle.Flat,
                                    Font = new Font("Segoe UI", 8, FontStyle.Regular),
                                    Cursor = Cursors.Hand,
                                    Enabled = !status.Equals("Completed", StringComparison.OrdinalIgnoreCase),
                                    BackColor = !status.Equals("Completed", StringComparison.OrdinalIgnoreCase)
                                                ? Color.LightBlue
                                                : Color.LightGray
                                };

                                btnViewProgress.Click += (s, e) => ShowJobProgress(rid);

                                // Expand/Collapse arrow button
                                Button btnToggle = new Button
                                {
                                    Size = new Size(30, 30),
                                    Location = new Point(panel.Width - 230, 5),
                                    Text = "►",
                                    FlatStyle = FlatStyle.Flat,
                                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                                    ForeColor = Color.WhiteSmoke,
                                    BackColor = Color.Transparent
                                };

                                btnToggle.Click += (s, e) =>
                                {
                                    lblDetails.Visible = !lblDetails.Visible;
                                    panel.Height = lblDetails.Visible ? 115 : 40;
                                    btnToggle.Text = lblDetails.Visible ? "▼" : "►";
                                };

                                // Add all controls to panel
                                panel.Controls.Add(lblSummary);
                                panel.Controls.Add(lblDetails);
                                panel.Controls.Add(btnStatus);
                                panel.Controls.Add(btnViewProgress);
                                panel.Controls.Add(btnToggle);

                                // Add panel to form
                                this.Controls.Add(panel);

                                yPos += panel.Height + 10;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading service statuses: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Displays job progress for a confirmed request.
        /// </summary>
        private void ShowJobProgress(int rid)
        {
            using (var conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    la.AssignedDate,
                    la.DriverName,
                    la.AssistantName,
                    la.Status,
                    la.VID,
                    vr.Vehicle_Number,
                    la.ContainerID,
                    c.ContainerNumber,
                    c.ContainerType
                FROM load_assignment la
                LEFT JOIN vehicle_registration vr ON la.VID = vr.VID
                LEFT JOIN container c ON la.ContainerID = c.ContainerID
                WHERE la.RID = @RID";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RID", rid);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string assignedDate = reader.GetDateTime("AssignedDate").ToShortDateString();
                                string driver = reader["DriverName"].ToString();
                                string assistant = reader["AssistantName"].ToString();
                                string jobStatus = reader["Status"].ToString();
                                string vid = reader["VID"].ToString();
                                string vehicleNumber = reader["Vehicle_Number"].ToString();
                                string containerID = reader["ContainerID"].ToString();
                                string containerNumber = reader["ContainerNumber"].ToString();
                                string containerType = reader["ContainerType"].ToString();

                                string message =
                                    $"Assigned Date: {assignedDate}\n" +
                                    $"Driver: {driver}\n" +
                                    $"Assistant: {assistant}\n" +
                                    $"Job Status: {jobStatus}\n" +
                                    $"Vehicle ID: {vid}\n" +
                                    $"Vehicle Number: {vehicleNumber}\n" +
                                    $"Container ID: {containerID}\n" +
                                    $"Container Number: {containerNumber}\n" +
                                    $"Container Type: {containerType}";

                                MessageBox.Show(message, "Job Progress",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No assignment details found for this request.",
                                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching job progress: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
