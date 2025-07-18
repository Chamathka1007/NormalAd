using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
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
    public partial class RContaineresView : Form
    {
        private string connStr = "server=localhost;database=ad;uid=root;pwd=2002;";
        private int selectedContainerID = -1;
        public RContaineresView()
        {
            InitializeComponent();
            
            LoadContainers();
        }



        private void dgvContainers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedContainerID = Convert.ToInt32(dgvContainers.Rows[e.RowIndex].Cells["ContainerID"].Value);
            }
        }

        private void RContaineresView_Load(object sender, EventArgs e)
        {
            LoadContainers();
        }


        private void LoadContainers()
        {
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = @"
                    SELECT ContainerID, ContainerNumber, ContainerType, Capacity, Status, Notes, CreatedAt, UpdatedAt 
                    FROM container 
                    ORDER BY ContainerID";

                using (var adapter = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvContainers.DataSource = dt;

                    dgvContainers.AutoResizeColumns();

                    if (dgvContainers.Columns.Contains("ContainerID"))
                        dgvContainers.Columns["ContainerID"].ReadOnly = true;
                }
            }

            selectedContainerID = -1;
         
        }
       

        private void dgvContainers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (selectedContainerID == -1)
            {
                MessageBox.Show("Please select a container to update.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Find the selected row
            DataGridViewRow rowToUpdate = null;
            foreach (DataGridViewRow row in dgvContainers.Rows)
            {
                if (row.Cells["ContainerID"].Value != null && Convert.ToInt32(row.Cells["ContainerID"].Value) == selectedContainerID)
                {
                    rowToUpdate = row;
                    break;
                }
            }

            if (rowToUpdate == null)
            {
                MessageBox.Show("Could not find selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Read updated values from the row
            string number = rowToUpdate.Cells["ContainerNumber"].Value?.ToString() ?? "";
            string type = rowToUpdate.Cells["ContainerType"].Value?.ToString() ?? "";
            string capacity = rowToUpdate.Cells["Capacity"].Value?.ToString() ?? "";
            string status = rowToUpdate.Cells["Status"].Value?.ToString() ?? "";
            string notes = rowToUpdate.Cells["Notes"].Value?.ToString() ?? "";

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = @"
            UPDATE container SET 
                ContainerNumber = @Number,
                ContainerType = @Type,
                Capacity = @Capacity,
                Status = @Status,
                Notes = @Notes,
                UpdatedAt = NOW()
            WHERE ContainerID = @ID";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Number", number);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Capacity", capacity);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.Parameters.AddWithValue("@ID", selectedContainerID);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Container updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadContainers();



            LoadContainers(); // refresh after editing
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (selectedContainerID == -1)
            {
                MessageBox.Show("Please select a container to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this container?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.No) return;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = @"DELETE FROM container WHERE ContainerID = @ID";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", selectedContainerID);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Container deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadContainers();
        }
    }
}
