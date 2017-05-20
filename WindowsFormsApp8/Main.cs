/**
 * authors:
 * 1)Fatima Abdel Monem
 * 2)Fatima El Halabi
 * here we can say that we used pair programming
 * to connect some of the parts 
 * 
 */
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

/**main Applicattion
 * the main user interface
 */
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
            fillData();

            /**
             * getting local date for donation details
             * author:Fatima Abdel Monem
             * */
            webBrowser1.Navigate("http://www.aabb.org/tm/donation/Pages/donatefaqs.aspx");
            DateTime localdate = DateTime.Now;
            label11.Text = localdate.ToShortDateString();
            label14.Text = "Date:  " + localdate.ToShortDateString();
        }
        /**
         * data for the search 
         * fills the grid view on the the main page of the form
         * author:Fatima Abdel Monem
         * */
        private void fillData()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            string sql = "select username, address, bloodgroup from dt";
            MySqlCommand command = new MySqlCommand(sql, conn);
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        /**
         * show the avaliable blood and donators in storage
         */
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
         * Edit user data 
         *uses EnterDB to access the mysql data base
         * author:Fatima El Halabi
         * thinking of adding a for loop to reduce lines
         **/
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string query = "Select * From dt where Username='" + textBox1.Text + "' ";
                string queryupdate = "update dt set username='" + this.textBox1.Text + "'";

                if (!EnterDB.ValidQuery(query))
                {
                    MessageBox.Show("username taken try another one");
                }
                else
                {
                    EnterDB.Query(queryupdate);
                }
            }
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }



        /**
         * Donation page of form
         * 
         * author:Fatima El Halabi
         *check donation conditions like age weight and last donation
         * if satisfied send email to user to inform 
         * if not a messagebox appears to say user cannot donate
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
                try
                {
                    string c = "select email from dt where username='" + login.getUsername() + "' ";
                    string dbstr = EnterDB.StoreQuery(c);
                    bool result = Email.SendEmail(dbstr);
                    if (result == true)
                    {
                        MessageBox.Show("Donation request sent");
                    }
                    if (result == false)
                    {
                        MessageBox.Show("Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        /**
         * author:Fatima El Halabi
         * Used webBrowser1 for FAQ 
         * also used builtin function Navigate
         */
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        /**
         * request blood
         * author:Fatima Abdel Monem
         * checks the main information for a donation request
         * if blood avliable user will be told his blood requested is avaliable
         * else he/she will be informed if avaliable via sms for the place to donate
         */

        private void button3_Click(object sender, EventArgs e)
        {
            int Emergency = 0, Rh = 0;
            string bg = "";
            if (radioButton3.Checked == true)
            {
                Emergency = 1;
            }
            else if (radioButton4.Checked == true)
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
            //stroe the data to database
            string query = "insert into requests (username,bloodgroup,Rh, Emergency) values('" + login.getUsername() + "','" + bg + "','" + Rh + "','" + Emergency + "')";
            EnterDB.Query(query);

            //check if the data is avaliable in datavase 
            string query2 = "select Blood, rh from donation where blood='" + bg + "' and rh='" + Rh + "'";

            //if avaliable pop a message that is avaliable and send sms for the meeting place
            //else a message is shown that no blood as requested exist
            if (!EnterDB.ValidQuery(query2))
            {
                MessageBox.Show("The blood you requested is Avaliable, we will contact you to inform you where to get it.");
                string query3 = "select TelNo from dt where username='" + login.getUsername() + "' ";
                string telno = EnterDB.StoreQuery(query3);
                if (telno !=" ")
                {
                    SMS.SmsSend(telno);
                }
            }
            else
            {
                MessageBox.Show("The blood you requested is not avaliable, as soon it is we will contact you");
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DeleteAccount delAcc = new DeleteAccount();
            delAcc.Show();
        }
        /**
         * Conatct us page 
         * author:Fatima Fatima El Halabi
         * contain info for users to reach us for any issue
         * 
         */
        private void tabPage7_Click(object sender, EventArgs e)
        {

        }
        /**About us page 
         * author:Fatima Abdel Monem
         * contains main info about us
         * 
         */
        private void tabPage5_Click(object sender, EventArgs e)
        {

        }
    }

}

