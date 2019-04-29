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
using Controller;

namespace Controller
{
    public class EnclosureController
    {
        private Enclosure newEnclosure;
        private Boundary.MainMenu currentMenu;
        public EnclosureController(Boundary.MainMenu menu)
        {
            newEnclosure = new Enclosure();
            currentMenu = menu;
        }
        public void addEnclosure()
        {
            AddEnclosureForm addEncForm = new AddEnclosureForm(this);
            addEncForm.Display(newEnclosure);

        }

        public void saveEnclosure(Enclosure enclosureToSave)
        {
            DBConnector dbConnect = new DBConnector();
            dbConnect.addEnclosure(enclosureToSave);

            AddEnclosureForm temp = new AddEnclosureForm(this);
            temp.displaySuccess();

            var timer = new System.Windows.Forms.Timer();
            timer.Tick += (s, e) =>
            {
                ((System.Windows.Forms.Timer)s).Stop(); //s is the Timer
                currentMenu.Close();
                Boundary.MainMenu mainMenu = new Boundary.MainMenu();

                mainMenu.open();
            };
            timer.Interval = 2000;
            timer.Start();

        }
    }
}