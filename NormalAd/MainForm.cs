using ADProject;
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
    public partial class MainForm : Form
    {
        private string fullText = "Welcome to the E-Shift!";
        private int currentIndex = 0;
        private Timer timer1;
        public MainForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        { 
        
        } 
        
           

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (currentIndex < fullText.Length)
            {
                label1.Text += fullText[currentIndex];
                currentIndex++;
            }
            else
            {
                timer1.Stop(); // Stop the timer when the text is fully displayed
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {
             
        
            // Initialize and start the typewriter effect timer here
            label1.Text = ""; // Clear label initially

            timer1 = new Timer();
            timer1.Interval = 100; // milliseconds delay for each character
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }
    }
}

