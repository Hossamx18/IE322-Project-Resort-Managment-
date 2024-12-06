using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Mysqlx.Crud;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace Hotel
{
    // Class for adding,updating,removing guests and getting guest informaiton
    class classGuest
    {
        // Object to connect with the database class
        classDataBaseConnect connect = new classDataBaseConnect();
        // Method to insert new guest 
        public bool insertGuest(string id, string firstname, string lastname, string phone, string city)
        {
            // SQL query to insert a new record into the guests table
            string insertQuerry = "INSERT INTO `guests`(`GuestID`, `GuestFirstName`, `GuestLastName`, `GuestPhone`, `GuestCity`) VALUES(@id,@firstname,@lastname,@phone,@city)";
            //  Object with the query and database connection
            MySqlCommand command = new MySqlCommand(insertQuerry, connect.GetConnection());
            // Assign values to the parameters in the SQL query
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            command.Parameters.Add("firstname", MySqlDbType.VarChar).Value = firstname;
            command.Parameters.Add("lastname", MySqlDbType.VarChar).Value = lastname;
            command.Parameters.Add("phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("city", MySqlDbType.VarChar).Value = city;

            // Open the database connection execute the query and close the connection
            connect.OpenCon();
            // Check if exactly one record was inserted
            if (command.ExecuteNonQuery() == 1)
            {
                // If yes
                connect.CloseCon();
                return true;  
            }
            else
            {
                // If no
                connect.CloseCon();
                return false;
            }
        }
        // Method to update guests information by id
        public bool editGuest(string id, string firstname, string lastname, string phone, string city)
        {
            // SQL query to update a guests information based on their id
            string editQuerry = "UPDATE `guests` SET `GuestFirstName` = @firstname, `GuestLastName` = @lastname, `GuestPhone` = @phone, `GuestCity` = @city WHERE `GuestID` = @id;";
            MySqlCommand command = new MySqlCommand(editQuerry, connect.GetConnection());
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            command.Parameters.Add("@firstname", MySqlDbType.VarChar).Value = firstname;
            command.Parameters.Add("@lastname", MySqlDbType.VarChar).Value = lastname;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@city", MySqlDbType.VarChar).Value = city;

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
        //Method to delete guest by id
        public bool removeGuest(string id)
            {
            // SQL query to delete a guest's record based on their id
            string insertQuerry = "DELETE FROM `guests` WHERE `GuestID`=@id";
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
        // Method to get the guest list from the database guest table
        public DataTable getGuest()
        {
            // SQL query to select all records from the guests table
            string selectQuery = "SELECT * FROM `guests`";
            MySqlCommand command = new MySqlCommand(selectQuery, connect.GetConnection());
            // Create a MySqlDataAdapter to execute the query and fill a datatable with the results
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            // Fill the datatable with the query results
            adapter.Fill(table);
            // Return the filled datatable
            return table;
        }
    }

    }


