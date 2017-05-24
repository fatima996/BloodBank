/**
 * author: Fatima El Halabi
 * 
 */
using System;
using System.Windows.Forms;

namespace WindowsFormsApp8
{
    public partial class Login : Form
    {
        private static string username;
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
  
        public string getUsername()
        {
            return username;
        }
        public void setUsername(string name)
        {
            username = name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            username = textBox1.Text;
            String query = "Select * From dt where Username='" + textBox1.Text + "' and Password='" + textBox2.Text + "' ";
            bool result = EnterDB.ValidQuery(query);

            if (!result)
            {
                this.Hide();
                Main main = new Main();
                main.Show();
            }
            else
            {
                MessageBox.Show("Check pass or username again");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Signup signup = new Signup();
            signup.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
