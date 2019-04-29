using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using Entity;
using Boundary;

namespace Controller
{
    class LoginController
    {


        //submit username and password 
        public static void Submit(string user, string pass)
        {
            //get User from the database through DBConnector
            DBConnector db = new DBConnector();
            User currentUser = db.getUser(user);

            //verify user's inputs
            bool validUser = LoginController.Verify(currentUser, pass);

            DBConnector dbcon1 = new DBConnector();
            if (validUser)
            {
                //save login session to database

                dbcon1.loginSuccess(currentUser);

                //open mainmenu
                LoginForm login = new LoginForm();
                login.Hide();

                EnclosureList enclosures = new EnclosureList();
                enclosures = dbcon1.getEnclosureList();

                Boundary.MainMenu main_menu = new Boundary.MainMenu();
                main_menu.FormClosed += (s, args) => login.Close();
                main_menu.display(enclosures, currentUser);
                //main_menu.Show();
                
            }
            else
            {
                MessageBox.Show("Invalid credentials.");
                dbcon1.loginFailure(DBConnector.theUser);
            }

        }

        //verify input password
        public static bool Verify(User theUser, string pass)
        {
            bool result;
            int returnValue = 0;

            using (SqlConnection connection = new SqlConnection(ZooSystemFinal.Properties.Settings.Default.con))
            using (SqlCommand cmd = new SqlCommand("dbo.VerifyUser", connection))
            {
                try
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    //parameters for uname, pass, return output for stored procedure
                    SqlParameter p_uname = new SqlParameter("@paramuname", SqlDbType.NVarChar, 50);
                    p_uname.Value = theUser.Uname;
                    SqlParameter p_pass = new SqlParameter("@paraminput", SqlDbType.NVarChar, 50);
                    p_pass.Value = pass;
                    cmd.Parameters.Add(p_uname);
                    cmd.Parameters.Add(p_pass);
                    SqlParameter returnValid = new SqlParameter("@valid", SqlDbType.Int);
                    returnValid.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(returnValid);

                    //execute stored procedure
                    cmd.ExecuteNonQuery();

                    //get return value from stored procedure
                    returnValue = (int)cmd.Parameters["@valid"].Value;

                    cmd.Parameters.Clear();
                }
                catch
                {
                    MessageBox.Show("Unable to connect to database.");
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }

            //input password matched hashed password
            if (returnValue == 1)
                result = true;
            //input password did not match hashed password
            else
                result = false;
            return result;
        }//method Verify
    }//class
}//namespace