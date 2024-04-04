using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();  
        }
        private void Login()
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password");
                return;
            }

            string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=test";
            MySqlConnection databaseConnection = new MySqlConnection(MySQLConnectionString);

            try
            {
                databaseConnection.Open();

                // Query to check if username and password match
                string query = "SELECT * FROM user WHERE userName = @username AND Password = @password";
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@username", username);
                commandDatabase.Parameters.AddWithValue("@password", password);

                MySqlDataReader reader = commandDatabase.ExecuteReader();

                if (reader.Read())
                {
                    MessageBox.Show("Login Successful");
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                databaseConnection.Close();
            }
        }
    }
}
