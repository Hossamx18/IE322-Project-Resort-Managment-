using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class frmReservation : Form
    {
        // Instance of Room Class
        classRoom room = new classRoom();
        // Instance of Reservation Class
        classReservation reservation = new classReservation();
        public frmReservation()
        {
            InitializeComponent();
        }

        private void frmReservation_Load(object sender, EventArgs e)
        {
            // To show room type in comobox from room class
            comboBox_RoomType.DataSource = room.getRoomType();
            comboBox_RoomType.DisplayMember = "Label";
            comboBox_RoomType.ValueMember = "CategoryId";

            // Set the room number combo box by roomtype
            int type = Convert.ToInt32(comboBox_RoomType.SelectedValue.ToString());
            comboBox_RoomNo.DataSource = reservation.roomByType(type);
            comboBox_RoomNo.DisplayMember = "RoomId";
            comboBox_RoomNo.ValueMember = "RoomId";

            //show reservation list in datagridview
            getReservation();

        }
        // Method to show reservation list in datagridview
        public void getReservation()
        {
            DataGridView_reservation.DataSource = reservation.getReservation();
        }


        private void comboBox_RoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When the Room Type combo box value change  update the Room Number combo box
            try
            {
                int type = Convert.ToInt32(comboBox_RoomType.SelectedValue.ToString());
                comboBox_RoomNo.DataSource = reservation.roomByType(type);
                comboBox_RoomNo.DisplayMember = "RoomId";
                comboBox_RoomNo.ValueMember = "RoomId";
            }
            catch (Exception)
            {
            
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve data from textboxes
                string guestId = txtGuestId.Text;
                string roomNo = comboBox_RoomNo.SelectedValue.ToString();
                DateTime dateIn = dateTimePicker_CheckIn.Value;
                DateTime dateOut = dateTimePicker_CheckOut.Value;

                // Validate check-in and check-out dates
                if (dateIn.Date < DateTime.Today.Date)
                {
                    MessageBox.Show("Check In Date Must Be Today or After", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dateOut.Date < dateIn.Date)
                {
                    MessageBox.Show("Check Out Must Be Same as Check in or After", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    // Add the reservation and mark the room as "Busy"
                    if (reservation.addReservation(guestId, roomNo, dateIn, dateOut) && reservation.setReservRoom(roomNo, "Busy"))
                    {
                        MessageBox.Show("New Reservation Successfully Added", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getReservation();

                    }
                    else
                    {
                        MessageBox.Show("Reservation Was not Successfully ", "Error Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {           
                MessageBox.Show(ex.Message, "Error Adding Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the reservation id and room number to delete the reservation
            int reservId = Convert.ToInt32(txtReservationId.Text);
            string rno = comboBox_RoomNo.Text;
            try
            {
                // Delete the reservation and set the room status to "Available"
                if (reservation.removeReservation(reservId) && reservation.setReservRoom(rno, "Availabe"))
                {
                    // Refresh the reservation list
                    getReservation();
                    MessageBox.Show("Delete Reservation Sucessfull", "Delete Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView_reservation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Show data in textboxes, , comboobox and datetimepicker with data from the selected row in the datagridview
            txtReservationId.Text = DataGridView_reservation.CurrentRow.Cells[0].Value.ToString();
            txtGuestId.Text = DataGridView_reservation.CurrentRow.Cells[1].Value.ToString();
         
            string rno = DataGridView_reservation.CurrentRow.Cells[2].Value.ToString();
            comboBox_RoomType.SelectedValue = reservation.typeByRoom(rno);
            comboBox_RoomNo.Text = rno;
            dateTimePicker_CheckIn.Text = DataGridView_reservation.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker_CheckOut.Text = DataGridView_reservation.CurrentRow.Cells[4].Value.ToString();


        }
        // Clear button
        private void btnClean_Click(object sender, EventArgs e)
        {
            txtGuestId.Clear();
            txtReservationId.Clear();
            comboBox_RoomNo.SelectedValue = 1;
            dateTimePicker_CheckIn.Value = DateTime.Now;
            dateTimePicker_CheckOut.Value = DateTime.Now;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Get updated reservation details
                int reservId = Convert.ToInt32(txtReservationId.Text);
                string guestId = txtGuestId.Text;
                string roomNo = comboBox_RoomNo.SelectedValue.ToString();
                DateTime dateIn = dateTimePicker_CheckIn.Value;
                DateTime dateOut = dateTimePicker_CheckOut.Value;


                // Validate dates
                if (dateIn.Date < DateTime.Today.Date)
                {
                    MessageBox.Show("Check In Date Must Be Today or After", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dateOut.Date < dateIn.Date)
                {

                    MessageBox.Show("Check Out Must Be Same as Check in or After", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    // Update the reservation and mark the room as "Busy"
                    if (reservation.editReservation(reservId, guestId, roomNo, dateIn, dateOut) && reservation.setReservRoom(roomNo, "Busy"))
                    {
                        MessageBox.Show("Reservation Updated", "Reservation Updated ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getReservation();
                    }
                    else
                    {
                        MessageBox.Show("Update was not Successful ", "Error Reservation Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Updating Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblExit_MouseLeave(object sender, EventArgs e)
        {
            lblExit.ForeColor = Color.White;
        }

        private void lblExit_MouseEnter(object sender, EventArgs e)
        {
            lblExit.ForeColor = Color.Red;
        }
    }
    // add a foreign key room id of the reservation table (guestid dosen't exists in guest table)
    // connect GuestsID In guests Table to Guestid to Reservation table
    // relation ship room number table and  room number of reservation

}



