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




        // Event handler for the "Registered Customers" menu item
        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisteredCustomerView customersForm = new RegisteredCustomerView();
            customersForm.MdiParent = this;
            customersForm.FormBorderStyle = FormBorderStyle.None;
            customersForm.Dock = DockStyle.Fill;
            customersForm.Show();
        }


        // Event handler for the "Employees" menu item
        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            EmployeeRegisteredView employeeFormview = new EmployeeRegisteredView();
            employeeFormview.MdiParent = this;
            employeeFormview.Dock = DockStyle.Fill;
            employeeFormview.FormBorderStyle = FormBorderStyle.None;
            employeeFormview.Show();

        }
        // Method to close all child forms in the MDI container
        private void CloseAllChildren()
        {
            foreach (Form child in this.MdiChildren)
                child.Close();
        }

        // Event handler for the "Employee Registration" menu item
        private void employeeRegistrationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CloseAllChildren();

            EmployeeRegistration employeeForm = new EmployeeRegistration();
            employeeForm.MdiParent = this;
            employeeForm.FormBorderStyle = FormBorderStyle.None;
            employeeForm.Dock = DockStyle.Fill;
            employeeForm.Show();

        }
        // Event handler for the "Customer Registration" menu item

        private void customerRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            CustomerReg customerForm = new CustomerReg();
            customerForm.MdiParent = this;
            customerForm.FormBorderStyle = FormBorderStyle.None;
            customerForm.Dock = DockStyle.Fill;
            customerForm.Show();

        }
        // Event handler for the "Job Requests" menu item

        private void jobRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            AdminServiceRequestView jobRequestsForm = new AdminServiceRequestView();
            jobRequestsForm.MdiParent = this;
            jobRequestsForm.Dock = DockStyle.Fill;
            jobRequestsForm.FormBorderStyle = FormBorderStyle.None;
            jobRequestsForm.Show();

        }
        // Event handler for the "Log Out" menu item
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
          LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close(); 
        }

        private void employeeRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // Event handler for the "Vehicle Registration" menu item
        private void vehicleResgistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            vehicleRegistrationForm vehicleRegistrationForm = new vehicleRegistrationForm();
            vehicleRegistrationForm.MdiParent = this;
            vehicleRegistrationForm.Dock = DockStyle.Fill;
            vehicleRegistrationForm.FormBorderStyle = FormBorderStyle.None;
            vehicleRegistrationForm.Show();
        }

        // Event handler for the "Vehicle View" menu item
        private void vehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            VehicleView vehicleViewForm = new VehicleView();
            vehicleViewForm.MdiParent = this;
            vehicleViewForm.Dock = DockStyle.Fill;
            vehicleViewForm.FormBorderStyle = FormBorderStyle.None;
            vehicleViewForm.Show();

        }

        // Event handler for the "About Jobs" menu item
        private void serviceHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            ServiceHistory serviceHistoryForm = new ServiceHistory();
            serviceHistoryForm.MdiParent = this;
            serviceHistoryForm.Dock = DockStyle.Fill;
            serviceHistoryForm.FormBorderStyle = FormBorderStyle.None;
            serviceHistoryForm.Show();

        }

        // Event handler for the "Job Status" menu item
        private void jobStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            JobStatusForm jobStatusForm = new JobStatusForm();
            jobStatusForm.MdiParent = this;
            jobStatusForm.Dock = DockStyle.Fill;
            jobStatusForm.FormBorderStyle = FormBorderStyle.None;
            jobStatusForm.Show();
        }

        // Event handler for the "Container Registration" menu item
        private void containerRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            ContainerForm containerForm = new ContainerForm();
            containerForm.MdiParent = this;
            containerForm.Dock = DockStyle.Fill;
            containerForm.FormBorderStyle = FormBorderStyle.None;
            containerForm.Show();
        }

        // Event handler for the "Containers" menu item
        private void containersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            RContaineresView containersViewForm = new RContaineresView();
            containersViewForm.MdiParent = this;
            containersViewForm.Dock = DockStyle.Fill;
            containersViewForm.FormBorderStyle = FormBorderStyle.None;
            containersViewForm.Show();
        }

        // Event handler for the "Transport" menu item
        private void transportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            AssigningForm assigningForm = new AssigningForm(1); 
            assigningForm.MdiParent = this;
            assigningForm.Dock = DockStyle.Fill;
            assigningForm.FormBorderStyle = FormBorderStyle.None;
            assigningForm.Show();
        }

        // Event handler for the "Personal Information" menu item
        private void personalInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildren();
            AdminProfile adminProfile = new AdminProfile(2); 
            adminProfile.MdiParent = this;
            adminProfile.Dock = DockStyle.Fill;
            adminProfile.FormBorderStyle = FormBorderStyle.None;
            adminProfile.Show();
        }

        // Event handler for the "Report Generation" menu item
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
