/**
 * author: Fatima Abdel Monem
*/

using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp8
{
    class EnterDB
    {

        public static void Query(string comand)
        {
            try
            {
                string connectionString = "server=localhost;uid=root;pwd=;database=sql";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand c = new MySqlCommand(comand, conn);
                MySqlDataReader reader = c.ExecuteReader();
                conn.Close();

            }catch(Exception e)
            {
                MessageBox.Show("An Error occured", "Error", MessageBoxButtons.OK);
            }
        }

        public static string StoreQuery(string command)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=sql";
            MySqlConnection conn = new MySqlConnection(connectionString);
            string x;
            conn.Open();
            MySqlCommand c = new MySqlCommand(command, conn);
            x = c.ExecuteScalar().ToString();
            conn.Close();
            return x;

        }

        public static bool ValidQuery(string command)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=sql";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand c = new MySqlCommand(command, conn);
            MySqlDataReader reader = c.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = count + 1;
            }
            conn.Close();
            if (count > 0)
            {
                return false;
            }
            return true;
        }

        public static bool DeleteAccount(string command)
        {

            try
            {
                string connectionString = "server=localhost;uid=root;pwd=;database=sql";
                MySqlConnection conn = new MySqlConnection(connectionString);
                MySqlCommand c = new MySqlCommand(command, conn);
                MySqlDataReader MyReader2;
                conn.Open();
                MyReader2 = c.ExecuteReader();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }

}
