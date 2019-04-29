using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Entity;
using Controller;

namespace Boundary
{
    //methods need
    //display(Enclosure): void
    //select(): void
    //save(): void
    //close(): void
    //acknowledge():void

    public partial class ScheduleView : Form
    {
        public static TableLayoutPanel schedule_Controls;

        public ScheduleView()
        {
            InitializeComponent();
            schedule_Controls = this.tableLayoutPanel1;
        }

        //display Enclosure's schedule
        public static void Display(Enclosure enclosure) 
        {
            //retrieve schedule
            string query_schedule = "Select day_of_week, hour_of_day from dbo.[Schedule] Where EnclosureNo = @penclosureNo";

            //connect to database
            using (SqlConnection connection = new SqlConnection(ZooSystemFinal.Properties.Settings.Default.con))
            using (SqlCommand command = new SqlCommand(query_schedule, connection))
            {
                try
                {
                    //open connection
                    connection.Open();
                    SqlParameter parameterDisplay = new SqlParameter();
                    parameterDisplay.ParameterName = "@penclosureNo";
                    parameterDisplay.Value = enclosure.EnclosureNo;
                    command.Parameters.Add(parameterDisplay);

                    //create instances of datatable and adapter to fill datatable
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dataTable);
                    
                    //columns of table are [day_of_week] and [hour_of_day]
                    //number of rows in the datable
                    int num_rows = dataTable.Rows.Count;

                    //interate each row in dataTable and assign it to 2d array of bools
                    for(int i = 0; i < num_rows; i++)
                    {
                        int day = (int)dataTable.Rows[i]["day_of_week"];
                        int hour = (int)dataTable.Rows[i]["hour_of_day"];

                        //day 1-7 and hours 8-17 (military hr)
                        //monday: 1 [0], tuesday: 2 [1], wednesday: 3 [2], thursday: 4 [3], friday: 5 [4]
                        //saturday: 6 [5], sunday: 7 [6]
                        //8AM: 8 [0], 9AM: 9 [1], 10AM: 10 [2], 11AM: 11 [3], 12PM: 12 [4]
                        //1PM: 13 [5], 2PM: 14 [6], 3PM: 15 [7], 4PM: 16 [8], 5PM: 17 [9]
                        
                        if (day >= 1 && day <= 7  && hour >= 8 && hour <= 17)
                        {
                            enclosure.Enclosure_Schedule[hour - 8, day - 1] = true;
                        }
                    } //end loop for 2d array of bools

                    //fill TableLayoutPabel with checkboxes (checked or unchecked) to match 2d bool array
                    int d = 0; //row
                    int hr = 0; //column

                    //loop through checkboxes ordered from left -> right and top -> bottom
                    foreach (Control c in schedule_Controls.Controls.OfType<CheckBox>().OrderByDescending(c => c.Name))
                    {
                        //if day_of_week and hour_of_day exist in database at specified position, then check checkbox
                        if (enclosure.Enclosure_Schedule[hr, d] == true)
                        {
                            CheckBox checkBox = (CheckBox)c;
                            checkBox.Checked = true;
                        }
                        //if day_of_week and hour_of_day do not exist in database, uncheck checkbox
                        else
                        {
                            CheckBox checkBox = (CheckBox)c;
                            checkBox.Checked = false;
                        }

                        //updating to the next day of the week (columns), Mon -> Tues ... -> Sun
                        d++; 
                        if (d == enclosure.Enclosure_Schedule.GetLength(1)) // day = 7 (Sunday is done)
                        {
                            d = 0; //go back to Monday and move to next row (hour_of_day)
                            if (hr != enclosure.Enclosure_Schedule.GetLength(0) - 1) //hour != 9 != 5PM
                                hr++; //continue moving to next hour until 5PM is done
                        }
                    }//foreach for checking checkboxes

                    DBConnector.currentEnclosure = enclosure;
                    command.Parameters.Clear();
                    dataTable.Clear();
                }
                catch
                {
                    MessageBox.Show("Unable to connect to database.");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }//end Display method

        public void display(string msg)
        {
            MessageBox.Show(msg);
            //user clicks OK on box to acknowledge()

            close();
        }
        private void Save()
        {
            ScheduleController.Save(DBConnector.currentEnclosure);
        }

        public void close()
        {
            this.Hide();
            this.Close();
        }

        private void home_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Closed += (s, args) => this.Close();
            mainMenu.display();
        }

        private void ScheduleView_Load(object sender, EventArgs e)
        {

        }

        //save()
        private void save_button_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void cb70_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
