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
    public partial class AddEnclosureForm : Form
    {
        private Enclosure newEnclosure;
        private EnclosureController encController;
        public AddEnclosureForm(EnclosureController controller)
        {
            encController = controller;
        }

        public void Display(Enclosure enc)
        {
            InitializeComponent();
            newEnclosure = enc;
            this.Show();
        }

        public void displaySuccess()
        {
            SuccessMessage MessSuccess = new SuccessMessage();
            MessSuccess.Show();
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += (s, e) =>
            {
                ((System.Windows.Forms.Timer)s).Stop(); //s is the Timer
                MessSuccess.Close();

            };
            timer.Interval = 2000;
            timer.Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            submit();
        }

        public void submit()
        {
            if (String.IsNullOrEmpty(AnimalTypeTextbox.Text))
            {
                label1.ForeColor = Color.Red;
            }

            else
            {
                try
                {
                    newEnclosure.Animal_Type = AnimalTypeTextbox.Text;
                    newEnclosure.Num_Of_Animal = int.Parse(AnimalNumberTextbox.Text);
                    encController.saveEnclosure(newEnclosure);
                    this.Close();
                }
                catch
                {

                }

            }
        }

        private void AnimalNumberTextbox_TextChanged (object sender, EventArgs e)
        {
                int ignored;
                bool valid = int.TryParse(AnimalNumberTextbox.Text, out ignored);
                AnimalNumberTextbox.ForeColor = valid ? Color.Black : Color.Red;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
