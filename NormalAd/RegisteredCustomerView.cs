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
    public partial class RegisteredCustomerView : Form
    {
        public RegisteredCustomerView()
        {
            InitializeComponent();
        }

        private void RegisteredCustomerView_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }


        private void LoadCustomers()
        {
            string connStr = "server=localhost;database=ad;uid=root;pwd=2002;";
            using (var conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT
                  ID,Full_Name,Email, Address, DOB,Gender,NIC, Martial_Status,Contact_Number FROM customer_registration";

                    using (var adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading customers: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}