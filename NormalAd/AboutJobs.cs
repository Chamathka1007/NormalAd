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
    public partial class AboutJobs : Form
    {
        private int _customerId;
        public AboutJobs(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;
        }
        // Event handlers for button clicks to navigate to different job-related forms

        private void btnCJob_Click(object sender, EventArgs e)
        {
            JobService jobService = new JobService(_customerId);
            jobService.MdiParent = this.MdiParent;  
            jobService.Dock = DockStyle.Fill;
            jobService.FormBorderStyle = FormBorderStyle.None;
            jobService.Show();
            this.Close();
        }

        private void btnPJob_Click(object sender, EventArgs e)
        {
            PastJob pastJobsForm = new PastJob(_customerId);
            pastJobsForm.MdiParent = this.MdiParent;
            pastJobsForm.Dock = DockStyle.Fill;
            pastJobsForm.FormBorderStyle = FormBorderStyle.None;
            pastJobsForm.Show();
            this.Close();
        }

        private void AboutJobs_Load(object sender, EventArgs e)
        {

        }
    }
}
