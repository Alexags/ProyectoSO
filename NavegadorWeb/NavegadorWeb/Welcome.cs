using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavegadorWeb
{
    public partial class Welcome : Form
    {
        System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        int timeLeft = 10;
        public Welcome()
        {
            InitializeComponent();
            //set properties for the Timer
            myTimer.Interval = 1000;
            myTimer.Enabled = true;

            //Set the event handler for the timer, named "myTimer_Tick"
            myTimer.Tick += timer1_Tick;

            //Start the timer as soon as the form is loaded
            myTimer.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLeft -= 1;

            if (timeLeft < 0)
            {
                myTimer.Stop();
                Thread thread = new Thread(() =>
                {
                    Application.Exit();
                    Application.Run(new Inicio());

                });
                thread.ApartmentState = ApartmentState.STA;
                thread.Start();
            }
        }

        private void Welcome_Load(object sender, EventArgs e)
        {

        }
    }
}
