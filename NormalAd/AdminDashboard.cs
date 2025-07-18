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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();

        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
        }

        private void registeredCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisteredCustomerView customersForm = new RegisteredCustomerView();
            customersForm.MdiParent = this;
            customersForm.FormBorderStyle = FormBorderStyle.None;
            customersForm.Dock = DockStyle.Fill;
            customersForm.Show();
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            EmployeeRegisteredView employeeFormview = new EmployeeRegisteredView();
            employeeFormview.MdiParent = this;
            employeeFormview.Dock = DockStyle.Fill;
            employeeFormview.FormBorderStyle = FormBorderStyle.None;
            employeeFormview.Show();

        }
        private void CloseAllChildren()
        {
            foreach (Form child in this.MdiChildren)
                child.Close();
        }

        private void employeeRegistrationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CloseAllChildren();

            EmployeeRegistration employeeForm = new EmployeeRegistration();
            employeeForm.MdiParent = this;
            employeeForm.FormBorderStyle = FormBorderStyle.None;
            employeeForm.Dock = DockStyle.Fill;
            employeeForm.Show();

        }

        private void customerRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            CustomerReg customerForm = new CustomerReg();
            customerForm.MdiParent = this;
            customerForm.FormBorderStyle = FormBorderStyle.None;
            customerForm.Dock = DockStyle.Fill;
            customerForm.Show();

        }

        private void jobRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            AdminServiceRequestView jobRequestsForm = new AdminServiceRequestView();
            jobRequestsForm.MdiParent = this;
            jobRequestsForm.Dock = DockStyle.Fill;
            jobRequestsForm.FormBorderStyle = FormBorderStyle.None;
            jobRequestsForm.Show();

        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
          LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close(); 
        }

        private void employeeRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void vehicleResgistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            vehicleRegistrationForm vehicleRegistrationForm = new vehicleRegistrationForm();
            vehicleRegistrationForm.MdiParent = this;
            vehicleRegistrationForm.Dock = DockStyle.Fill;
            vehicleRegistrationForm.FormBorderStyle = FormBorderStyle.None;
            vehicleRegistrationForm.Show();
        }

        private void vehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            VehicleView vehicleViewForm = new VehicleView();
            vehicleViewForm.MdiParent = this;
            vehicleViewForm.Dock = DockStyle.Fill;
            vehicleViewForm.FormBorderStyle = FormBorderStyle.None;
            vehicleViewForm.Show();

        }

        private void serviceHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            ServiceHistory serviceHistoryForm = new ServiceHistory();
            serviceHistoryForm.MdiParent = this;
            serviceHistoryForm.Dock = DockStyle.Fill;
            serviceHistoryForm.FormBorderStyle = FormBorderStyle.None;
            serviceHistoryForm.Show();

        }

        private void jobStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            JobStatusForm jobStatusForm = new JobStatusForm();
            jobStatusForm.MdiParent = this;
            jobStatusForm.Dock = DockStyle.Fill;
            jobStatusForm.FormBorderStyle = FormBorderStyle.None;
            jobStatusForm.Show();
        }

        private void containerRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            ContainerForm containerForm = new ContainerForm();
            containerForm.MdiParent = this;
            containerForm.Dock = DockStyle.Fill;
            containerForm.FormBorderStyle = FormBorderStyle.None;
            containerForm.Show();
        }

        private void containersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            RContaineresView containersViewForm = new RContaineresView();
            containersViewForm.MdiParent = this;
            containersViewForm.Dock = DockStyle.Fill;
            containersViewForm.FormBorderStyle = FormBorderStyle.None;
            containersViewForm.Show();
        }

        private void transportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            AssigningForm assigningForm = new AssigningForm(1); 
            assigningForm.MdiParent = this;
            assigningForm.Dock = DockStyle.Fill;
            assigningForm.FormBorderStyle = FormBorderStyle.None;
            assigningForm.Show();
        }

        private void personalInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            AdminProfile adminProfile = new AdminProfile(2); // Assuming 1 is the admin ID, replace with actual ID as needed
            adminProfile.MdiParent = this;
            adminProfile.Dock = DockStyle.Fill;
            adminProfile.FormBorderStyle = FormBorderStyle.None;
            adminProfile.Show();
        }

        private void reportGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            ReportForm reportForm = new ReportForm();
            reportForm.MdiParent = this;
            reportForm.Dock = DockStyle.Fill;
            reportForm.FormBorderStyle = FormBorderStyle.None;
            reportForm.Show();
        }
    }
}
