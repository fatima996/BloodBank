using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp8
{
    class EnterDB
    {
        public static void Query( string comand)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=sql";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand command = new MySqlCommand(comand, conn);
            MySqlDataReader reader = command.ExecuteReader();
            conn.Close();
        }
    }
}
