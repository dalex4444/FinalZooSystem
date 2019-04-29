using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
using Controller;

namespace Boundary
{
    public partial class LoginForm : Form
    {


        public LoginForm()
        {
            InitializeComponent();
        }

        //open application
        public static void Main(string[] args)
        {
            Application.Run(new LoginForm());
            
        }

        public static void Open()
        {
            LoginForm logForm = new LoginForm();
            logForm.Show();
        }




        private void Login_Button_Click(object sender, EventArgs e)
        {
            Submit();
        }//login button clicked

        private void Submit()
        {
            //assign user inputs to local variables
            string user = unameTextbox.Text;
            string pass = pwordTextbox.Text;

            //empty username or password input
            if (user == "" || user == null || pass == "" || pass == null)
            {
                MessageBox.Show("Please enter username and password.");
            }

            //inputs are not empty
            else
            {
                LoginController.Submit(user, pass);

                //clear textbox fields after done using them
                unameTextbox.Text = String.Empty;
                pwordTextbox.Text = String.Empty;
            }

        }//end Submit method

        private void LoginForm_Load(object sender, EventArgs e)
        {


        }
    }
}

