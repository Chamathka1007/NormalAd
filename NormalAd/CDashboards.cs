using ADProject;
using Org.BouncyCastle.Asn1.Ocsp;
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
    public partial class CDashboards : Form
    {
        private int _customerId;
        private string _loggedInEmail;
        private string email;

        public CDashboards(int customerId, string email)
        {
            InitializeComponent();
            _customerId = customerId;
            _loggedInEmail = email;

            this.IsMdiContainer = true;
        }

        public CDashboards()
        {
        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();

            if (!string.IsNullOrEmpty(_loggedInEmail))
            {
                ProfileUpdationForm profileForm = new ProfileUpdationForm(_loggedInEmail);
                profileForm.MdiParent = this;
                profileForm.Dock = DockStyle.Fill;
                profileForm.FormBorderStyle = FormBorderStyle.None;
                profileForm.Show();
            }
            else
            {
                MessageBox.Show("User email not found. Please login again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            private void CloseAllChildren()
        {
            foreach (Form child in this.MdiChildren)
                child.Close();
        }

        private void jobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();

            

            AboutJobs aboutJobs = new AboutJobs(_customerId);
            aboutJobs.MdiParent = this;
            aboutJobs.Dock = DockStyle.Fill;
            aboutJobs.FormBorderStyle = FormBorderStyle.None;
            aboutJobs.Show();
        }

        private void CDashboards_Load(object sender, EventArgs e)
        {

        }

        private void jobStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            ServiceStatus serviceStatusForm = new ServiceStatus(_customerId);
            serviceStatusForm.MdiParent = this;
            serviceStatusForm.Dock = DockStyle.Fill;
            serviceStatusForm.FormBorderStyle = FormBorderStyle.None;
            serviceStatusForm.Show();

        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            CHome cHome = new CHome();
            cHome.MdiParent = this; 
            cHome.Dock = DockStyle.Fill;
            cHome.FormBorderStyle = FormBorderStyle.None;
            cHome.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            CAbout cAbout = new CAbout();
            cAbout.MdiParent = this;
            cAbout.Dock = DockStyle.Fill;
            cAbout.FormBorderStyle = FormBorderStyle.None;
            cAbout.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }
    }
}
