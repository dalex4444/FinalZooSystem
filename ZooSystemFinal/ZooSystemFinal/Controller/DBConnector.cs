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
using Entity;
using Boundary;

namespace Controller
{

    class DBConnector
    {
        SqlConnection connection = new SqlConnection((ZooSystemFinal.Properties.Settings.Default.con));

        //shared instance of User 
        public static User theUser { get; } = new User();
        public static Enclosure currentEnclosure = new Enclosure();
        public static EnclosureList currentEncloseList = new EnclosureList();
        public static string SaltValue { get; set; }

        //method to retrieve User
        public User getUser(string uname)
        {
            //retrieve User
            string query = "Select uname, salt, pword, type_of_user from dbo.[User] Where uname = @puname";

            //connect to database
            using (connection)
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    //open connection
                    connection.Open();

                    //create parameter and assign username input into parameter
                    SqlParameter param_uname = new SqlParameter("@puname", SqlDbType.NVarChar, 40);
                    param_uname.Value = uname;
                    command.Parameters.Add(param_uname);

                    //read data and store in variable salt
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            ////get salt value
                            SaltValue = dataReader.GetValue(1).ToString();

                            //username exists
                            if (SaltValue != null)
                            {
                                //get uname
                                theUser.Uname = dataReader["uname"].ToString();
                                //get type of user
                                theUser.Type = dataReader["type_of_user"].ToString();
                                //get salt hashed pword
                                byte[] binaryPass = (byte[])dataReader.GetSqlBinary(2);
                                string bytesPass = BitConverter.ToString(binaryPass);
                                theUser.Pword = bytesPass.Replace("-", string.Empty);
                            }
                            else
                                MessageBox.Show("Username not found.");
                        }
                        dataReader.Close();
                    }

