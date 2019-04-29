using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Boundary;
using Entity;

namespace Controller
{

    public class ScheduleController
    {
        SqlConnection connection = new SqlConnection((ZooSystemFinal.Properties.Settings.Default.con));

        public static void Update(int enclosureNo)
        {
            DBConnector dbCon = new DBConnector();
            DBConnector.currentEnclosure = dbCon.GetEnclosure(enclosureNo);

            ScheduleView schedule_view = new ScheduleView();
            schedule_view.Show();

            ScheduleView.Display(DBConnector.currentEnclosure);

        }


        public static void Save(Enclosure enclosure)
        {
            int d = 0;
            int hr = 0;

            foreach (Control c in ScheduleView.schedule_Controls.Controls.OfType<CheckBox>().OrderByDescending(c => c.Name))
            {
                CheckBox checkBox = (CheckBox)c;
                if (checkBox.Checked == true)
                    enclosure.Enclosure_Schedule[hr, d] = true;
                else
                    enclosure.Enclosure_Schedule[hr, d] = false;

                d++;
                if (d == enclosure.Enclosure_Schedule.GetLength(1)) //7 
                {
                    d = 0;
                    if (hr != enclosure.Enclosure_Schedule.GetLength(0) - 1)
                        hr++;
                }
            }//foreach
            DBConnector.SaveChange(enclosure);

            string message = "Changes saved to database successfully.";

            ScheduleView sview = new ScheduleView();
            sview.display(message);
        }

    }
}