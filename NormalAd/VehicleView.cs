using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace NormalAd
{
    public partial class VehicleView : Form
    {
        private string connectionString = "server=localhost;database=ad;uid=root;pwd=2002;";
        private DataTable vehicleTable;
        private MySqlDataAdapter adapter;

        public VehicleView()
        {
            InitializeComponent();
        }

        private void VehicleView_Load(object sender, EventArgs e)
        {
            LoadVehicles();
        }

        private void LoadVehicles()
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM vehicle_registration";

                    adapter = new MySqlDataAdapter(query, conn);
                    vehicleTable = new DataTable();
                    adapter.Fill(vehicleTable);

                    dataGridView1.DataSource = vehicleTable;

                    // make ID read-only
                    if (dataGridView1.Columns.Contains("VID"))
                        dataGridView1.Columns["VID"].ReadOnly = true;
                }

                // Prepare update & delete commands
                adapter.UpdateCommand = new MySqlCommand(@"
                    UPDATE vehicle_registration SET
                        Vehicle_Number=@VehicleNumber,
                        Vehicle_Type=@VehicleType,
                        OwnerContact=@OwnerContact,
                        Status=@Status,
                        Note=@Note
                    WHERE VID=@VID");

                adapter.UpdateCommand.Parameters.Add("@VehicleNumber", MySqlDbType.VarChar, 50, "Vehicle_Number");
                adapter.UpdateCommand.Parameters.Add("@VehicleType", MySqlDbType.VarChar, 50, "Vehicle_Type");
                adapter.UpdateCommand.Parameters.Add("@OwnerContact", MySqlDbType.VarChar, 50, "OwnerContact");
                adapter.UpdateCommand.Parameters.Add("@Status", MySqlDbType.VarChar, 20, "Status");
                adapter.UpdateCommand.Parameters.Add("@Note", MySqlDbType.VarChar, 255, "Note");
                adapter.UpdateCommand.Parameters.Add("@VID", MySqlDbType.Int32, 0, "VID").SourceVersion = DataRowVersion.Original;

                adapter.DeleteCommand = new MySqlCommand("DELETE FROM vehicle_registration WHERE VID=@VID");
                adapter.DeleteCommand.Parameters.Add("@VID", MySqlDbType.Int32, 0, "VID").SourceVersion = DataRowVersion.Original;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading vehicles: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.IsCurrentCellInEditMode)
                    dataGridView1.EndEdit();

                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    adapter.UpdateCommand.Connection = conn;
                    adapter.DeleteCommand.Connection = conn;

                    adapter.Update(vehicleTable);
                }

                MessageBox.Show("✅ Changes saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadVehicles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete the selected vehicle?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dataGridView1.Rows.Remove(row);
                }

                btnSave_Click(sender, e); // Save changes to DB after delete
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // You can handle cell-specific logic here if needed
        }
    }
}
