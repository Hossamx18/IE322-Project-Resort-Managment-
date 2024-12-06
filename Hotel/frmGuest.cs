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
    public partial class frmGuest : Form
    {
        // Using classGuest to handle the guests
        classGuest guest = new classGuest();
        public frmGuest()
        {
            InitializeComponent();
        }
        // When the form is loaded the datagridview gets the table
        private void frmGuest_Load(object sender, EventArgs e)
        {
            DataGridView_guest.DataSource = guest.getGuest();

        }
        // Method to refresh the datagridview to show the latest guest data
        private void getTable()
        {
            // Calls the method from classGuest to get the list of guests and put it in the datagridview
            DataGridView_guest.DataSource = guest.getGuest();
        }
        // Clears all textboxes when the button is clicked
        private void btnClean_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtCity.Clear();
        }
        // Adds a new guest 
        private void btnSave_Click(object sender, EventArgs e)
        {
            // If textboxes are empty 
            if (txtID.Text == "" || txtFirstName.Text == "" || txtPhone.Text == "")
            {
                // Show error message
                MessageBox.Show("Required Field - ID Number, First Name and Phone Number", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // If not
            else
            {
             // Using try and catch ensures if any errors happen in try the catch will try to give an error with details
                try
                {
                    // Gets the guest details from the textboxes
                    string id = txtID.Text;
                    string firstName = txtFirstName.Text;
                    string lastName = txtLastName.Text;
                    string phone = txtPhone.Text;
                    string city = txtCity.Text;

                    // Insert the new guest into the database (uses insertGuest method)
                    Boolean insertGuest = guest.insertGuest(id, firstName, lastName, phone, city);
                    if (insertGuest)
                    {
                        MessageBox.Show("New Guest Saved Successfully", "Guest Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Refresh the datagridview
                        getTable();
                        // Clear the textboxes
                        btnClean.PerformClick();

                    }


                    else
                    {
                        MessageBox.Show("ERROR Guest Not Inserted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        // The color of the exit is white
        private void lblExit_MouseLeave(object sender, EventArgs e)
        {
            lblExit.ForeColor = Color.White;
        }
        // When putting the mouse on it changes to red
        private void lblExit_MouseEnter(object sender, EventArgs e)
        {
            lblExit.ForeColor = Color.Red;
        }
        // When clicked it shutsdown the program
        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Update guest information 
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtFirstName.Text == "" || txtPhone.Text == "")
            {
                MessageBox.Show("Required Field - ID Number, First Name and Phone Number", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                try
                {
                    string id = txtID.Text;
                    string firstName = txtFirstName.Text;
                    string lastName = txtLastName.Text;
                    string phone = txtPhone.Text;
                    string city = txtCity.Text;

                    // Update the guest information in the database (uses editGuest method)
                    Boolean editGuest = guest.editGuest(id, firstName, lastName, phone, city);
                    if (editGuest)
                    {
                        MessageBox.Show("New Guest Updated Successfully", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getTable();
                        btnClean.PerformClick();
                    }


                    else
                    {
                        MessageBox.Show("ERROR Guest Data not Updated", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        // When cell is clicked the data is filled with information from table to textboxes
        private void DataGridView_guest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = DataGridView_guest.CurrentRow.Cells[0].Value.ToString();
            txtFirstName.Text = DataGridView_guest.CurrentRow.Cells[1].Value.ToString();
            txtLastName.Text = DataGridView_guest.CurrentRow.Cells[2].Value.ToString();
            txtPhone.Text = DataGridView_guest.CurrentRow.Cells[3].Value.ToString();
            txtCity.Text = DataGridView_guest.CurrentRow.Cells[4].Value.ToString();
        }
        // Delete guest 
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Required Field - ID Number", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string id = txtID.Text;
                    // Deletes guest information by id (uses removeGuest method)
                    Boolean deleteGuest = guest.removeGuest(id);
                    if (deleteGuest)
                    {
                        MessageBox.Show(" Guest Data Removed uccessfully", "Guest Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getTable();
                        btnClean.PerformClick();
                    }


                    else
                    {
                        MessageBox.Show("ERROR Guest not Removed", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
          
        }
    }
    } 
