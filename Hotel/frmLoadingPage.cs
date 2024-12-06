using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class frmLoadingPage : Form
    {
        public frmLoadingPage()
        {
            InitializeComponent();
        }

        // When the form is loaded the timer start
        private void frmLoadingPage_Load(object sender, EventArgs e)
        {
            timer.Start();
        }
        // Initial point for progress bar is 0
        int startPoint = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            // Increment the progress by 2 units each time the timer ticks
            startPoint += 2;
            // Update the progress bar value
            MyProgressBar.Value = startPoint;
            // When the progress bar reaches 100
            if (MyProgressBar.Value == 100)
            {
                // Stop the timer once progress bar is full
                timer.Stop();
                MyProgressBar.Value = 0;
                // Reset the progress bar value to 0
                frmLogin login = new frmLogin();
                // Hide the loading page
                this.Hide();
                // Show the login form
                login.Show();
            }
        }
    }
}
