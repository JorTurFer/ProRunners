using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace ProRunners
{
    static class Almacenamiento
    {

        static Almacenamiento()
        {
            m_lstPacientes = GetRecordedPacientes();
        }

        static List<Paciente> m_lstPacientes = null;

        public static List<Paciente> GetRecordedPacientes(string strPaciente = "")
        {
            if (m_lstPacientes != null)
                return m_lstPacientes.Where(x => x.Nombre.Contains(strPaciente, StringComparison.OrdinalIgnoreCase)).ToList();
            Regex reg = new Regex(@"(\d{1,2}-\d{1,2}-\d{4})");
            List<Paciente> tmp = new List<Paciente>();
            foreach (var item in new DirectoryInfo($"{Directorios.ObtenerSubdirectorioDeAplicacion("Datos")}").GetDirectories().Where(x => x.Name.Contains(strPaciente, StringComparison.OrdinalIgnoreCase)))
            {
                Paciente pac = new Paciente();
                string[] datos = item.Name.Split('_');
                pac.Nombre = datos[0];
                pac.Nacimiento = Convert.ToDateTime(reg.Match(item.Name).Groups[0].Value);
                pac.FullPath = item.FullName;
                GetUserRecordedDays(pac);

                tmp.Add(pac);
            }
            return tmp;
        }

        private static void GetUserRecordedDays(Paciente pac)
        {
            pac.SesionesVideo = new List<DateTime>();
            pac.SesionesFoto = new List<DateTime>();

            foreach (var item in new DirectoryInfo(pac.FullPath).GetDirectories())
            {
                foreach (var item2 in item.GetDirectories())
                {
                    switch (item2.Name)
                    {
                        case "Videos":
                            if (item2.GetFiles().Count() > 0)
                                pac.SesionesVideo.Add(Convert.ToDateTime(item.Name));
                            break;
                        case "Fotos":
                            if (item2.GetFiles().Count() > 0)
                                pac.SesionesFoto.Add(Convert.ToDateTime(item.Name));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static string GetRecordFolder(Paciente pac)
        {
            GenerateFoldersForDay(pac);
            if (!pac.SesionesVideo.Contains(DateTime.Now.Date))
                pac.SesionesVideo.Add(DateTime.Now.Date);
            return $"{Directorios.ObtenerSubdirectorioDeAplicacion("Datos")}\\{Auxiliares.RebuildName(pac.Nombre)}_{pac.Nacimiento.ToString("dd-MM-yyyy")}\\{DateTime.Now.ToString("dd-MM-yyyy")}\\Videos";
        }
        public static string GetPictureFolder(Paciente pac)
        {
            GenerateFoldersForDay(pac);
            if (!pac.SesionesFoto.Contains(DateTime.Now.Date))
                pac.SesionesFoto.Add(DateTime.Now.Date);
            return $"{Directorios.ObtenerSubdirectorioDeAplicacion("Datos")}\\{Auxiliares.RebuildName(pac.Nombre)}_{pac.Nacimiento.ToString("dd-MM-yyyy")}\\{DateTime.Now.ToString("dd-MM-yyyy")}\\Fotos";
        }
        public static string GetDayFolder(Paciente pac)
        {
            GenerateFoldersForDay(pac);
            return $"{Directorios.ObtenerSubdirectorioDeAplicacion("Datos")}\\{Auxiliares.RebuildName(pac.Nombre)}_{pac.Nacimiento.ToString("dd-MM-yyyy")}\\{DateTime.Now.ToString("dd-MM-yyyy")}";
        }
        public static bool GenerateFoldersForDay(Paciente pac)
        {
            try
            {
                string strBasePath = $"{Directorios.ObtenerSubdirectorioDeAplicacion("Datos")}\\{Auxiliares.RebuildName(pac.Nombre)}_{pac.Nacimiento.ToString("dd-MM-yyyy")}\\{DateTime.Now.ToString("dd-MM-yyyy")}";

                if (!Directory.Exists(strBasePath + "\\Videos"))
                    Directory.CreateDirectory(strBasePath + "\\Videos");
                if (!Directory.Exists(strBasePath + "\\Fotos"))
                    Directory.CreateDirectory(strBasePath + "\\Fotos");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AgregarPaciente(Paciente pac)
        {
            bool registrar = !m_lstPacientes.Any(x => x.Nombre == pac.Nombre && x.Nacimiento == pac.Nacimiento);

            if (registrar)
            {
                m_lstPacientes.Add(pac);
                GenerateFoldersForDay(pac);
            }
            return registrar;
        }

    }
}
