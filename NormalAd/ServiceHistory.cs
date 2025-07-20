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
    public partial class ServiceHistory : Form
    {
        private readonly string connectionString = "server=localhost;database=ad;uid=root;pwd=2002;";
        private DataTable historyTable;
        public ServiceHistory()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadServiceHistory(DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            RID,
                            CustomerID,
                            Item_Type,
                            Date,
                            PickupLocation,
                            DeliveryLocation,
                            Quantity,
                            Status,
                            SpecialNote
                        FROM service_request
                        {0}
                        ORDER BY Date DESC;";

                    string whereClause = "";

                    if (fromDate.HasValue && toDate.HasValue)
                    {
                        whereClause = "WHERE Date BETWEEN @From AND @To";
                    }

                    using (var cmd = new MySqlCommand(string.Format(query, whereClause), conn))
                    {
                        if (fromDate.HasValue && toDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@From", fromDate.Value.Date);
                            cmd.Parameters.AddWithValue("@To", toDate.Value.Date);
                        }

                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            historyTable = new DataTable();
                            adapter.Fill(historyTable);
                            dataGridView1.DataSource = historyTable;

                            // Optional: set nice column headers
                            dataGridView1.Columns["RID"].HeaderText = "Request ID";
                            dataGridView1.Columns["CustomerID"].HeaderText = "Customer ID";
                            dataGridView1.Columns["Item_Type"].HeaderText = "Item Type";
                            dataGridView1.Columns["Date"].HeaderText = "Date";
                            dataGridView1.Columns["PickupLocation"].HeaderText = "Pickup";
                            dataGridView1.Columns["DeliveryLocation"].HeaderText = "Delivery";
                            dataGridView1.Columns["Quantity"].HeaderText = "Quantity";
                            dataGridView1.Columns["Status"].HeaderText = "Status";
                            dataGridView1.Columns["SpecialNote"].HeaderText = "Note";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading service history: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ServiceHistory_Load(object sender, EventArgs e)
        {
            LoadServiceHistory();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Status"].Value?.ToString() == "Completed")
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

        }


        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (dtpFrom.Value.Date > dtpTo.Value.Date)
            {
                MessageBox.Show("Start date cannot be after end date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadServiceHistory(dtpFrom.Value, dtpTo.Value);
        }

        private void btnCl_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;
            LoadServiceHistory(); // Reload without filters
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
