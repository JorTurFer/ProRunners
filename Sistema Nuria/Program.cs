using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Sistema_Nuria
{
    static class Program
    {
        public static CameraManager CameraMgr2;

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Sintetizador.Decir("Iniciando grabacion en 3, 2, 1. Grabando...");

            CameraMgr2 = new CameraManager(2);

            Application.Run(new MainForm());
        }


    }
}
