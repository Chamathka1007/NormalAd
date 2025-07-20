using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace NormalAd
{
    public partial class AssigningForm : Form
    {
        private string connStr = "server=localhost;database=ad;uid=root;pwd=2002;";
        private int selectedRID = -1;

        public AssigningForm(int rid)
        {
            InitializeComponent();
            selectedRID = rid;
        }

        private void AssigningForm_Load(object sender, EventArgs e)
        {
            LoadRequests();
            LoadVehicleTypes();
            LoadContainers();
            LoadDrivers();
            LoadAssistants();

            cmbStatu.Items.Clear();
            cmbStatu.Items.Add("-- Select Status --");
            cmbStatu.Items.AddRange(new string[] { "Assigned", "In Progress", "Completed" });
            cmbStatu.SelectedIndex = 0;

            dtpAssignedDate.Value = DateTime.Today;

            if (selectedRID > 0)
            {
                cmbRID.SelectedItem = selectedRID.ToString();
            }
        }

        private void LoadRequests()
        {
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                var dt = new DataTable();
                var adapter = new MySqlDataAdapter("SELECT * FROM service_request WHERE Status = 'Pending'", conn);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                cmbRID.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    cmbRID.Items.Add(row["RID"].ToString());
                }
            }
        }

        private void LoadVehicleTypes()
        {
            cmbVehicleType.Items.Clear();
            cmbVehicleType.Items.Add("-- Select Vehicle Type --");

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT DISTINCT Vehicle_Type FROM vehicle_registration WHERE Status = 'Available'", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbVehicleType.Items.Add(reader.GetString(0));
                }
            }
            cmbVehicleType.SelectedIndex = 0;
        }

        private void LoadVehiclesByType(string vehicleType)
        {
            cmbVehicle.Items.Clear();
            cmbVehicle.Items.Add("-- Select Vehicle --");

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "SELECT VID, Vehicle_Number FROM vehicle_registration WHERE Vehicle_Type = @Type AND Status = 'Available'", conn);
                cmd.Parameters.AddWithValue("@Type", vehicleType);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbVehicle.Items.Add($"{reader["VID"]} - {reader["Vehicle_Number"]}");
                }
            }
            cmbVehicle.SelectedIndex = 0;
        }

        private void LoadContainers()
        {
            cmbContain.Items.Clear();
            cmbContain.Items.Add("-- Select Container --");
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
                    SELECT ContainerID, ContainerNumber, ContainerType 
                    FROM container 
                    WHERE Status = 'Available'", conn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader["ContainerID"].ToString();
                    string number = reader["ContainerNumber"].ToString();
                    string type = reader["ContainerType"].ToString();
                    cmbContain.Items.Add($"{id} - {number} ({type})");
                }
            }
            cmbContain.SelectedIndex = 0;
        }

        private void LoadDrivers()
        {
            cmbDriver.Items.Clear();
            cmbDriver.Items.Add("-- Select Driver --");

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT Name FROM emp_registration WHERE JobRole = 'Driver'", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbDriver.Items.Add(reader.GetString(0));
                }
            }
            cmbDriver.SelectedIndex = 0;
        }

        private void LoadAssistants()
        {
            cmbAssistant.Items.Clear();
            cmbAssistant.Items.Add("-- Select Assistant --");

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT Name FROM emp_registration WHERE JobRole = 'Assistant'", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbAssistant.Items.Add(reader.GetString(0));
                }
            }
            cmbAssistant.SelectedIndex = 0;
        }

        private void cmbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVehicleType.SelectedIndex <= 0)
                return;

            string selectedType = cmbVehicleType.SelectedItem.ToString();
            LoadVehiclesByType(selectedType);
        }

        private void cmbRID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRID.SelectedItem != null)
                selectedRID = Convert.ToInt32(cmbRID.SelectedItem.ToString());
        }

        private void btnAss_Click(object sender, EventArgs e)
        {
            if (selectedRID == -1 || cmbVehicle.SelectedIndex <= 0 || cmbContain.SelectedIndex <= 0 || cmbStatu.SelectedIndex <= 0
                || cmbDriver.SelectedIndex <= 0 || cmbAssistant.SelectedIndex <= 0)
            {
                MessageBox.Show("⚠️ Please fill all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string vid = cmbVehicle.SelectedItem.ToString().Split('-')[0].Trim();
            string containerID = cmbContain.SelectedItem.ToString().Split('-')[0].Trim();

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                // ✅ FIRST: Insert into load_assignment
                var cmd = new MySqlCommand(@"
            INSERT INTO load_assignment
            (RID, VID, DriverName, AssistantName, ContainerID, AssignedDate, Status)
            VALUES
            (@RID, @VID, @Driver, @Assistant, @ContainerID, @Date, @Status)", conn);

                cmd.Parameters.AddWithValue("@RID", selectedRID);
                cmd.Parameters.AddWithValue("@VID", vid);
                cmd.Parameters.AddWithValue("@Driver", cmbDriver.SelectedItem?.ToString() ?? "");
                cmd.Parameters.AddWithValue("@Assistant", cmbAssistant.SelectedItem?.ToString() ?? "");
                cmd.Parameters.AddWithValue("@ContainerID", containerID);
                cmd.Parameters.AddWithValue("@Date", dtpAssignedDate.Value.Date);
                cmd.Parameters.AddWithValue("@Status", cmbStatu.SelectedItem.ToString());

                cmd.ExecuteNonQuery();

                // ✅ THEN: Update service_request status
                var updateCmd = new MySqlCommand(
                    "UPDATE service_request SET Status = 'Confirmed' WHERE RID = @RID", conn);
                updateCmd.Parameters.AddWithValue("@RID", selectedRID);
                updateCmd.ExecuteNonQuery();

                MessageBox.Show("✅ Assignment recorded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide(); // hide admin dashboard

                AssigningForm assigningForm = new AssigningForm(-1);
                assigningForm.StartPosition = FormStartPosition.CenterScreen;
                assigningForm.Show(); this.Close();
            }

            ClearForm();
            LoadRequests();
            LoadContainers();
            LoadVehicleTypes();
            LoadDrivers();
            LoadAssistants();
        }
        private void btnCl_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            cmbRID.SelectedIndex = -1;
            cmbVehicleType.SelectedIndex = 0;
            cmbVehicle.SelectedIndex = 0;
            cmbContain.SelectedIndex = 0;
            cmbStatu.SelectedIndex = 0;
            cmbDriver.SelectedIndex = 0;
            cmbAssistant.SelectedIndex = 0;

            dtpAssignedDate.Value = DateTime.Today;
            selectedRID = -1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["RID"].Value);
                cmbRID.SelectedItem = selectedRID.ToString();
            }
        }

        private void cmbVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbVehicleType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbVehicleType.SelectedIndex <= 0)
            {
                cmbVehicle.Items.Clear();
                cmbVehicle.Items.Add("-- Select Vehicle --");
                cmbVehicle.SelectedIndex = 0;
                return;
            }

            string selectedType = cmbVehicleType.SelectedItem.ToString();
            LoadVehiclesByType(selectedType);
        }
    }
}
