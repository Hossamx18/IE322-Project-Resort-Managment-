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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        // Opens the Dashobard
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Animates the sliding panel to match the height and top position of the clicked button
            panel_slide.Height = btnDashboard.Height;
            panel_slide.Top = btnDashboard.Top;
            // Clears the existing controls from the main panel and adds the dashboard panel
            panel_main.Controls.Clear();
            // panel_cover is assumed to be the dashboard content
            panel_main.Controls.Add(panel_cover);
        }
        // Open the guest form
        private void btnGuest_Click(object sender, EventArgs e)
        {
            panel_slide.Height = btnGuest.Height;
            panel_slide.Top = btnGuest.Top;


            panel_main.Controls.Clear();
            // Gets the guest form
            frmGuest guest = new frmGuest();
            guest.TopLevel = false;
            // Makes the guest form fill the panel
            guest.Dock = DockStyle.Fill;
            // Adds the guest form to the panel
            panel_main.Controls.Add(guest);
            // Shows the guest form
            guest.Show();
        }
        // Open the reception form
        private void btnReception_Click(object sender, EventArgs e)
        {
            panel_slide.Height = btnReception.Height;
            panel_slide.Top = btnReception.Top;

            panel_main.Controls.Clear();
            frmReservation reservation = new frmReservation();
            reservation.TopLevel = false;
            reservation.Dock = DockStyle.Fill;
            panel_main.Controls.Add(reservation);
            reservation.Show();
        }
        // Open room form
        private void btnRoom_Click(object sender, EventArgs e)
        {
            panel_slide.Height = btnRoom.Height;
            panel_slide.Top = btnRoom.Top;

            panel_main.Controls.Clear();
            frmRoom room = new frmRoom();
            room.TopLevel = false;
            room.Dock = DockStyle.Fill;
            panel_main.Controls.Add(room);
            room.Show();
        }
        // Open the login form
        private void btnLogout_Click(object sender, EventArgs e)
        {
            panel_slide.Height = btnLogout.Height;
            panel_slide.Top = btnLogout.Top;

            frmLogin login = new frmLogin();
            this.Hide();
            login.Show();
        }
        private void lblExit_MouseLeave(object sender, EventArgs e)
        {
            lblExit.ForeColor = Color.White;
        }
        private void lblExit_MouseEnter(object sender, EventArgs e)
        {
            lblExit.ForeColor = Color.Red;
        }
        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
