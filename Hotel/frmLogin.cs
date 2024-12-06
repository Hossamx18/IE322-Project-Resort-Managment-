using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace Hotel
{
    public partial class frmLogin : Form
    {
        classDataBaseConnect connect = new classDataBaseConnect();
        public frmLogin()
        {
            InitializeComponent();
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

        // Method for login (guna button)
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Checks if the username and password textboxs are empty 
            if (txtUsername.Text.Trim().Equals("") || txtPassword.Text == "")
            {
                MessageBox.Show("Enter Your Username and Password to login", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Prepare a datatable to hold query results
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                string selectquery = "SELECT * FROM `users` WHERE `username` = @user AND `password` = @pass";
                MySqlCommand command = new MySqlCommand(selectquery, connect.GetConnection());

                command.Parameters.Add("@user", MySqlDbType.VarChar).Value = txtUsername.Text;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = txtPassword.Text;
                adapter.SelectCommand = command;
                adapter.Fill(table);

                // Check if the username and password are in the table
                if (table.Rows.Count > 0)
                {
                    this.Hide();
                    frmMain frmmain = new frmMain();
                    frmmain.Show();
                }
                // If no matching user is found show an error message
                else
                {
                     MessageBox.Show("Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }