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
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            
            /**
             * author:Fatima Abdel Monem
             * */
            webBrowser1.Navigate("http://www.aabb.org/tm/donation/Pages/donatefaqs.aspx");
            DateTime localdate = DateTime.Now;
            label11.Text = localdate.ToShortDateString();
            label14.Text = "Date:  " + localdate.ToShortDateString();
        }
        /**
         * author:Fatima Abdel Monem
         * */
        private void fillData()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            string sql = "select username, Rh, bloodgroup from dt";
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
                dv.RowFilter = string.Format("Bloodgroup like '%{0}%'", comboBox1.SelectedItem.ToString());
                dataGridView1.DataSource = dv;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        /**
         * author:Fatima El Halabi
         **/
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string query = "Select * From dt where Username='" + textBox1.Text + "' ";
                string queryupdate = "update dt set username='" + this.textBox1.Text + "' where username='" +login.getUsername() + "' ";

                if (!EnterDB.ValidQuery(query))
                {
                    MessageBox.Show("username taken try another one");
                    textBox1.Text = "";
                }

                else
                {
                    EnterDB.Query(queryupdate);
                    login.setUsername(textBox1.Text);
              
                   MessageBox.Show("saved");
                    
                }
            }
            if (textBox2.Text != "")
            {

                String query = "update dt set fname='" + this.textBox2.Text + "'  where username='" + login.getUsername() + "' ";
                EnterDB.Query(query);
                
            }
            if (textBox3.Text != "")
            {

                String query = "update dt set lname='" + this.textBox3.Text + "'  where username='" + login.getUsername() + "'  ";
                EnterDB.Query(query);
            }
            if (textBox4.Text != "")
            {

                String query = "update dt set password='" + this.textBox4.Text + "'  where username='" + login.getUsername() + "' ";
                EnterDB.Query(query);

            }
            if (textBox5.Text != "")
            {

                String query = "update dt set bloodgroup='" + this.textBox5.Text + "'  where username='" + login.getUsername() + "' ";
                EnterDB.Query(query);
               
            }
            if (textBox6.Text != "")
            {

                String query = "update dt set Address='" + this.textBox6.Text + "'  where username='" + login.getUsername() + "' ";
                EnterDB.Query(query);
               

            }
            if (textBox7.Text != "")
            {

                String query = "update dt set TelNo='" + this.textBox7.Text + "'  where username='" + login.getUsername() + "' ";
                EnterDB.Query(query);
                

            }
            if (textBox8.Text != "")
            {

                String query = "update dt set email='" + this.textBox8.Text + "'  where username='" + login.getUsername() + "' ";
                EnterDB.Query(query);
               
            }
            
                   


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }



        /**
         * author:Fatima El Halabi
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
                    string query1 = "select Bloodgroup from dt where username='" + login.getUsername() + "'";
                    string B =EnterDB.StoreQuery(query1);

                    string query2 = "select Rh from dt where username='" + login.getUsername() + "'";
                    string Rh = EnterDB.StoreQuery(query2);

                    string query3 = "insert into donation (username,blood,Rh) values('" + login.getUsername() + "','" + B + "','" + Convert.ToInt32(Rh) + "')";
                    EnterDB.Query(query3);
                   

                    string E = "select email from dt where username='" + login.getUsername() + "' ";
                    string dbstr = EnterDB.StoreQuery(E);
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
         */
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        /**
         *
         * author:Fatima Abdel Monem
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
           
            string query = "insert into requests (username,bloodgroup,Rh, Emergency) values('" + login.getUsername() + "','" + bg + "','" + Rh + "','" + Emergency + "')";
            EnterDB.Query(query);


            string query2 = "select Blood, rh from donation where blood='" + bg + "' and rh='" + Rh + "'";

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
         * 
         * 
         */
        private void tabPage7_Click(object sender, EventArgs e)
        {

        }
        /**About us page 
         * author:Fatima Abdel Monem
         * 
         * 
         */
        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }
    }

}

