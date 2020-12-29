using System;
using System.Windows.Forms;

namespace ProRunners
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Inicio las camaras aqui porque tardan en cargar
            CameraMgr.AddCamera(Auxiliares.CameraFactory(CameraIndex.Cam1));
            CameraMgr.AddCamera(Auxiliares.CameraFactory(CameraIndex.Cam2));

            Application.Run(new MainForm());
        }
    }
}
