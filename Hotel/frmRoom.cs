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
    public partial class frmRoom : Form
    {
        // Instance of Room Class
        classRoom room = new classRoom();

        public frmRoom()
        {
            InitializeComponent();

        }
        // Put data from room table to datagridview
        private void getRoomList()
        {
            DataGridView_room.DataSource = room.getRoomList();
        }
        private void frmRoom_Load(object sender, EventArgs e)
        {

            //  Show room type in combo box
            comboBox_RoomType.DataSource = room.getRoomType();
            comboBox_RoomType.DisplayMember = "Label";
            comboBox_RoomType.ValueMember = "CategoryId";

           // Refersh data 
            getRoomList();
        }

        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get data from textboxes, combo box, radiobutton
            string no = txtID.Text;
            int type = Convert.ToInt16(comboBox_RoomType.SelectedValue.ToString());
            string ph = txtPhone.Text;
            string status = rbtnAvailabe.Checked ? "Availabe" : "Busy";

            try
            {
                // Attempt to add the room using the `addRoom` method
                if (room.addRoom(no, type, ph, status))
                {
                    MessageBox.Show("Room Added Sucessfully", "Add Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getRoomList();
                    btnClean.PerformClick();
                }
                else
                {
                    MessageBox.Show("Room Not Added", "Add Room", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            comboBox_RoomType.SelectedIndex = 0;
            txtPhone.Clear();

        }
   
       
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get data from textboxes, combo box, radiobutton
            string no = txtID.Text;
            int type = Convert.ToInt16(comboBox_RoomType.SelectedValue.ToString());
            string ph = txtPhone.Text;
            string status = rbtnAvailabe.Checked ? "Availabe" : "Busy";

            try
            { 
            
                if (room.editRoom(no, type, ph, status))
                {
                    MessageBox.Show("Room Update Sucessfully", "Update Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getRoomList();
                    btnClean.PerformClick();
                }
                else
                {
                    MessageBox.Show("Room Not Added", "Update Room", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // When selected it shows in text boxes/combo/rdbtn
        private void DataGridView_room_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = DataGridView_room.CurrentRow.Cells[0].Value.ToString();
            comboBox_RoomType.SelectedValue = DataGridView_room.CurrentRow.Cells[1].Value.ToString();
            txtPhone.Text = DataGridView_room.CurrentRow.Cells[2].Value.ToString();
            string rButton = DataGridView_room.CurrentRow.Cells[3].Value.ToString();
            if (rButton.Equals("Availabe"))
            {
                rbtnAvailabe.Checked = true;
            }
            else
            {
                rbtnBusy.Checked = true;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                // If id is empty
                MessageBox.Show("Required Field - Room Number", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string id = txtID.Text;
                    Boolean deleteGuest = room.removeRoom(id);
                    if (deleteGuest)
                    {
                        MessageBox.Show("Room  Data Removed uccessfully", "Room Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getRoomList();
                        btnClean.PerformClick();
                    }


                    else
                    {
                        MessageBox.Show("ERROR Room not Removed", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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
