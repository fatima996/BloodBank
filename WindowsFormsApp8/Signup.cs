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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=sql";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                String query = "Select * From dt where Username='" + textBox1.Text + "' ";

                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = count + 1;
                }
                if (count == 0)
                {
                    label4.Text = textBox1.Text + " is Available";
                    this.label4.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    label4.Text = textBox1.Text + " is not Available";
                    this.label4.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=sql";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                String query = "Insert into dt (username,password,fname,lname,Address,TelNo,email,Bloodgroup) Values( '" + textBox1.Text + "','" + textBox3.Text + "','" + textBox10.Text + "','" + textBox9.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox4.Text + "') ";


                if (label4.ForeColor == System.Drawing.Color.Green)
                {
                    if (textBox2.Text == textBox3.Text)
                    {

                        if (textBox4.Text != "")
                        {

                            MySqlCommand command = new MySqlCommand(query, conn);
                            MySqlDataReader reader = command.ExecuteReader();
                            MessageBox.Show("Successfully Signed up!");
                            conn.Close();
                        }

                        else
                        {
                            MessageBox.Show("must insert your Blood group");
                        }
                    }

                    else
                    {
                        MessageBox.Show("your passwords do not match");
                    }
                }
                else
                {
                    MessageBox.Show("Try another username");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=sql";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                String query = "Select * From dt where Username='" + textBox1.Text + "' ";

                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = count + 1;
                }
                if (count == 0)
                {
                    label4.Text = textBox1.Text + " is Available";
                    this.label4.ForeColor = System.Drawing.Color.Green;
                }
                else if (count > 0)
                {
                    label4.Text = textBox1.Text + " is not Available";
                    this.label4.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}