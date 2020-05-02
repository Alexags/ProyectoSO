using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavegadorWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(() =>
            {

                Application.Run(new Inicio());
            });
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }
    }
}
