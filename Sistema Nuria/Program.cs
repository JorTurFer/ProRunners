using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Sistema_Nuria
{
    static class Program
    {
        public static List<CameraManager> lstCameras = new List<CameraManager>();

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool x64 = Environment.Is64BitProcess;

            lstCameras.Add(new CameraManager(1));
            lstCameras.Add(new CameraManager(2));

            Application.Run(new MainForm());
        }


    }
}
