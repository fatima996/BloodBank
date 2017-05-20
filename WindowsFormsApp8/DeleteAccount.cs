using System;
using System.Windows.Forms;
/**
 * a form for deleting account
 * author: Fatima Abdel Monem
 * 
 */ 
namespace WindowsFormsApp8
{
    public partial class DeleteAccount : Form
    {
        public DeleteAccount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            //access database to delete account
            string Query = "drop username '"+ login.getUsername() +"'@'localhost'";
            bool result=EnterDB.DeleteAccount(Query);
            //if successful delete and inform
            if (result) {
                MessageBox.Show("Account successfuly deleted!");
                Environment.Exit(0);
            }
            //not successful inform and close delet form
            //main remains
            else
            {
                MessageBox.Show("An error occured!");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
