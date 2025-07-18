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
    public partial class JobStatusForm : Form
    {
        private string connStr = "server=localhost;database=ad;uid=root;pwd=2002;";
        private int selectedRID = -1;

        public JobStatusForm()
        {
            InitializeComponent();
            cmbStatus.SelectedIndex = 0;
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void JobStatusForm_Load(object sender, EventArgs e)
        {
            LoadJobs();
            cmbStatus.Items.AddRange(new string[] { "Pending", "In Progress", "Completed", "Cancelled" });
        }

        private void LoadJobs()
        {
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = @"
            SELECT sr.RID,
                   IFNULL(js.status, 'Pending') AS CurrentStatus,
                   js.UpdatedAt
            FROM service_request sr
            LEFT JOIN (
                SELECT js1.RID, js1.status, js1.UpdatedAt
                FROM job_status js1
                INNER JOIN (
                    SELECT RID, MAX(UpdatedAt) AS LatestUpdate
                    FROM job_status
                    GROUP BY RID
                ) latest
                ON js1.RID = latest.RID AND js1.UpdatedAt = latest.LatestUpdate
            ) js
            ON sr.RID = js.RID
            ORDER BY sr.RID ASC;";

                using (var adapter = new MySqlDataAdapter(query, conn))
                {
                    DataTable jobsTable = new DataTable();
                    adapter.Fill(jobsTable);
                    dgvJobs.DataSource = jobsTable;

                    if (jobsTable.Columns.Contains("RID"))
                        dgvJobs.Columns["RID"].HeaderText = "Request ID";
                    if (jobsTable.Columns.Contains("CurrentStatus"))
                        dgvJobs.Columns["CurrentStatus"].HeaderText = "Status";
                    if (jobsTable.Columns.Contains("UpdatedAt"))
                        dgvJobs.Columns["UpdatedAt"].HeaderText = "Updated At";
                }
            }
        }
        

        private void dgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRID = Convert.ToInt32(dgvJobs.Rows[e.RowIndex].Cells["RID"].Value);
                LoadHistory(selectedRID);
            }
        }

        private void LoadHistory(int rid)
        {
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = @"SELECT status, UpdatedAt FROM job_status WHERE RID = @RID ORDER BY UpdatedAt DESC";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RID", rid);
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable historyTable = new DataTable();
                        adapter.Fill(historyTable);
                        dgvHistory.DataSource = historyTable;
                    }
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (selectedRID == -1)
            {
                MessageBox.Show("Select a job to update.");
                return;
            }

            string newStatus = cmbStatus.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(newStatus))
            {
                MessageBox.Show("Select a new status.");
                return;
            }


            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = @"INSERT INTO job_status (RID, status, UpdatedAt) VALUES (@RID, @Status, CURDATE())";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RID", selectedRID);
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Status updated.");
            LoadJobs();
            LoadHistory(selectedRID);
        }

        
    }
}
