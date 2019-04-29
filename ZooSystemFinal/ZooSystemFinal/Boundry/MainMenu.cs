using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity;
using Controller;

namespace Boundary
{
    public partial class MainMenu : Form
    {
        public static int encl_no;
        private EnclosureList EncloseList;
        List<string> encloselist = new List<string>();
        //List<Enclosure> 

        public void display(EnclosureList EnclosList, User useName)
        {
            InitializeComponent();
            
            List<string> nameList = new List<string>();
            DBConnector.currentEncloseList = EnclosList;
            EncloseList=EnclosList;
            foreach(var name in EnclosList)
            {
                nameList.Add(name.Animal_Type);
            }

            EnclosureListBox.DataSource = nameList;

            //show AddEnclosure Button only if user is admin type
            if (DBConnector.theUser.Type.ToLower() != "admin")
                AddEnclosureButton.Visible = false;

            this.Show();
        }//displays enclosure list in list box

        public void open()
        {
            //display main
            //display(DBConnector.currentEncloseList, DBConnector.theUser);


            InitializeComponent();

            List<string> nameList = new List<string>();
            //DBConnector.currentEncloseList = EnclosList;
            //EncloseList = EnclosList;
            foreach (var name in DBConnector.currentEncloseList)
            {
                nameList.Add(name.Animal_Type);
            }

            EnclosureListBox.DataSource = nameList;

            //show AddEnclosure Button only if user is admin type
            if (DBConnector.theUser.Type.ToLower() != "admin")
                AddEnclosureButton.Visible = false;

            this.Show();

        }

        public void close()
        {
            this.Close();
        }

        public void display()
        {
            
            //display(DBConnector.currentEncloseList, DBConnector.theUser);
            InitializeComponent();

            List<string> nameList = new List<string>();
            //DBConnector.currentEncloseList = EnclosList;
            //EncloseList = EnclosList;
            foreach (var name in DBConnector.currentEncloseList)
            {
                nameList.Add(name.Animal_Type);
            }

            EnclosureListBox.DataSource = nameList;

            //show AddEnclosure Button only if user is admin type
            if (DBConnector.theUser.Type.ToLower() != "admin")
                AddEnclosureButton.Visible = false;

            this.Show();

        }

        void EnclosureListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.EnclosureListBox.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                MessageBox.Show(index.ToString());
            }
        }

        private void EnclosureListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            //MessageBox.Show(EnclosureList[EnclosureListBox.SelectedIndex]);
        }

        private void AddEnclosureButton_Click(object sender, EventArgs e)
        {
            addEnclosure();
        }
        public void addEnclosure()
        {
            //MessageBox.Show("Call AddEnclosure");
            EnclosureController encController = new EnclosureController(this);
            encController.addEnclosure();
        }

        //logout
        private void LogoutButton_Click(object sender, EventArgs e)
        {
            select();
            //MessageBox.Show("Call Logout");
        }

        public void select()
        {
            LogoutController logcontrol = new LogoutController();
            this.close();
            /*LogoutForm logoutForm = new LogoutForm();
            logoutForm.FormClosed += (s, args) => this.Close();
            logoutForm.Show();*/
            //System.Threading.Thread.Sleep(5);
            logcontrol.logout(DBConnector.theUser);
           
            //LogoutForm logout2 = new LogoutForm();
            

        }

        //Click to Update Schedules
        private void UpdateSchedule_Click(object sender, EventArgs e)
        {
            encl_no = 1 + EnclosureListBox.SelectedIndex;
            ScheduleController.Update(encl_no);
            this.close();
        }

    }
}
