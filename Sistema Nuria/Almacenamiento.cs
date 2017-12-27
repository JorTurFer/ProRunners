﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace Sistema_Nuria
{
    static class Almacenamiento
    {

        static Almacenamiento()
        {
            m_lstPacientes= GetRecordedPacientes();
        }

        static List<Paciente> m_lstPacientes = null;

        public static List<Paciente> GetRecordedPacientes()
        {
            if (m_lstPacientes != null)
                return m_lstPacientes;
            Regex reg = new Regex(@"(\d{1,2}-\d{1,2}-\d{4})");
            List<Paciente> tmp = new List<Paciente>();
            foreach (var item in new DirectoryInfo($"{Properties.Settings.Default.strPathFiles}").GetDirectories())
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

        public static bool GenerateFoldersForDay(Paciente pac)
        {
            try
            {
                string strBasePath = $"{Properties.Settings.Default.strPathFiles}\\{Auxiliares.RebuildName(pac.Nombre)}_{pac.Nacimiento.ToString("dd-MM-yyyy")}\\{DateTime.Now.ToString("dd-MM-yyyy")}";

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