                    command.Parameters.Clear();
                }
                catch
                {
                    MessageBox.Show("Unable to connect to database.");
                    theUser.Uname = "";

                }
                finally //close connection to database
                {
                    command.Dispose();
                    connection.Close();
                }
                return theUser;
            }
        }//end getUser method 

        public void loginSuccess(User user)
        {
            // we need to change the database to have a way to store login/logout success/failure
            // can either add a column to user table, or add a table for authentication attempts
            //store success

            string SessionLogout = "INSERT INTO dbo.[Session] (event, time, uname) VALUES(@event, @time, @uname)";
            SqlParameter sessParam = new SqlParameter();
            sessParam.ParameterName = "@event";
            sessParam.Value = "login success";
            SqlParameter timeParam = new SqlParameter();
            timeParam.ParameterName = "@time";
            timeParam.Value = DateTime.Now.ToString();
            SqlParameter UserParam = new SqlParameter();
            UserParam.ParameterName = "@uname";
            UserParam.Value = user.Uname; ;

            using (SqlCommand SessionLogoutCmd = new SqlCommand(SessionLogout, connection))
            {
                connection.Open();
                SessionLogoutCmd.Parameters.Add(sessParam);
                SessionLogoutCmd.Parameters.Add(timeParam);
                SessionLogoutCmd.Parameters.Add(UserParam);
                SessionLogoutCmd.ExecuteNonQuery();
                connection.Close();
            }



        }

        public void loginFailure(User user)
        {
            try
            {
                //store failure
                string SessionLogout = "INSERT INTO dbo.[Session] (event, time, uname) VALUES(@event, @time, @uname)";
                SqlParameter sessParam = new SqlParameter();
                sessParam.ParameterName = "@event";
                sessParam.Value = "login failure";
                SqlParameter timeParam = new SqlParameter();
                timeParam.ParameterName = "@time";
                timeParam.Value = DateTime.Now.ToString();
                SqlParameter UserParam = new SqlParameter();
                UserParam.ParameterName = "@uname";
                UserParam.Value = user.Uname; ;

                using (SqlCommand SessionLogoutCmd = new SqlCommand(SessionLogout, connection))
                {
                    connection.Open();
                    SessionLogoutCmd.Parameters.Add(sessParam);
                    SessionLogoutCmd.Parameters.Add(timeParam);
                    SessionLogoutCmd.Parameters.Add(UserParam);
                    SessionLogoutCmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch
            {
                //MessageBox.Show("No Connection");
            }
        }

        public EnclosureList getEnclosureList()
        {
            connection.Open();
            EnclosureList encList = new EnclosureList();

            //SqlParameter 
            string getEnclosures = "Select * from dbo.[Enclosure]";
            SqlCommand getEnclosuresCmd = new SqlCommand(getEnclosures, connection);
            using (SqlDataReader reader = getEnclosuresCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Enclosure tmpEnclosure = new Enclosure();
                    tmpEnclosure.EnclosureNo = (int)reader["EnclosureNo"];
                    tmpEnclosure.Animal_Type = (string)reader["animal_type"];
                    tmpEnclosure.Num_Of_Animal = (int)reader["num_of_animal"];
                    //tmpEnclosure.Schedule = (int)reader["schedule_no"];
                    encList.Add(tmpEnclosure);
                }

            }

            connection.Close();
            return encList;
        }

        public Enclosure GetEnclosure(int enclosureNo)
        {
            connection.Open();
            Enclosure requestedEnclosure = new Enclosure();
            requestedEnclosure.EnclosureNo = enclosureNo;
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@EnclosureNo";
            param.Value = enclosureNo;

            string getEnclosure = "Select * from dbo.[Enclosure] Where EnclosureNo = @EnclosureNo";

            SqlCommand getEnclosureCmd = new SqlCommand(getEnclosure, connection);
            getEnclosureCmd.Parameters.Add(param);

            using (SqlDataReader reader = getEnclosureCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    requestedEnclosure.Animal_Type = reader["animal_type"].ToString();
                    requestedEnclosure.Num_Of_Animal = (int)reader["num_of_animal"];
                }
                reader.Close();
            }

            getEnclosureCmd.Parameters.Clear();
            getEnclosureCmd.Dispose();
            connection.Close();
            return requestedEnclosure;
        }


        public static void SaveChange(Enclosure enclosure)
        {
            int d = 0;
            int hr = 0;

            using (SqlConnection connection = new SqlConnection(ZooSystemFinal.Properties.Settings.Default.con))
            {
                //insert command using stored procedure
                SqlCommand cmdInsert = new SqlCommand("dbo.SaveSchedule", connection);

                //insert parameters for EnclosureNo, day_of_week, hour_of_week
                SqlParameter p_enclosureNo = new SqlParameter("@paramEnclosureNo", SqlDbType.Int);
                p_enclosureNo.Value = enclosure.EnclosureNo;
                SqlParameter p_day = new SqlParameter("@param_day", SqlDbType.Int);
                p_day.Value = d;
                SqlParameter p_hour = new SqlParameter("@param_hour", SqlDbType.Int);
                p_hour.Value = hr;

                //add parameters to insert command
                cmdInsert.Parameters.Add(p_enclosureNo);
                cmdInsert.Parameters.Add(p_day);
                cmdInsert.Parameters.Add(p_hour);
                cmdInsert.CommandType = CommandType.StoredProcedure;

                //query to delete 
                string delete = "DELETE FROM dbo.Schedule WHERE enclosureNo = @pEnclosureNo AND day_of_week = @pday AND hour_of_day = @phour";

                //delete parameters
                SqlCommand cmdDelete = new SqlCommand(delete, connection);
                SqlParameter pm_enclosureNo = new SqlParameter("@pEnclosureNo", SqlDbType.Int);
                pm_enclosureNo.Value = enclosure.EnclosureNo;
                SqlParameter pm_day = new SqlParameter("@pday", SqlDbType.Int);
                pm_day.Value = d;
                SqlParameter pm_hour = new SqlParameter("@phour", SqlDbType.Int);
                pm_hour.Value = hr;

                //add parameters to delete command
                cmdDelete.Parameters.Add(pm_enclosureNo);
                cmdDelete.Parameters.Add(pm_day);
                cmdDelete.Parameters.Add(pm_hour);
                cmdDelete.CommandType = CommandType.Text;

                //open database connection
                connection.Open();

                //loop through 2d array and insert/delete in Schedule table
                for (int i = 0; i < enclosure.Enclosure_Schedule.GetLength(0); i++)
                    for (int j = 0; j < enclosure.Enclosure_Schedule.GetLength(1); j++)
                    {
                        hr = i + 8;
                        d = j + 1;

                        //boxes are checked
                        if (enclosure.Enclosure_Schedule[i, j] == true)
                        {
                            //execute stored procedure to insert records if not exists in table
                            p_day.Value = d;
                            p_hour.Value = hr;
                            cmdInsert.ExecuteNonQuery();

                        }
                        else //enclosure.Enclosure_Schedule[i, j] == false = checkboxes not checked
                        {
                            //execute query to delete any record that matches
                            pm_day.Value = d;
                            pm_hour.Value = hr;
                            cmdDelete.ExecuteNonQuery();
                        }
                    }

                //clean parameters and commands
                cmdInsert.Parameters.Clear();
                cmdInsert.Dispose();
                cmdDelete.Parameters.Clear();
                cmdDelete.Dispose();
                connection.Close();
            }
        }//end method to save change to schedule

        public void addEnclosure(Enclosure newEnclosure)
        {
            string addEnclosure = "INSERT INTO dbo.[Enclosure] (animal_type, num_of_animal) VALUES(@animal_type, @num_of_animal)";
            SqlParameter animalTypeParam = new SqlParameter();
            animalTypeParam.ParameterName = "@animal_type";
            animalTypeParam.Value = newEnclosure.Animal_Type;
            SqlParameter numAnimalParam = new SqlParameter();
            numAnimalParam.ParameterName = "@num_of_animal";
            numAnimalParam.Value = newEnclosure.Num_Of_Animal;
            //SqlParameter scheduleParam = new SqlParameter();
            //scheduleParam.ParameterName = "@schedule_no";
            //scheduleParam.Value = theEnclosure.Schedule;
            using (SqlCommand addEnclosureCmd = new SqlCommand(addEnclosure, connection))
            {
                connection.Open();
                addEnclosureCmd.Parameters.Add(animalTypeParam);
                addEnclosureCmd.Parameters.Add(numAnimalParam);
                //addEnclosureCmd.Parameters.Add(scheduleParam);
                addEnclosureCmd.ExecuteNonQuery();
                connection.Close();
            }

            currentEncloseList.Add(newEnclosure);
        }

        public void logout(User user)
        {
            string SessionLogout = "INSERT INTO dbo.[Session] (event, time, uname) VALUES(@event, @time, @uname)";
            SqlParameter sessParam = new SqlParameter();
            sessParam.ParameterName = "@event";
            sessParam.Value = "logout";
            SqlParameter timeParam = new SqlParameter();
            timeParam.ParameterName = "@time";
            timeParam.Value = DateTime.Now.ToString();
            SqlParameter UserParam = new SqlParameter();
            UserParam.ParameterName = "@uname";
            UserParam.Value = user.Uname; ;

            using (SqlCommand SessionLogoutCmd = new SqlCommand(SessionLogout, connection))
            {
                connection.Open();
                SessionLogoutCmd.Parameters.Add(sessParam);
                SessionLogoutCmd.Parameters.Add(timeParam);
                SessionLogoutCmd.Parameters.Add(UserParam);
                SessionLogoutCmd.ExecuteNonQuery();
                connection.Close();
            }
            //store logout info for user
        }
    }//end class
}