using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sistema_Nuria
{
    public static class Directorios
    {
        /// <summary>
        /// Retorna el directorio donde se encuentra el ejecutable de la aplicación.
        /// </summary>
        /// <returns></returns>
        public static string ObtenerDirectorioDeAplicacion()
        {
            string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string assemblyPath = Path.GetDirectoryName(assemblyLocation);
            return assemblyPath;
        }

        public static string ObtenerSubdirectorioDeAplicacion(string i_subfolderName)
        {
            string subfolderPath = Path.Combine(ObtenerDirectorioDeAplicacion(), i_subfolderName);
            if (Directory.Exists(subfolderPath) == false)
            {
                Directory.CreateDirectory(subfolderPath);
            }
            return subfolderPath;
        }

        public static string ObtenerNombreDeApplicacion()
        {
            string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return Path.GetFileNameWithoutExtension(assemblyLocation);
        }
    }
}
