using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Boundary;

namespace Controller
{
    class LogoutController
    {
        public void logout(User uname)
        {

            DBConnector dbcon = new DBConnector();
            dbcon.logout(uname);

            LogoutForm logoutForm2 = new LogoutForm();
            logoutForm2.Show();
            //LoginForm NewLogin = new LoginForm();
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += (s, e) =>
            {
                ((System.Windows.Forms.Timer)s).Stop(); //s is the Timer
                logoutForm2.Close();
                //NewLogin.Show();

            };
            timer.Interval = 5000;
            timer.Start();

            //LoginForm logForm = new LoginForm();
            //LoginForm.Open();

        }
    }
}
