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
    
    public partial class ContainerForm : Form
    {
        private string connStr = "server=localhost;database=ad;uid=root;pwd=2002;";
        private int editingContainerID = -1;
        public ContainerForm()
        {
            InitializeComponent();
            this.Load += ContainerForm_Load;
        }

        /// <summary>
        /// Handles the CellClick event of the dgvContainers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvContainers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvContainers.Rows[e.RowIndex];
                editingContainerID = Convert.ToInt32(row.Cells["ContainerID"].Value);
                txtNumber.Text = row.Cells["ContainerNumber"].Value.ToString();
                cmbType.SelectedItem = row.Cells["ContainerType"].Value.ToString();
                txtCapacity.Text = row.Cells["Capacity"].Value.ToString();

                txtNotes.Text = row.Cells["Notes"].Value.ToString();

                btnSave.Text = "Update";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }
        /// <summary>
        /// Loads the containers.
        /// </summary>
        private void LoadContainers()
        {
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT ContainerID, ContainerNumber, ContainerType, Capacity, Status, Notes, CreatedAt, UpdatedAt FROM container ORDER BY ContainerID";
                using (var adapter = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvContainers.DataSource = dt;

                    dgvContainers.Columns["ContainerID"].HeaderText = "ID";
                    dgvContainers.Columns["ContainerNumber"].HeaderText = "Number";
                    dgvContainers.Columns["ContainerType"].HeaderText = "Type";
                    dgvContainers.Columns["Capacity"].HeaderText = "Capacity";
                    dgvContainers.Columns["Status"].HeaderText = "Status";
                    dgvContainers.Columns["Notes"].HeaderText = "Notes";
                    dgvContainers.Columns["CreatedAt"].HeaderText = "Created";
                    dgvContainers.Columns["UpdatedAt"].HeaderText = "Updated";

                    dgvContainers.AutoResizeColumns();
                }
            }
        }

       private void ClearForm()
        {
            editingContainerID = -1;
            txtNumber.Clear();
            cmbType.SelectedIndex = 0; // back to SELECT
            txtCapacity.Clear();
            txtNotes.Clear();
            btnSave.Text = "Save";
        }

        

        /// <summary>
        /// Validates the inputs.
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                MessageBox.Show("Container Number is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select Container Type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCapacity.Text))
            {
                MessageBox.Show("Capacity is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void InitializeComboBox()
        {
            cmbType.Items.Clear();
            cmbType.Items.AddRange(new string[] { "SELECT", "Dry", "Refrigerated", "Open Top", "Flat Rack", "Tank" });
            cmbType.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
          private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query;

                if (editingContainerID == -1)
                {
                    // Insert new container
                    query = @"
                        INSERT INTO container
                        (ContainerNumber, ContainerType, Capacity, Status, Notes, CreatedAt, UpdatedAt)
                        VALUES
                        (@ContainerNumber, @ContainerType, @Capacity, @Status, @Notes, NOW(), NOW())";
                }
                else
                {
                    // Update existing container
                    query = @"
                        UPDATE container SET
                        ContainerNumber = @ContainerNumber,
                        ContainerType = @ContainerType,
                        Capacity = @Capacity,
                        Status = @Status,
                        Notes = @Notes,
                        UpdatedAt = NOW()
                        WHERE ContainerID = @ContainerID";
                }

                // Assuming Status is always 'Available' for new containers
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ContainerNumber", txtNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@ContainerType", cmbType.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Capacity", txtCapacity.Text.Trim());
                    cmd.Parameters.AddWithValue("@Notes", txtNotes.Text.Trim());
                    cmd.Parameters.AddWithValue("@Status", "Available"); 


                    if (editingContainerID != -1)
                        cmd.Parameters.AddWithValue("@ContainerID", editingContainerID);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Container saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadContainers();

                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCl_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvContainers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ContainerForm_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            LoadContainers();
            ClearForm();
        }
    }
}

    
