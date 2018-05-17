using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ProRunners
{
  static class Program
  {
    public static List<Camera> lstCameras = new List<Camera>();

    /// <summary>
    /// Punto de entrada principal para la aplicación.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      //Inicio las camaras aqui porque tardan en cargar
      lstCameras.Add(new Camera(1));
      lstCameras.Add(new Camera(2));

      Application.Run(new MainForm());
    }


  }
}
