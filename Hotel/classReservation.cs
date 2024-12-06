using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;

namespace Hotel
{
    // Class for reservations
    class classReservation
    {
        classDataBaseConnect connect = new classDataBaseConnect();
        // Method to get all rooms of a specific type that are available (for combobox booking)
        public DataTable roomByType(int type)
        {
            // Query to get rooms based on type and availability
            string selectQuery = "SELECT * FROM `room` WHERE `RoomType`=@type AND `RoomStatus`= 'Availabe'";
            MySqlCommand command = new MySqlCommand(selectQuery, connect.GetConnection());
            command.Parameters.Add("@type", MySqlDbType.Int32).Value = type;
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }

        // Method to get the room type (as an integer) by its room number
        public int typeByRoom(string rno)
        {
            // Query to get the room type based on the room number
            string selectQuery = "SELECT `RoomType` FROM `room` WHERE `RoomId`=@rno";
            MySqlCommand command = new MySqlCommand(selectQuery, connect.GetConnection());
            command.Parameters.Add("@rno", MySqlDbType.VarChar).Value = rno;
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(table);
            // Convert the first result to an integer and return it
            return Convert.ToInt32(table.Rows[0][0].ToString());
        }

        // Method room status changed if room is reserved
        public bool setReservRoom(string rno, string sts)
        {
            // Query to update the room status based on room number
            string updateQuery = "UPDATE `room` SET `RoomStatus`=@sts WHERE `RoomId`=@rno";
            MySqlCommand command = new MySqlCommand(updateQuery, connect.GetConnection());

            command.Parameters.Add("@rno", MySqlDbType.VarString).Value = rno;
            command.Parameters.Add("@sts", MySqlDbType.VarString).Value = sts;
            connect.OpenCon();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseCon();
                return true;
            }
            else
            {
                connect.CloseCon();
                return false;
            }
        }
        // Method to get all reservations from table

        public DataTable getReservation()
        {
            // Query to get all reservations
            string selectQuery = "SELECT * FROM `reservation`";
            MySqlCommand command = new MySqlCommand(selectQuery, connect.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }


        // Method to add a new reservation
        public bool addReservation(string guestId, string roomNo, DateTime dateIn, DateTime dateOut)
        {
            // Query to insert a new reservation
            string insertQuerry = "INSERT INTO `reservation`(`GuestId`, `RoomNo`, `DateIn`, `DateOut`) VALUES (@GuestId,@RoomNo,@DateIn,@Dateout)";
            MySqlCommand command = new MySqlCommand(insertQuerry, connect.GetConnection());
            command.Parameters.Add("@GuestId", MySqlDbType.VarChar).Value = guestId;
            command.Parameters.Add("@RoomNo", MySqlDbType.VarChar).Value = roomNo;
            command.Parameters.Add("@DateIn", MySqlDbType.Date).Value = dateIn;
            command.Parameters.Add("@DateOut", MySqlDbType.Date).Value = dateOut;


            connect.OpenCon();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseCon();
                return true;
            }
            else
            {
                connect.CloseCon();
                return false;
            }
        }
        // Method to delete reservation by id
        public bool removeReservation(int id)
        {
            // Query to delete a reservation by id
            string insertQuerry = "DELETE FROM `reservation` WHERE `ReservationId`=@id";
            MySqlCommand command = new MySqlCommand(insertQuerry, connect.GetConnection());
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;

            connect.OpenCon();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseCon();
                return true;
            }
            else
            {
                connect.CloseCon();
                return false;
            }

        }
        // Method to update reservation by id
        public bool editReservation(int reservId, string guestId, string roomNo, DateTime dateIn, DateTime dateOut)
        {
            // Query to update reservation details by id
            string insertQuerry = "UPDATE `reservation` SET `GuestId`=@GuestId,`RoomNo`=@RoomNo,`DateIn`=@DateIn ,`DateOut`=@DateOut WHERE `ReservationId`=@ReservationId";
            MySqlCommand command = new MySqlCommand(insertQuerry, connect.GetConnection());
            command.Parameters.Add("@ReservationId", MySqlDbType.Int32).Value = reservId;
            command.Parameters.Add("@GuestId", MySqlDbType.VarChar).Value = guestId;
            command.Parameters.Add("@RoomNo", MySqlDbType.VarChar).Value = roomNo;
            command.Parameters.Add("@DateIn", MySqlDbType.Date).Value = dateIn;
            command.Parameters.Add("@DateOut", MySqlDbType.Date).Value = dateOut;


            connect.OpenCon();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseCon();
                return true;
            }
            else
            {
                connect.CloseCon();
                return false;
            }
        }
       
    }
}
