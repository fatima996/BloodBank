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
    public partial class Main : Form
    {
        static string connectionString = "server=localhost;uid=root;pwd=;database=sql";
        MySqlConnection conn = new MySqlConnection(connectionString);
        DataTable dt = new DataTable();
        Login login = new Login();
        public Main()
        {
            InitializeComponent();
        }

        public void Main_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.aabb.org/tm/donation/Pages/donatefaqs.aspx");
            fillData();
            DateTime localdate = DateTime.Now;
            label11.Text = localdate.ToShortDateString();
            label14.Text = "Date:  "+localdate.ToShortDateString();

        }

        private void fillData()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            string sql = "select username, address, bloodgroup from dt";
            MySqlCommand command = new MySqlCommand(sql, conn);
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataView dv = new DataView(dt);
            if (comboBox1.SelectedItem.ToString() == "All")
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dv.RowFilter = string.Format("Blood like '%{0}%'", comboBox1.SelectedItem.ToString());
                // dv.RowFilter = string.Format("Rh like '%{0}%'", comboBox2.SelectedItem.ToString());
                dataGridView1.DataSource = dv;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /**
         * Edit username 
         *checks if username is taken
         **/
        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=sql";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                if (textBox1.Text != "")
                {

                    conn.Open();
                    String querycheck = "Select * From dt where Username='" + textBox1.Text + "' ";

                    MySqlCommand command = new MySqlCommand(querycheck, conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        count = count + 1;
                    }
                    if (count > 0)
                    {
                        MessageBox.Show("username taken! Try another one");
                    }
                   
                }
                /**Edit User Data
                 * Retrieve data from class EnterDB
                 */

                if (textBox2.Text != "")
                {
                    String query = "update dt set fname='" + this.textBox2.Text + "'";
                    EnterDB.Query(query);
                }
                if (textBox3.Text != "")
                {

                    String query = "update dt set lname='" + this.textBox3.Text + "'";
                    EnterDB.Query(query);


                }
                if (textBox4.Text != "")
                {
                    String query = "update dt set password='" + this.textBox4.Text + "'";
                    EnterDB.Query(query);

                }
                if (textBox5.Text != "")
                {

                    String query = "update blood set blood='" + this.textBox5.Text + "'";
                    EnterDB.Query(query);

                }
                if (textBox6.Text != "")
                {

                    String query = "update dt set Address='" + this.textBox6.Text + "'";
                    EnterDB.Query(query);


                }
                if (textBox7.Text != "")
                {

                    String query = "update dt set TelNo='" + this.textBox7.Text + "'";
                    EnterDB.Query(query);

                }
                if (textBox8.Text != "")
                {
                    String query = "update dt set email='" + this.textBox8.Text + "'";
                    EnterDB.Query(query);

                }

                MessageBox.Show("Saved");
               

            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

    
 
        /**
         * check donation conditions
         */
        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox9.Text) < 18 || Convert.ToInt32(textBox9.Text) > 60)
            {
                MessageBox.Show("You can NOT Donate!");
            }
            if (Convert.ToInt32(textBox10.Text) < 55)
            {
                MessageBox.Show("You can NOT Donate!");
            }
            if (radioButton2.Checked == true)
            {
                MessageBox.Show("You can NOT Donate!");
            }
            else
            {
                string  subject, body;
                bool result;
                

                string query = "select email from dt where username='" + login.getUsername() + "' ";
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(query, conn);
                adapter.SelectCommand = command;


                subject = "Donor Avaliable";
                body = "You have had sent a Request to Donate blood. " +
                       "Thank you for saving lives." +
                       "We will inform you as soon as an emergrnt is found :)";
                result = Email.SendEmail(adapter.ToString(), subject, body);

                if (result == true)
                {
                    MessageBox.Show("Donation request sent");
                }
                if (result == false)
                {
                    MessageBox.Show("Error");
                }

            }

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        /**
         * request blood
         */

        private void button3_Click(object sender, EventArgs e)
        {
            int Emergency=0, Rh=0;
            string bg="",date="";
            if (radioButton3.Checked == true)
            {
                Emergency = 1;
            }
            else if(radioButton4.Checked==true)
            {
                Emergency = 0;
            }
            if (radioButton9.Checked == true)
            {
                Rh = 1;
            }
            else if (radioButton10.Checked == true)
            {
                Rh = 0;
            }
            if (radioButton5.Checked == true)
            {
                bg = "A";
            }
            if (radioButton6.Checked == true)
            {
                bg = "B";
            }
            if (radioButton7.Checked == true)
            {
                bg = "AB";
            }
            if (radioButton8.Checked == true)
            {
                bg = "O";
            }

            string query = "insert into request (username,bloodgroup,Rh, Emergency, Date) values('"+login.getUsername()+"','"+bg+"','"+Rh+"','"+Emergency+"','"+label11.Text+"')";
            EnterDB.Query(query);
        }   
    }

}

