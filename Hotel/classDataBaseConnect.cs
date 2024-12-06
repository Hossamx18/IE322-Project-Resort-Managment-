using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hotel
{
    // Class to manage database connectivity
    class classDataBaseConnect
    {
        // Create an object from mysql library from the class mysqlconnection (connection) 
        private MySqlConnection connection = new MySqlConnection("datasource = ;port=3306;username=;password=;database=");
        //  Method that gives access to the private (connection) object
        public MySqlConnection GetConnection()
        {
            // This allows the user to interact with the database
            return connection;
        }
        // Method to open the connection
        public void OpenCon()
        {
            // Check if the connection is currently closed
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                // Open the connection
                connection.Open();
            }
        }
        // Method to close the connection
        public void CloseCon()
        {
            // Check if the connection is currently open
            if (connection.State == System.Data.ConnectionState.Open)
            {
                // Close the connection
                connection.Close();
            }
        }

    }
}